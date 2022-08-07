using Drutol.FigureRepository.Shared.Models.Enums;

namespace Drutol.FigureRepository.Shared.Models.Figure;

public record FigureDownloadResource
{
    public string Sha256 { get; init; }
    public FigureDownloadType Type { get; init; }
    public FigureDownloadFileFormat Format { get; set; }
}