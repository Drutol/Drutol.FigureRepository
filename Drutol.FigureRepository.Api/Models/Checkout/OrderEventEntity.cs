using Drutol.FigureRepository.Shared.Models.Orders;

namespace Drutol.FigureRepository.Api.Models.Checkout
{
    public class OrderEventEntity
    {
        public OrderStatus StatusChange { get; init; }
        public DateTime DateTime { get; init; }
        public string Data { get; set; }

        public OrderEvent ToModel() => new OrderEvent
        {
            Data = Data,
            DateTime = DateTime,
            StatusChange = StatusChange,
        };
    }
}
