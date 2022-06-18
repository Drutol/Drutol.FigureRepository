using Drutol.FigureRepository.BlazorApp.Models.Enums;

namespace Drutol.FigureRepository.BlazorApp.Models;

public record FigureMedia
{
    public string Url { get; init; }

    public string Name { get; set; }
    public string Description { get; set; }

    public int Width { get; set; }
    public int Height { get; set; }

    public bool IsPortraitOrientation { get; init; }

    public FigureMediaKind MediaKind { get; init; }
    public FigureMediaType MediaType { get; init; }
    public FigurePhotoGravity Gravity { get; init; }
    public FigurePhotoIntendedDisplay Display { get; init; }
}