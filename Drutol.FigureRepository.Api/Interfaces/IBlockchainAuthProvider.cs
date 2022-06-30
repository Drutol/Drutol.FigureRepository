using Drutol.FigureRepository.Shared.Blockchain;

namespace Drutol.FigureRepository.Api.Interfaces;

public interface IBlockchainAuthProvider
{
    Task Initialize();

    ValueTask<StartAuthenticationResult> StartAuthentication(StartAuthenticationRequest startAuthenticationRequest);
    ValueTask<FinishAuthenticationResult> FinishAuthentication(FinishAuthenticationRequest finishAuthenticationRequest);
}