using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Api.Models.Configuration;
using Drutol.FigureRepository.Shared.Checkout;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Drutol.FigureRepository.Api.Controllers
{
    [ApiController]
    [Route("api/checkout")]
    public class CheckoutController : ControllerBase
    {
        private readonly ICheckoutProvider _checkoutProvider;

        public CheckoutController(ICheckoutProvider checkoutProvider)
        {
            _checkoutProvider = checkoutProvider;
        }

        [HttpPost("order")]
        public async Task<CheckoutOrderResponse> GetOrder(CheckoutOrderRequest request)
        {
            return await _checkoutProvider.CreateOrder(request);
        } 
        
        [HttpPost("transaction")]
        public async Task<CheckoutTransactionResponse> Transact(CheckoutTransactionRequest transactionRequest)
        {
            return await _checkoutProvider.CreateTransaction(transactionRequest);
        }
    }
}
