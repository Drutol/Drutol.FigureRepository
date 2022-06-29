using Drutol.FigureRepository.BlazorApp.Models.Enums;

namespace Drutol.FigureRepository.BlazorApp.Models;

public record Figure
{
    public long Id { get; init; }
    public string Name { get; init; }
    public string Flavour { get; init; }
    public string Description { get; init; }
    public string Notes { get; init; }
    public FigureEyeType EyeType { get; set; }
    public FigureMediaKind? InitialGalleryKindDisplay { get; set; }

    public FigureCharacter FigureCharacter { get; set; }
    public FigureTechnicalStatistics TechnicalStatistics { get; init; }
    public FigureDimensions FigureDimensions { get; init; }
    public FigureNftDetails NftDetails { get; init; }
    public FigurePrintDetails PrintDetails { get; init; }

    public List<FigureMedia> Media { get; init; }
    public List<FigureExternalLink> ExternalLinks { get; init; }
    public List<FigureTimelineEntry> Timeline { get; set; }
    public List<FigureDownloadResource> DownloadResources { get; set; }
}