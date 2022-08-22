using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Api.Logging;
using Drutol.FigureRepository.Api.Util;
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

    [HttpPost("StartAuthentication")]
    public async Task<StartAuthenticationResult> Start(StartAuthenticationRequest request)
    {
        var result = await _authProvider.StartAuthentication(request).ConfigureAwait(false);

        if (result.Status == StartAuthenticationResult.StatusCode.Ok)
        {
            _logger.LogInformation(
                EventIds.AuthSessionCreated.Ev(),
                $"Created auth session {result.SessionGuid} for {request.WalletAddress} for figure {request.FigureGuid}");
        }
        else
        {
            _logger.LogError(
                EventIds.AuthSessionCreationFailed.Ev(),
                $"Failed to create auth session for {request.WalletAddress} for figure {request.FigureGuid} with status code {result.Status}");
        }

        return result;
    }      
        
    [HttpPost("FinishAuthentication")]
    public async Task<FinishAuthenticationResult> Finish(FinishAuthenticationRequest request)
    {
        var result =  await _authProvider.FinishAuthentication(request).ConfigureAwait(false);

        if (result.Status == FinishAuthenticationResult.StatusCode.Ok)
        {
            _logger.LogInformation(
                EventIds.AuthSessionAuthenticated.Ev(),
                $"Successfully authenticated session {request.SessionGuid}.");
        }
        else
        {
            _logger.LogInformation(
                EventIds.AuthSessionAuthenticationFailed.Ev(),
                $"Failed to authenticate session {request.SessionGuid}, status code: {result.Status}");
        }

        return result;
    }
}