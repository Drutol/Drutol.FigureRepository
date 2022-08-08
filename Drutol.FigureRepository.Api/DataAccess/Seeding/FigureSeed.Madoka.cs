using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Shared.Models;
using Drutol.FigureRepository.Shared.Models.Enums;
using Drutol.FigureRepository.Shared.Models.Figure;
using Drutol.FigureRepository.Shared.Models.Figure.Enums;

namespace Drutol.FigureRepository.Api.DataAccess.Seeding;

public class MadokaFigureSeed : IFigureSeed
{
    public Figure Figure { get; } = new Figure()
    {
        Guid = Guid.Parse("8EE2F4AC-40F9-4FD7-A551-FE679A015ED7"),
        Name = "Madoka",
        Description = "Madoka based on her Onsen CG artwork. " +
                      "It's a bit more risque figure but any naughty bits are not present. " +
                      "She is talking on the phone with a smug aura around, with a cute bear keychain included. " +
                      "The base is imagined to remind of the Onsen with wooden planks and paved path with bonsai tree and an Andon lamp.",
        InitialGalleryKindDisplay = FigureMediaKind.ShadedRender,
        Media = new List<FigureMedia>
        {
            #region Photos

            new FigureMedia
            {
                Url = "images/Madoka/Photos/PrintPrototype.jpg",
                MediaKind = FigureMediaKind.PrintPrototype,
                Width = 1080,
                Height = 1440
            },

            #endregion

            #region Render

            new FigureMedia
            {
                Url = "images/Madoka/Render/Front.jpg",
                Display = FigurePhotoIntendedDisplay.All,
                MediaKind = FigureMediaKind.ShadedRender,
                Gravity = FigurePhotoGravity.Top,
                Width = 1080,
                Height = 1920
            },
            new FigureMedia
            {
                Url = "images/Madoka/Render/FaceCloseup.jpg",
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1080,
                Height = 1920
            },
            new FigureMedia
            {
                Url = "images/Madoka/Render/BottomLeft.jpg",
                MediaKind = FigureMediaKind.ShadedRender,
                Gravity = FigurePhotoGravity.Bottom,
                Width = 1080,
                Height = 1920
            },
            new FigureMedia
            {
                Url = "images/Madoka/Render/BackRight.jpg",
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1080,
                Height = 1920
            },
            new FigureMedia
            {
                Url = "images/Madoka/Render/Bear.jpg",
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Madoka/Render/Base.jpg",
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1920,
                Height = 1080
            },

            #endregion

            #region Sculpt

            new FigureMedia
            {
                Url = "images/Madoka/Sculpt/Left.jpg",
                MediaKind = FigureMediaKind.SculptRender,
                Width = 1080,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Madoka/Sculpt/LeftSide.jpg",
                MediaKind = FigureMediaKind.SculptRender,
                Width = 1080,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Madoka/Sculpt/RightSide.jpg",
                MediaKind = FigureMediaKind.SculptRender,
                Width = 1080,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Madoka/Sculpt/Andon.jpg",
                MediaKind = FigureMediaKind.SculptRender,
                Width = 1080,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Madoka/Sculpt/Bonsai.jpg",
                MediaKind = FigureMediaKind.SculptRender,
                Width = 1080,
                Height = 1080
            },

            #endregion
        },
        FigureDimensions = new FigureDimensions
        {
            Width = 94,
            Height = 257,
            Length = 108,
        },
        PrintDetails = new FigurePrintDetails
        {
            BiggestPartDimension = new()
            {
                Height = 210,
                Width = 55,
                Length = 57
            },
            PrintResinVolume = 408,
            MaxNumberOfParts = 15,
            MinNumberOfParts = 15,
            NumberOfClearParts = 0,
            NumberOfPrintBatches = 3,
        },
        TechnicalStatistics = new()
        {
            StlsSize = 1_150_818_060,
            LycheeScenesSize = 415_004_928,
            BlendFileSize = 1_671_331_840,
            Vertices = 11_504_498,
            Faces = 12_845_106,
            Triangles = 23_009_872
        },
        Timeline = new()
        {
            new()
            {
                Date = new DateTime(2021, 3, 22),
                Event = FigureTimelineEvent.ProjectInception
            },
            new()
            {
                Date = new(2022, 1, 30),
                Event = FigureTimelineEvent.SculptDone
            },
            new()
            {
                Date = new(2022, 2, 14),
                Event = FigureTimelineEvent.PrintDone
            }
        },
        ExternalLinks = new List<FigureExternalLink>
        {
            new()
            {
                Url = "https://www.artstation.com/artwork/3qmJEm",
                Type = FigureExternalLinkType.ArtStation
            },
            new()
            {
                Url = "https://twitter.com/Drutol/status/1487903753130893323",
                Type = FigureExternalLinkType.Twitter
            },
        },
        NftDetails = new("0xb06146103f0b05e55c7b8ccf4dcd8df70b5a105c")
        {
            TokenId = 11111
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
            Price = 40m
        },
        FigureCharacter = new FigureCharacter
        {
            NameEnglish = "Aoyagi Madoka",
            NameJapanese = "青柳 窓果",

            OriginNameEnglish = "Aokana: Four Rhythm Across the Blue",
            OriginNameJapanese = "蒼の彼方のフォーリズム"
        },
        EyeType = FigureEyeType.PartSculpted,
        ExternalPurchaseOptions = new()
        {
            new FigureExternalPurchaseOption(FigureExternalPurchaseOptionType.CgTrader, "https://www.cgtrader.com/3d-print-models/art/sculptures/ganyu-from-genshin-impact-sculpture-for-3d-printing", false),
            new FigureExternalPurchaseOption(FigureExternalPurchaseOptionType.GameStopMarketplace, "https://nft.gamestop.com", true)
        }
    };
}