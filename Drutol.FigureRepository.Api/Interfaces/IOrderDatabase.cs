using Drutol.FigureRepository.Api.Models.Blockchain;
using Drutol.FigureRepository.Api.Models.Checkout;
using Drutol.FigureRepository.Shared.Orders;
using Drutol.FigureRepository.Shared.Statistics;

namespace Drutol.FigureRepository.Api.Interfaces;

public interface IOrderDatabase
{
    ValueTask<bool> CreateOrder(OrderEntity order);
    ValueTask<OrderEntity?> GetOrderByCheckoutId(string checkoutId);
    ValueTask<OrderEntity?> GetOrderByGuid(Guid orderGuid);
    ValueTask UpdateOrder(OrderEntity orderEntity);
    ValueTask<GetOrdersRequestResult> GetOrders(GetOrdersRequest request, bool limitTake = true);
    ValueTask<FigureStatistics> GetFigureStatistics(Guid figureGuid, GetStatisticsRequest request);
    ValueTask ProcessNftTransferResult(OrderEntity order, NftTransferResult result);
}