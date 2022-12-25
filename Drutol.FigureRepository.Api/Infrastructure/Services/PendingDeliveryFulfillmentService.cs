using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Api.Models.Checkout;
using Drutol.FigureRepository.Shared.Checkout;
using Drutol.FigureRepository.Shared.Orders;

namespace Drutol.FigureRepository.Api.Infrastructure.Services;

public class PendingDeliveryFulfillmentService : BackgroundService
{
    private readonly ILogger<PendingDeliveryFulfillmentService> _logger;
    private readonly IOrderDatabase _orderDatabase;
    private readonly INftTransferProvider _nftTransferProvider;

    public PendingDeliveryFulfillmentService(
        ILogger<PendingDeliveryFulfillmentService> logger,
        IOrderDatabase orderDatabase,
        INftTransferProvider nftTransferProvider)
    {
        _logger = logger;
        _orderDatabase = orderDatabase;
        _nftTransferProvider = nftTransferProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromMinutes(10));
        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                var result = await _orderDatabase.GetOrders(new GetOrdersRequest
                {
                    StatusFilter = new List<OrderStatus>
                    {
                        OrderStatus.DeliveryPending,
                    },
                    Take = int.MaxValue,
                    From = DateTime.UnixEpoch,
                    To = DateTime.UtcNow,
                }, false);

                if (result.Orders.Any())
                {
                    _logger.LogInformation($"Found {result.Orders.Count} orders pending for delivery.");
                    foreach (var order in result.Orders)
                    {
                        var orderEntity = await _orderDatabase.GetOrderByGuid(order.Guid);
                        var transferResult = await _nftTransferProvider.TransferNft(order);

                        await _orderDatabase.ProcessNftTransferResult(orderEntity, transferResult);

                        if (transferResult.Success)
                        {
                            _logger.LogInformation("Delivered pending order {OrderGuid}.", order.Guid);
                        }
                        else
                        {
                            _logger.LogInformation("Failed to deliver pending order {OrderGuid}.", order.Guid);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to deliver pending orders.");
            }
        }
    }
}
