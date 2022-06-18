using Drutol.FigureRepository.BlazorApp.Models;
using Drutol.FigureRepository.BlazorApp.Models.Enums;

namespace Drutol.FigureRepository.BlazorApp.Infrastructure;

public class FigureProvider
{
    public List<Figure> Figures { get; } = new()
    {
        new Figure
        {
            Name = "Ganyu",
            Description = "Ganyu from GenshinImpact based on her Banner Art.",
            Media = new List<FigureMedia>
            {
                new FigureMedia
                {
                    Url = "images/Ganyu.jpg",
                    IsPortraitOrientation = true,
                    Display = FigurePhotoIntendedDisplay.All,
                    Gravity = FigurePhotoGravity.Top,
                    MediaKind = FigureMediaKind.PrintPrototype,
                    MediaType = FigureMediaType.Photo,    
                    Width = 768,
                    Height = 1024
                },
                new FigureMedia
                {
                    Url = "images/Ganyu.jpg",
                    IsPortraitOrientation = true,
                    Display = FigurePhotoIntendedDisplay.All,
                    Gravity = FigurePhotoGravity.Top,
                    MediaKind = FigureMediaKind.PrintPrototype,
                    MediaType = FigureMediaType.Photo,
                    Width = 768,
                    Height = 1024
                },
                new FigureMedia
                {
                    Url = "images/Ganyu.jpg",
                    IsPortraitOrientation = true,
                    Display = FigurePhotoIntendedDisplay.All,
                    Gravity = FigurePhotoGravity.Top,
                    MediaKind = FigureMediaKind.PrintPrototype,
                    MediaType = FigureMediaType.Photo,
                    Width = 768,
                    Height = 1024
                },
                new FigureMedia
                {
                    Url = "images/Ganyu.jpg",
                    IsPortraitOrientation = true,
                    Display = FigurePhotoIntendedDisplay.All,
                    Gravity = FigurePhotoGravity.Top,
                    MediaKind = FigureMediaKind.PrintPrototype,
                    MediaType = FigureMediaType.Photo,
                    Width = 768,
                    Height = 1024
                },
                new FigureMedia
                {
                    Url = "images/Ganyu.jpg",
                    IsPortraitOrientation = true,
                    Display = FigurePhotoIntendedDisplay.All,
                    Gravity = FigurePhotoGravity.Top,
                    MediaKind = FigureMediaKind.PrintPrototype,
                    MediaType = FigureMediaType.Photo,
                    Width = 768,
                    Height = 1024
                },
            },
            PublishDate = new DateOnly(2021, 5, 1)
        },
        new Figure
        {
            Name = "Lumine",
            Description = "Lumine from GenshinImpact based on her scenic park collaboration artwork.",
            Media = new List<FigureMedia>
            {
                new FigureMedia
                {
                    Url = "images/Lumine.jpg",
                    IsPortraitOrientation = true,
                    Display = FigurePhotoIntendedDisplay.All,
                    Gravity = FigurePhotoGravity.Top,
                    MediaKind = FigureMediaKind.PrintPrototype,
                    MediaType = FigureMediaType.Photo,
                    Width = 768,
                    Height = 1024
                }
            },
            PublishDate = new DateOnly(2021, 10, 22)
        },   
        new Figure
        {
            Name = "Asuka",
            Description = "Asuka from AoKana based on tapestry art in a different outfit.",
            Media = new List<FigureMedia>
            {
                new FigureMedia
                {
                    Url = "images/Asuka.jpg",
                    IsPortraitOrientation = false,
                    Display = FigurePhotoIntendedDisplay.All,
                    Gravity = FigurePhotoGravity.Center,
                    MediaKind = FigureMediaKind.PrintPrototype,
                    MediaType = FigureMediaType.Photo,
                    Width = 1152,
                    Height = 768
                }
            },
            PublishDate = new DateOnly(2022, 6, 17)
        },
    };
}