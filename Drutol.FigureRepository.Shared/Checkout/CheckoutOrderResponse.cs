using System;

namespace Drutol.FigureRepository.Shared.Checkout;

public record CheckoutOrderRequest(Guid FigureGuid, string WalletAddress);

public record CheckoutOrderResponse(bool Success, string OrderId = null);