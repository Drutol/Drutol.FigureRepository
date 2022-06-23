using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Api.Models.Configuration;
using Drutol.FigureRepository.Shared.Blockchain;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Nethereum.Contracts.Services;
using Nethereum.Contracts.Standards.ERC721;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.JsonRpc.Client;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.RPC.Eth.Transactions;
using Nethereum.Signer.EIP712;
using Nethereum.Util;
using Nethereum.Web3;

namespace Drutol.FigureRepository.Api.Infrastructure;

public class BlockchainAuthProvider : IBlockchainAuthProvider
{
    private readonly ILogger<BlockchainAuthProvider> _logger;
    private readonly IOptions<BlockchainAuthConfig> _configuration;

    private readonly ConcurrentDictionary<Guid, BlockchainAuthSession> _sessions = new();
    private PeriodicTimer _cleanerTimer;

    public BlockchainAuthProvider(
        ILogger<BlockchainAuthProvider> logger,
        IOptions<BlockchainAuthConfig> configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public async Task Initialize()
    {
        _ = Task.Factory.StartNew(
            AuthSessionsCleanLoop,
            CancellationToken.None,
            TaskCreationOptions.LongRunning,
            TaskScheduler.Default);
    }

    public ValueTask<StartAuthenticationResult> StartAuthentication(StartAuthenticationRequest startAuthenticationRequest)
    {
        var activeSession = _sessions.Values.FirstOrDefault(session => session.Request == startAuthenticationRequest);

        if (activeSession != default)
        {
            return ValueTask.FromResult(
                new StartAuthenticationResult(
                    activeSession.SessionGuid,
                    activeSession.GetSerializedDataToSign(),
                    StartAuthenticationResult.StatusCode.Ok));
        }

        var session = new BlockchainAuthSession(startAuthenticationRequest);
        _sessions[session.SessionGuid] = session;

        return ValueTask.FromResult(
            new StartAuthenticationResult(
                session.SessionGuid,
                session.GetSerializedDataToSign(),
                StartAuthenticationResult.StatusCode.Ok));
    }

    public async ValueTask<FinishAuthenticationResult> FinishAuthentication(FinishAuthenticationRequest finishAuthenticationRequest)
    {
        if (!_sessions.TryGetValue(finishAuthenticationRequest.SessionGuid, out var activeSession))
        {
            return 
                new FinishAuthenticationResult(FinishAuthenticationResult.StatusCode.SessionDoesNotExist);
        }

        var walletAddress = Eip712TypedDataSigner.Current.RecoverFromSignatureV4(
            activeSession.GetMessage(),
            activeSession.GetTypedData(),
            finishAuthenticationRequest.SignedDataHash);

        if(!walletAddress.Equals(activeSession.Request.WalletAddress, StringComparison.OrdinalIgnoreCase))
            return new FinishAuthenticationResult(FinishAuthenticationResult.StatusCode.InvalidSignedData);

        var service = new ERC721ContractService(
            new EthApiContractService(new RpcClient(new Uri("http://127.0.0.1:7545"))),
            activeSession.Request.TokenAddress);

        var amount = await service
            .BalanceOfQueryAsync(walletAddress, BlockParameter.CreateLatest())
            .ConfigureAwait(false);

        if (amount > 0)
        {
            return new FinishAuthenticationResult(
                FinishAuthenticationResult.StatusCode.Ok,
                GenerateJwtForToken(activeSession.Request.TokenAddress));
        }
        else
        {
            return new FinishAuthenticationResult(
                FinishAuthenticationResult.StatusCode.NotAnOwner);
        }
    }

    private string GenerateJwtForToken(string token)
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration.Value.JwtSigningKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
            Claims = new Dictionary<string, object>
            {
                {BlockChainAuthClaims.TokenOwnerClaim, token}
            }
        };

        var jwt = jwtHandler.CreateToken(tokenDescriptor);
        return jwtHandler.WriteToken(jwt);
    }

    private async void AuthSessionsCleanLoop()
    {
        _cleanerTimer = new PeriodicTimer(TimeSpan.FromMinutes(10));

        while (await _cleanerTimer.WaitForNextTickAsync())
        {
            foreach (var session in _sessions.Values.ToList())
            {
                if(session.ExpiresAt > DateTime.UtcNow)
                    _sessions.TryRemove(session.SessionGuid, out _);
            }
        }
    }

}