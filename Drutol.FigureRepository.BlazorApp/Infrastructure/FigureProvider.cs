using Drutol.FigureRepository.Shared.Models;
using Drutol.FigureRepository.Shared.Models.Enums;

namespace Drutol.FigureRepository.BlazorApp.Infrastructure;

public class FigureProvider
{
    public List<Figure> Figures { get; } = new()
    {
        new Figure
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
                    Url = "https://www.cgtrader.com/3d-print-models/art/sculptures/ganyu-from-genshin-impact-sculpture-for-3d-printing",
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
            NftDetails = new("0x7267f3256289ad424835309275511C0BD225D6E1"),
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
        },
        new Figure
        {           
            Guid = Guid.Parse("E46284DB-90D7-4A2B-BA97-36C599C1FA87"),
            Name = "Lumine",
            Description = "Lumine based on her scenic park collaboration artwork. " +
                          "Having seen the base opportunity I instantly decided she will be the next project. " +
                          "Sculpted in a pose where she flips her front hair while looking smug. " +
                          "Comes with a number of details including custom engravings on the base.",
            InitialGalleryKindDisplay = FigureMediaKind.PrintPrototype,
            Media = new List<FigureMedia>
            {
                #region Photos

                new FigureMedia
                {
                    Url = "images/Lumine/Photos/Prototype.jpg",
                    Display = FigurePhotoIntendedDisplay.All,
                    Gravity = FigurePhotoGravity.Top,
                    MediaKind = FigureMediaKind.PrintPrototype,
                    Width = 1080,
                    Height = 1440
                },

                #endregion

                #region Render

                new FigureMedia
                {
                    Url = "images/Lumine/Render/Front.jpg",
                    Display = FigurePhotoIntendedDisplay.All,
                    Gravity = FigurePhotoGravity.Top,
                    MediaKind = FigureMediaKind.ShadedRender,
                    Width = 1080,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Lumine/Render/Back.jpg",
    
                    Gravity = FigurePhotoGravity.Top,
                    MediaKind = FigureMediaKind.ShadedRender,
                    Width = 1080,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Lumine/Render/RightSide.jpg",
    
                    Gravity = FigurePhotoGravity.Top,
                    MediaKind = FigureMediaKind.ShadedRender,
                    Width = 1080,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Lumine/Render/ShoesCloseup.jpg",
    
                    Gravity = FigurePhotoGravity.Top,
                    MediaKind = FigureMediaKind.ShadedRender,
                    Width = 1080,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Lumine/Render/Side.jpg",
    
                    Gravity = FigurePhotoGravity.Top,
                    MediaKind = FigureMediaKind.ShadedRender,
                    Width = 1080,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Lumine/Render/TopBody.jpg",
    
                    Gravity = FigurePhotoGravity.Top,
                    MediaKind = FigureMediaKind.ShadedRender,
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
                    Date = new(2021, 4, 26)
                },
                new FigureTimelineEntry
                {
                    Event = FigureTimelineEvent.SculptDone,
                    Date = new(2021, 11, 10)
                },
                new FigureTimelineEntry
                {
                    Event = FigureTimelineEvent.PrintDone,
                    Date = new(2021, 11, 28)
                }
            },
            FigureDimensions = new FigureDimensions()
            {
                Width = 207,
                Height = 270,
                Length = 150
            },
            PrintDetails = new FigurePrintDetails()
            {
                BiggestPartDimension = new FigureDimensions
                {
                    Width = 140,
                    Height = 113,
                    Length = 144
                },
                MaxNumberOfParts = 26,
                MinNumberOfParts = 26,
                NumberOfClearParts = 3,
                NumberOfPrintBatches = 5,
                PrintResinVolume = 942
            },
            NftDetails = new("0x7267f3256289ad424835309275511C0BD225D6E1"),
            TechnicalStatistics = new FigureTechnicalStatistics
            {
                LycheeScenesSize = 450_092_877,
                StlsSize = 1_238_851_584,
                BlendFileSize = 1_823_485_952,
                Faces = 13_531_405,
                Triangles = 24_776_988,
                Vertices = 12_388_856
            },
            ExternalLinks = new List<FigureExternalLink>
            {
                new()
                {
                    Url = "https://www.artstation.com/artwork/DAQ4Le",
                    Type = FigureExternalLinkType.ArtStation
                },
                new()
                {
                    Url = "https://twitter.com/Drutol/status/1464873712474337283",
                    Type = FigureExternalLinkType.Twitter
                },
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
                NameEnglish = "Lumine",
                NameJapanese = "蛍",

                OriginNameEnglish = "Genshin Impact",
                OriginNameJapanese = "原神"
            },
            EyeType = FigureEyeType.FullDecals
        },
        new Figure
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
            CheckoutDetails = new FigureCheckoutDetails
            {
                Price = 40m
            },
            NftDetails = new("0x7267f3256289ad424835309275511C0BD225D6E1"),
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
                NameEnglish = "Kurashina Asuka",
                NameJapanese = "倉科 明日香",

                OriginNameEnglish = "Aokana: Four Rhythm Across the Blue",
                OriginNameJapanese = "蒼の彼方のフォーリズム"
            },
            EyeType = FigureEyeType.IrisDecals
        },
        new Figure()
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
                    Date = new DateOnly(2021, 3, 22),
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
            NftDetails = new("0x7267f3256289ad424835309275511C0BD225D6E1"),
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
                NameEnglish = "Aoyagi Madoka",
                NameJapanese = "青柳 窓果",

                OriginNameEnglish = "Aokana: Four Rhythm Across the Blue",
                OriginNameJapanese = "蒼の彼方のフォーリズム"
            },
            EyeType = FigureEyeType.PartSculpted
        }
    };
}