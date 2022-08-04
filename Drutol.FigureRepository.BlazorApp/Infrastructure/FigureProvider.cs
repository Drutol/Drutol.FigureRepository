using System.Net.Http.Json;
using System.Text.Json;
using Blazored.SessionStorage;
using Drutol.FigureRepository.BlazorApp.Interfaces;
using Drutol.FigureRepository.Shared.Models;
using Drutol.FigureRepository.Shared.Models.Enums;

namespace Drutol.FigureRepository.BlazorApp.Infrastructure;

public class FigureProvider : IFigureProvider
{
    private const string CacheKey = "FigureDataCache";

    private readonly ILogger<FigureProvider> _logger;
    private readonly ISessionStorageService _sessionStorageService;
    private readonly IApiHttpClient _apiHttpClient;
    public TaskCompletionSource DataReady { get; } = new();

    public List<Figure> Figures { get; } = new();

    public FigureProvider(
        ILogger<FigureProvider> logger,
        ISessionStorageService sessionStorageService,
        IApiHttpClient apiHttpClient)
    {
        _logger = logger;
        _sessionStorageService = sessionStorageService;
        _apiHttpClient = apiHttpClient;
    }

    public async ValueTask Initialize()
    {
        if (await _sessionStorageService.ContainKeyAsync(CacheKey))
        {
            var data = await _sessionStorageService.GetItemAsync<List<Figure>>(CacheKey);
            Figures.AddRange(data);
        }

        while (true)
        {
            try
            {
                var data = await _apiHttpClient.Client.GetFromJsonAsync<List<Figure>>("figure/all");
                await _sessionStorageService.SetItemAsync(CacheKey, data);
                Figures.AddRange(data);

                break;
            }
            catch (Exception e)
            {   
                _logger.LogError(e, "Failed to fetch figure data.");
            }

            await Task.Delay(TimeSpan.FromSeconds(5));
        }

        DataReady.SetResult();
    }
}