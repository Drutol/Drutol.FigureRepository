namespace Drutol.FigureRepository.Shared.Models.Figure;

public record FigureTechnicalStatistics
{
    public int Vertices { get; init; }
    public int Triangles { get; init; }
    public int Faces { get; init; }

    public long BlendFileSize { get; init; }
    public double LycheeScenesSize { get; init; }
    public double StlsSize { get; init; }
}