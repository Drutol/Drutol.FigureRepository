using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Shared.Blockchain;
using Microsoft.AspNetCore.Mvc;

namespace Drutol.FigureRepository.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class BlockchainAuthController : ControllerBase
{
    private readonly IBlockchainAuthProvider _authProvider;
    private readonly ILogger<BlockchainAuthController> _logger;

    public BlockchainAuthController(
        IBlockchainAuthProvider authProvider,
        ILogger<BlockchainAuthController> logger)
    {
        _authProvider = authProvider;
        _logger = logger;
    }

    [HttpGet("StartAuthentication")]
    public async Task<StartAuthenticationResult> Start(StartAuthenticationRequest startAuthenticationRequest)
    {
        return await _authProvider.StartAuthentication(startAuthenticationRequest).ConfigureAwait(false);
    }      
        
    [HttpPost("FinishAuthentication")]
    public async Task<FinishAuthenticationResult> Finish(FinishAuthenticationRequest finishAuthenticationRequest)
    {
        return await _authProvider.FinishAuthentication(finishAuthenticationRequest).ConfigureAwait(false);
    }
}