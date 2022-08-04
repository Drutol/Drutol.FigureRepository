using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Shared.Models;
using Drutol.FigureRepository.Shared.Models.Enums;

namespace Drutol.FigureRepository.Api.DataAccess.Seeding;

public class GanyuFigureSeed : IFigureSeed
{
    public Figure Figure { get; } = new Figure
    {
        Guid = Guid.Parse("046BA215-AE76-44E4-BB14-01BBEAB30527"),
        Name = "Ganyu",
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
                Url = "images/Ganyu/Photos/Main.jpg",
                Display = FigurePhotoIntendedDisplay.All,
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.PrintPrototype,
                Width = 1080,
                Height = 1440
            },
            new FigureMedia
            {
                Url = "images/Ganyu/Photos/BacklightSilhouette.jpg",
                Display = FigurePhotoIntendedDisplay.All,
                MediaKind = FigureMediaKind.PrintPrototype,
                Width = 1080,
                Height = 1440
            },

            #endregion

            #region Sculpt

            new FigureMedia
            {
                Url = "images/Ganyu/Sculpt/Front.jpg",
                Display = FigurePhotoIntendedDisplay.All,
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.SculptRender,
                Width = 1080,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Ganyu/Sculpt/Face.jpg",
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.SculptRender,
                Width = 1080,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Ganyu/Sculpt/FrontBottom.jpg",
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.SculptRender,
                Width = 1080,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Ganyu/Sculpt/LeftSide.jpg",
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.SculptRender,
                Width = 1080,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Ganyu/Sculpt/Base.jpg",
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.SculptRender,
                Width = 1080,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Ganyu/Sculpt/BackBottom.jpg",
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.SculptRender,
                Width = 1080,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Ganyu/Sculpt/Back.jpg",
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
                Date = new DateOnly(2021, 3, 13)
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
        NftDetails = new("0xb90810d7a02287b28dd1afabe2aa4d031d29bee2")
        {
            TokenId = 32769,
            NftData = "0x1bdf94fb9ee519b8edfb1d9d9525abc6e0c73248641f508090be2dde644a96f1",
            NftId = "0x51c6e0ca9657c48b86861a2c4b3020b35191c8d6047aeacecb5db776ee3a7252"
        },
        CheckoutDetails = new FigureCheckoutDetails
        {
            Price = 40m
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
        FigureCharacter = new FigureCharacter
        {
            NameEnglish = "Ganyu",
            NameJapanese = "甘雨",

            OriginNameEnglish = "Genshin Impact",
            OriginNameJapanese = "原神"
        },
        EyeType = FigureEyeType.FullSculpted
    };
}