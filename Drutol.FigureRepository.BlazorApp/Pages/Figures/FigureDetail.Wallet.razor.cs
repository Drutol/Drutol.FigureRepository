using System.Diagnostics;
using System.Net.Http.Json;
using System.Numerics;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Drutol.FigureRepository.BlazorApp.Interfaces;
using Drutol.FigureRepository.Shared.Blockchain;
using Drutol.FigureRepository.Shared.Blockchain.Loopring;
using LoopringSharp;
using MetaMask.Blazor;
using MetaMask.Blazor.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.RPC.Web3;
using Nethereum.Util;
using PoseidonSharp;

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
        public bool CanCheckOwnership { get; set; }

        private readonly HttpClient _loopringClient = new HttpClient
        {
            BaseAddress = new Uri("https://api.loopring.network/api/v3/")
        };

        [Inject]
        public MetaMaskService MetaMaskService { get; set; } 
        
        [Inject]
        public ISnackbar Snackbar { get; set; }  
        
        [Inject]
        public IApiHttpClient HttpClient { get; set; }

        #region MetaMask

        private async Task MetaMaskChainChangedEvent((long, Chain) arg)
        {
            if (_connectingToMetaMask)
                return;

            await UpdateSelectedNetwork();
            await UpdateCanCheckOwnership();
            Snackbar.Add($"Changed MetaMask chain to: {arg}", Severity.Info);
            StateHasChanged();
        }

        private async Task MetaMaskAccountChangedEvent(string arg)
        {
            if (_connectingToMetaMask)
                return;

            await UpdateSelectedAddress();
            await UpdateCanCheckOwnership();
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
                await UpdateCanCheckOwnership();
                await UpdateSelectedNetwork();
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

        private async Task UpdateCanCheckOwnership()
        {
            if (Figure == null || string.IsNullOrEmpty(SelectedAccountAddress))
            {
                IsFigureOwned = false;
                return;
            }

            try
            {
                CanCheckOwnership = await GetLoopringAccount(SelectedAccountAddress) switch
                {
                    IAccountResponseModel.Success => true,
                    _ => false,
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                CanCheckOwnership = false;
            }
        }
        
        private async Task UpdateOwnership()
        {
            if (Figure == null || string.IsNullOrEmpty(SelectedAccountAddress) || !CanCheckOwnership)
            {
                IsFigureOwned = false;
                return;
            }

            try
            {
                //var sha = Sha3Keccack.Current.CalculateHash("balanceOf(address)");
                //var methodSignature = sha.Substring(0, 8); //first 4 bytes make the function signature
                //var methodArgument = SelectedAccountAddress[2..].PadLeft(64, '0'); //arguments have to be encoded as 32 bytes
                //var result = (JsonElement)await MetaMaskService.GenericRpc("eth_call", new
                //{
                //    to = Figure.NftDetails.TokenAddress,
                //    data = $"0x{methodSignature}{methodArgument}"
                //}, "latest");
                //var amount = (int)result.GetString().HexToBigInteger(false);

                //ethereum.request({
                //    method: 'personal_sign',
                //        params: ['Sign this message to access Loopring Exchange: 0x0BABA1Ad5bE3a5C0a66E7ac838a129Bf948f1eA4 with key nonce: 0', '0x59eb7c1e8e357ef2b4eb7532cab64c6538292ac6']
                //});

                using var authStartContent = JsonContent.Create(new StartAuthenticationRequest(
                    StartAuthenticationRequest.AuthenticationType.Loopring,
                    ((int?)SelectedChain) ?? 0,
                    SelectedAccountAddress,
                    Figure.Guid));
                var response = await
                    (await HttpClient.Client.PostAsync("/api/auth/StartAuthentication", authStartContent)).Content.ReadFromJsonAsync<StartAuthenticationResult>();

                var signature = (JsonElement)await MetaMaskService.GenericRpc("personal_sign", new string[]
                {
                    response.DataToSign,
                    SelectedAccountAddress
                });

                var authFinishContent = JsonContent.Create(new FinishAuthenticationRequest(response.SessionGuid, signature.GetString()));
                var auth = await
                    (await HttpClient.Client.PostAsync("/api/auth/FinishAuthentication", authFinishContent)).Content.ReadFromJsonAsync<FinishAuthenticationResult>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                IsFigureOwned = false;
            }
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

        #endregion

        private async void Download(MouseEventArgs obj)
        {
            using var authStartContent = JsonContent.Create(new StartAuthenticationRequest(
                StartAuthenticationRequest.AuthenticationType.Loopring,
                (int)SelectedChain.Value,
                SelectedAccountAddress,
                Figure.Guid));
            var response = await 
                (await HttpClient.Client.PostAsync("/api/auth/StartAuthentication", authStartContent)).Content.ReadFromJsonAsync<StartAuthenticationResult>();
            
            var signResult = await MetaMaskService.SignTypedDataV4(response.DataToSign.Trim('\''));

            var authFinishContent = JsonContent.Create(new FinishAuthenticationRequest(response.SessionGuid, signResult));
            var auth = await
                (await HttpClient.Client.PostAsync("/api/auth/FinishAuthentication", authFinishContent)).Content.ReadFromJsonAsync<FinishAuthenticationResult>();
        }

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
                await UpdateCanCheckOwnership();
            }
        }

        private async ValueTask<IAccountResponseModel> GetLoopringAccount(string walletAddress)
        {
            try
            {
                var result = await _loopringClient
                    .GetFromJsonAsync<IAccountResponseModel.Success>($"account?owner={walletAddress}")
                    .ConfigureAwait(false);

                if (result == null)
                    return new IAccountResponseModel.Fail();

                return result;
            }
            catch (Exception e)
            {
                return new IAccountResponseModel.Fail();
            }
        }

        public void Dispose()
        {
            MetaMaskService.AccountChangedEvent -= MetaMaskAccountChangedEvent;
            MetaMaskService.ChainChangedEvent -= MetaMaskChainChangedEvent;
        }
    }
}
