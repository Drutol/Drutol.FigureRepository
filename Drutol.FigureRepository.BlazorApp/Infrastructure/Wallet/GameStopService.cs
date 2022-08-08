﻿using System.Numerics;
using System.Text.Json;
using MetaMask.Blazor.Enums;
using MetaMask.Blazor.Exceptions;
using MetaMask.Blazor.Extensions;
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
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public static event Func<string, Task>? AccountChangedEvent;
        public static event Func<(long, Chain), Task>? ChainChangedEvent;
        //public static event Func<Task>? ConnectEvent;
        //public static event Func<Task>? DisconnectEvent;

        public GameStopService(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => LoadScripts(jsRuntime).AsTask());
        }

        public ValueTask<IJSObjectReference> LoadScripts(IJSRuntime jsRuntime)
        {
            return jsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/gameStopIntegration.js");
        }

        public async ValueTask ConnectGameStop()
        {
            var module = await moduleTask.Value;
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
            var module = await moduleTask.Value;
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

        public async ValueTask<(long chainId, Chain chain)> GetSelectedChain()
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

        private static (long chainId, Chain chain) ChainHexToChainResponse(string chainHex)
        {
            long chainId = chainHex.HexToInt();
            return (chainId, (Chain)chainId);
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
                    long intValue = resultString.HexToInt();
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

        public async ValueTask<string> SendTransaction(string to, BigInteger weiValue, string? data = null)
        {
            var module = await moduleTask.Value;
            try
            {
                string hexValue = "0x" + weiValue.ToString("X");
                return await module.InvokeAsync<string>("sendTransaction", to, hexValue, data);
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

        public async Task<string> RequestAccounts()
        {
            var result = await GenericRpc("eth_requestAccounts");

            return result.ToString();
        }

        public async Task<long> GetBalance(string address, string block = "latest")
        {
            var result = await GenericRpc("eth_getBalance", address, block);

            string hex = result.ToString();

            return hex.HexToInt();
        }

        //[JSInvokable()]
        //public static async Task OnConnect()
        //{
        //    Console.WriteLine("connected");
        //    if (ConnectEvent != null)
        //    {
        //        await ConnectEvent.Invoke();
        //    }
        //}

        //[JSInvokable()]
        //public static async Task OnDisconnect()
        //{
        //    Console.WriteLine("disconnected");
        //    if (DisconnectEvent != null)
        //    {
        //        await DisconnectEvent.Invoke();
        //    }
        //}

        [JSInvokable()]
        public static async Task OnAccountsChanged(string selectedAccount)
        {
            if (AccountChangedEvent != null)
            {
                await AccountChangedEvent.Invoke(selectedAccount);
            }
        }

        [JSInvokable()]
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
                case "NoGameStop":
                    throw new NoMetaMaskException();
                case "UserDenied":
                    throw new UserDeniedException();
            }
        }
    }
}