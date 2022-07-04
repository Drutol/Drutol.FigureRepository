using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Shared.Models;
using Drutol.FigureRepository.Shared.Models.Enums;

namespace Drutol.FigureRepository.Api.DataAccess.Seeding
{
    public class LumineFigureSeed : IFigureSeed
    {
        public Figure Figure { get; } = new Figure
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
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Top,
                    MediaKind = FigureMediaKind.ShadedRender,
                    Width = 1080,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Lumine/Render/RightSide.jpg",
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Top,
                    MediaKind = FigureMediaKind.ShadedRender,
                    Width = 1080,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Lumine/Render/ShoesCloseup.jpg",
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Top,
                    MediaKind = FigureMediaKind.ShadedRender,
                    Width = 1080,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Lumine/Render/Side.jpg",
                    Display = FigurePhotoIntendedDisplay.Gallery,
                    Gravity = FigurePhotoGravity.Top,
                    MediaKind = FigureMediaKind.ShadedRender,
                    Width = 1080,
                    Height = 1080
                },
                new FigureMedia
                {
                    Url = "images/Lumine/Render/TopBody.jpg",
                    Display = FigurePhotoIntendedDisplay.Gallery,
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
            NftDetails = new("0xb06146103f0b05e55c7b8ccf4dcd8df70b5a105c")
            {
                TokenId = 32769
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
}
