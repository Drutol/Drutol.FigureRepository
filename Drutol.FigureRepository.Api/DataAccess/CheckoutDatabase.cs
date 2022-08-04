using Ardalis.GuardClauses;
using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Api.Models.Checkout;
using Drutol.FigureRepository.Api.Models.Configuration;
using LiteDB;
using Microsoft.Extensions.Options;

namespace Drutol.FigureRepository.Api.DataAccess;

public class CheckoutDatabase : ICheckoutDatabase
{
    private readonly ILogger<CheckoutDatabase> _logger;
    private readonly IOptions<CheckoutDatabaseConfiguration> _config;
    private readonly LiteDatabase _database;

    public CheckoutDatabase(
        ILogger<CheckoutDatabase> logger,
        IOptions<CheckoutDatabaseConfiguration> config)
    {
        _logger = logger;
        _config = config;
        _database = new LiteDatabase(config.Value.ConnectionString);

        _database.Mapper.Entity<OrderEntity>()
            .Id(entity => entity.Guid)
            .DbRef(entity => entity.Events, nameof(OrderEvent));

        var collection = _database.GetCollection<OrderEntity>();
        collection.EnsureIndex(entity => entity.Guid);
        collection.EnsureIndex(entity => entity.CreatedAt);
        collection.EnsureIndex(entity => entity.CheckoutId);
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
            Guard.Against.AgainstExpression(status => status != OrderStatus.Created, order.Status, $"Order must be in {OrderStatus.Created} status.");

            var result = _database.GetCollection<OrderEntity>().Insert(order);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to store order in database.");
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
}