namespace Drutol.FigureRepository.BlazorApp.Models;

public record FigurePrintDetails
{
    public int MinNumberOfParts { get; init; }
    public int MaxNumberOfParts { get; init; }
    public int NumberOfClearParts { get; init; }
    public int NumberOfPrintBatches { get; set; }

    public FigureDimensions BiggestPartDimension { get; init; }

    public double PrintResinVolumeParts { get; set; }
    public double PrintResinVolumeSupports { get; set; }
}