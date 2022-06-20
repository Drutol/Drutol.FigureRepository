using Drutol.FigureRepository.Shared.Blockchain;
using Microsoft.AspNetCore.Mvc;

namespace Drutol.FigureRepository.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class BlockchainAuthController : ControllerBase
{
    private readonly ILogger<BlockchainAuthController> _logger;

    public BlockchainAuthController(ILogger<BlockchainAuthController> logger)
    {
        _logger = logger;
    }

    [HttpGet("StartAuthentication")]
    public async Task<StartAuthenticationResult> Start()
    {

    }      
        
    [HttpPost("FinishAuthentication")]
    public async Task<FinishAuthenticationResult> Finish()
    {

    }
}