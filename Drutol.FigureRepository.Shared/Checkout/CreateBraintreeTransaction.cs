namespace Drutol.FigureRepository.Shared.Checkout
{
    public record CheckoutTransactionRequest(string CheckoutId);

    public record CheckoutTransactionResponse(CheckoutTransactionResponse.StatusCode Status, string DownloadToken = null)
    {
        public enum StatusCode
        {
            Ok,
            DeliveryPending,
            OrderNotFound,
            PayPalError,
            Error
        }
    }
}
