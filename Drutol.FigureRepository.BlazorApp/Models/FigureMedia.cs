﻿using Drutol.FigureRepository.BlazorApp.Models.Enums;

namespace Drutol.FigureRepository.BlazorApp.Models;

public record FigureMedia
{
    public string Url { get; init; }

    public string Name { get; init; }
    public string Description { get; init; }

    public int Width { get; set; }
    public int Height { get; set; }

    public bool IsPortraitOrientation { get; init; }

    public FigureMediaKind MediaKind { get; init; }
    public FigureMediaType MediaType { get; init; }
    public FigurePhotoGravity Gravity { get; init; }
    public FigurePhotoIntendedDisplay Display { get; init; }
}