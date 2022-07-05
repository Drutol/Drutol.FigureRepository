using Drutol.FigureRepository.Shared.Checkout;

namespace Drutol.FigureRepository.Api.Interfaces;

public interface ICheckoutProvider
{
    ValueTask<CheckoutOrderResponse> CreateOrder(CheckoutOrderRequest orderRequest);
    ValueTask<CheckoutTransactionResponse> CreateTransaction(CheckoutTransactionRequest nonce);
}