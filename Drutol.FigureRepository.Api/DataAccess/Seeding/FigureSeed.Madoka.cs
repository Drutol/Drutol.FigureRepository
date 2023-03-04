using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Shared.Models;
using Drutol.FigureRepository.Shared.Models.Enums;
using Drutol.FigureRepository.Shared.Models.Figure;
using Drutol.FigureRepository.Shared.Models.Figure.Enums;
using Drutol.FigureRepository.Shared.Util;

namespace Drutol.FigureRepository.Api.DataAccess.Seeding;

public class MadokaFigureSeed : IFigureSeed
{
    public Figure Figure { get; } = new Figure()
    {
        Guid = KnownFigureUtils.Guids[KnownFigures.Madoka],
        Index = 3,
        Name = "Madoka",
        Flavour = "Smug Onsen",
        Description = "Madoka based on her Onsen CG artwork. " +
                      "It's a bit more risque figure but any naughty bits are not present. " +
                      "She is talking on the phone with a smug aura around, with a cute bear keychain included. " +
                      "The base is imagined to remind of the Onsen with wooden planks and paved path with bonsai tree and an Andon lamp.",
        InitialGalleryKindDisplay = FigureMediaKind.Painted,
        Media = new List<FigureMedia>
        {
            #region Photos

            new FigureMedia
            {
                Url = "images/Madoka/Photos/PrintPrototype.webp",
                MediaKind = FigureMediaKind.PrintPrototype,
                Width = 1080,
                Height = 1440
            },    
            new FigureMedia
            {
                Url = "images/Madoka/Photos/Main.webp",
                Display = FigurePhotoIntendedDisplay.All,
                Gravity = FigurePhotoGravity.Top,
                MediaKind = FigureMediaKind.Painted,
                Width = 721,
                Height = 1080
            },   
            new FigureMedia
            {
                Url = "images/Madoka/Photos/Face.webp",
                MediaKind = FigureMediaKind.Painted,
                Width = 1080,
                Height = 1080
            },  
            new FigureMedia
            {
                Url = "images/Madoka/Photos/FrontTop.webp",
                Gravity = FigurePhotoGravity.Center,
                MediaKind = FigureMediaKind.Painted,
                Width = 721,
                Height = 1080
            },   
            new FigureMedia
            {
                Url = "images/Madoka/Photos/FrontBottom.webp",
                MediaKind = FigureMediaKind.Painted,
                Width = 721,
                Height = 1080
            },   
            new FigureMedia
            {
                Url = "images/Madoka/Photos/BackLeft.webp",
                MediaKind = FigureMediaKind.Painted,
                Width = 721,
                Height = 1080
            },       
            new FigureMedia
            {
                Url = "images/Madoka/Photos/Sakura.webp",
                MediaKind = FigureMediaKind.Painted,
                Width = 1080,
                Height = 1080
            },

            #endregion

            #region Render

            new FigureMedia
            {
                Url = "images/Madoka/Render/Front.webp",
                MediaKind = FigureMediaKind.ShadedRender,
                Gravity = FigurePhotoGravity.Top,
                Width = 1080,
                Height = 1920
            },
            new FigureMedia
            {
                Url = "images/Madoka/Render/FaceCloseup.webp",
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1080,
                Height = 1920
            },
            new FigureMedia
            {
                Url = "images/Madoka/Render/BottomLeft.webp",
                MediaKind = FigureMediaKind.ShadedRender,
                Gravity = FigurePhotoGravity.Bottom,
                Width = 1080,
                Height = 1920
            },
            new FigureMedia
            {
                Url = "images/Madoka/Render/BackRight.webp",
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1080,
                Height = 1920
            },
            new FigureMedia
            {
                Url = "images/Madoka/Render/Bear.webp",
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1920,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Madoka/Render/Base.webp",
                MediaKind = FigureMediaKind.ShadedRender,
                Width = 1920,
                Height = 1080
            },

            #endregion

            #region Sculpt

            new FigureMedia
            {
                Url = "images/Madoka/Sculpt/Left.webp",
                MediaKind = FigureMediaKind.SculptRender,
                Width = 1080,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Madoka/Sculpt/LeftSide.webp",
                MediaKind = FigureMediaKind.SculptRender,
                Width = 1080,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Madoka/Sculpt/RightSide.webp",
                MediaKind = FigureMediaKind.SculptRender,
                Width = 1080,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Madoka/Sculpt/Andon.webp",
                MediaKind = FigureMediaKind.SculptRender,
                Width = 1080,
                Height = 1080
            },
            new FigureMedia
            {
                Url = "images/Madoka/Sculpt/Bonsai.webp",
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
            },
            new()
            {
                Date = new(2022, 10, 10),
                Event = FigureTimelineEvent.PaintDone
            }, 
            new()
            {
                Date = new(2023, 3, 4),
                Event = FigureTimelineEvent.Publish
            }
        },
        ExternalLinks = new List<FigureExternalLink>
        {
            new()
            {
                Url = "https://www.artstation.com/artwork/VJgaBN",
                Type = FigureExternalLinkType.ArtStation
            },
            new()
            {
                Url = "https://twitter.com/Drutol/status/1631982651371388928",
                Type = FigureExternalLinkType.Twitter
            },
        },
        NftDetails = new("0xe3111f751c5649216fb806bcaf4e614faf92e55c")
        {
            TokenId = 32772,
            NftData = "0x0210174a8074b41f4da240628e1fa45398f6efe2474d2bcff1bafec014d5ac9e",
            NftId = "0xb5e81d64e02e4fcf76ea1a673ff37705eb21e113abb0f0b9c2f03fa82c722504"
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
            Price = 20m
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
            new FigureExternalPurchaseOption(FigureExternalPurchaseOptionType.CgTrader, "https://www.cgtrader.com/3d-print-models/art/sculptures/madoka-from-aokana-smug-onsen", false),
            new FigureExternalPurchaseOption(FigureExternalPurchaseOptionType.GameStopMarketplace, "https://nft.gamestop.com", true)
        },
        FigureMetadata = new FigureMetadata
        {
            Title = "Madoka - DruFigures",
            Description = "Garage kit of AoKana's Madoka for 3D printing. Check out renders, painted photos and get one for yourself!",
            ImageUrl = "https://figure.drutol.com/images/Madoka/Meta/Meta.webp",
            ImageWidth = 1200,
            ImageHeight = 628,
            ImageMimeType = "image/jpg",
            Type = "website",
            Url = "https://figure.drutol.com/Figures/Madoka"
        }
    };
}