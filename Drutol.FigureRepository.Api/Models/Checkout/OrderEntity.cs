using Drutol.FigureRepository.Shared.Models.Orders;

namespace Drutol.FigureRepository.Api.Models.Checkout;

public class OrderEntity
{
    public Guid Guid { get; set; }

    public DateTime CreatedAt { get; set; }
    public Guid FigureId { get; set; }
    public decimal Price { get; set; }

    public string WalletAddress { get; set; }
    public int AccountId { get; set; }
    public bool IncludesWalletActivation { get; set; }
    public string PaidFee { get; set; }

    public string CheckoutId { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderEventEntity> Events { get; set; }
    public string PayPalObject { get; set; }
    public string TransactionHash { get; set; }

    public Order ToModel() => new Order
    {
        Status = Status,
        WalletAddress = WalletAddress,
        CheckoutId = CheckoutId,
        AccountId = AccountId,
        CreatedAt = CreatedAt,
        Events = Events?.Select(entity => entity.ToModel()).ToList(),
        FigureId = FigureId,
        Guid = Guid,
        IncludesWalletActivation = IncludesWalletActivation,
        PaidFee = PaidFee,
        PayPalObject = PayPalObject,
        Price = Price,
        TransactionHash = TransactionHash,
    };

    public override string ToString()
    {
        return $"Order: " +
               $"{nameof(Guid)}: {Guid}, " +
               $"{nameof(FigureId)}: {FigureId}, " +
               $"{nameof(WalletAddress)}: {WalletAddress}, " +
               $"{nameof(CheckoutId)}: {CheckoutId}";
    }
}