using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Shared.Models.Enums;
using Drutol.FigureRepository.Shared.Models.Figure;
using Drutol.FigureRepository.Shared.Models.Figure.Enums;
using Drutol.FigureRepository.Shared.Util;

namespace Drutol.FigureRepository.Api.DataAccess.Seeding;

public class LumineFigureSeed : IFigureSeed
{
    public Figure Figure { get; } = new()
    {
        Guid = KnownFigureUtils.Guids[KnownFigures.Lumine],
        Index = 1,
        Name = "Lumine",
        Flavour = "Modern casual outfit",
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
                Url = "images/Lumine/Photos/Main.webp",
                Display = FigurePhotoIntendedDisplay.All,
                MediaKind = FigureMediaKind.Painted,
                Height = 1080,
                Width = 1618
            },
            new()
            {
                Url = "images/Lumine/Photos/Front.webp",
                MediaKind = FigureMediaKind.Painted,
                Height = 1080,
                Width = 1618
            },        
            new()
            {
                Url = "images/Lumine/Photos/FrontClose.webp",
                MediaKind = FigureMediaKind.Painted,
                Height = 1080,
                Width = 1618
            },      
            new()
            {
                Url = "images/Lumine/Photos/Face.webp",
                MediaKind = FigureMediaKind.Painted,
                Height = 1080,
                Width = 1080
            },      
            new()
            {
                Url = "images/Lumine/Photos/DarkLight.webp",
                Display = FigurePhotoIntendedDisplay.All,
                MediaKind = FigureMediaKind.Painted,
                Height = 1080,
                Width = 1618
            },      
            new()
            {
                Url = "images/Lumine/Photos/Base.webp",
                MediaKind = FigureMediaKind.Painted,
                Height = 1080,
                Width = 1618
            },    
            new()
            {
                Url = "images/Lumine/Photos/Back.webp",
                MediaKind = FigureMediaKind.Painted,
                Height = 1080,
                Width = 1618
            },  
            new()
            {
                Url = "images/Lumine/Photos/Skew.webp",
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.Painted,
                Height = 1080,
                Width = 1618
            },

            #endregion

            #region Prototype

            new()
            {
                Url = "images/Lumine/Photos/Prototype.webp",
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.PrintPrototype,
                Width = 1080,
                Height = 1440
            },

            #endregion

            #region Render

            new()
            {
                Url = "images/Lumine/Render/Main.webp",
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1080,
                Height = 1920
            },
            new()
            {
                Url = "images/Lumine/Render/FaceCloseup.webp",
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1080,
                Height = 1080
            },
            new()
            {
                Url = "images/Lumine/Render/BodyCloseup.webp",
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1080,
                Height = 1080
            },
            new()
            {
                Url = "images/Lumine/Render/FrontLeft.webp",
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1080,
                Height = 1080
            },  
            new()
            {
                Url = "images/Lumine/Render/FrontCloseup.webp",
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1080,
                Height = 1080
            },
            new()
            {
                Url = "images/Lumine/Render/LeftRightSide.webp",
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1080,
                Height = 1080
            },
            new()
            {
                Url = "images/Lumine/Render/LeftSide.webp",
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1080,
                Height = 1080
            },
            new()
            {
                Url = "images/Lumine/Render/LegsCloseup.webp",
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1080,
                Height = 1080
            },    
            new()
            {
                Url = "images/Lumine/Render/Back.webp",
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
                Date = new DateTime(2021, 4, 26)
            },
            new()
            {
                Event = FigureTimelineEvent.SculptDone,
                Date = new DateTime(2021, 11, 10)
            },
            new()
            {
                Event = FigureTimelineEvent.PrintDone,
                Date = new DateTime(2021, 11, 28)
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
            MaxNumberOfParts = 27,
            MinNumberOfParts = 27,
            NumberOfClearParts = 3,
            NumberOfPrintBatches = 5,
            PrintResinVolume = 942
        },
        NftDetails = new FigureNftDetails("0xe3111f751c5649216fb806bcaf4e614faf92e55c")
        {
            TokenId = 32771,
            NftData = "0x2009c6f916cf4a1c97997896a6aa37908740ec1033332b91926e1fc6affa6b8e",
            NftId = "0x6c4de015a95b31be48a83d1629bfd613b6e38eae8cc6fa68d7cb086b2ca1ec27"
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
                Type = FigureDownloadType.BlenderScene,
                Sha256 = "SHASHASHA256"
            },
            new()
            {
                Type = FigureDownloadType.SlicedScenes,
                Sha256 = "SHASHASHA256"
            },
            new()
            {
                Type = FigureDownloadType.Stls,
                Sha256 = "SHASHASHA256"
            }
        },
        CheckoutDetails = new FigureCheckoutDetails
        {
            Price = 30m
        },
        FigureCharacter = new FigureCharacter
        {
            NameEnglish = "Lumine",
            NameJapanese = "蛍",

            OriginNameEnglish = "Genshin Impact",
            OriginNameJapanese = "原神"
        },
        EyeType = FigureEyeType.FullDecals,
        ExternalPurchaseOptions = new()
        {
            new FigureExternalPurchaseOption(FigureExternalPurchaseOptionType.CgTrader, "https://www.cgtrader.com/3d-print-models/art/sculptures/lumine-from-genshin-impact-casual-outfit", false),
            new FigureExternalPurchaseOption(FigureExternalPurchaseOptionType.GameStopMarketplace, "https://nft.gamestop.com", true)
        },
        FigureMetadata = new FigureMetadata
        {
            Title = "Lumine - DruFigures",
            Description = "Garage kit of Genshin's Lumine for 3D printing. Check out renders, painted photos and get one for yourself!",
            ImageUrl = "https://figure.drutol.com/images/Lumine/Meta/Meta.webp",
            ImageWidth = 1200,
            ImageHeight = 628,
            ImageMimeType = "image/jpg",
            Type = "website",
            Url = "https://figure.drutol.com/Figures/Lumine"
        }
    };
}