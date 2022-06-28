using System.Text.Json;
using System.Text.Json.Serialization;
using Blazored.SessionStorage;
using Drutol.FigureRepository.BlazorApp.Infrastructure;
using Drutol.FigureRepository.BlazorApp.Interfaces;
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
        builder.Services.AddMudServices(configuration =>
        {
            configuration.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
        });
        builder.Services.AddMetaMaskBlazor();
        builder.Services.AddBlazoredSessionStorage(config =>
            {
                config.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;

                config.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                config.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
                config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                config.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
                config.JsonSerializerOptions.WriteIndented = false;
            }
        );

        builder.Services.AddScoped<FigureProvider>();


        await builder.Build().RunAsync();
    }
}