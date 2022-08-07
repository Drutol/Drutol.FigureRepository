using System.Text.Json;
using Drutol.FigureRepository.BlazorApp.Enums;
using Drutol.FigureRepository.BlazorApp.Interfaces.Wallet;
using Drutol.FigureRepository.BlazorApp.Models;
using Drutol.FigureRepository.BlazorApp.Util;
using Functional.Maybe;
using MetaMask.Blazor.Enums;

namespace Drutol.FigureRepository.BlazorApp.Infrastructure.Wallet;

public class GameStopWalletConnector : IWalletConnector
{
    public WalletType WalletType { get; } = WalletType.GameStop;

    public event EventHandler StateHasChanged;
    public event EventHandler WalletConnected;
    public event EventHandler WalletDisconnected;
    public event EventHandler WalletConnectionFailed;

    private readonly Dictionary<int, string> _signCache = new();
    private readonly ILogger<GameStopWalletConnector> _logger;
    private readonly GameStopService _gameStopService;
    private bool _connectingToGameStop;

    public bool IsWalletPresent { get; private set; }

    public Maybe<WalletAccount> SelectedAccount { get; set; }

    public GameStopWalletConnector(
        ILogger<GameStopWalletConnector> logger,
        GameStopService gameStopService)
    {
        _logger = logger;
        _gameStopService = gameStopService;
    }

    public async Task Initialize()
    {
        IsWalletPresent = await _gameStopService.HasGameStop();

        if (IsWalletPresent)
        {
            await _gameStopService.ListenToEvents();
        }

        if (await _gameStopService.IsSiteConnected())
        {
            await UpdateSelectedAccount();
        }
    }

    public async Task<bool> Connect()
    {
        try
        {
            _connectingToGameStop = true;
            await _gameStopService.ConnectGameStop();

            GameStopService.AccountChangedEvent += GameStopAccountChangedEvent;
            GameStopService.ChainChangedEvent += GameStopChainChangedEvent;

            await UpdateSelectedAccount();

            WalletConnected?.Invoke(this, EventArgs.Empty);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to connect to GameStop.");
            WalletConnectionFailed?.Invoke(this, EventArgs.Empty);
            return false;
        }
        finally
        {
            _connectingToGameStop = false;
        }
    }

    public async Task Disconnect()
    {
        GameStopService.AccountChangedEvent -= GameStopAccountChangedEvent;
        GameStopService.ChainChangedEvent -= GameStopChainChangedEvent;
        WalletDisconnected?.Invoke(this, EventArgs.Empty);
    }

    private async Task GameStopChainChangedEvent((long, Chain) arg)
    {
        if (_connectingToGameStop)
            return;

        await UpdateSelectedAccount();
    }

    private async Task GameStopAccountChangedEvent(string arg)
    {
        _signCache.Clear();

        if (_connectingToGameStop)
            return;

        await UpdateSelectedAccount();
    }

    public async Task UpdateSelectedAccount()
    {
        var address = await _gameStopService.GetSelectedAddress();

        if (string.IsNullOrEmpty(address))
        {
            SelectedAccount = Maybe<WalletAccount>.Nothing;
        }
        else
        {
            SelectedAccount = new WalletAccount(
                address,
                (await _gameStopService.GetSelectedChain()).chainId).ToMaybe();
        }

        StateHasChanged?.Invoke(this, EventArgs.Empty);
    }

    public async Task<bool> CheckIsWalletPresent()
    {
        IsWalletPresent = await _gameStopService.HasGameStop();
        return IsWalletPresent;
    }

    public async Task<string> PersonalSign(string dataToSign)
    {
        var hash = dataToSign.GetDeterministicHashCode();
        if (_signCache.TryGetValue(hash, out var signature))
            return signature;

        var result = ((JsonElement)await _gameStopService.GenericRpc(
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
        return await _gameStopService.SignTypedDataV4(dataToSign.Trim('\''));
    }
}