using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Drutol.FigureRepository.BlazorApp.Infrastructure;
using Drutol.FigureRepository.BlazorApp.Infrastructure.Figures;
using Drutol.FigureRepository.BlazorApp.Infrastructure.Wallet;
using Drutol.FigureRepository.BlazorApp.Interfaces.Figures;
using Drutol.FigureRepository.BlazorApp.Interfaces.Http;
using Drutol.FigureRepository.BlazorApp.Interfaces.Wallet;
using MetaMask.Blazor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;

namespace Drutol.FigureRepository.BlazorApp.Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped<IApiHttpClient>(_ => new HttpClientWrapper(new HttpClient { BaseAddress = new Uri("http://localhost:5000") }));
        builder.Services.AddScoped<IHostHttpClient>(_ => new HttpClientWrapper(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }));
        builder.Services.AddScoped<ILoopringHttpClient>(_ => new HttpClientWrapper(new HttpClient { BaseAddress = new Uri("https://uat2.loopring.io/api/v3/") }));
        builder.Services.AddMudServices(configuration =>
        {
            configuration.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
        });
        builder.Services.AddMetaMaskBlazor();
        builder.Services.AddBlazoredSessionStorage();
        builder.Services.AddBlazoredLocalStorage();

        builder.Services.AddScoped<IFigureProvider, FigureProvider>();
        builder.Services.AddScoped<IFigureIconProvider, FigureIconProvider>();
        builder.Services.AddScoped<IWalletProvider, WalletProvider>();
        builder.Services.AddScoped<IFigureDownloadTokenManager, FigureDownloadTokenManager>();

        builder.Services.AddScoped<IWalletConnector, MetaMaskWalletConnector>();
        builder.Services.AddScoped<IWalletConnector, GameStopWalletConnector>();

        await builder.Build().RunAsync();
    }
}