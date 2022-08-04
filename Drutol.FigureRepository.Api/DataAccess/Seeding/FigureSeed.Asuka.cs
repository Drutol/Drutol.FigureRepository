using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Shared.Models;
using Drutol.FigureRepository.Shared.Models.Enums;

namespace Drutol.FigureRepository.Api.DataAccess.Seeding;

public class AsukaFigureSeed : IFigureSeed
{
    public Figure Figure { get; } = new Figure
    {
        Guid = Guid.Parse("DF7449B8-4915-487A-A244-0E2E4C0B55E2"),
        Name = "Asuka",
        Description = "Asuka based on the tapestry art. " +
                      "Here she is re-imagined with more dynamic pose as if she was skimming at high speed on the sea pointing in a given direction. " +
                      "Outfit was changed from swimsuit to flight suit with a few adjustments to make it more interesting, the flaps are semi transparent with red gradient now for example.",
        InitialGalleryKindDisplay = FigureMediaKind.Painted,
        Media = new()
        {
            #region Photos

            new FigureMedia
            {
                Url = "images/Asuka/Photos/FrontAlt.jpg",
                Display = FigurePhotoIntendedDisplay.All,
                MediaKind = FigureMediaKind.Painted,
                Width = 1619,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Photos/Front.jpg",

                MediaKind = FigureMediaKind.Painted,
                Width = 1619,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Photos/FrontRight.jpg",
                Display = FigurePhotoIntendedDisplay.All,
                MediaKind = FigureMediaKind.Painted,
                Width = 1619,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Photos/LeftAlt.jpg",
                Display = FigurePhotoIntendedDisplay.Gallery | FigurePhotoIntendedDisplay.HomeShowcase,
                MediaKind = FigureMediaKind.Painted,
                Width = 1619,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Photos/LeftBack.jpg",

                MediaKind = FigureMediaKind.Painted,
                Width = 1619,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Photos/Left.jpg",

                MediaKind = FigureMediaKind.Painted,
                Width = 1619,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Photos/Back.jpg",

                MediaKind = FigureMediaKind.Painted,
                Width = 1619,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Photos/Right.jpg",

                MediaKind = FigureMediaKind.Painted,
                Width = 1619,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Photos/RightDetail.jpg",

                MediaKind = FigureMediaKind.Painted,
                Width = 1619,
                Height = 1080
            },

            #endregion

            #region Render

            new FigureMedia
            {
                Url = "images/Asuka/Render/AsukaAltFrontBottom.jpg",
                Display = FigurePhotoIntendedDisplay.All,
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Render/AsukaBackFlare.jpg",
                Display = FigurePhotoIntendedDisplay.Gallery | FigurePhotoIntendedDisplay.HomeShowcase,
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Render/AsukaSideFar.jpg",
                Display = FigurePhotoIntendedDisplay.Gallery | FigurePhotoIntendedDisplay.HomeShowcase,
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Render/AsukaBodyCloseup.jpg",

                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Render/AsukaBackCloseup.jpg",

                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Render/AsukaAltFaceCloseup.jpg",

                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Render/AsukaLeftSideCloseup.jpg",

                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Render/AsukaFrontRightBottom.jpg",

                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Render/AsukaAltBack.jpg",

                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Render/AsukaBootCloseup.jpg",

                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Render/AsukaAltRightSide.jpg",

                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1920,
                Height = 1080
            },

            #endregion

            #region Sculpt

            new FigureMedia
            {
                Url = "images/Asuka/Sculpt/SculptMainAlt.jpg",

                MediaKind = FigureMediaKind.SculptRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Sculpt/SculptFront.jpg",

                MediaKind = FigureMediaKind.SculptRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Sculpt/SculptLeftCloseup.jpg",

                MediaKind = FigureMediaKind.SculptRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Sculpt/SculptFace.jpg",

                MediaKind = FigureMediaKind.SculptRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Sculpt/SculptBack.jpg",

                MediaKind = FigureMediaKind.SculptRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Sculpt/SculptShoeLeft.jpg",

                MediaKind = FigureMediaKind.SculptRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Sculpt/SculptShoeDetailRight.jpg",

                MediaKind = FigureMediaKind.SculptRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Sculpt/SculptRightCloseup.jpg",

                MediaKind = FigureMediaKind.SculptRender,
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
            PrintResinVolume = 1148,
        },
        NftDetails = new("0xb90810d7a02287b28dd1afabe2aa4d031d29bee2")
        {
            TokenId = 32770,
            NftData = "0x0a123123307817c3dc0a764a6d28013339e0e2905b85ce5086b0c207775ab74b",
            NftId = "0x8cf085e080defc5f9d67aa087310f93515995db725a576375e4bb42fb98a5452"
        },
        DownloadResources = new List<FigureDownloadResource>
        {
            new FigureDownloadResource
            {
                FigureDownloadType = FigureDownloadType.BlenderScene,
                Sha256 = "SHASHASHA256"
            },
            new FigureDownloadResource
            {
                FigureDownloadType = FigureDownloadType.SlicedScenes,
                Sha256 = "SHASHASHA256"
            },
            new FigureDownloadResource
            {
                FigureDownloadType = FigureDownloadType.Stls,
                Sha256 = "SHASHASHA256"
            },
        },
        CheckoutDetails = new FigureCheckoutDetails
        {
            Price = 40m
        },
        FigureCharacter = new FigureCharacter
        {
            NameEnglish = "Kurashina Asuka",
            NameJapanese = "倉科 明日香",

            OriginNameEnglish = "Aokana: Four Rhythm Across the Blue",
            OriginNameJapanese = "蒼の彼方のフォーリズム"
        },
        EyeType = FigureEyeType.IrisDecals
    };
}