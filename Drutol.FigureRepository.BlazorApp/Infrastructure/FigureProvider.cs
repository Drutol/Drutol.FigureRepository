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
            PublishDate = new DateOnly(2022, 6, 17)
        },
    };
}