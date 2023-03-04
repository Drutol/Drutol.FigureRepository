using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Shared.Models;
using Drutol.FigureRepository.Shared.Models.Enums;
using Drutol.FigureRepository.Shared.Models.Figure;
using Drutol.FigureRepository.Shared.Models.Figure.Enums;
using Drutol.FigureRepository.Shared.Util;

namespace Drutol.FigureRepository.Api.DataAccess.Seeding;

public class AsukaFigureSeed : IFigureSeed
{
    public Figure Figure { get; } = new Figure
    {
        Guid = KnownFigureUtils.Guids[KnownFigures.Asuka],
        Index = 0,
        Name = "Asuka",
        Flavour = "Tobiko Skipping",
        Description = "Asuka based on the tapestry art. " +
                      "Here she is re-imagined with more dynamic pose as if she was skimming at high speed on the sea pointing in a given direction. " +
                      "Outfit was changed from swimsuit to flight suit with a few adjustments to make it more interesting, the flaps are semi transparent with red gradient for instance.",
        InitialGalleryKindDisplay = FigureMediaKind.Painted,
        Media = new()
        {
            #region Photos

            new FigureMedia
            {
                Url = "images/Asuka/Photos/FrontAlt.webp",
                Display = FigurePhotoIntendedDisplay.All,
                MediaKind = FigureMediaKind.Painted,
                Width = 1619,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Photos/Front.webp",
                MediaKind = FigureMediaKind.Painted,
                Width = 1619,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Photos/FrontRight.webp",
                MediaKind = FigureMediaKind.Painted,
                Width = 1619,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Photos/LeftAlt.webp",
                MediaKind = FigureMediaKind.Painted,
                Width = 1619,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Photos/LeftBack.webp",
                MediaKind = FigureMediaKind.Painted,
                Width = 1619,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Photos/Left.webp",
                MediaKind = FigureMediaKind.Painted,
                Width = 1619,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Photos/Back.webp",
                Display = FigurePhotoIntendedDisplay.All,
                MediaKind = FigureMediaKind.Painted,
                Width = 1619,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Photos/Right.webp",
                MediaKind = FigureMediaKind.Painted,
                Width = 1619,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Photos/RightDetail.webp",
                MediaKind = FigureMediaKind.Painted,
                Width = 1619,
                Height = 1080
            },

            #endregion

            #region Render

            new FigureMedia
            {
                Url = "images/Asuka/Render/AsukaAltFrontBottom.webp",
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Render/AsukaBackFlare.webp",
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Render/AsukaSideFar.webp",
                Display = FigurePhotoIntendedDisplay.Gallery | FigurePhotoIntendedDisplay.HomeShowcase,
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Render/AsukaBodyCloseup.webp",
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Render/AsukaBackCloseup.webp",
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Render/AsukaAltFaceCloseup.webp",
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Render/AsukaLeftSideCloseup.webp",
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Render/AsukaFrontRightBottom.webp",
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Render/AsukaAltBack.webp",
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Render/AsukaBootCloseup.webp",
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Render/AsukaAltRightSide.webp",
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1920,
                Height = 1080
            },

            #endregion

            #region Sculpt

            new FigureMedia
            {
                Url = "images/Asuka/Sculpt/SculptMainAlt.webp",
                MediaKind = FigureMediaKind.SculptRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Sculpt/SculptFront.webp",
                MediaKind = FigureMediaKind.SculptRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Sculpt/SculptLeftCloseup.webp",
                MediaKind = FigureMediaKind.SculptRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Sculpt/SculptFace.webp",
                MediaKind = FigureMediaKind.SculptRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Sculpt/SculptBack.webp",
                MediaKind = FigureMediaKind.SculptRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Sculpt/SculptShoeLeft.webp",
                MediaKind = FigureMediaKind.SculptRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Sculpt/SculptShoeDetailRight.webp",
                MediaKind = FigureMediaKind.SculptRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Asuka/Sculpt/SculptRightCloseup.webp",
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
                Date = new DateTime(2022, 2, 22),
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
                Date = new DateTime(2023, 3, 4),
                Event = FigureTimelineEvent.Publish
            }
        },
        ExternalLinks = new List<FigureExternalLink>
        {
            new()
            {
                Url = "https://www.artstation.com/artwork/1x8Rbo",
                Type = FigureExternalLinkType.ArtStation
            },
            new()
            {
                Url = "https://twitter.com/Drutol/status/1631982849841676289",
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
            MaxNumberOfParts = 38,
            MinNumberOfParts = 38,
            NumberOfClearParts = 4,
            NumberOfPrintBatches = 6,
            PrintResinVolume = 1148,
        },
        NftDetails = new("0xe3111f751c5649216fb806bcaf4e614faf92e55c")
        {
            TokenId = 32769,
            NftData = "0x13675076af2641d8bb5fe69bdd948912d8cf3a3fccf2174a9a3329cb565c64a2",
            NftId = "0xa06f00fde7c4b0354c40f8c4ba6494306b861777e178846b5befcb77d7ae286d"
        },
        DownloadResources = new List<FigureDownloadResource>
        {
            new FigureDownloadResource
            {
                Type = FigureDownloadType.BlenderScene,
                Sha256 = "SHASHASHA256"
            },
            new FigureDownloadResource
            {
                Type = FigureDownloadType.SlicedScenes,
                Sha256 = "SHASHASHA256"
            },
            new FigureDownloadResource
            {
                Type = FigureDownloadType.Stls,
                Sha256 = "SHASHASHA256"
            },
        },
        CheckoutDetails = new FigureCheckoutDetails
        {
            Price = 30m
        },
        FigureCharacter = new FigureCharacter
        {
            NameEnglish = "Kurashina Asuka",
            NameJapanese = "倉科 明日香",

            OriginNameEnglish = "Aokana: Four Rhythm Across the Blue",
            OriginNameJapanese = "蒼の彼方のフォーリズム"
        },
        EyeType = FigureEyeType.IrisDecals,
        ExternalPurchaseOptions = new()
        {
            new FigureExternalPurchaseOption(FigureExternalPurchaseOptionType.CgTrader, "https://www.cgtrader.com/3d-print-models/art/sculptures/asuka-from-aokana-tobiko-skipping", false),
            new FigureExternalPurchaseOption(FigureExternalPurchaseOptionType.GameStopMarketplace, "https://nft.gamestop.com", true)
        },
        FigureMetadata = new FigureMetadata
        {
            Title = "Asuka - DruFigures",
            Description = "Garage kit of AoKana's Asuka for 3D printing. Check out renders, painted photos and get one for yourself!",
            ImageUrl = "https://figure.drutol.com/images/Asuka/Meta/Meta.webp",
            ImageWidth = 1200,
            ImageHeight = 628,
            ImageMimeType = "image/jpg",
            Type = "website",
            Url = "https://figure.drutol.com/Figures/Asuka"
        }
    };
}