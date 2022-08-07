using Drutol.FigureRepository.Shared.Models.Enums;
using Functional.Maybe;

namespace Drutol.FigureRepository.Api.Interfaces;

public interface IFigureDownloadManager
{
    ValueTask<Maybe<Dictionary<FigureDownloadType, string>>> CreateDownloadPackage(
        Guid figureGuid,
        string token);
}