using Drutol.FigureRepository.BlazorApp.Enums;

namespace Drutol.FigureRepository.BlazorApp.Infrastructure.Wallet;

public class NoWalletException : Exception
{
    public NoWalletException(WalletType walletType) : base($"No {walletType} wallet found.")
    {

    }
}
