using Drutol.FigureRepository.BlazorApp.Enums;
using Drutol.FigureRepository.BlazorApp.Models;
using Functional.Maybe;

namespace Drutol.FigureRepository.BlazorApp.Interfaces.Wallet;

public interface IWalletProvider
{
    event EventHandler StateHasChanged;
    event EventHandler WalletPromptRequest;

    Task Initialize();
    bool EthereumAvailable { get; set; }
    Dictionary<WalletType, IWalletConnector> Wallets { get; }

    Task<bool> SwitchToWallet(WalletType walletType);
    Maybe<IWalletConnector> CurrentWallet { get; }

    void RequestWalletPrompt();
    Task CheckWalletAvailability();

    WalletMetadata GetWalletMetadata(WalletType walletType);
}