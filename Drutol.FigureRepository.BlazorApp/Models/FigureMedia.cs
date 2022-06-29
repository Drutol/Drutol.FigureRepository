using System.Text.Json.Serialization;
using Drutol.FigureRepository.BlazorApp.Models.Enums;

namespace Drutol.FigureRepository.BlazorApp.Models;

public record FigureMedia
{
    public string Url { get; init; }

    public int Width { get; set; }
    public int Height { get; set; }

    [JsonIgnore]
    public bool IsPortraitOrientation => Height > Width;

    public FigureMediaKind MediaKind { get; init; }
    public FigureMediaType MediaType { get; init; } = FigureMediaType.Photo;
    public FigurePhotoGravity Gravity { get; init; } = FigurePhotoGravity.Center;
    public FigurePhotoIntendedDisplay Display { get; init; }
}