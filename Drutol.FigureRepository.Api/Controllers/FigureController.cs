using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Shared.Models.Figure;
using Microsoft.AspNetCore.Mvc;

namespace Drutol.FigureRepository.Api.Controllers;

[ApiController]
[Route("figure")]
public class FigureController : ControllerBase
{
    private readonly IFigureSeedManager _seedManager;

    public FigureController(IFigureSeedManager seedManager)
    {
        _seedManager = seedManager;
    }

    [HttpGet("all")]
    public List<Figure> GetFigures()
    {
        return _seedManager.Figures;
    }
}