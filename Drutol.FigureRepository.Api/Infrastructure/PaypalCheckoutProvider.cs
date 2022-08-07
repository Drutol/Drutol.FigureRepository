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

namespace Drutol.FigureRepository.Api.Infrastructure;

public class PaypalCheckoutProvider : ICheckoutProvider
{
    private readonly ILogger<PaypalCheckoutProvider> _logger;
    private readonly IFigureSeedManager _figureSeedManager;
    private readonly ICheckoutDatabase _checkoutDatabase;
    private readonly IDownloadTokenManager _downloadTokenManager;
    private readonly INftTransferProvider _nftTransferProvider;
    private readonly ILoopringCommunicator _loopringCommunicator;
    private readonly IOptions<PaypalCheckoutConfiguration> _config;
    private readonly PayPalHttpClient _payPal;

    public PaypalCheckoutProvider(
        ILogger<PaypalCheckoutProvider> logger,
        IFigureSeedManager figureSeedManager,
        ICheckoutDatabase checkoutDatabase,
        IDownloadTokenManager downloadTokenManager,
        INftTransferProvider nftTransferProvider,
        ILoopringCommunicator loopringCommunicator,
        IOptions<PaypalCheckoutConfiguration> config)
    {
        _logger = logger;
        _figureSeedManager = figureSeedManager;
        _checkoutDatabase = checkoutDatabase;
        _downloadTokenManager = downloadTokenManager;
        _nftTransferProvider = nftTransferProvider;
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
                    Description =
                        $"Purchase of {figure.Name} figure. Credited to {orderRequest.WalletAddress} wallet.",
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

        if (response.StatusCode == HttpStatusCode.Created)
        {
            var orderResult = response.Result<Order>();
            var orderEntity = new OrderEntity
            {
                Guid = Guid.NewGuid(),
                WalletAddress = orderRequest.WalletAddress,
                CheckoutId = orderResult.Id,
                CreatedAt = DateTime.UtcNow,
                Events = new List<OrderEventEntity>
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

    public async ValueTask<CheckoutTransactionResponse> CreateTransaction(
        CheckoutTransactionRequest transactionRequest)
    {
        var orderEntity = await _checkoutDatabase.GetOrderByCheckoutId(transactionRequest.CheckoutId);
        if (orderEntity is null)
        {
            return new CheckoutTransactionResponse(CheckoutTransactionResponse.StatusCode.OrderNotFound);
        }

        try
        {
            var request = new OrdersCaptureRequest(transactionRequest.CheckoutId);
            request.RequestBody(new OrderActionRequest());
            var response = await _payPal.Execute(request);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                var order = response.Result<Order>();
                if (order.Status == "COMPLETED")
                {
                    var loopringAccountResult = await _loopringCommunicator.GetAccount(orderEntity.WalletAddress);
                    if (loopringAccountResult is IAccountResponseModel.Success loopringAccount)
                    {
                        orderEntity.AccountId = loopringAccount.AccountId;
                    }
                    else
                    {
                        orderEntity.IncludesWalletActivation = true;
                    }
                    
                    var downloadToken =
                        _downloadTokenManager.GenerateDownloadTokenForFigure(
                            _figureSeedManager[orderEntity.FigureId]);

                    var transferResult = await _nftTransferProvider.TransferNft(orderEntity);

                    if (transferResult.Success)
                    {
                        orderEntity.Status = OrderStatus.Delivered;
                        orderEntity.Events.Add(new OrderEventEntity
                        {
                            DateTime = DateTime.UtcNow,
                            StatusChange = OrderStatus.Delivered
                        });

                        orderEntity.TransactionHash = transferResult.Hash;
                        orderEntity.PaidFee = transferResult.Fee;

                        return new(
                            CheckoutTransactionResponse.StatusCode.Ok,
                            downloadToken);
                    }
                    else
                    {
                        orderEntity.Status = OrderStatus.DeliveryPending;
                        orderEntity.Events.Add(new OrderEventEntity
                        {
                            DateTime = DateTime.UtcNow,
                            StatusChange = OrderStatus.DeliveryPending
                        });
                        return new(
                            CheckoutTransactionResponse.StatusCode.DeliveryPending,
                            downloadToken);
                    }
                }
                else
                {
                    var payload = JsonSerializer.Serialize(order);
                    orderEntity.Status = OrderStatus.PayPalError;
                    orderEntity.Events.Add(new OrderEventEntity
                    {
                        DateTime = DateTime.UtcNow,
                        StatusChange = OrderStatus.PayPalError,
                        Data = payload
                    });
                    _logger.LogError($"PayPal order capture failed with payload: {payload}");
                    return new(CheckoutTransactionResponse.StatusCode.PayPalError);
                }
            }
            else
            {
                orderEntity.Status = OrderStatus.PayPalError;
                orderEntity.Events.Add(new OrderEventEntity
                {
                    DateTime = DateTime.UtcNow,
                    StatusChange = OrderStatus.PayPalError,
                    Data = JsonSerializer.Serialize(new { response.StatusCode })
                });
                _logger.LogError($"PayPal order capture failed with status code: {response.StatusCode}");
                return new(CheckoutTransactionResponse.StatusCode.PayPalError);
            }
        }
        catch (Exception e)
        {
            orderEntity.Status = OrderStatus.PayPalError;
            orderEntity.Events.Add(new OrderEventEntity
            {
                DateTime = DateTime.UtcNow,
                StatusChange = OrderStatus.PayPalError,
                Data = JsonSerializer.Serialize(new { Exception = e.ToString() })
            });
            _logger.LogError(e, "Failed to process transaction.");
            return new(CheckoutTransactionResponse.StatusCode.Error);
        }
        finally
        {
            await _checkoutDatabase.UpdateOrder(orderEntity);
        }
    }
}