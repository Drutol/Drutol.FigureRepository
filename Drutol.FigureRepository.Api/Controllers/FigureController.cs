using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Api.Logging;
using Drutol.FigureRepository.Api.Util;
using Drutol.FigureRepository.Shared.Models.Figure;
using Microsoft.AspNetCore.Mvc;

namespace Drutol.FigureRepository.Api.Controllers;

[ApiController]
[Route("api/figure")]
public class FigureController : ControllerBase
{
    private readonly ILogger<FigureController> _logger;
    private readonly IFigureSeedManager _seedManager;

    public FigureController(
        ILogger<FigureController> logger,
        IFigureSeedManager seedManager)
    {
        _logger = logger;
        _seedManager = seedManager;
    }

    [HttpGet("all")]
    public List<Figure> GetFigures()
    {
        _logger.LogTrace(DruEventId.FiguresFetched.Ev(), "Figures fetched.");
        return _seedManager.Figures;
    }
}