using Drutol.FigureRepository.BlazorApp.Models.Enums;

namespace Drutol.FigureRepository.BlazorApp.Models;

public record FigureMedia
{
    public string Url { get; init; }

    public bool IsPortraitOrientation { get; init; }

    public FigureMediaKind MediaKind { get; init; }
    public FigureMediaType MediaType { get; init; }
    public FigurePhotoGravity Gravity { get; init; }
    public FigurePhotoIntendedDisplay Display { get; init; }
}