namespace Drutol.FigureRepository.BlazorApp.Models;

public record Figure
{
    public string Name { get; init; }
    public string Description { get; init; }
    public string Notes { get; init; }

    public FigureTechnicalStatistics TechnicalStatistics { get; init; }
    public FigureDimensions FigureDimensions { get; init; }
    public FigureNftDetails NftDetails { get; init; }
    public FigurePrintDetails PrintDetails { get; init; }

    public List<FigureMedia> Media { get; init; }
    public List<FigureExternalLink> ExternalLinks { get; init; }

    public DateOnly ProjectStartDate { get; init; }
    public DateOnly SculptDoneDate { get; init; }
    public DateOnly PrintDoneDate { get; init; }
    public DateOnly PaintDoneDate { get; init; }
    public DateOnly PublishDate { get; init; }
}