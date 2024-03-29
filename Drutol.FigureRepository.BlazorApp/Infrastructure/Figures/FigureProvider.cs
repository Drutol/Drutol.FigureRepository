﻿using System.Net.Http.Json;
using Blazored.SessionStorage;
using Drutol.FigureRepository.BlazorApp.Interfaces.Figures;
using Drutol.FigureRepository.BlazorApp.Interfaces.Http;
using Drutol.FigureRepository.BlazorApp.Util;
using Drutol.FigureRepository.Shared.Models.Figure;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Drutol.FigureRepository.BlazorApp.Infrastructure.Figures;

public class FigureProvider : IFigureProvider
{
    private const string CacheKey = "FigureDataCache";

    private readonly ILogger<FigureProvider> _logger;
    private readonly IWebAssemblyHostEnvironment _webAssemblyHostEnvironment;
    private readonly ISessionStorageService _sessionStorageService;
    private readonly IApiHttpClient _apiHttpClient;

    private bool _initialized;

    public TaskCompletionSource DataReady { get; } = new();

    public List<Figure> Figures { get; } = new();

    public FigureProvider(
        ILogger<FigureProvider> logger,
        IWebAssemblyHostEnvironment webAssemblyHostEnvironment,
        ISessionStorageService sessionStorageService,
        IApiHttpClient apiHttpClient)
    {
        _logger = logger;
        _webAssemblyHostEnvironment = webAssemblyHostEnvironment;
        _sessionStorageService = sessionStorageService;
        _apiHttpClient = apiHttpClient;
    }

    public async ValueTask Initialize()
    {
        if (_initialized)
            return;

        _initialized = true;

        if (!_webAssemblyHostEnvironment.IsPrerendering())
        {
            if (await _sessionStorageService.ContainKeyAsync(CacheKey))
            {
                var data = await _sessionStorageService.GetItemAsync<List<Figure>>(CacheKey);
                Figures.Clear();
                Figures.AddRange(data);
            }
        }


        while (true)
        {
            try
            {
                var data = await _apiHttpClient.Client.GetFromJsonAsync<List<Figure>>("api/figure/all");

                if (!_webAssemblyHostEnvironment.IsPrerendering())
                {
                    await _sessionStorageService.SetItemAsync(CacheKey, data);
                }

                Figures.Clear();
                Figures.AddRange(data.OrderBy(figure => figure.Index));

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