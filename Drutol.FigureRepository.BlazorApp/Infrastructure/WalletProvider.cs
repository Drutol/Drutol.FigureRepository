using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Drutol.FigureRepository.BlazorApp.Interfaces;
using Drutol.FigureRepository.Shared.Blockchain;
using Drutol.FigureRepository.Shared.Blockchain.Loopring;
using Drutol.FigureRepository.Shared.Models;
using MetaMask.Blazor;
using MetaMask.Blazor.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Drutol.FigureRepository.BlazorApp.Infrastructure;

public class WalletProvider : IWalletProvider
{
    public event EventHandler StateHasChanged;
    private bool _connectingToMetaMask;

    private readonly MetaMaskService _metaMaskService;
    private readonly ISnackbar _snackbar;
    private readonly Dictionary<int, string> _signCache = new();
    public bool EthereumAvailable { get; set; }
    public bool HasMetaMask { get; set; }
    public string SelectedAccountAddress { get; set; }
    public Chain? SelectedChain { get; set; }


    public WalletProvider(
        MetaMaskService metaMaskService,
        ISnackbar snackbar)
    {
        _metaMaskService = metaMaskService;
        _snackbar = snackbar;

        MetaMaskService.AccountChangedEvent += MetaMaskAccountChangedEvent;
        MetaMaskService.ChainChangedEvent += MetaMaskChainChangedEvent;
    }

    public async Task Initialize()
    {
        HasMetaMask = await _metaMaskService.HasMetaMask();
        if (HasMetaMask)
        {
            await _metaMaskService.ListenToEvents();
        }

        if (await _metaMaskService.IsSiteConnected())
        {
            EthereumAvailable = true;
            await UpdateSelectedAddress();
            await UpdateSelectedNetwork();
        }
    }

    #region MetaMask

    private async Task MetaMaskChainChangedEvent((long, Chain) arg)
    {
        if (_connectingToMetaMask)
            return;

        await UpdateSelectedNetwork();
        _snackbar.Add($"Changed MetaMask chain to: {arg}", Severity.Info);
        StateHasChanged?.Invoke(this, EventArgs.Empty);
    }

    private async Task MetaMaskAccountChangedEvent(string arg)
    {
        if (_connectingToMetaMask)
            return;

        await UpdateSelectedAddress();
        _snackbar.Add($"Changed MetaMask account to: {arg}", Severity.Info);
        StateHasChanged?.Invoke(this, EventArgs.Empty);
    }

    public async Task ConnectMetaMask()
    {
        try
        {
            _connectingToMetaMask = true;
            await _metaMaskService.ConnectMetaMask();
            _snackbar.Add("Successfully connected with MetaMask!", Severity.Success);
            await UpdateSelectedAddress();
            await UpdateSelectedNetwork();
            EthereumAvailable = true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            _snackbar.Add($"Cancelled MetaMask wallet connection.", Severity.Warning);
        }
        finally
        {
            _connectingToMetaMask = false;
        }
    }

    public async Task UpdateSelectedAddress()
    {
        SelectedAccountAddress = await _metaMaskService.GetSelectedAddress();
    }

    public async Task UpdateSelectedNetwork()
    {
        var chainInfo = await _metaMaskService.GetSelectedChain();
        SelectedChain = chainInfo.chain;
    }

    public async Task<string> PersonalSign(string dataToSign)
    {
        var hash = GetDeterministicHashCode(dataToSign);
        if (_signCache.TryGetValue(hash, out var signature))
            return signature;

        var result = ((JsonElement)await _metaMaskService.GenericRpc(
                "personal_sign",
                new string[]
                {
                    dataToSign,
                    SelectedAccountAddress
                }))
            .GetString();

        _signCache[hash] = result;
        return result;
    }

    public async Task<string> SignTypedDataV4(string dataToSign)
    {
        return await _metaMaskService.SignTypedDataV4(dataToSign.Trim('\''));
    }

    private int GetDeterministicHashCode(string str)
    {
        unchecked
        {
            int hash1 = (5381 << 16) + 5381;
            int hash2 = hash1;

            for (int i = 0; i < str.Length; i += 2)
            {
                hash1 = ((hash1 << 5) + hash1) ^ str[i];
                if (i == str.Length - 1)
                    break;
                hash2 = ((hash2 << 5) + hash2) ^ str[i + 1];
            }

            return hash1 + (hash2 * 1566083941);
        }
    }

    #endregion
}