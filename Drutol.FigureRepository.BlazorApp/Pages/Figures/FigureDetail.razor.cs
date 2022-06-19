using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Nethereum.UI;

namespace Drutol.FigureRepository.BlazorApp.Pages.Figures
{
    public partial class FigureDetail : IDisposable
    {
        public bool EthereumAvailable { get; set; }
        public string SelectedAccount { get; set; }
        public int SelectedChainId { get; set; }

        private IEthereumHostProvider _ethereumHostProvider;

        [Inject]
        private SelectedEthereumHostProviderService SelectedHostProviderService { get; set; }

        //[Inject]
        //private AuthenticationStateProvider SiweAuthenticationStateProvider { get; set; }

        protected override void OnInitialized()
        {
            _ethereumHostProvider = SelectedHostProviderService.SelectedHost;
            _ethereumHostProvider.SelectedAccountChanged += HostProviderSelectedAccountChanged;
            _ethereumHostProvider.NetworkChanged += HostProviderNetworkChanged;
            _ethereumHostProvider.EnabledChanged += HostProviderOnEnabledChanged;
        }

        protected override async Task OnInitializedAsync()
        {
            EthereumAvailable = await _ethereumHostProvider.CheckProviderAvailabilityAsync();
            if (EthereumAvailable)
            {
                SelectedAccount = await _ethereumHostProvider.GetProviderSelectedAccountAsync();
                await UpdateChainId();
            }
        }

        private async Task UpdateChainId()
        {
            var web3 = await _ethereumHostProvider.GetWeb3Async();
            var chainId = await web3.Eth.ChainId.SendRequestAsync();
            SelectedChainId = (int)chainId.Value;
        }

        private async Task HostProviderOnEnabledChanged(bool enabled)
        {
            if (enabled)
            {
                await UpdateChainId();
            }
        }

        private Task HostProviderNetworkChanged(int chainId)
        {
            SelectedChainId = chainId;
            return Task.CompletedTask;
        }

        private async Task HostProviderSelectedAccountChanged(string account)
        {
            SelectedAccount = account;
            await UpdateChainId();
        }

        public void Dispose()
        {
            _ethereumHostProvider.SelectedAccountChanged -= HostProviderSelectedAccountChanged;
            _ethereumHostProvider.NetworkChanged -= HostProviderNetworkChanged;
            _ethereumHostProvider.EnabledChanged -= HostProviderOnEnabledChanged;
        }
    }
}
