using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Shared.Models.Enums;
using Drutol.FigureRepository.Shared.Models.Figure;
using Drutol.FigureRepository.Shared.Models.Figure.Enums;
using Drutol.FigureRepository.Shared.Util;

namespace Drutol.FigureRepository.Api.DataAccess.Seeding;

public class GanyuFigureSeed : IFigureSeed
{
    public Figure Figure { get; } = new Figure
    {
        Guid = KnownFigureUtils.Guids[KnownFigures.Ganyu],
        Index = 2,
        Name = "Ganyu",
        Flavour = "Banner Art",
        Notes = "Printed back in the day on small Photon S printer.",
        Description = "Ganyu based on her banner art sculpted with gorgeous base " +
                      "with lilies in attempt to imitate mist gathering at her feet." +
                      "Sculpted in dynamic pose with Quillin adapted for printing.",
        InitialGalleryKindDisplay = FigureMediaKind.PrintPrototype,
        Media = new List<FigureMedia>
        {
            #region Photos

            new FigureMedia
            {
                Url = "images/Ganyu/Photos/Main.webp",
                Display = FigurePhotoIntendedDisplay.All,
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.PrintPrototype,
                Width = 1080,
                Height = 1440
            },
            new FigureMedia
            {
                Url = "images/Ganyu/Photos/BacklightSilhouette.webp",
                Display = FigurePhotoIntendedDisplay.All,
                MediaKind = FigureMediaKind.PrintPrototype,
                Width = 1080,
                Height = 1440
            },

            #endregion

            #region Sculpt

            new FigureMedia
            {
                Url = "images/Ganyu/Sculpt/Front.webp",
                Display = FigurePhotoIntendedDisplay.All,
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.SculptRender,
                Width = 1080,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Ganyu/Sculpt/Face.webp",
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.SculptRender,
                Width = 1080,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Ganyu/Sculpt/FrontBottom.webp",
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.SculptRender,
                Width = 1080,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Ganyu/Sculpt/LeftSide.webp",
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.SculptRender,
                Width = 1080,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Ganyu/Sculpt/Base.webp",
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.SculptRender,
                Width = 1080,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Ganyu/Sculpt/BackBottom.webp",
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.SculptRender,
                Width = 1080,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Ganyu/Sculpt/Back.webp",
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.SculptRender,
                Width = 1080,
                Height = 1080
            },

            #endregion
        },
        Timeline = new List<FigureTimelineEntry>
        {
            new()
            {
                Event = FigureTimelineEvent.ProjectInception,
                Date = new(2020, 12, 19)
            },
            new()
            {
                Event = FigureTimelineEvent.SculptDone,
                Date = new(2021, 2, 15)
            },
            new()
            {
                Event = FigureTimelineEvent.PrintDone,
                Date = new(2021, 3, 4)
            },
            new()
            {
                Event = FigureTimelineEvent.Publish,
                Date = new DateTime(2021, 3, 13)
            }
        },
        FigureDimensions = new()
        {
            Height = 282,
            Width = 184,
            Length = 191
        },
        TechnicalStatistics = new()
        {
            Vertices = 11_414_652,
            Faces = 13_850_121,
            Triangles = 22_829_528,
            LycheeScenesSize = 1_763_750_838,
            BlendFileSize = 2_333_720_576,
            StlsSize = 1_197_367_696
        },
        PrintDetails = new()
        {
            MinNumberOfParts = 39,
            MaxNumberOfParts = 42,
            NumberOfClearParts = 7,
            BiggestPartDimension = new FigureDimensions
            {
                Width = 100,
                Height = 68,
                Length = 190,
            },
            PrintResinVolume = 1488,
            NumberOfPrintBatches = 18
        },
        ExternalLinks = new()
        {
            new()
            {
                Type = FigureExternalLinkType.CgTrader,
                Url =
                    "https://www.cgtrader.com/3d-print-models/art/sculptures/ganyu-from-genshin-impact-sculpture-for-3d-printing",
            },
            new()
            {
                Type = FigureExternalLinkType.Twitter,
                Url = "https://twitter.com/Drutol/status/1369716421907873794"
            },
            new()
            {
                Type = FigureExternalLinkType.ArtStation,
                Url = "https://www.artstation.com/artwork/DAQ49A"
            }
        },
        NftDetails = new("0xe3111f751c5649216fb806bcaf4e614faf92e55c")
        {
            TokenId = 32770,
            NftData = "0x2d30871c3f9b55f272a6e682a98d5862bd03c599da4636b30c9733ab8b28ed86",
            NftId = "0x76c56430fb00d8e96ba9b2c3e7e700136d86342dcef448c730d2ef00f63c7a06"
        },
        CheckoutDetails = new FigureCheckoutDetails
        {
            Price = 30m
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
        FigureCharacter = new FigureCharacter
        {
            NameEnglish = "Ganyu",
            NameJapanese = "甘雨",

            OriginNameEnglish = "Genshin Impact",
            OriginNameJapanese = "原神"
        },
        EyeType = FigureEyeType.FullSculpted,
        ExternalPurchaseOptions = new()
        {
            new FigureExternalPurchaseOption(FigureExternalPurchaseOptionType.CgTrader, "https://www.cgtrader.com/3d-print-models/art/sculptures/ganyu-from-genshin-impact-sculpture-for-3d-printing", false),
            new FigureExternalPurchaseOption(FigureExternalPurchaseOptionType.GameStopMarketplace, "https://nft.gamestop.com", true)
        },
        FigureMetadata = new FigureMetadata
        {
            Title = "Ganyu - DruFigures",
            Description = "Garage kit of Genshin's Ganyu for 3D printing. Check out renders, painted photos and get one for yourself!",
            ImageUrl = "https://figure.drutol.com/images/Ganyu/Meta/Meta.webp",
            ImageWidth = 1200,
            ImageHeight = 628,
            ImageMimeType = "image/jpg",
            Type = "website",
            Url = "https://figure.drutol.com/Figures/Ganyu"
        }
    };
}