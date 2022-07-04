using Braintree;
using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Api.Models.Configuration;
using Drutol.FigureRepository.Shared.Braintree;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Drutol.FigureRepository.Api.Controllers
{
    [ApiController]
    [Route("api/braintree")]
    public class BraintreeController : ControllerBase
    {
        private readonly IBraintreeProvider _braintreeProvider;


        public BraintreeController(IBraintreeProvider braintreeProvider)
        {
            _braintreeProvider = braintreeProvider;
        }

        [HttpGet("token")]
        public async Task<BraintreeTokenResponse> GetToken()
        {
            var token = await _braintreeProvider.GenerateToken();
            return new BraintreeTokenResponse(token);
        } 
        
        [HttpPost("transaction")]
        public async Task<BraintreeTokenResponse> Transact(BraintreeTransactionRequest transactionRequest)
        {
            var token = await _braintreeProvider.CreateTransaction(transactionRequest);
            return new BraintreeTokenResponse("abc");
        }
    }
}
