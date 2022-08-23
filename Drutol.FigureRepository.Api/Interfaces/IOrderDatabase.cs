using Drutol.FigureRepository.Api.Models.Checkout;
using Drutol.FigureRepository.Shared.Orders;
using Drutol.FigureRepository.Shared.Statistics;

namespace Drutol.FigureRepository.Api.Interfaces;

public interface IOrderDatabase
{
    ValueTask<bool> CreateOrder(OrderEntity order);
    ValueTask<OrderEntity?> GetOrderByCheckoutId(string checkoutId);
    ValueTask UpdateOrder(OrderEntity orderEntity);
    ValueTask<GetOrdersRequestResult> GetOrders(GetOrdersRequest request);
    ValueTask<FigureStatistics> GetFigureStatistics(Guid figureGuid, GetStatisticsRequest request);
}