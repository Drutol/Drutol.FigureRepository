using Drutol.FigureRepository.BlazorApp.Enums;
using Drutol.FigureRepository.BlazorApp.Models;
using Functional.Maybe;

namespace Drutol.FigureRepository.BlazorApp.Interfaces.Wallet;

public interface IWalletConnector
{
    WalletType WalletType { get; }

    event EventHandler StateHasChanged;
    event EventHandler WalletConnected;
    event EventHandler WalletDisconnected;
    event EventHandler WalletConnectionFailed;

    bool IsWalletPresent { get; }
    Task<bool> CheckIsWalletPresent();

    Maybe<WalletAccount> SelectedAccount { get; }

    Task Initialize();
    Task<bool> Connect();
    Task Disconnect();

    Task<string> PersonalSign(string dataToSign);
    Task<string> SignTypedDataV4(string dataToSign);
}