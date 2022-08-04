using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Api.Models.Configuration;
using Drutol.FigureRepository.Shared.Blockchain;
using Drutol.FigureRepository.Shared.Blockchain.Loopring.Nft;
using Drutol.FigureRepository.Shared.Models;
using LoopringSharp;
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
    private readonly ILoopringCommunicator _loopringCommunicator;
    private readonly IDownloadTokenGenerator _downloadTokenGenerator;
    private readonly Func<StartAuthenticationRequest, BlockchainAuthSession> _authSessionFactory;
    private readonly IOptions<BlockchainAuthConfig> _configuration;

    private readonly ConcurrentDictionary<Guid, BlockchainAuthSession> _sessions = new();
    private PeriodicTimer _cleanerTimer;

    public BlockchainAuthProvider(
        ILogger<BlockchainAuthProvider> logger,
        ILoopringCommunicator loopringCommunicator,
        IDownloadTokenGenerator downloadTokenGenerator,
        Func<StartAuthenticationRequest, BlockchainAuthSession> authSessionFactory,
        IOptions<BlockchainAuthConfig> configuration)
    {
        _logger = logger;
        _loopringCommunicator = loopringCommunicator;
        _downloadTokenGenerator = downloadTokenGenerator;
        _authSessionFactory = authSessionFactory;
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

    public async ValueTask<StartAuthenticationResult> StartAuthentication(
        StartAuthenticationRequest startAuthenticationRequest)
    {
        var activeSession = _sessions.Values.FirstOrDefault(session => session.Request == startAuthenticationRequest);

        if (activeSession != default)
        {
            return new StartAuthenticationResult(
                    activeSession.SessionGuid,
                    await activeSession.GetSerializedDataToSign(),
                    StartAuthenticationResult.StatusCode.Ok);
        }

        var session = _authSessionFactory(startAuthenticationRequest);
        _sessions[session.SessionGuid] = session;

        return new StartAuthenticationResult(
                session.SessionGuid,
                await session.GetSerializedDataToSign(),
                StartAuthenticationResult.StatusCode.Ok);
    }

    public async ValueTask<FinishAuthenticationResult> FinishAuthentication(FinishAuthenticationRequest finishAuthenticationRequest)
    {
        if (!_sessions.TryGetValue(finishAuthenticationRequest.SessionGuid, out var activeSession))
        {
            return 
                new FinishAuthenticationResult(FinishAuthenticationResult.StatusCode.SessionDoesNotExist);
        }

        int ownedAmount = 0;
        if (activeSession.Request.Type == StartAuthenticationRequest.AuthenticationType.Loopring)
        {
            ownedAmount = await _loopringCommunicator.GetBalances(
                    finishAuthenticationRequest.SignedDataHash,
                    activeSession.Request.WalletAddress,
                    activeSession.LoopringAccount.AccountId,
                    activeSession.Figure.NftDetails.TokenId) switch
                {
                    INftBalancesResponseModel.Success success => int.Parse(success.NftData[0].Total),
                    _ => 0
                };
        }
        else if (activeSession.Request.Type == StartAuthenticationRequest.AuthenticationType.Mainnet)
        {
            //var walletAddress = Eip712TypedDataSigner.Current.RecoverFromSignatureV4(
            //    activeSession.GetMessage(),
            //    activeSession.GetTypedData(),
            //    finishAuthenticationRequest.SignedDataHash);

            //if (!walletAddress.Equals(activeSession.Request.WalletAddress, StringComparison.OrdinalIgnoreCase))
            //    return new FinishAuthenticationResult(FinishAuthenticationResult.StatusCode.InvalidSignedData);

            //var service = new ERC721ContractService(
            //    new EthApiContractService(new RpcClient(new Uri(_configuration.Value.RpcUrl))),
            //    activeSession.Request.TokenAddress);

            //var amount = await service
            //    .BalanceOfQueryAsync(walletAddress, BlockParameter.CreateLatest())
            //    .ConfigureAwait(false);
        }

        if (ownedAmount > 0)
        {
            return new FinishAuthenticationResult(
                FinishAuthenticationResult.StatusCode.Ok,
                _downloadTokenGenerator.GenerateDownloadTokenForFigure(activeSession.Figure));
        }
        else
        {
            return new FinishAuthenticationResult(
                FinishAuthenticationResult.StatusCode.NotAnOwner);
        }
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