using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Shared.Download;
using Functional.Maybe;
using Microsoft.AspNetCore.Mvc;

namespace Drutol.FigureRepository.Api.Controllers
{
    [ApiController]
    [Route("download")]
    public class DownloadController : ControllerBase
    {
        private readonly IFigureDownloadManager _downloadManager;

        public DownloadController(IFigureDownloadManager downloadManager)
        {
            _downloadManager = downloadManager;
        }

        [HttpPost("downloadData")]
        public async ValueTask<DownloadFigureResponse> GetDownloadData(DownloadFigureRequest request)
        {
            var result = await _downloadManager.CreateDownloadPackage(request.FigureGuid, request.Token);
            return result.SelectOrElse(
                urls => new DownloadFigureResponse(true, urls),
                () => new DownloadFigureResponse(false));
        }
    }
}
