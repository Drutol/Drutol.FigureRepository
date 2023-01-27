using System.Numerics;
using System.Text.Json;
using Drutol.FigureRepository.BlazorApp.Enums;
using Microsoft.JSInterop;

namespace Drutol.FigureRepository.BlazorApp.Infrastructure.Wallet
{
    // This class provides JavaScript functionality for GameStop wrapped
    // in a .NET class for easy consumption. The associated JavaScript module is
    // loaded on demand when first needed.
    //
    // This class can be registered as scoped DI service and then injected into Blazor
    // components for use.

    public class GameStopService : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

        public static event Func<string, Task>? AccountChangedEvent;
        public static event Func<(long, int), Task>? ChainChangedEvent;
        //public static event Func<Task>? ConnectEvent;
        //public static event Func<Task>? DisconnectEvent;

        public GameStopService(IJSRuntime jsRuntime)
        {
            _moduleTask = new(() => LoadScripts(jsRuntime).AsTask());
        }

        public ValueTask<IJSObjectReference> LoadScripts(IJSRuntime jsRuntime)
        {
            return jsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/gameStopIntegration.js");
        }

        public async ValueTask ConnectGameStop()
        {
            var module = await _moduleTask.Value;
            try
            {
                await module.InvokeVoidAsync("checkGameStop");
            }
            catch (Exception ex)
            {
                HandleExceptions(ex);
                throw;
            }
        }

        public async ValueTask<bool> HasGameStop()
        {
            var module = await _moduleTask.Value;
            try
            {
                return await module.InvokeAsync<bool>("hasGameStop");
            }
            catch (Exception ex)
            {
                HandleExceptions(ex);
                throw;
            }
        }  
        
        public async ValueTask<bool> HasMetaMask()
        {
            var module = await _moduleTask.Value;
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
            var module = await _moduleTask.Value;
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
            var module = await _moduleTask.Value;
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
            var module = await _moduleTask.Value;
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
            var module = await _moduleTask.Value;
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

        public async ValueTask<dynamic> GenericRpc(string method, params dynamic?[]? args)
        {
            var module = await _moduleTask.Value;
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
        public static async Task OnAccountsChangedMetaMask(string selectedAccount)
        {
            if (AccountChangedEvent != null)
            {
                await AccountChangedEvent.Invoke(selectedAccount);
            }
        }

        [JSInvokable]
        public static async Task OnChainChangedMetaMask(string chainhex)
        {
            if (ChainChangedEvent != null)
            {
                await ChainChangedEvent.Invoke(ChainHexToChainResponse(chainhex));
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_moduleTask.IsValueCreated)
            {
                var module = await _moduleTask.Value;
                await module.DisposeAsync();
            }
        }

        private void HandleExceptions(Exception ex)
        {
            switch (ex.Message)
            {
                case "NoGameStop":
                    throw new NoWalletException(WalletType.GameStop);
                case "UserDenied":
                    throw new WalletDeniedException(WalletType.GameStop);
            }
        }
    }
}