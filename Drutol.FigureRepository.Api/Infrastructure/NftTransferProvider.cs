using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Api.Models.Checkout;
using Drutol.FigureRepository.Api.Models.Configuration;
using Drutol.FigureRepository.Shared.Blockchain.Loopring;
using Microsoft.Extensions.Options;

namespace Drutol.FigureRepository.Api.Infrastructure
{
    public class NftTransferProvider
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

        public async Task<> TransferNft(OrderEntity order)
        {
            var figure = _figureSeedManager[order.FigureId];

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
                    _config.Value.SourceAccountId);

                if (feesResponse is IGetOffchainFeeResponseModel.Success fees &&
                    storageIdResponse is IGetStorageIdResponseModel.Success storageId)
                {
                    var transferResponse = _loopringCommunicator.TransferFigureNft(
                        _config.Value.SourceAccountId,
                        _config.Value.SourceAccountAddress,
                        order.AccountId,
                        order.WalletAddress,
                        figure.NftDetails.TokenId,
                        figure.NftDetails.TokenNftData,
                        amount: 1,
                        fees.Fees.First(fee => fee.TokenId == 0).RequiredFee,
                        storageId.OffchainId,
                        order.IncludesWalletActivation
                    );
                }
            }

        }
    }
}
