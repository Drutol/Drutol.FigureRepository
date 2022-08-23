using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Api.Logging;
using Drutol.FigureRepository.Api.Util;
using Drutol.FigureRepository.Shared.Download;
using Functional.Maybe;
using Microsoft.AspNetCore.Mvc;

namespace Drutol.FigureRepository.Api.Controllers;

[ApiController]
[Route("api/download")]
public class DownloadController : ControllerBase
{
    private readonly ILogger<DownloadController> _logger;
    private readonly IFigureDownloadManager _downloadManager;

    public DownloadController(
        ILogger<DownloadController> logger,
        IFigureDownloadManager downloadManager)
    {
        _logger = logger;
        _downloadManager = downloadManager;
    }

    [HttpPost("downloadData")]
    public async ValueTask<DownloadFigureResponse> GetDownloadData(DownloadFigureRequest request)
    {
        var result = await _downloadManager.CreateDownloadPackage(request.FigureGuid, request.Token);
        
        return result.SelectOrElse(
            urls =>
            {
                _logger.LogInformation(
                    DruEventId.DownloadPackageCreated.Ev(),
                    $"Created download package for figure {request.FigureGuid}.");
                return new DownloadFigureResponse(true, urls);
            },
            () =>
            {
                _logger.LogError(
                    DruEventId.DownloadPackageCreationFailed.Ev(),
                    $"Download package creation for figure {request.FigureGuid} failed.");
                return new DownloadFigureResponse(false);
            });
    }
}