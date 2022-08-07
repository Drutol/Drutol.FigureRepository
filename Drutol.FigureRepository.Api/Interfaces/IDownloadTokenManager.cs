using Drutol.FigureRepository.Shared.Models.Auth;
using Drutol.FigureRepository.Shared.Models.Figure;

namespace Drutol.FigureRepository.Api.Interfaces;

public interface IDownloadTokenManager
{
    TokenResponse GenerateDownloadTokenForFigure(Figure figure);
    bool ValidateToken(Guid figureGuid, string token);
}