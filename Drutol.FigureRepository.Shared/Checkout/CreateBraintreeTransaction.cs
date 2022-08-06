using Drutol.FigureRepository.Shared.Models.Auth;

namespace Drutol.FigureRepository.Shared.Checkout;

public record CheckoutTransactionRequest(string CheckoutId);

public record CheckoutTransactionResponse(CheckoutTransactionResponse.StatusCode Status, TokenResponse TokenResponse = null)
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