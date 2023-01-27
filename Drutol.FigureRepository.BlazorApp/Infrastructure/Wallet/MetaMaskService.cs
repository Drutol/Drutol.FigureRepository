using Microsoft.JSInterop;
using System.Numerics;
using System.Text.Json;
using Drutol.FigureRepository.BlazorApp.Enums;

namespace Drutol.FigureRepository.BlazorApp.Infrastructure.Wallet;

//Courtesy of https://github.com/michielpost/MetaMask.Blazor, extracted due to unneeded dependency bloat

public class MetaMaskService : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public static event Func<string, Task>? AccountChangedEvent;
    public static event Func<(long, int), Task>? ChainChangedEvent;

    public MetaMaskService(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => LoadScripts(jsRuntime).AsTask());
    }

    public ValueTask<IJSObjectReference> LoadScripts(IJSRuntime jsRuntime)
    {
        return jsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/metaMaskIntegration.js");
    }

    public async ValueTask ConnectMetaMask()
    {
        var module = await moduleTask.Value;
        try
        {
            await module.InvokeVoidAsync("checkMetaMask");
        }
        catch (Exception ex)
        {
            HandleExceptions(ex);
            throw;
        }
    }

    public async ValueTask<bool> HasMetaMask()
    {
        var module = await moduleTask.Value;
        try
        {
            return await module.InvokeAsync<bool>("hasMetaMask");
        }
        catch (Exception ex)
        {
            HandleExceptions(ex);
            throw;
        }
    }

    public async ValueTask<bool> IsSiteConnected()
    {
        var module = await moduleTask.Value;
        try
        {
            return await module.InvokeAsync<bool>("isSiteConnected");
        }
        catch (Exception ex)
        {
            HandleExceptions(ex);
            throw;
        }
    }

    public async ValueTask ListenToEvents()
    {
        var module = await moduleTask.Value;
        try
        {
            await module.InvokeVoidAsync("listenToChangeEvents");
        }
        catch (Exception ex)
        {
            HandleExceptions(ex);
            throw;
        }
    }

    public async ValueTask<string> GetSelectedAddress()
    {
        var module = await moduleTask.Value;
        try
        {
            return await module.InvokeAsync<string>("getSelectedAddress", null);
        }
        catch (Exception ex)
        {
            HandleExceptions(ex);
            throw;
        }
    }

    public async ValueTask<(long chainId, int chain)> GetSelectedChain()
    {
        var module = await moduleTask.Value;
        try
        {
            string chainHex = await module.InvokeAsync<string>("getSelectedChain", null);
            return ChainHexToChainResponse(chainHex);
        }
        catch (Exception ex)
        {
            HandleExceptions(ex);
            throw;
        }
    }

    private static (long chainId, int chain) ChainHexToChainResponse(string chainHex)
    {
        long chainId = Convert.ToInt32(chainHex, 16);
        return (chainId, (int)chainId);
    }

    public async ValueTask<long> GetTransactionCount()
    {
        var module = await moduleTask.Value;
        try
        {
            var result = await module.InvokeAsync<JsonElement>("getTransactionCount");
            var resultString = result.GetString();
            if (resultString != null)
            {
                long intValue = Convert.ToInt32(resultString, 16);
                return intValue;
            }
            return 0;
        }
        catch (Exception ex)
        {
            HandleExceptions(ex);
            throw;
        }
    }

    public async ValueTask<string> SignTypedData(string label, string value)
    {
        var module = await moduleTask.Value;
        try
        {
            return await module.InvokeAsync<string>("signTypedData", label, value);
        }
        catch (Exception ex)
        {
            HandleExceptions(ex);
            throw;
        }
    }

    public async ValueTask<string> SignTypedDataV4(string typedData)
    {
        var module = await moduleTask.Value;
        try
        {
            return await module.InvokeAsync<string>("signTypedDataV4", typedData);
        }
        catch (Exception ex)
        {
            HandleExceptions(ex);
            throw;
        }
    }

    public async ValueTask<dynamic> GenericRpc(string method, params dynamic?[]? args)
    {
        var module = await moduleTask.Value;
        try
        {
            return await module.InvokeAsync<dynamic>("genericRpc", method, args);
        }
        catch (Exception ex)
        {
            HandleExceptions(ex);
            throw;
        }
    }


    [JSInvokable]
    public static async Task OnAccountsChanged(string selectedAccount)
    {
        if (AccountChangedEvent != null)
        {
            await AccountChangedEvent.Invoke(selectedAccount);
        }
    }

    [JSInvokable]
    public static async Task OnChainChanged(string chainhex)
    {
        if (ChainChangedEvent != null)
        {
            await ChainChangedEvent.Invoke(ChainHexToChainResponse(chainhex));
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }

    private void HandleExceptions(Exception ex)
    {
        switch (ex.Message)
        {
            case "NoMetaMask":
                throw new NoWalletException(WalletType.MetaMask);
            case "UserDenied":
                throw new WalletDeniedException(WalletType.MetaMask);
        }
    }
}
