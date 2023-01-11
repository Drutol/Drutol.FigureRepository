using Blazored.LocalStorage;
using Drutol.FigureRepository.BlazorApp.Enums;
using Drutol.FigureRepository.BlazorApp.Interfaces.Wallet;
using Drutol.FigureRepository.BlazorApp.Models;
using Functional.Maybe;
using MudBlazor;

namespace Drutol.FigureRepository.BlazorApp.Infrastructure.Wallet;

public class WalletProvider : IWalletProvider
{
    private const string CurrentWalletProviderKey = nameof(CurrentWalletProviderKey);

    public event EventHandler StateHasChanged;
    public event EventHandler WalletPromptRequest;

    private readonly ISnackbar _snackbar;
    private readonly ISyncLocalStorageService _localStorageService;


    public Dictionary<WalletType, IWalletConnector> Wallets { get; }
    private bool _initialized;

    public bool EthereumAvailable { get; set; }
    public Maybe<IWalletConnector> CurrentWallet { get; private set; }

    public WalletProvider(
        ISnackbar snackbar,
        ISyncLocalStorageService localStorageService,
        IEnumerable<IWalletConnector> connectors)
    {
        _snackbar = snackbar;
        _localStorageService = localStorageService;
        Wallets = connectors.ToDictionary(connector => connector.WalletType, connector => connector);
    }

    public async Task Initialize()
    {
        if (_initialized)
            return;

        _initialized = true;

        foreach (var walletConnector in Wallets.Values)
        {
            await walletConnector.Initialize();

            walletConnector.WalletConnected += WalletConnectorOnWalletConnected;
            walletConnector.StateHasChanged += WalletConnectorOnStateHasChanged;
            walletConnector.WalletConnectionFailed += WalletConnectorOnWalletConnectionCancelled;
            walletConnector.WalletDisconnected += WalletConnectorOnWalletDisconnected;
        }

    }

    public async Task SwitchToWalletIfWasPreviouslyConnected()
    {
        if (CurrentWallet.IsNothing())
        {
            if (_localStorageService.ContainKey(CurrentWalletProviderKey))
            {
                var currentWalletProviderType = _localStorageService.GetItem<WalletType>(CurrentWalletProviderKey);
                await SwitchToWallet(currentWalletProviderType);
            }
        }
    }

    public ValueTask SetCachedWalletProviderType(WalletType walletType)
    {
        _localStorageService.SetItem(CurrentWalletProviderKey, walletType);
        return ValueTask.CompletedTask;
    }

    public async Task<bool> SwitchToWallet(WalletType walletType)
    {
        CurrentWallet.Do(connector =>
        {
            connector.Disconnect();
            connector.StateHasChanged -= CurrentWalletOnStateHasChanged;
        });
        CurrentWallet = Maybe<IWalletConnector>.Nothing;
        _localStorageService.RemoveItem(CurrentWalletProviderKey);

        var connector = Wallets[walletType];
        if (await connector.Connect())
        {
            CurrentWallet = connector.ToMaybe();
            _localStorageService.SetItem(CurrentWalletProviderKey, walletType);
            connector.StateHasChanged += CurrentWalletOnStateHasChanged;
            StateHasChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }

        StateHasChanged?.Invoke(this, EventArgs.Empty);
        return false;
    }

    private void CurrentWalletOnStateHasChanged(object? sender, EventArgs e)
    {
        StateHasChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RequestWalletPrompt()
    {
        WalletPromptRequest?.Invoke(this, EventArgs.Empty);
    }

    public async Task CheckWalletAvailability()
    {
        foreach (var connector in Wallets.Values)
        {
            await Wallets[connector.WalletType].CheckIsWalletPresent();
        }
    }

    public WalletMetadata GetWalletMetadata(WalletType walletType)
    {
        return walletType switch
        {
            WalletType.MetaMask => new WalletMetadata("https://metamask.io", "svg/wallet/MetaMask.svg"),
            WalletType.GameStop => new WalletMetadata("https://wallet.gamestop.com", "svg/wallet/Gme.svg"),
            _ => throw new ArgumentOutOfRangeException(nameof(walletType), walletType, null)
        };
    }

    #region Snackbar

    private void WalletConnectorOnWalletConnectionCancelled(object? sender, EventArgs e)
    {
        var connector = (IWalletConnector)sender!;
        _snackbar.Add($"Failed to establish {connector.WalletType} wallet connection.", Severity.Warning);
    }

    private void WalletConnectorOnWalletDisconnected(object? sender, EventArgs e)
    {
        var connector = (IWalletConnector)sender!;
        _snackbar.Add($"Disconnected {connector.WalletType} wallet.", Severity.Info);
    }

    private void WalletConnectorOnStateHasChanged(object? sender, EventArgs e)
    {
        var connector = (IWalletConnector)sender!;
        connector.SelectedAccount
            .Do(account =>
            {
                if (!string.IsNullOrEmpty(account.Address))
                {
                    _snackbar.Add($"Changed {connector.WalletType} account to: {account.Address} ({account.ChainId})",
                        Severity.Info);
                }
            });
    }

    private void WalletConnectorOnWalletConnected(object? sender, EventArgs e)
    {
        var connector = (IWalletConnector)sender!;
        connector.SelectedAccount
            .Do(account =>
            {
                if (!string.IsNullOrEmpty(account.Address))
                {
                    _snackbar.Add($"Changed {connector.WalletType} account to: {account.Address} ({account.ChainId})",
                        Severity.Info);
                }
            });
    }

    #endregion
}