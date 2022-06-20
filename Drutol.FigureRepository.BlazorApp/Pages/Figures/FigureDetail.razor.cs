using System.Security.Claims;
using System.Text.Json;
using MetaMask.Blazor;
using MetaMask.Blazor.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.RPC.Web3;
using Nethereum.Util;

namespace Drutol.FigureRepository.BlazorApp.Pages.Figures
{
    public partial class FigureDetail : IDisposable
    {
        private bool _connectingToMetaMask;

        public bool EthereumAvailable { get; set; }
        public string SelectedAccountAddress { get; set; }
        public Chain? SelectedChain { get; set; }
        public bool HasMetaMask { get; set; }
        public bool IsFigureOwned { get; set; }
        public int OwnedTokensCount { get; set; }

        [Inject]
        public MetaMaskService MetaMaskService { get; set; } 
        
        [Inject]
        public ISnackbar Snackbar { get; set; }

        protected override async Task OnInitializedAsync()
        {
            MetaMaskService.AccountChangedEvent += MetaMaskAccountChangedEvent;
            MetaMaskService.ChainChangedEvent += MetaMaskChainChangedEvent;

            HasMetaMask = await MetaMaskService.HasMetaMask();
            if (HasMetaMask)
            {
                await MetaMaskService.ListenToEvents();
            }

            if (await MetaMaskService.IsSiteConnected())
            {
                EthereumAvailable = true;
                await UpdateSelectedAddress();
                await UpdateSelectedNetwork();
                await UpdateOwnership();
            }
        }

        private async Task MetaMaskChainChangedEvent((long, Chain) arg)
        {
            if(_connectingToMetaMask)
                return;

            await UpdateSelectedNetwork();
            await UpdateOwnership();
            Snackbar.Add($"Changed MetaMask chain to: {arg}", Severity.Info);
            StateHasChanged();
        }

        private async Task MetaMaskAccountChangedEvent(string arg)
        {
            if(_connectingToMetaMask)
                return;

            await UpdateSelectedAddress();
            await UpdateOwnership();
            Snackbar.Add($"Changed MetaMask account to: {arg}", Severity.Info);
            StateHasChanged();
        }

        public async Task ConnectMetaMask()
        {
            try
            {
                _connectingToMetaMask = true;
                await MetaMaskService.ConnectMetaMask();
                Snackbar.Add("Successfully connected with MetaMask!", Severity.Success);
                await UpdateSelectedAddress();
                await UpdateOwnership();
                EthereumAvailable = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Snackbar.Add($"Cancelled MetaMask wallet connection.", Severity.Warning);
            }
            finally
            {
                _connectingToMetaMask = false;
            }
        }

        private async Task UpdateOwnership()
        {
            if (Figure == null || string.IsNullOrEmpty(SelectedAccountAddress))
            {
                OwnedTokensCount = 0;
                IsFigureOwned = false;
                return;
            }

            var sha = Sha3Keccack.Current.CalculateHash("balanceOf(address)");
            var methodSignature = sha.Substring(0, 8); //first 4 bytes make the function signature
            var methodArgument = SelectedAccountAddress[2..].PadLeft(64, '0'); //arguments have to be encoded as 32 bytes
            var result = (JsonElement)await MetaMaskService.GenericRpc("eth_call", new
            {
                to = Figure.NftDetails.TokenAddress,
                data = $"0x{methodSignature}{methodArgument}"
            });
            var amount = (int)result.GetString().HexToBigInteger(false);

            OwnedTokensCount = amount;
            IsFigureOwned = amount > 0;
        }

        public async Task UpdateSelectedAddress()
        {
            SelectedAccountAddress = await MetaMaskService.GetSelectedAddress();
        }

        public async Task UpdateSelectedNetwork()
        {
            var chainInfo = await MetaMaskService.GetSelectedChain();
            SelectedChain = chainInfo.chain;
        }

        public void Dispose()
        {
            MetaMaskService.AccountChangedEvent -= MetaMaskAccountChangedEvent;
            MetaMaskService.ChainChangedEvent -= MetaMaskChainChangedEvent;
        }
    }
}
