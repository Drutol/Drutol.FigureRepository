﻿using System.Text.Json;
using Drutol.FigureRepository.Api.DataAccess.Seeding;
using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Api.Models.Blockchain;
using Drutol.FigureRepository.Api.Models.Configuration;
using Drutol.FigureRepository.Api.Util;
using Drutol.FigureRepository.Shared.Blockchain;
using Drutol.FigureRepository.Shared.Blockchain.Loopring;
using Drutol.FigureRepository.Shared.Models.Figure;
using Microsoft.Extensions.Options;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.KeyStore.Crypto;
using Nethereum.Signer.EIP712;

namespace Drutol.FigureRepository.Api.Infrastructure.Blockchain;

public record BlockchainAuthSession(
    StartAuthenticationRequest Request,
    IFigureSeedManager FigureSeedManager,
    IOptions<BlockchainAuthConfig> Config,
    ILoopringCommunicator LoopringCommunicator)
{
    public Guid SessionGuid { get; } = Guid.NewGuid();
    public DateTime ExpiresAt { get; } = DateTime.UtcNow.Add(TimeSpan.FromMinutes(10));
    public Figure Figure { get; } = FigureSeedManager.Figures.First(figure => figure.Guid == Request.FigureGuid);

    public IAccountResponseModel.Success LoopringAccount { get; private set; }

    private static readonly RandomBytesGenerator RandomBytesGenerator = new RandomBytesGenerator();
    private static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web)
    {
        Converters =
        {
            new BigIntegerJsonConverter()
        }
    };

    public async ValueTask<string> GetSerializedDataToSign()
    {
        if (Request.Type == StartAuthenticationRequest.AuthenticationType.Mainnet)
        {
            throw new NotImplementedException();

            //var typedData = GetTypedData();
            //var node = JsonSerializer.SerializeToNode(typedData, SerializerOptions)!;
            //node[nameof(TypedData<Domain>.Message).ToLower()] = JsonSerializer.SerializeToNode(GetMessage(), SerializerOptions);

            //return node.ToJsonString(SerializerOptions);
        }

        if (Request.Type == StartAuthenticationRequest.AuthenticationType.Loopring)
        {
            var account = await LoopringCommunicator.GetAccount(Request.WalletAddress);

            switch (account)
            {
                case IAccountResponseModel.Success success:
                    {
                        LoopringAccount = success;
                        return
                            $"Sign this message to access Loopring Exchange: {Config.Value.LoopringExchangeAddress} with key nonce: {success.Nonce - 1}";
                    }
            }
        }

        return string.Empty;
    }

    //public TypedData<DomainWithSalt> GetTypedData()
    //{
    //    return new TypedData<DomainWithSalt>
    //    {
    //        Domain = new DomainWithSalt
    //        {
    //            Name = "figure.drutol.com",
    //            Version = "1",
    //            ChainId = Request.ChainId,
    //            VerifyingContract = Figure.NftDetails.TokenAddress,
    //            Salt = RandomBytesGenerator.GenerateRandomSalt()
    //        },
    //        Types = MemberDescriptionFactory.GetTypesMemberDescription(typeof(Domain), typeof(AddressOwnerMessage)),
    //        PrimaryType = nameof(AddressOwnerMessage),
    //    };
    //}

    public AddressOwnerMessage GetMessage() => new()
    {
        OwnerAddress = Request.WalletAddress
    };
}