using Drutol.FigureRepository.Shared.Models.Figure;
using Functional.Maybe;

namespace Drutol.FigureRepository.Api.Interfaces;

public interface IDownloadLinkGenerator
{
    ValueTask<Maybe<string>> GenerateDownloadTokenForFigure(
        Figure figure,
        FigureDownloadResource resource);
}