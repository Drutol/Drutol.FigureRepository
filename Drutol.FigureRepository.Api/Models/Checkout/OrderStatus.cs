namespace Drutol.FigureRepository.Api.Models.Checkout
{
    public enum OrderStatus
    {
        Created = 0,

        Paid = 10,

        WalletActivated = 20,

        Delivered = 30,
        DeliveryPending = 31
    }
}
