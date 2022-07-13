using System.Net;
using System.Text.Json;
using Drutol.FigureRepository.Api.DataAccess;
using Drutol.FigureRepository.Api.DataAccess.Seeding;
using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Api.Models.Checkout;
using Drutol.FigureRepository.Api.Models.Configuration;
using Drutol.FigureRepository.Shared.Blockchain.Loopring;
using Drutol.FigureRepository.Shared.Checkout;
using Microsoft.Extensions.Options;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;

namespace Drutol.FigureRepository.Api.Infrastructure
{
    public class PaypalCheckoutProvider : ICheckoutProvider
    {
        private readonly ILogger<PaypalCheckoutProvider> _logger;
        private readonly IFigureSeedManager _figureSeedManager;
        private readonly ICheckoutDatabase _checkoutDatabase;
        private readonly ILoopringCommunicator _loopringCommunicator;
        private readonly IOptions<PaypalCheckoutConfiguration> _config;
        private readonly PayPalHttpClient _payPal;

        public PaypalCheckoutProvider(
            ILogger<PaypalCheckoutProvider> logger,
            IFigureSeedManager figureSeedManager,
            ICheckoutDatabase checkoutDatabase,
            ILoopringCommunicator loopringCommunicator,
            IOptions<PaypalCheckoutConfiguration> config)
        {
            _logger = logger;
            _figureSeedManager = figureSeedManager;
            _checkoutDatabase = checkoutDatabase;
            _loopringCommunicator = loopringCommunicator;
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
                            Value = figure.CheckoutDetails.Price.ToString("N2"),
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

            var request = new OrdersCreateRequest();
            request.Prefer("return=representation");
            request.RequestBody(order);
            var response = await _payPal.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var orderResult = response.Result<Order>();
                var orderEntity = new OrderEntity
                {
                    Guid = Guid.NewGuid(),
                    WalletAddress = orderRequest.WalletAddress,
                    CheckoutId = orderResult.Id,
                    CreatedAt = DateTime.UtcNow,
                    Events = new List<OrderEvent>
                    {
                        new()
                        {
                            StatusChange = OrderStatus.Created,
                            DateTime = DateTime.UtcNow
                        }
                    },
                    Price = figure.CheckoutDetails.Price,
                    FigureId = figure.Guid,
                    Status = OrderStatus.Created
                };

                if (await _checkoutDatabase.CreateOrder(orderEntity))
                {
                    _logger.LogInformation($"Created order: {orderEntity}");
                    return new CheckoutOrderResponse(true, orderResult.Id);
                }
                else
                {
                    return new CheckoutOrderResponse(false);
                }
            }
            else
            {
                _logger.LogInformation($"Failed to create order, PayPal status code: {response.StatusCode}");
                return new CheckoutOrderResponse(false);
            }
        }

        public async ValueTask<CheckoutTransactionResponse> CreateTransaction(CheckoutTransactionRequest transactionRequest)
        {
            var orderEntity = await _checkoutDatabase.GetOrderByCheckoutId(transactionRequest.CheckoutId);
            if (orderEntity is null)
            {
                return new CheckoutTransactionResponse(CheckoutTransactionResponse.StatusCode.OrderNotFound);
            }

            var request = new OrdersCaptureRequest(transactionRequest.CheckoutId);
            request.RequestBody(new OrderActionRequest());
            var response = await _payPal.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var order = response.Result<Order>();
                if (order.Status == "COMPLETED")
                {
                    var loopringAccountResult = await _loopringCommunicator.GetAccount(orderEntity.WalletAddress);
                    if (loopringAccountResult is IAccountResponseModel.Success)
                    {
                        orderEntity.IncludesWalletActivation = false;
                    }
                    else
                    {
                        orderEntity.IncludesWalletActivation = true;
                        var activationResult = await _loopringCommunicator.ActivateWallet(orderEntity.WalletAddress);

                        loopringAccountResult = await _loopringCommunicator.GetAccount(orderEntity.WalletAddress);
                        if (loopringAccountResult is IAccountResponseModel.Fail)
                        {
                            return new CheckoutTransactionResponse(CheckoutTransactionResponse.StatusCode.DeliveryPending);
                        }
                    }

                    await _loopringCommunicator.TransferFigureNft(orderEntity.FigureId, orderEntity.WalletAddress);
                }
                else
                {
                    _logger.LogError($"PayPal order capture failed with payload: {JsonSerializer.Serialize(order)}");
                    return new CheckoutTransactionResponse(CheckoutTransactionResponse.StatusCode.PayPalError);
                }
            }
            else
            {
                _logger.LogError($"PayPal order capture failed with status code: {response.StatusCode}");
                return new CheckoutTransactionResponse(CheckoutTransactionResponse.StatusCode.PayPalError);
            }

            return new CheckoutTransactionResponse(CheckoutTransactionResponse.StatusCode.Error);
        }
    }
}
