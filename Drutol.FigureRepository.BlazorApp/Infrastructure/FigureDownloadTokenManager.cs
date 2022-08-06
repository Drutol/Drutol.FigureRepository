using Blazored.LocalStorage;
using Drutol.FigureRepository.BlazorApp.Interfaces;
using Drutol.FigureRepository.Shared.Models.Auth;
using Drutol.FigureRepository.Shared.Models.Figure;

namespace Drutol.FigureRepository.BlazorApp.Infrastructure;

public class FigureDownloadTokenManager : IFigureDownloadTokenManager
{
    private const string TokenStorageKey = "FigureDownloadToken-";
    private readonly ILocalStorageService _storageService;

    public FigureDownloadTokenManager(ILocalStorageService storageService)
    {
        _storageService = storageService;
    }

    public async ValueTask<TokenResponse?> GetDownloadTokenForFigure(Figure figure)
    {
        var key = $"{TokenStorageKey}{figure.Guid}";

        if (await _storageService.ContainKeyAsync(key))
        {
            var token = await _storageService.GetItemAsync<TokenResponse>(key);

            if (DateTime.UtcNow.AddMinutes(1) < token.Expiration)
            {
                return token;
            }

            return default;
        }
        else
        {
            return default;
        }
    }

    public async ValueTask SetDownloadTokenForFigure(Figure figure, TokenResponse token)
    {
        var key = $"{TokenStorageKey}{figure.Guid}";
        await _storageService.SetItemAsync(key, token);
    }
}