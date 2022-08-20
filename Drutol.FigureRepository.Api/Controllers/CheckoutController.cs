using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Api.Logging;
using Drutol.FigureRepository.Api.Models.Configuration;
using Drutol.FigureRepository.Api.Util;
using Drutol.FigureRepository.Shared.Checkout;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Drutol.FigureRepository.Api.Controllers;

[ApiController]
[Route("api/checkout")]
public class CheckoutController : ControllerBase
{
    private readonly ILogger<CheckoutController> _logger;
    private readonly ICheckoutProvider _checkoutProvider;

    public CheckoutController(
        ILogger<CheckoutController> logger,
        ICheckoutProvider checkoutProvider)
    {
        _logger = logger;
        _checkoutProvider = checkoutProvider;
    }

    [HttpPost("order")]
    public async Task<CheckoutOrderResponse> GetOrder(CheckoutOrderRequest request)
    {
        var order = await _checkoutProvider.CreateOrder(request);

        if (order.Success)
        {
            _logger.LogInformation(EventIds.OrderCreated.Ev(),
                $"Created order {order.OrderId} for figure {request.FigureGuid} by {request.WalletAddress}");
        }
        else
        {
            _logger.LogError(EventIds.OrderCreationFailed.Ev(),
                $"Failed to create order for figure {request.FigureGuid} by {request.WalletAddress}");
        }

        return order;
    } 
        
    [HttpPost("transaction")]
    public async Task<CheckoutTransactionResponse> Transact(CheckoutTransactionRequest transactionRequest)
    {
        var transaction = await _checkoutProvider.CreateTransaction(transactionRequest);

        if (transaction.Status == CheckoutTransactionResponse.StatusCode.Ok)
        {
            _logger.LogInformation(
                EventIds.TransactionCompleted.Ev(),
                $"Completed transaction for order {transactionRequest.CheckoutId}.");
        }
        else
        {
            _logger.LogError(
                EventIds.TransactionFailed.Ev(),
                $"Completed transaction for order {transactionRequest.CheckoutId} with status code {transaction.Status}.");
        }

        return transaction;
    }
}