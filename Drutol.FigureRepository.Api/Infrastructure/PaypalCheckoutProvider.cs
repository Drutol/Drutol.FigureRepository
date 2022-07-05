using System.Text.Json;
using Drutol.FigureRepository.Api.DataAccess.Seeding;
using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Api.Models.Configuration;
using Drutol.FigureRepository.Shared.Checkout;
using Microsoft.Extensions.Options;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;

namespace Drutol.FigureRepository.Api.Infrastructure
{
    public class PaypalCheckoutProvider : ICheckoutProvider
    {
        private readonly IFigureSeedManager _figureSeedManager;
        private readonly IOptions<PaypalCheckoutConfiguration> _config;
        private readonly PayPalHttpClient _payPal;


        public PaypalCheckoutProvider(
            IFigureSeedManager figureSeedManager,
            IOptions<PaypalCheckoutConfiguration> config)
        {
            _figureSeedManager = figureSeedManager;
            _config = config;
            
            var environment = new SandboxEnvironment(_config.Value.ClientId, _config.Value.ClientSecret);
            _payPal = new PayPalHttpClient(environment);
        }

        public async ValueTask<CheckoutOrderResponse> CreateOrder(CheckoutOrderRequest orderRequest)
        {
            var figure = _figureSeedManager[orderRequest.FigureGuid];

            var order = new OrderRequest()
            {
                CheckoutPaymentIntent = "CAPTURE",
                PurchaseUnits = new List<PurchaseUnitRequest>()
                {
                    new PurchaseUnitRequest()
                    {
                        AmountWithBreakdown = new AmountWithBreakdown()
                        {
                            CurrencyCode = "USD",
                            Value = "40.00",
                        },
                        Description = $"Purchase of {figure.Name} figure. Credited to {orderRequest.WalletAddress} wallet.",
                    }
                },
                ApplicationContext = new ApplicationContext()
                {
                    ReturnUrl = "https://www.example.com",
                    CancelUrl = "https://www.example.com",
                }
            };


            // Call API with your client and get a response for your call
            var request = new OrdersCreateRequest();
            request.Prefer("return=representation");
            request.RequestBody(order);
            var response = await _payPal.Execute(request);
            var statusCode = response.StatusCode;
            var orderResult = response.Result<Order>();
            Console.WriteLine("Status: {0}", orderResult.Status);
            Console.WriteLine("Order Id: {0}", orderResult.Id);
            Console.WriteLine("Links:");
            foreach (LinkDescription link in orderResult.Links)
            {
                Console.WriteLine("\t{0}: {1}\tCall Type: {2}", link.Rel, link.Href, link.Method);
            }

            return new CheckoutOrderResponse(orderResult.Id);
        }

        public async ValueTask<CheckoutTransactionResponse> CreateTransaction(CheckoutTransactionRequest transactionRequest)
        {
            var request = new OrdersCaptureRequest(transactionRequest.OrderId);
            request.RequestBody(new OrderActionRequest());
            var response = await _payPal.Execute(request);
            var statusCode = response.StatusCode;
            Order result = response.Result<Order>();
            Console.WriteLine("Status: {0}", result.Status);
            Console.WriteLine("Capture Id: {0}", result.Id);

            return new CheckoutTransactionResponse();
        }
    }
}
