﻿using Drutol.FigureRepository.BlazorApp.Enums;
using Drutol.FigureRepository.BlazorApp.Models;
using Functional.Maybe;

namespace Drutol.FigureRepository.BlazorApp.Interfaces.Wallet;

public interface IWalletProvider
{
    event EventHandler StateHasChanged;
    event EventHandler WalletPromptRequest;

    Task Initialize();
    bool EthereumAvailable { get; set; }
    ValueTask SetCachedWalletProviderType(WalletType walletType);
    Dictionary<WalletType, IWalletConnector> Wallets { get; }

    Task<bool> SwitchToWallet(WalletType walletType);
    Task SwitchToWalletIfWasPreviouslyConnected();
    Maybe<IWalletConnector> CurrentWallet { get; }

    void RequestWalletPrompt();
    Task CheckWalletAvailability();

    WalletMetadata GetWalletMetadata(WalletType walletType);
}