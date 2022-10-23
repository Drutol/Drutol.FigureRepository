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
                DruEventId.AuthSessionCreated.Ev(),
                "Created auth session {SessionGuid} for {WalletAddress} for figure {FigureGuid}",
                result.SessionGuid, request.WalletAddress, request.FigureGuid);
        }
        else
        {
            _logger.LogError(
                DruEventId.AuthSessionCreationFailed.Ev(),
                "Failed to create auth session for {WalletAddress} for figure {FigureGuid} with status code {Status}",
                request.WalletAddress, request.FigureGuid, result.Status);
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
                DruEventId.AuthSessionAuthenticated.Ev(),
                "Successfully authenticated session {SessionGuid}.", request.SessionGuid);
        }
        else
        {
            _logger.LogInformation(
                DruEventId.AuthSessionAuthenticationFailed.Ev(),
                "Failed to authenticate session {SessionGuid}, status code: {Status}", request.SessionGuid, result.Status);
        }

        return result;
    }
}