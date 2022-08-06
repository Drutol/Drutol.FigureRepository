using System.Numerics;
using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Api.Models.Checkout;
using Drutol.FigureRepository.Api.Models.Configuration;
using Drutol.FigureRepository.Shared.Blockchain.Loopring;
using Drutol.FigureRepository.Shared.Blockchain.Loopring.Nft;
using LoopringSharp;
using Microsoft.Extensions.Options;
using Nethereum.Signer;
using Nethereum.Signer.EIP712;
using Nethereum.Util;

namespace Drutol.FigureRepository.Api.Infrastructure;

public class LoopringCommunicator : ILoopringCommunicator
{
    private readonly ILogger<LoopringCommunicator> _logger;
    private readonly IOptions<BlockchainAuthConfig> _config;
    private readonly HttpClient _client;

    public LoopringCommunicator(
        ILogger<LoopringCommunicator> logger,
        IOptions<BlockchainAuthConfig> config)
    {
        _logger = logger;
        _config = config;
        _client = new HttpClient
        {
            BaseAddress = new Uri(_config.Value.LoopringApiUrl)
        };
    }

    public async ValueTask<IAccountResponseModel> GetAccount(string walletAddress)
    {
        try
        {
            var result = await _client
                .SendAsync(new HttpRequestMessage(HttpMethod.Get, $"account?owner={walletAddress}"))
                .ConfigureAwait(false);

            return result.IsSuccessStatusCode switch
            {
                true => (await result.Content.ReadFromJsonAsync<IAccountResponseModel.Success>())!,
                false => (await result.Content.ReadFromJsonAsync<IAccountResponseModel.Fail>())!,
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to obtain account.");
            return new IAccountResponseModel.Fail();
        }
    }

    public async ValueTask<INftBalancesResponseModel> GetBalances(string apiKey, int accountId, string nftData)
    {
        try
        {
            var result = await _client
                .SendAsync(new HttpRequestMessage(HttpMethod.Get, $"user/nft/balances?accountId={accountId}&nftDatas={nftData}")
                {
                    Headers =
                    {
                        { "X-API-KEY", apiKey }
                    }
                }).ConfigureAwait(false);


            return result.IsSuccessStatusCode switch
            {
                true => (await result.Content.ReadFromJsonAsync<INftBalancesResponseModel.Success>())!,
                false => (await result.Content.ReadFromJsonAsync<INftBalancesResponseModel.Fail>())!,
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to obtain nft balances.");
            return new INftBalancesResponseModel.Fail();
        }
    }

    public async ValueTask<IGetOffchainFeeResponseModel> GetOffchainFee(string apiKey, int accountId, NftTransferType type, string nftTokenAddress, int amount)
    {
        var result = await _client
            .SendAsync(new HttpRequestMessage(HttpMethod.Get, $"user/nft/offchainFee?accountId={accountId}&requestType={(int)type}&tokenSymbol={nftTokenAddress}&amount={amount}")
            {
                Headers =
                {
                    { "X-API-KEY", apiKey }
                }
            }).ConfigureAwait(false);

        return result.IsSuccessStatusCode switch
        {
            true => (await result.Content.ReadFromJsonAsync<IGetOffchainFeeResponseModel.Success>())!,
            false => (await result.Content.ReadFromJsonAsync<IGetOffchainFeeResponseModel.Fail>())!,
        };
    }

    public async ValueTask<IGetStorageIdResponseModel> GetStorageId(string apiKey, int accountId)
    {
        var result = await _client
            .SendAsync(new HttpRequestMessage(HttpMethod.Get, $"storageId?accountId={accountId}&sellTokenId=0")
            {
                Headers =
                {
                    { "X-API-KEY", apiKey }
                }
            }).ConfigureAwait(false);

        return result.IsSuccessStatusCode switch
        {
            true => (await result.Content.ReadFromJsonAsync<IGetStorageIdResponseModel.Success>())!,
            false => (await result.Content.ReadFromJsonAsync<IGetStorageIdResponseModel.Fail>())!,
        };
    }

    public async ValueTask<IApiKeyResponseModel> GetApiKey(string signedDataHash, string walletAddress, int accountId)
    {
        var key = EDDSAHelper.RipKeyAppart((signedDataHash, walletAddress));
        var signature = EDDSAHelper.EddsaSignUrl(
            key.secretKey,
            HttpMethod.Get,
            new List<(string Key, string Value)>
            {
                ("accountId", accountId.ToString())
            },
            null,
            "apiKey",
            _config.Value.LoopringApiUrl);

        try
        {
            var result = await _client
                .SendAsync(new HttpRequestMessage(HttpMethod.Get, $"apiKey?accountId={accountId}")
                {
                    Headers =
                    {
                        { "X-API-SIG", signature }
                    }
                }).ConfigureAwait(false);

            return result.IsSuccessStatusCode switch
            {
                true => (await result.Content.ReadFromJsonAsync<IApiKeyResponseModel.Success>())!,
                false => (await result.Content.ReadFromJsonAsync<IApiKeyResponseModel.Fail>())!,
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to obtain api key.");
            return new IApiKeyResponseModel.Fail();
        }
    }

    public async ValueTask<ITransferNftResponseModel> TransferFigureNft(string apiKey, TransferNftRequestModel requestModel)
    {
        // Courtesy of https://github.com/cobmin/LoopringBatchNftTransferDemoSharp
        var primaryTypeName = "Transfer";
        var validUntil = (int)DateTimeOffset.UtcNow.AddDays(100).ToUnixTimeSeconds();
        var eip712TypedData = new TypedData
        {
            Domain = new Domain()
            {
                Name = "Loopring Protocol",
                Version = "3.6.0",
                ChainId = _config.Value.LoopringChainId,
                VerifyingContract = _config.Value.LoopringExchangeAddress,
            },
            PrimaryType = primaryTypeName,
            Types = new Dictionary<string, MemberDescription[]>()
            {
                ["EIP712Domain"] = new[]
                {
                    new MemberDescription { Name = "name", Type = "string" },
                    new MemberDescription { Name = "version", Type = "string" },
                    new MemberDescription { Name = "chainId", Type = "uint256" },
                    new MemberDescription { Name = "verifyingContract", Type = "address" },
                },
                [primaryTypeName] = new[]
                {
                    new MemberDescription { Name = "from", Type = "address" }, // payerAddr
                    new MemberDescription { Name = "to", Type = "address" }, // toAddr
                    new MemberDescription { Name = "tokenID", Type = "uint16" }, // token.tokenId 
                    new MemberDescription { Name = "amount", Type = "uint96" }, // token.volume 
                    new MemberDescription { Name = "feeTokenID", Type = "uint16" }, // maxFee.tokenId
                    new MemberDescription { Name = "maxFee", Type = "uint96" }, // maxFee.volume
                    new MemberDescription { Name = "validUntil", Type = "uint32" }, // validUntill
                    new MemberDescription { Name = "storageID", Type = "uint32" } // storageId
                },
            },
            Message = new[]
            {
                new MemberValue { TypeName = "address", Value = requestModel.FromAddress },
                new MemberValue { TypeName = "address", Value = requestModel.ToAddress },
                new MemberValue { TypeName = "uint16", Value = requestModel.TokenData.TokenId },
                new MemberValue { TypeName = "uint96", Value = BigInteger.Parse(requestModel.TokenData.Amount) },
                new MemberValue { TypeName = "uint16", Value = requestModel.Fee.TokenId },
                new MemberValue { TypeName = "uint96", Value = BigInteger.Parse(requestModel.Fee.Amount) },
                new MemberValue { TypeName = "uint32", Value = validUntil },
                new MemberValue { TypeName = "uint32", Value = requestModel.StorageId },
            }
        };

        requestModel.Exchange = _config.Value.LoopringExchangeAddress;
        requestModel.ValidUntil = validUntil;

        var signer = new Eip712TypedDataSigner();
        var ethECKey = new EthECKey(_config.Value.SourceAccountL1Key);
        var encodedTypedData = signer.EncodeTypedData(eip712TypedData);
        var ECDRSASignature = ethECKey.SignAndCalculateV(Sha3Keccack.Current.CalculateHash(encodedTypedData));
        var serializedECDRSASignature = EthECDSASignature.CreateStringSignature(ECDRSASignature);
        var ecdsaSignature = $"{serializedECDRSASignature}02";

        requestModel.EcdsaSignature = ecdsaSignature;

        using var content = JsonContent.Create(requestModel);
        var result = await _client
            .SendAsync(new HttpRequestMessage(HttpMethod.Post, $"nft/transfer")
            {
                Headers =
                {
                    { "X-API-KEY", apiKey },
                    { "X-API-SIG", ecdsaSignature }
                },
                Content = content
            }).ConfigureAwait(false);

        return result.IsSuccessStatusCode switch
        {
            true => (await result.Content.ReadFromJsonAsync<ITransferNftResponseModel.Success>())!,
            false => (await result.Content.ReadFromJsonAsync<ITransferNftResponseModel.Fail>())!,
        };
    }
}