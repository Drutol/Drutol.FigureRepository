using Drutol.FigureRepository.Shared.Models.Enums;

namespace Drutol.FigureRepository.Shared.Models.Figure;

public record FigureExternalLink
{
    public string Url { get; set; }
    public FigureExternalLinkType Type { get; set; }
}