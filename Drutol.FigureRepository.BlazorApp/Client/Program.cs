using Drutol.FigureRepository.BlazorApp.Infrastructure;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Nethereum.Blazor;
using Nethereum.Metamask;
using Nethereum.Metamask.Blazor;
using Nethereum.UI;

namespace Drutol.FigureRepository.BlazorApp.Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        builder.Services.AddMudServices();

        builder.Services.AddScoped<FigureProvider>();

        #region Nethereum

        builder.Services.AddSingleton<IMetamaskInterop, MetamaskBlazorInterop>();
        builder.Services.AddSingleton<MetamaskInterceptor>();
        builder.Services.AddSingleton<MetamaskHostProvider>();

        //Add metamask as the selected ethereum host provider
        builder.Services.AddSingleton(services =>
        {
            var metamaskHostProvider = services.GetService<MetamaskHostProvider>();
            var selectedHostProvider = new SelectedEthereumHostProviderService();
            selectedHostProvider.SetSelectedEthereumHostProvider(metamaskHostProvider);
            return selectedHostProvider;
        });

        builder.Services.AddSingleton<AuthenticationStateProvider, EthereumAuthenticationStateProvider>();

        #endregion


        await builder.Build().RunAsync();
    }
}