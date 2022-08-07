using Drutol.FigureRepository.BlazorApp.Enums;
using Drutol.FigureRepository.BlazorApp.Interfaces.Wallet;
using Drutol.FigureRepository.BlazorApp.Models;
using Functional.Maybe;

namespace Drutol.FigureRepository.BlazorApp.Infrastructure.Wallet;

public class GameStopWalletConnector : IWalletConnector
{
    public WalletType WalletType { get; } = WalletType.GameStop;

    public event EventHandler? StateHasChanged;
    public event EventHandler? WalletConnected;
    public event EventHandler? WalletDisconnected;
    public event EventHandler? WalletConnectionFailed;

    public bool IsWalletPresent { get; }

    public Maybe<WalletAccount> SelectedAccount { get; }

    public Task Initialize()
    {
        return Task.CompletedTask;
    }

    public Task<bool> Connect()
    {
        return Task.FromResult(false);
    }

    public Task Disconnect()
    {
        return Task.CompletedTask;
    }

    public async Task<bool> CheckIsWalletPresent()
    {
        return false;
    }

    public Task<string> PersonalSign(string dataToSign)
    {
        return Task.FromResult<string>("");
    }

    public Task<string> SignTypedDataV4(string dataToSign)
    {
        return Task.FromResult<string>("");
    }
}