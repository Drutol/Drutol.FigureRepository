using System.IdentityModel.Tokens.Jwt;
using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Shared.Models.Enums;
using Drutol.FigureRepository.Shared.Models.Figure;
using Functional.Maybe;
using Microsoft.IdentityModel.Tokens;

namespace Drutol.FigureRepository.Api.Infrastructure.Downloads
{
    public class FigureDownloadManager : IFigureDownloadManager
    {
        private readonly IFigureSeedManager _figureSeedManager;
        private readonly IDownloadTokenManager _tokenManager;
        private readonly IDownloadLinkGenerator _linkGenerator;

        public FigureDownloadManager(
            IFigureSeedManager figureSeedManager,
            IDownloadTokenManager tokenManager,
            IDownloadLinkGenerator linkGenerator)
        {
            _figureSeedManager = figureSeedManager;
            _tokenManager = tokenManager;
            _linkGenerator = linkGenerator;
        }

        public async ValueTask<Maybe<Dictionary<FigureDownloadType, string>>> CreateDownloadPackage(
            Guid figureGuid,
            string token)
        {
            var figure = _figureSeedManager.Figures.FirstOrDefault(f => f.Guid == figureGuid);

            if(figure == null)
                return Maybe<Dictionary<FigureDownloadType, string>>.Nothing;

            var authorized = _tokenManager.ValidateToken(figureGuid, token);

            if (authorized)
            {
                var downloadData = new Dictionary<FigureDownloadType, string>();
                foreach (var downloadResource in figure.DownloadResources)
                {
                    var link = await _linkGenerator.GenerateDownloadTokenForFigure(figure, downloadResource);
                    link.Do(l => downloadData[downloadResource.FigureDownloadType] = l);
                }

                return downloadData.ToMaybe();
            }
            else
            {
                return Maybe<Dictionary<FigureDownloadType, string>>.Nothing;
            }
        }
    }
}
