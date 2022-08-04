using Drutol.FigureRepository.Shared.Models.Enums;

namespace Drutol.FigureRepository.Shared.Models;

public record FigureDownloadResource
{
    public string Name { get; init; }
    public string Sha256 { get; init; }
    public FigureDownloadType FigureDownloadType { get; init; }
}