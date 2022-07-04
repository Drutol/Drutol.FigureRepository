using Braintree;
using Drutol.FigureRepository.Api.DataAccess.Seeding;
using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Api.Models.Configuration;
using Drutol.FigureRepository.Shared.Braintree;
using Microsoft.Extensions.Options;

namespace Drutol.FigureRepository.Api.Infrastructure
{
    public class BraintreeProvider : IBraintreeProvider
    {
        private readonly IFigureSeedManager _figureSeedManager;
        private readonly IOptions<BraintreeConfiguration> _config;

        private readonly BraintreeGateway _gateway;

        public BraintreeProvider(
            IFigureSeedManager figureSeedManager,
            IOptions<BraintreeConfiguration> config)
        {
            _figureSeedManager = figureSeedManager;
            _config = config;
            _gateway = new BraintreeGateway(
                _config.Value.ClientId,
                _config.Value.ClientSecret);
        }

        public async ValueTask<string> GenerateToken()
        {
            var token = await _gateway.ClientToken.GenerateAsync();
            return token;
        }

        public async ValueTask<BraintreeTransactionResponse> CreateTransaction(BraintreeTransactionRequest transactionRequest)
        {
            var figure = _figureSeedManager[transactionRequest.FigureGuid];

            var request = new TransactionRequest
            {
                Amount = figure.CheckoutDetails.Price,
                MerchantAccountId = "USD",
                PaymentMethodNonce = transactionRequest.Nonce,
                //DeviceData = Request.Form["device_data"],
                OrderId = Guid.NewGuid().ToString(),
                Descriptor = new DescriptorRequest
                {
                    Name = "DRUFIGURES"
                },
                Options = new TransactionOptionsRequest
                {
                    PayPal = new TransactionOptionsPayPalRequest
                    {
                        CustomField = "PayPal custom field",
                        Description = "Credited Wallet Address",
                        SupplementaryData = new Dictionary<string, string>
                        {
                            {"Credited Wallet Address", transactionRequest.WalletAddress},
                            {"Figure", figure.Name}
                        }
                    },
                    SubmitForSettlement = true
                }
            };

            var result = await _gateway.Transaction.SaleAsync(request);

            return new BraintreeTransactionResponse();
        }
    }
}
