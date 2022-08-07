using System.Text.Json;
using Drutol.FigureRepository.BlazorApp.Enums;
using Drutol.FigureRepository.BlazorApp.Interfaces.Wallet;
using Drutol.FigureRepository.BlazorApp.Models;
using Drutol.FigureRepository.BlazorApp.Util;
using Functional.Maybe;
using MetaMask.Blazor;
using MetaMask.Blazor.Enums;
using MudBlazor;

namespace Drutol.FigureRepository.BlazorApp.Infrastructure.Wallet;

public class MetaMaskWalletConnector : IWalletConnector
{
    public WalletType WalletType { get; } = WalletType.MetaMask;

    public event EventHandler StateHasChanged;
    public event EventHandler WalletConnected;
    public event EventHandler WalletDisconnected;
    public event EventHandler WalletConnectionFailed;

    private readonly Dictionary<int, string> _signCache = new();
    private readonly ILogger<MetaMaskWalletConnector> _logger;
    private readonly MetaMaskService _metaMaskService;
    private bool _connectingToMetaMask;

    public bool IsWalletPresent { get; private set; }

    public Maybe<WalletAccount> SelectedAccount { get; set; }

    public MetaMaskWalletConnector(
        ILogger<MetaMaskWalletConnector> logger,
        MetaMaskService metaMaskService)
    {
        _logger = logger;
        _metaMaskService = metaMaskService;
    }

    public async Task Initialize()
    {
        IsWalletPresent = await _metaMaskService.HasMetaMask();

        if (IsWalletPresent)
        {
            await _metaMaskService.ListenToEvents();
        }

        if (await _metaMaskService.IsSiteConnected())
        {
            await UpdateSelectedAccount();
        }
    }

    public async Task<bool> Connect()
    {
        try
        {
            _connectingToMetaMask = true;
            await _metaMaskService.ConnectMetaMask();

            MetaMaskService.AccountChangedEvent += MetaMaskAccountChangedEvent;
            MetaMaskService.ChainChangedEvent += MetaMaskChainChangedEvent;

            await UpdateSelectedAccount();

            WalletConnected?.Invoke(this, EventArgs.Empty);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to connect to MetaMask.");
            WalletConnectionFailed?.Invoke(this, EventArgs.Empty);
            return false;
        }
        finally
        {
            _connectingToMetaMask = false;
        }
    }

    public async Task Disconnect()
    {
        MetaMaskService.AccountChangedEvent -= MetaMaskAccountChangedEvent;
        MetaMaskService.ChainChangedEvent -= MetaMaskChainChangedEvent;
        WalletDisconnected?.Invoke(this, EventArgs.Empty);
    }

    private async Task MetaMaskChainChangedEvent((long, Chain) arg)
    {
        if (_connectingToMetaMask)
            return;

        await UpdateSelectedAccount();
    }

    private async Task MetaMaskAccountChangedEvent(string arg)
    {
        _signCache.Clear();

        if (_connectingToMetaMask)
            return;

        await UpdateSelectedAccount();
    }

    public async Task UpdateSelectedAccount()
    {
        SelectedAccount = new WalletAccount(
            await _metaMaskService.GetSelectedAddress(),
            (await _metaMaskService.GetSelectedChain()).chainId).ToMaybe();

        StateHasChanged?.Invoke(this, EventArgs.Empty);
    }

    public async Task<bool> CheckIsWalletPresent()
    {
        IsWalletPresent = await _metaMaskService.HasMetaMask();
        return IsWalletPresent;
    }

    public async Task<string> PersonalSign(string dataToSign)
    {
        var hash = dataToSign.GetDeterministicHashCode();
        if (_signCache.TryGetValue(hash, out var signature))
            return signature;

        var result = ((JsonElement)await _metaMaskService.GenericRpc(
                "personal_sign",
                new[]
                {
                    dataToSign,
                    SelectedAccount.SelectOrElse(account => account.Address, () => null!)
                }))
            .GetString()!;

        _signCache[hash] = result;
        return result;
    }

    public async Task<string> SignTypedDataV4(string dataToSign)
    {
        return await _metaMaskService.SignTypedDataV4(dataToSign.Trim('\''));
    }
}