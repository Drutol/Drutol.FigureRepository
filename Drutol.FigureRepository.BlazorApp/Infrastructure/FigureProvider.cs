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
            },
            Timeline = new List<FigureTimelineEntry>
            {
                new FigureTimelineEntry
                {
                    Event = FigureTimelineEvent.Publish,
                    Date = new DateOnly(2021, 5, 1)
                }
            }
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
            Timeline = new List<FigureTimelineEntry>
            {
                new FigureTimelineEntry
                {
                    Event = FigureTimelineEvent.Publish,
                    Date = new(2021, 10, 22)
                }
            }
        },
        new Figure
        {
            Name = "Asuka",
            Description = "Asuka from AoKana based on tapestry art in a different outfit.",
            Media = new()
            {
                #region Photos

                new FigureMedia
                {
                    Url = "images/Asuka/Photos/FrontAlt.jpg",
                    Name = "Front",
                    IsPortraitOrientation = false,
                    Display = FigurePhotoIntendedDisplay.All,
                    Gravity = FigurePhotoGravity.Center,
                    MediaKind = FigureMediaKind.Painted,
                    MediaType = FigureMediaType.Photo,
                    Width = 1619,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Asuka/Photos/Front.jpg",
                    Name = "Front Alternative",
                    IsPortraitOrientation = false,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Center,
                    MediaKind = FigureMediaKind.Painted,
                    MediaType = FigureMediaType.Photo,
                    Width = 1619,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Asuka/Photos/FrontRight.jpg",
                    Name = "Front Right",
                    IsPortraitOrientation = false,
                    Display = FigurePhotoIntendedDisplay.All,
                    Gravity = FigurePhotoGravity.Center,
                    MediaKind = FigureMediaKind.Painted,
                    MediaType = FigureMediaType.Photo,
                    Width = 1619,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Asuka/Photos/LeftAlt.jpg",
                    Name = "Left Alternative",
                    IsPortraitOrientation = false,
                    Display = FigurePhotoIntendedDisplay.Gallery | FigurePhotoIntendedDisplay.HomeShowcase,
                    Gravity = FigurePhotoGravity.Center,
                    MediaKind = FigureMediaKind.Painted,
                    MediaType = FigureMediaType.Photo,
                    Width = 1619,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Asuka/Photos/FrontAlt.jpg",
                    Name = "Front",
                    IsPortraitOrientation = false,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Center,
                    MediaKind = FigureMediaKind.Painted,
                    MediaType = FigureMediaType.Photo,
                    Width = 1619,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Asuka/Photos/LeftBack.jpg",
                    Name = "Back Left",
                    IsPortraitOrientation = false,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Center,
                    MediaKind = FigureMediaKind.Painted,
                    MediaType = FigureMediaType.Photo,
                    Width = 1619,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Asuka/Photos/Left.jpg",
                    Name = "Left",
                    IsPortraitOrientation = false,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Center,
                    MediaKind = FigureMediaKind.Painted,
                    MediaType = FigureMediaType.Photo,
                    Width = 1619,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Asuka/Photos/Back.jpg",
                    Name = "Back",
                    IsPortraitOrientation = false,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Center,
                    MediaKind = FigureMediaKind.Painted,
                    MediaType = FigureMediaType.Photo,
                    Width = 1619,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Asuka/Photos/Right.jpg",
                    Name = "Right",
                    IsPortraitOrientation = false,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Center,
                    MediaKind = FigureMediaKind.Painted,
                    MediaType = FigureMediaType.Photo,
                    Width = 1619,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Asuka/Photos/RightDetail.jpg",
                    Name = "Right Detail",
                    IsPortraitOrientation = false,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Center,
                    MediaKind = FigureMediaKind.Painted,
                    MediaType = FigureMediaType.Photo,
                    Width = 1619,
                    Height = 1080
                },

                #endregion

                #region Render

                new FigureMedia
                {
                    Url = "images/Asuka/Render/AsukaAltFrontBottom.jpg",
                    Name = "Front Bottom",
                    IsPortraitOrientation = false,
                    Display = FigurePhotoIntendedDisplay.All,
                    Gravity = FigurePhotoGravity.Center,
                    MediaKind = FigureMediaKind.ShadedRender,
                    MediaType = FigureMediaType.Photo,
                    Width = 1920,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Asuka/Render/AsukaMainAlt.jpg",
                    Name = "Main Alternative",
                    IsPortraitOrientation = false,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Center,
                    MediaKind = FigureMediaKind.ShadedRender,
                    MediaType = FigureMediaType.Photo,
                    Width = 1920,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Asuka/Render/AsukaBackFlare.jpg",
                    Name = "Back with Flare",
                    IsPortraitOrientation = false,
                    Display = FigurePhotoIntendedDisplay.Gallery | FigurePhotoIntendedDisplay.HomeShowcase,
                    Gravity = FigurePhotoGravity.Center,
                    MediaKind = FigureMediaKind.ShadedRender,
                    MediaType = FigureMediaType.Photo,
                    Width = 1920,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Asuka/Render/AsukaSideFar.jpg",
                    Name = "Far Side",
                    IsPortraitOrientation = false,
                    Display = FigurePhotoIntendedDisplay.Gallery | FigurePhotoIntendedDisplay.HomeShowcase,
                    Gravity = FigurePhotoGravity.Center,
                    MediaKind = FigureMediaKind.ShadedRender,
                    MediaType = FigureMediaType.Photo,
                    Width = 1920,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Asuka/Render/AsukaBodyCloseup.jpg",
                    Name = "Body Closeup",
                    IsPortraitOrientation = false,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Center,
                    MediaKind = FigureMediaKind.ShadedRender,
                    MediaType = FigureMediaType.Photo,
                    Width = 1920,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Asuka/Render/AsukaBackCloseup.jpg",
                    Name = "Back Closeup",
                    IsPortraitOrientation = false,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Center,
                    MediaKind = FigureMediaKind.ShadedRender,
                    MediaType = FigureMediaType.Photo,
                    Width = 1920,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Asuka/Render/AsukaAltFaceCloseup.jpg",
                    Name = "Face Closeup",
                    IsPortraitOrientation = false,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Center,
                    MediaKind = FigureMediaKind.ShadedRender,
                    MediaType = FigureMediaType.Photo,
                    Width = 1920,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Asuka/Render/AsukaLeftSideCloseup.jpg",
                    Name = "Left Closeup",
                    IsPortraitOrientation = false,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Center,
                    MediaKind = FigureMediaKind.ShadedRender,
                    MediaType = FigureMediaType.Photo,
                    Width = 1920,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Asuka/Render/AsukaFrontRightBottom.jpg",
                    Name = "Back Right",
                    IsPortraitOrientation = false,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Center,
                    MediaKind = FigureMediaKind.ShadedRender,
                    MediaType = FigureMediaType.Photo,
                    Width = 1920,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Asuka/Render/AsukaAltBack.jpg",
                    Name = "Back Alternative",
                    IsPortraitOrientation = false,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Center,
                    MediaKind = FigureMediaKind.ShadedRender,
                    MediaType = FigureMediaType.Photo,
                    Width = 1920,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Asuka/Render/AsukaBootCloseup.jpg",
                    Name = "Boot closeup",
                    IsPortraitOrientation = false,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Center,
                    MediaKind = FigureMediaKind.ShadedRender,
                    MediaType = FigureMediaType.Photo,
                    Width = 1920,
                    Height = 1080
                },

                #endregion

                #region Sculpt

                new FigureMedia
                {
                    Url = "images/Asuka/Sculpt/SculptMainAlt.jpg",
                    Name = "Main",
                    IsPortraitOrientation = false,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Center,
                    MediaKind = FigureMediaKind.SculptRender,
                    MediaType = FigureMediaType.Photo,
                    Width = 1920,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Asuka/Sculpt/SculptFront.jpg",
                    Name = "Body",
                    IsPortraitOrientation = false,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Center,
                    MediaKind = FigureMediaKind.SculptRender,
                    MediaType = FigureMediaType.Photo,
                    Width = 1920,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Asuka/Sculpt/SculptLeftCloseup.jpg",
                    Name = "Left Closeup",
                    IsPortraitOrientation = false,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Center,
                    MediaKind = FigureMediaKind.SculptRender,
                    MediaType = FigureMediaType.Photo,
                    Width = 1920,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Asuka/Sculpt/SculptFace.jpg",
                    Name = "Face",
                    IsPortraitOrientation = false,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Center,
                    MediaKind = FigureMediaKind.SculptRender,
                    MediaType = FigureMediaType.Photo,
                    Width = 1920,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Asuka/Sculpt/SculptBack.jpg",
                    Name = "Back",
                    IsPortraitOrientation = false,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Center,
                    MediaKind = FigureMediaKind.SculptRender,
                    MediaType = FigureMediaType.Photo,
                    Width = 1920,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Asuka/Sculpt/SculptShoeLeft.jpg",
                    Name = "Boot Left",
                    IsPortraitOrientation = false,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Center,
                    MediaKind = FigureMediaKind.SculptRender,
                    MediaType = FigureMediaType.Photo,
                    Width = 1920,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Asuka/Sculpt/SculptShoeDetailRight.jpg",
                    Name = "Boot Right",
                    IsPortraitOrientation = false,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Center,
                    MediaKind = FigureMediaKind.SculptRender,
                    MediaType = FigureMediaType.Photo,
                    Width = 1920,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Asuka/Sculpt/SculptRightCloseup.jpg",
                    Name = "Right Closeup",
                    IsPortraitOrientation = false,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Center,
                    MediaKind = FigureMediaKind.SculptRender,
                    MediaType = FigureMediaType.Photo,
                    Width = 1920,
                    Height = 1080
                },

                #endregion
            },
            Timeline = new()
            {
                new()
                {
                    Date = new DateOnly(2022, 2, 22),
                    Event = FigureTimelineEvent.ProjectInception
                },
                new()
                {
                    Date = new(2022, 3, 25),
                    Event = FigureTimelineEvent.SculptDone
                },
                new()
                {
                    Date = new(2022, 4, 5),
                    Event = FigureTimelineEvent.PrintDone
                },
                new()
                {
                    Date = new(2022, 6, 12),
                    Event = FigureTimelineEvent.PaintDone
                },
                new()
                {
                    Date = new DateOnly(2022, 7, 15),
                    Event = FigureTimelineEvent.Publish
                }
            },
            ExternalLinks = new List<FigureExternalLink>
            {
                new()
                {
                    Url = "https://www.artstation.com/artwork/G8gomz",
                    Type = FigureExternalLinkType.ArtStation
                },
                new()
                {
                    Url = "https://twitter.com/Drutol/status/1523677655769382914",
                    Type = FigureExternalLinkType.Twitter
                },
            },
            FigureDimensions = new()
            {
                Width = 331,
                Height = 190,
                Length = 330
            },
            TechnicalStatistics = new()
            {
                BlendFileSize = 1_470_000_000,
                LycheeScenesSize = 430_000_000,
                StlsSize = 1_170_000_000,
                Vertices = 12_741_292,
                Faces = 13_537_682,
                Triangles = 25_482_519,
            },
            PrintDetails = new FigurePrintDetails
            {
                BiggestPartDimension = new()
                {
                    Width = 157,
                    Height = 32,
                    Length = 132
                },
                MaxNumberOfParts = 50,
                MinNumberOfParts = 37,
                NumberOfClearParts = 4,
                NumberOfPrintBatches = 6,
                PrintResinVolumeParts = 811,
                PrintResinVolumeSupports = 237,
            },
            NftDetails = new()
            {
                TokenAddress = "0xTestTest123TestTest123TestTest123"
            }
        },
    };
}