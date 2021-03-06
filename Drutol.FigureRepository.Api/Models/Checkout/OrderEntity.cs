namespace Drutol.FigureRepository.Api.Models.Checkout
{
    public class OrderEntity
    {
        public Guid Guid { get; set; }

        public DateTime CreatedAt { get; set; }
        public Guid FigureId { get; set; }
        public decimal Price { get; set; }

        public string WalletAddress { get; set; }
        public bool IncludesWalletActivation { get; set; }
        public decimal WalletActivationFee { get; set; }

        public string CheckoutId { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderEvent> Events { get; set; }
        public string PayPalObject { get; set; }

        public override string ToString()
        {
            return $"Order: " +
                   $"{nameof(Guid)}: {Guid}, " +
                   $"{nameof(FigureId)}: {FigureId}, " +
                   $"{nameof(WalletAddress)}: {WalletAddress}, " +
                   $"{nameof(CheckoutId)}: {CheckoutId}";
        }
    }

    public class OrderEvent
    {
        public OrderStatus StatusChange { get; init; }
        public DateTime DateTime { get; init; }
    }
}
