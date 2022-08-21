using Drutol.FigureRepository.Api.Models.Checkout;
using Drutol.FigureRepository.Shared.Orders;

namespace Drutol.FigureRepository.Api.Interfaces;

public interface ICheckoutDatabase
{
    ValueTask<bool> CreateOrder(OrderEntity order);
    ValueTask<OrderEntity?> GetOrderByCheckoutId(string checkoutId);
    ValueTask UpdateOrder(OrderEntity orderEntity);
    ValueTask<GetOrdersRequestResult> GetOrders(GetOrdersRequest request);
}