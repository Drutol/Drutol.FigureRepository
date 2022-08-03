using Drutol.FigureRepository.Api.Models.Checkout;
using Drutol.FigureRepository.Shared.Blockchain.Loopring;

namespace Drutol.FigureRepository.Api.Interfaces;

public interface ILoopringCommunicator
{
    ValueTask<IAccountResponseModel> GetAccount(string walletAddress);
    ValueTask<INftBalancesResponseModel> GetBalances(string signedDataHash, string walletAddress, int accountId, int tokenId);

    ValueTask<IGetOffchainFeeResponseModel> GetOffchainFee(string apiKey, int accountId, NftTransferType type, string nftTokenAddress, int amount);
    ValueTask<IGetStorageIdResponseModel> GetStorageId(string apiKey, int accountId);

    ValueTask<IApiKeyResponseModel> GetApiKey(string signedDataHash, string walletAddress, int accountId);

    ValueTask<ITransferNftResponseModel> TransferFigureNft(
        int sourceAccountId,
        string sourceAccountAddress,
        int targetAccountId,
        string targetAccountAddress,
        int tokenId,
        string tokenNftData,
        int amount,
        string maxFee,
        int storageId,
        bool activateTargetAccount);

}