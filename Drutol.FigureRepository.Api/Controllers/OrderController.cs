using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Api.Models.Configuration;
using Drutol.FigureRepository.Api.Util;
using Drutol.FigureRepository.Shared.Checkout;
using Drutol.FigureRepository.Shared.Logs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Drutol.FigureRepository.Api.Controllers;

[ApiController]
[Route("api/order")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly ICheckoutProvider _checkoutProvider;

    public OrderController(
        ILogger<OrderController> logger,
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
            _logger.LogInformation(DruEventId.OrderCreated.Ev(),
                "Created order {OrderId} for figure {FigureGuid} by {WalletAddress}", order.OrderId, request.FigureGuid, request.WalletAddress);
        }
        else
        {
            _logger.LogError(DruEventId.OrderCreationFailed.Ev(),
                "Failed to create order for figure {FigureGuid} by {WalletAddress}", request.FigureGuid, request.WalletAddress);
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
                DruEventId.TransactionCompleted.Ev(),
                "Completed transaction for order {CheckoutId}.", transactionRequest.CheckoutId);
        }
        else
        {
            _logger.LogError(
                DruEventId.TransactionFailed.Ev(),
                "Failed transaction for order {CheckoutId} with status code {Status}.", transactionRequest.CheckoutId, transaction.Status);
        }

        return transaction;
    }
}