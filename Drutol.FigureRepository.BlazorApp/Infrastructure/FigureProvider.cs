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
            Notes = "Printed back in the day on small Photon S printer.",
            Description = "Ganyu from GenshinImpact based on her Banner Art.",
            Media = new List<FigureMedia>
            {
                #region Photos

                new FigureMedia
                {
                    Url = "images/Ganyu/Photos/Main.jpg",
                    Name = "Main",
                    IsPortraitOrientation = true,
                    Display = FigurePhotoIntendedDisplay.All,
                    Gravity = FigurePhotoGravity.Top,
                    MediaKind = FigureMediaKind.PrintPrototype,
                    MediaType = FigureMediaType.Photo,
                    Width = 1080,
                    Height = 1440
                },
                new FigureMedia
                {
                    Url = "images/Ganyu/Photos/BacklightSilhouette.jpg",
                    Name = "Silhouette on lit background.",
                    IsPortraitOrientation = true,
                    Display = FigurePhotoIntendedDisplay.All,
                    Gravity = FigurePhotoGravity.Top,
                    MediaKind = FigureMediaKind.PrintPrototype,
                    MediaType = FigureMediaType.Photo,
                    Width = 1080,
                    Height = 1440
                },

                #endregion

                #region Sculpt

                new FigureMedia
                {
                    Url = "images/Ganyu/Sculpt/Front.jpg",
                    Name = "Front",
                    IsPortraitOrientation = true,
                    Display = FigurePhotoIntendedDisplay.All,
                    Gravity = FigurePhotoGravity.Top,
                    MediaKind = FigureMediaKind.SculptRender,
                    MediaType = FigureMediaType.Photo,
                    Width = 1080,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Ganyu/Sculpt/Face.jpg",
                    Name = "Face",
                    IsPortraitOrientation = true,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Top,
                    MediaKind = FigureMediaKind.SculptRender,
                    MediaType = FigureMediaType.Photo,
                    Width = 1080,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Ganyu/Sculpt/FrontBottom.jpg",
                    Name = "Bottom Closeup",
                    IsPortraitOrientation = true,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Top,
                    MediaKind = FigureMediaKind.SculptRender,
                    MediaType = FigureMediaType.Photo,
                    Width = 1080,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Ganyu/Sculpt/LeftSide.jpg",
                    Name = "Left Side",
                    IsPortraitOrientation = true,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Top,
                    MediaKind = FigureMediaKind.SculptRender,
                    MediaType = FigureMediaType.Photo,
                    Width = 1080,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Ganyu/Sculpt/Base.jpg",
                    Name = "Base",
                    IsPortraitOrientation = true,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Top,
                    MediaKind = FigureMediaKind.SculptRender,
                    MediaType = FigureMediaType.Photo,
                    Width = 1080,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Ganyu/Sculpt/BackBottom.jpg",
                    Name = "Back Bottom",
                    IsPortraitOrientation = true,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Top,
                    MediaKind = FigureMediaKind.SculptRender,
                    MediaType = FigureMediaType.Photo,
                    Width = 1080,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Ganyu/Sculpt/Back.jpg",
                    Name = "Back",
                    IsPortraitOrientation = true,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Top,
                    MediaKind = FigureMediaKind.SculptRender,
                    MediaType = FigureMediaType.Photo,
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
            NftDetails = new()
            {
                TokenAddress = "0x7267f3256289ad424835309275511C0BD225D6E1"
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
        },
        new Figure
        {
            Name = "Lumine",
            Description = "Lumine from GenshinImpact based on her scenic park collaboration artwork.",
            Media = new List<FigureMedia>
            {
                #region Photos

                new FigureMedia
                {
                    Url = "images/Lumine/Photos/Prototype.jpg",
                    IsPortraitOrientation = true,
                    Display = FigurePhotoIntendedDisplay.All,
                    Gravity = FigurePhotoGravity.Top,
                    MediaKind = FigureMediaKind.PrintPrototype,
                    MediaType = FigureMediaType.Photo,
                    Width = 1080,
                    Height = 1440
                },

                #endregion

                #region Render

                new FigureMedia
                {
                    Url = "images/Lumine/Render/Front.jpg",
                    IsPortraitOrientation = true,
                    Display = FigurePhotoIntendedDisplay.All,
                    Gravity = FigurePhotoGravity.Top,
                    MediaKind = FigureMediaKind.ShadedRender,
                    MediaType = FigureMediaType.Photo,
                    Width = 1080,
                    Height = 1080
                },    
                new FigureMedia
                {
                    Url = "images/Lumine/Render/Back.jpg",
                    IsPortraitOrientation = true,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Top,
                    MediaKind = FigureMediaKind.ShadedRender,
                    MediaType = FigureMediaType.Photo,
                    Width = 1080,
                    Height = 1080
                },    
                new FigureMedia
                {
                    Url = "images/Lumine/Render/RightSide.jpg",
                    IsPortraitOrientation = true,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Top,
                    MediaKind = FigureMediaKind.ShadedRender,
                    MediaType = FigureMediaType.Photo,
                    Width = 1080,
                    Height = 1080
                },    
                new FigureMedia
                {
                    Url = "images/Lumine/Render/ShoesCloseup.jpg",
                    IsPortraitOrientation = true,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Top,
                    MediaKind = FigureMediaKind.ShadedRender,
                    MediaType = FigureMediaType.Photo,
                    Width = 1080,
                    Height = 1080
                },    
                new FigureMedia
                {
                    Url = "images/Lumine/Render/Side.jpg",
                    IsPortraitOrientation = true,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Top,
                    MediaKind = FigureMediaKind.ShadedRender,
                    MediaType = FigureMediaType.Photo,
                    Width = 1080,
                    Height = 1080
                },    
                new FigureMedia
                {
                    Url = "images/Lumine/Render/TopBody.jpg",
                    IsPortraitOrientation = true,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Top,
                    MediaKind = FigureMediaKind.ShadedRender,
                    MediaType = FigureMediaType.Photo,
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
            NftDetails = new()
            {
                TokenAddress = "0x7267f3256289ad424835309275511C0BD225D6E1"
            },
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
                new FigureMedia
                {
                    Url = "images/Asuka/Render/AsukaAltRightSide.jpg",
                    Name = "Right Side",
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
                PrintResinVolume = 1148,
            },
            NftDetails = new()
            {
                TokenAddress = "0x7267f3256289ad424835309275511C0BD225D6E1"
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
                NameEnglish = "Kurashina Asuka",
                NameJapanese = "倉科 明日香",

                OriginNameEnglish = "Aokana: Four Rhythm Across the Blue",
                OriginNameJapanese = "蒼の彼方のフォーリズム"
            },
            EyeType = FigureEyeType.IrisDecals
        },
        new Figure()
        {
            Name = "Madoka",
            Description = "Madoka from AoKana based on her onsen CG artwork.",
            Media = new List<FigureMedia>
            {
                #region Photos

                new FigureMedia
                {
                    Url = "images/Madoka/Photos/FrontAlt.jpg",
                    Name = "Front",
                    IsPortraitOrientation = false,
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Center,
                    MediaKind = FigureMediaKind.PrintPrototype,
                    MediaType = FigureMediaType.Photo,
                    Width = 1080,
                    Height = 1440
                },

                #endregion

                #region Sculpt

                

                #endregion
            }
        }
    };
}