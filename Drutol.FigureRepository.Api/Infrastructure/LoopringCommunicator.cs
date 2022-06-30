using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Api.Models.Configuration;
using Drutol.FigureRepository.Shared.Blockchain.Loopring;
using LoopringSharp;
using Microsoft.Extensions.Options;

namespace Drutol.FigureRepository.Api.Infrastructure
{
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
                    .GetFromJsonAsync<IAccountResponseModel.Success>($"account?owner={walletAddress}")
                    .ConfigureAwait(false);

                if (result == null)
                    return new IAccountResponseModel.Fail();

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to obtain account.");
                return new IAccountResponseModel.Fail();
            }
        }

        public async ValueTask<INftBalancesResponseModel> GetBalances(string signedDataHash, string walletAddress, int accountId, int tokenId)
        {
            try
            {
                return await GetApiKey(signedDataHash, walletAddress, accountId) switch
                {
                    IApiKeyResponseModel.Success success => await GetBalances(success.ApiKey).ConfigureAwait(false),
                    IApiKeyResponseModel.Fail fail => new INftBalancesResponseModel.Fail(),
                    _ => new INftBalancesResponseModel.Fail()
                };

                async ValueTask<INftBalancesResponseModel> GetBalances(string apiKey)
                {
                    var result = await _client
                        .SendAsync(new HttpRequestMessage(HttpMethod.Get, $"user/nft/balances?accountId={accountId}&tokenIds={tokenId}")
                        {
                            Headers =
                            {
                                { "X-API-KEY", apiKey }
                            }
                        }).ConfigureAwait(false);

                    var model = await result.Content.ReadFromJsonAsync<INftBalancesResponseModel.Success>()
                        .ConfigureAwait(false);

                    if (model == null)
                        return new INftBalancesResponseModel.Fail();

                    return model;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to obtain nft balances.");
                return new INftBalancesResponseModel.Fail();
            }
        }

        private async ValueTask<IApiKeyResponseModel> GetApiKey(string signedDataHash, string walletAddress, int accountId)
        {
            var key = EDDSAHelper.RipKeyAppart((signedDataHash, walletAddress));
            var signature = EDDSAHelper.EddsaSignUrl(key.secretKey, HttpMethod.Get, new List<(string Key, string Value)>
            {
                ("accountId", accountId.ToString())
            }, null, "apiKey", _config.Value.LoopringApiUrl);

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

                var model = await result.Content.ReadFromJsonAsync<IApiKeyResponseModel.Success>()
                    .ConfigureAwait(false);

                if (model == null)
                    return new IApiKeyResponseModel.Fail();

                return model;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to obtain api key.");
                return new IApiKeyResponseModel.Fail();
            }
        }
    }
}
