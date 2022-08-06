using Drutol.FigureRepository.Shared.Models.Auth;
using Drutol.FigureRepository.Shared.Models.Figure;

namespace Drutol.FigureRepository.Api.Interfaces;

public interface IDownloadTokenGenerator
{
    TokenResponse GenerateDownloadTokenForFigure(Figure figure);
}