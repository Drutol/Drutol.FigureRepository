﻿using System.Text.Json;
using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Api.Models.Blockchain;
using Drutol.FigureRepository.Api.Models.Checkout;
using Drutol.FigureRepository.Api.Models.Configuration;
using Drutol.FigureRepository.Api.Util;
using Drutol.FigureRepository.Shared.Blockchain.Loopring;
using Drutol.FigureRepository.Shared.Blockchain.Loopring.Nft;
using Drutol.FigureRepository.Shared.Logs;
using Drutol.FigureRepository.Shared.Models.Orders;
using Microsoft.Extensions.Options;

namespace Drutol.FigureRepository.Api.Infrastructure.Blockchain;

public class NftTransferProvider : INftTransferProvider
{
    private readonly ILogger<NftTransferProvider> _logger;
    private readonly IOptions<BlockchainAuthConfig> _config;
    private readonly IFigureSeedManager _figureSeedManager;
    private readonly ILoopringCommunicator _loopringCommunicator;

    public NftTransferProvider(
        ILogger<NftTransferProvider> logger,
        IOptions<BlockchainAuthConfig> config,
        IFigureSeedManager figureSeedManager,
        ILoopringCommunicator loopringCommunicator)
    {
        _logger = logger;
        _config = config;
        _figureSeedManager = figureSeedManager;
        _loopringCommunicator = loopringCommunicator;
    }

    public async Task<NftTransferResult> TransferNft(Order order)
    {
        var figure = _figureSeedManager[order.FigureId];

        try
        {
            var apiKeyResponse = await _loopringCommunicator.GetApiKey(
            _config.Value.SourceAccountL2Key,
            _config.Value.SourceAccountAddress,
            _config.Value.SourceAccountId);

            if (apiKeyResponse is IApiKeyResponseModel.Success apiKey)
            {
                var feesResponse = await _loopringCommunicator.GetOffchainFee(
                    apiKey.ApiKey,
                    _config.Value.SourceAccountId,
                    order.IncludesWalletActivation ? NftTransferType.TransferAndActivate : NftTransferType.Transfer,
                    figure.NftDetails.TokenAddress,
                    amount: 1);

                var storageIdResponse = await _loopringCommunicator.GetStorageId(
                    apiKey.ApiKey,
                    _config.Value.SourceAccountId,
                    figure.NftDetails.TokenId);

                if (feesResponse is IGetOffchainFeeResponseModel.Success fees &&
                    storageIdResponse is IGetStorageIdResponseModel.Success storageId)
                {
                    var fee = fees.Fees.First(fee => fee.TokenId == 0).RequiredFee;
                    var transferResponse = await _loopringCommunicator.TransferFigureNft(apiKey.ApiKey,
                        new TransferNftRequestModel
                        {
                            FromAccountId = _config.Value.SourceAccountId,
                            FromAddress = _config.Value.SourceAccountAddress,
                            ToAccountId = order.AccountId,
                            ToAddress = order.WalletAddress,
                            TokenData = new TransferNftRequestModel.Token
                            {
                                Amount = "1",
                                NftData = figure.NftDetails.NftData,
                                TokenId = figure.NftDetails.TokenId
                            },

                            Fee = new TransferNftRequestModel.MaxFee
                            {
                                Amount = fee,
                                TokenId = 0
                            },
                            StorageId = storageId.OffchainId,
                            PayPayeeUpdateAccount = order.IncludesWalletActivation,
                            Memo = $"{figure.Name} - {order.CheckoutId}",
                        }
                    );

                    if (transferResponse is ITransferNftResponseModel.Success transfer)
                    {
                        return new NftTransferResult(true)
                        {
                            Fee = fee,
                            Hash = transfer.Hash
                        };
                    }

                    if (transferResponse is ITransferNftResponseModel.Fail transferFail)
                    {
                        var error = JsonSerializer.Serialize(transferFail);
                        _logger.LogError(DruEventId.LoopringError.Ev(), $"Failed to transfer nft. {error}");
                        return new NftTransferResult(false)
                        {
                            ErrorMessages = new() { error }
                        };
                    }
                }
                else
                {
                    var errors = new List<string>();
                    if (feesResponse is IGetOffchainFeeResponseModel.Fail feesFail)
                    {
                        var error = JsonSerializer.Serialize(feesFail);
                        errors.Add(error);
                        _logger.LogError(DruEventId.LoopringError.Ev(), $"Failed to obtain loopring fees. {error}");
                    }

                    if (storageIdResponse is IGetStorageIdResponseModel.Fail storageFail)
                    {
                        var error = JsonSerializer.Serialize(storageFail);
                        errors.Add(error);
                        _logger.LogError(DruEventId.LoopringError.Ev(), $"Failed to obtain loopring storage. {error}");
                    }

                    return new NftTransferResult(false)
                    {
                        ErrorMessages = errors
                    };
                }
            }
            else if (apiKeyResponse is IApiKeyResponseModel.Success apiKeyFail)
            {
                var error = JsonSerializer.Serialize(apiKeyFail);
                _logger.LogError($"Failed to obtain loopring api key. {error}");

                return new NftTransferResult(false)
                {
                    ErrorMessages = new() { error }
                };
            }

            return new NftTransferResult(false);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to transfer nft.");
            return new NftTransferResult(false)
            {
                ErrorMessages = new() {e.ToString()}
            };
        }
    }
}