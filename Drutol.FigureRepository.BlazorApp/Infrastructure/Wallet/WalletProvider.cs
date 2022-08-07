﻿using System.Text.Json;
using Drutol.FigureRepository.BlazorApp.Enums;
using Drutol.FigureRepository.BlazorApp.Interfaces.Wallet;
using Drutol.FigureRepository.BlazorApp.Models;
using Functional.Maybe;
using MetaMask.Blazor;
using MetaMask.Blazor.Enums;
using MudBlazor;

namespace Drutol.FigureRepository.BlazorApp.Infrastructure.Wallet;

public class WalletProvider : IWalletProvider
{
    public event EventHandler StateHasChanged;
    public event EventHandler WalletPromptRequest;

    private readonly ISnackbar _snackbar;
    public Dictionary<WalletType, IWalletConnector> Wallets { get; }
    private bool _initialized;

    public bool EthereumAvailable { get; set; }

    public Maybe<IWalletConnector> CurrentWallet { get; private set; }

    public WalletProvider(ISnackbar snackbar, IEnumerable<IWalletConnector> connectors)
    {
        _snackbar = snackbar;
        Wallets = connectors.ToDictionary(connector => connector.WalletType, connector => connector);
    }

    public async Task Initialize()
    {
        if(_initialized)
            return;

        _initialized = true;

        foreach (var walletConnector in Wallets.Values)
        {
            walletConnector.WalletConnected += WalletConnectorOnWalletConnected;
            walletConnector.StateHasChanged += WalletConnectorOnStateHasChanged;
            walletConnector.WalletConnectionFailed += WalletConnectorOnWalletConnectionCancelled;
            walletConnector.WalletDisconnected += WalletConnectorOnWalletDisconnected;

            await walletConnector.Initialize();
        }
    }

    public async Task<bool> SwitchToWallet(WalletType walletType)
    {
        CurrentWallet.Do(connector => connector.Disconnect());
        CurrentWallet = Maybe<IWalletConnector>.Nothing;

        var connector = Wallets[walletType];
        if (await connector.Connect())
        {
            CurrentWallet = connector.ToMaybe();
            return true;
        }

        return false;
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
        _snackbar.Add($"Cancelled {connector.WalletType} wallet connection.", Severity.Warning);
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
                _snackbar.Add($"Changed {connector.WalletType} chain to: {account.Address} ({account.ChainId})",
                    Severity.Info);
            });
    }

    private void WalletConnectorOnWalletConnected(object? sender, EventArgs e)
    {
        var connector = (IWalletConnector)sender!;
        connector.SelectedAccount
            .Do(account =>
            {
                _snackbar.Add($"Changed {connector.WalletType} chain to: {account.Address} ({account.ChainId})",
                    Severity.Info);
            });
    }

    #endregion
}