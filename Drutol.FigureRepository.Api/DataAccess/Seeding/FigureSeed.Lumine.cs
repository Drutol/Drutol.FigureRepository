using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Shared.Models;
using Drutol.FigureRepository.Shared.Models.Enums;

namespace Drutol.FigureRepository.Api.DataAccess.Seeding;

public class LumineFigureSeed : IFigureSeed
{
    public Figure Figure { get; } = new()
    {
        Guid = Guid.Parse("E46284DB-90D7-4A2B-BA97-36C599C1FA87"),
        Name = "Lumine",
        Description = "Lumine based on her scenic park collaboration artwork. " +
                      "Having seen the base opportunity I instantly decided she will be the next project. " +
                      "Sculpted in a pose where she flips her front hair while looking smug. " +
                      "Comes with a number of details including custom engravings on the base.",
        InitialGalleryKindDisplay = FigureMediaKind.Painted,
        Media = new List<FigureMedia>
        {
            #region Photos

            new()
            {
                Url = "images/Lumine/Photos/Main.jpg",
                Display = FigurePhotoIntendedDisplay.All,
                MediaKind = FigureMediaKind.PrintPrototype,
                Width = 1080,
                Height = 1618
            },
            new()
            {
                Url = "images/Lumine/Photos/Front.jpg",
                MediaKind = FigureMediaKind.PrintPrototype,
                Width = 1080,
                Height = 1618
            },        
            new()
            {
                Url = "images/Lumine/Photos/FrontClose.jpg",
                MediaKind = FigureMediaKind.PrintPrototype,
                Width = 1080,
                Height = 1618
            },      
            new()
            {
                Url = "images/Lumine/Photos/Face.jpg",
                MediaKind = FigureMediaKind.PrintPrototype,
                Width = 1080,
                Height = 1080
            },      
            new()
            {
                Url = "images/Lumine/Photos/DarkLight.jpg",
                MediaKind = FigureMediaKind.PrintPrototype,
                Width = 1080,
                Height = 1618
            },      
            new()
            {
                Url = "images/Lumine/Photos/Base.jpg",
                MediaKind = FigureMediaKind.PrintPrototype,
                Width = 1080,
                Height = 1618
            },    
            new()
            {
                Url = "images/Lumine/Photos/Back.jpg",
                MediaKind = FigureMediaKind.PrintPrototype,
                Width = 1080,
                Height = 1618
            },  
            new()
            {
                Url = "images/Lumine/Photos/Skew.jpg",
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.PrintPrototype,
                Width = 1080,
                Height = 1618
            },

            #endregion

            #region Prototype

            new()
            {
                Url = "images/Lumine/Photos/Prototype.jpg",
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.PrintPrototype,
                Width = 1080,
                Height = 1440
            },

            #endregion

            #region Render

            new()
            {
                Url = "images/Lumine/Render/Main.jpg",
                Display = FigurePhotoIntendedDisplay.All,
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1080,
                Height = 1920
            },
            new()
            {
                Url = "images/Lumine/Render/FaceCloseup.jpg",
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1080,
                Height = 1080
            },
            new()
            {
                Url = "images/Lumine/Render/BodyCloseup.jpg",
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1080,
                Height = 1080
            },
            new()
            {
                Url = "images/Lumine/Render/FrontLeft.jpg",
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1080,
                Height = 1080
            },  
            new()
            {
                Url = "images/Lumine/Render/FrontCloseup.jpg",
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1080,
                Height = 1080
            },
            new()
            {
                Url = "images/Lumine/Render/LeftRightSide.jpg",
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1080,
                Height = 1080
            },
            new()
            {
                Url = "images/Lumine/Render/LeftSide.jpg",
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1080,
                Height = 1080
            },
            new()
            {
                Url = "images/Lumine/Render/LegsCloseup.jpg",
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1080,
                Height = 1080
            },    
            new()
            {
                Url = "images/Lumine/Render/Back.jpg",
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
                Date = new DateOnly(2021, 4, 26)
            },
            new()
            {
                Event = FigureTimelineEvent.SculptDone,
                Date = new DateOnly(2021, 11, 10)
            },
            new()
            {
                Event = FigureTimelineEvent.PrintDone,
                Date = new DateOnly(2021, 11, 28)
            }
        },
        FigureDimensions = new FigureDimensions
        {
            Width = 207,
            Height = 270,
            Length = 150
        },
        PrintDetails = new FigurePrintDetails
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
        NftDetails = new FigureNftDetails("0xb90810d7a02287b28dd1afabe2aa4d031d29bee2")
        {
            TokenId = 32771,
            NftData = "0x0a979c7308097d7847e8b8a4979546757774877131d695f208889d628bd05d36",
            NftId = "0x9e67eb098d20115a197fe07cf11822b280cc8fe88e2fc9108f2f5a10b0dbe77d"
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
            }
        },
        DownloadResources = new List<FigureDownloadResource>
        {
            new()
            {
                FigureDownloadType = FigureDownloadType.BlenderScene,
                Sha256 = "SHASHASHA256"
            },
            new()
            {
                FigureDownloadType = FigureDownloadType.SlicedScenes,
                Sha256 = "SHASHASHA256"
            },
            new()
            {
                FigureDownloadType = FigureDownloadType.Stls,
                Sha256 = "SHASHASHA256"
            }
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
    };
}