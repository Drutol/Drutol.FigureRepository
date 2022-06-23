using Drutol.FigureRepository.BlazorApp.Infrastructure;
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

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5000"/*builder.HostEnvironment.BaseAddress*/) });
        builder.Services.AddMudServices(configuration =>
        {
            configuration.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
        });
        builder.Services.AddMetaMaskBlazor();

        builder.Services.AddScoped<FigureProvider>();


        await builder.Build().RunAsync();
    }
}