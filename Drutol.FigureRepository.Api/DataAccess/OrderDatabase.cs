using Ardalis.GuardClauses;
using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Api.Models.Checkout;
using Drutol.FigureRepository.Api.Models.Configuration;
using Drutol.FigureRepository.Api.Util;
using Drutol.FigureRepository.Shared.Logs;
using Drutol.FigureRepository.Shared.Orders;
using Drutol.FigureRepository.Shared.Statistics;
using LiteDB;
using Microsoft.Extensions.Options;

namespace Drutol.FigureRepository.Api.DataAccess;

public class OrderDatabase : IOrderDatabase
{
    private readonly ILogger<OrderDatabase> _logger;
    private readonly IOptions<CheckoutDatabaseConfiguration> _config;
    private readonly LiteDatabase _database;

    public OrderDatabase(
        ILogger<OrderDatabase> logger,
        IOptions<CheckoutDatabaseConfiguration> config)
    {
        _logger = logger;
        _config = config;
        _database = new LiteDatabase(config.Value.ConnectionString);

        _database.Mapper.Entity<OrderEntity>()
            .Id(entity => entity.Guid);

        var orders = _database.GetCollection<OrderEntity>();
        orders.EnsureIndex(entity => entity.Guid);
        orders.EnsureIndex(entity => entity.CreatedAt);
        orders.EnsureIndex(entity => entity.CheckoutId);
        orders.EnsureIndex(entity => entity.Status);
    }

    public async ValueTask<bool> CreateOrder(OrderEntity order)
    {
        try
        {
            Guard.Against.Default(order.Guid);
            Guard.Against.Default(order.FigureId);
            Guard.Against.Default(order.CreatedAt);
            Guard.Against.NullOrWhiteSpace(order.WalletAddress);
            Guard.Against.NullOrWhiteSpace(order.CheckoutId);
            Guard.Against.AgainstExpression(status => status == OrderStatus.Created, order.Status, $"Order must be in {OrderStatus.Created} status.");

            var result = _database.GetCollection<OrderEntity>().Insert(order);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(DruEventId.DatabaseError.Ev(), e, "Failed to store order in database.");
            return false;
        }
    }

    public ValueTask<OrderEntity?> GetOrderByCheckoutId(string checkoutId)
    {
        return ValueTask.FromResult(_database.GetCollection<OrderEntity>().FindOne(entity => entity.CheckoutId == checkoutId))!;
    }

    public ValueTask UpdateOrder(OrderEntity orderEntity)
    {
        _database.GetCollection<OrderEntity>().Update(orderEntity);
        return ValueTask.CompletedTask;
    }

    public ValueTask<GetOrdersRequestResult> GetOrders(GetOrdersRequest request)
    {
        var query = _database.GetCollection<OrderEntity>().Query();

        if (request.StatusFilter is { Count: > 0 })
            query = query.Where(entity => request.StatusFilter.Contains(entity.Status));

        if (request.From is { })
            query = query.Where(entity => entity.CreatedAt >= request.From);

        if (request.To is { })
            query = query.Where(entity => entity.CreatedAt <= request.To);

        var totalCount = query.Count();

        if (request.Take == 0)
            request.Take = 100;

        var orders = query.Limit(Math.Min(100, request.Take)).Offset(request.Skip).ToList();

        return ValueTask.FromResult(new GetOrdersRequestResult(totalCount, orders.Select(entity => entity.ToModel()).ToList()));
    }

    public ValueTask<FigureStatistics> GetFigureStatistics(Guid figureGuid, GetStatisticsRequest request)
    {
        var counts = new Dictionary<OrderStatus, int>();

        foreach (var status in Enum.GetValues<OrderStatus>())
        {
            var query = _database.GetCollection<OrderEntity>().Query().Where(e => e.FigureId == figureGuid);

            if (request.From is { })
                query = query.Where(entity => entity.CreatedAt >= request.From);

            if (request.To is { })
                query = query.Where(entity => entity.CreatedAt <= request.To);

            counts[status] = query.Where(entity => entity.Status == status).Count();
        }

        return ValueTask.FromResult<FigureStatistics>(new(figureGuid, counts));
    }
}