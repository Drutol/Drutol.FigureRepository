using Drutol.FigureRepository.BlazorApp.Enums;

namespace Drutol.FigureRepository.BlazorApp.Infrastructure.Wallet;

public class WalletDeniedException : Exception
{
    public WalletDeniedException(WalletType walletType) : base($"User denied {walletType} wallet access.")
    {

    }
}
