using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Drutol.FigureRepository.Api.Models;
using Drutol.FigureRepository.BlazorApp.Infrastructure;
using Drutol.FigureRepository.BlazorApp.Infrastructure.Figures;
using Drutol.FigureRepository.BlazorApp.Infrastructure.Wallet;
using Drutol.FigureRepository.BlazorApp.Interfaces;
using Drutol.FigureRepository.BlazorApp.Interfaces.Figures;
using Drutol.FigureRepository.BlazorApp.Interfaces.Http;
using Drutol.FigureRepository.BlazorApp.Interfaces.Wallet;
using Drutol.FigureRepository.BlazorApp.Util;
using Drutol.FigureRepository.Shared.Admin;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Logging.Abstractions;
using MudBlazor;
using MudBlazor.Services;

namespace Drutol.FigureRepository.BlazorApp;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");


        ConfigureServices(builder.Services, builder.HostEnvironment.BaseAddress);

        await builder.Build().RunAsync();
    }

    // method for prerendering nuget compatibility
    private static void ConfigureServices(IServiceCollection services, string baseAddress)
    {
#if DEBUG && false
        services.AddScoped<IApiHttpClient>(_ => new HttpClientWrapper(new HttpClient { BaseAddress = new Uri("http://localhost:5000") }));
#else
        services.AddScoped<IApiHttpClient>(_ => new HttpClientWrapper(new HttpClient { BaseAddress = new Uri("https://figure.drutol.com") }));
#endif

        services.AddScoped<IHostHttpClient>(_ => new HttpClientWrapper(new HttpClient { BaseAddress = new Uri(baseAddress) }));
        services.AddScoped<ILoopringHttpClient>(_ => new HttpClientWrapper(new HttpClient { BaseAddress = new Uri("https://uat2.loopring.io/api/v3/") }));


        services.AddMudServices(configuration =>
        {
            configuration.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
        });

        services.AddScoped<GameStopService>();
        services.AddScoped<MetaMaskService>();

        services.AddBlazoredSessionStorage();
        services.AddBlazoredLocalStorage();

        services.AddScoped<IFigureProvider, FigureProvider>();
        services.AddScoped<IFigureIconProvider, FigureIconProvider>();
        services.AddScoped<IWalletProvider, WalletProvider>();
        services.AddScoped<IFigureDownloadTokenManager, FigureDownloadTokenManager>();

        services.AddScoped<IWalletConnector, MetaMaskWalletConnector>();
        services.AddScoped<IWalletConnector, GameStopWalletConnector>();

        services.AddOptions();
        services.AddAuthorizationCore(options =>
            options.AddPolicy(
                AuthPolicies.AdminPolicy,
                policyBuilder => policyBuilder.RequireClaim(AdminClaims.AdminClaim)));
        services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
        services.AddScoped<IApiAuthenticationStateProvider>(provider =>
        {
            if (provider.GetRequiredService<IWebAssemblyHostEnvironment>().IsPrerendering())
                return new ApiAuthenticationStateProvider(NullLogger<ApiAuthenticationStateProvider>.Instance,
                    new HttpClientWrapper(new HttpClient()));

            return (ApiAuthenticationStateProvider)provider.GetRequiredService<AuthenticationStateProvider>();
        });
    }
}