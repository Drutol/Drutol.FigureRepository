using Drutol.FigureRepository.Shared.Blockchain.Loopring;

namespace Drutol.FigureRepository.Api.Interfaces;

public interface ILoopringCommunicator
{
    ValueTask<IAccountResponseModel> GetAccount(string walletAddress);
    ValueTask<INftBalancesResponseModel> GetBalances(string signedDataHash, string walletAddress, int accountId, int tokenId);
}