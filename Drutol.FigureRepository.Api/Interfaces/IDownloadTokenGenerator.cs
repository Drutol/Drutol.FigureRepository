using Drutol.FigureRepository.Shared.Models;

namespace Drutol.FigureRepository.Api.Interfaces;

public interface IDownloadTokenGenerator
{
    string GenerateDownloadTokenForFigure(Figure figure);
}