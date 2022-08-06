using Drutol.FigureRepository.Shared.Models.Auth;
using Drutol.FigureRepository.Shared.Models.Figure;

namespace Drutol.FigureRepository.BlazorApp.Interfaces;

public interface IFigureDownloadTokenManager
{
    ValueTask<TokenResponse?> GetDownloadTokenForFigure(Figure figure);
    ValueTask SetDownloadTokenForFigure(Figure figure, TokenResponse token);
}