using System.Text.Json;
using Drutol.FigureRepository.Api.Models.Blockchain;
using Drutol.FigureRepository.Api.Util;
using Drutol.FigureRepository.Shared.Blockchain;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.KeyStore.Crypto;
using Nethereum.Signer.EIP712;

namespace Drutol.FigureRepository.Api.Infrastructure
{
    public record BlockchainAuthSession(StartAuthenticationRequest Request)
    {
        public Guid SessionGuid { get; } = Guid.NewGuid();
        public DateTime ExpiresAt { get; } = DateTime.UtcNow.Add(TimeSpan.FromMinutes(10));

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
                var typedData = GetTypedData();
                var node = JsonSerializer.SerializeToNode(typedData, SerializerOptions)!;
                node[nameof(TypedData<Domain>.Message).ToLower()] = JsonSerializer.SerializeToNode(GetMessage(), SerializerOptions);

                return node.ToJsonString(SerializerOptions);
            }
            else if(Request.Type == StartAuthenticationRequest.AuthenticationType.Loopring)
            {
                return $"Sign this message to access Loopring Exchange: {} with key nonce: 0"
            }

        }

        public TypedData<DomainWithSalt> GetTypedData()
        {
            return new TypedData<DomainWithSalt>
            {
                Domain = new DomainWithSalt
                {
                    Name = "figure.drutol.com",
                    Version = "1",
                    ChainId = Request.ChainId,
                    VerifyingContract = Request.TokenAddress,
                    Salt = RandomBytesGenerator.GenerateRandomSalt()
                },
                Types = MemberDescriptionFactory.GetTypesMemberDescription(typeof(Domain), typeof(AddressOwnerMessage)),
                PrimaryType = nameof(AddressOwnerMessage),
            };
        }

        public AddressOwnerMessage GetMessage() => new()
        {
            OwnerAddress = Request.WalletAddress
        };
    }
}
