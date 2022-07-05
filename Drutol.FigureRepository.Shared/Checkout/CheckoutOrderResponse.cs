using System;

namespace Drutol.FigureRepository.Shared.Checkout
{
    public record CheckoutOrderRequest(Guid FigureGuid, string WalletAddress);

    public record CheckoutOrderResponse(string OrderId);
}
