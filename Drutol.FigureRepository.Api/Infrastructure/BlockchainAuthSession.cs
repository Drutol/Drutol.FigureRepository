using System.Text.Json;
using Drutol.FigureRepository.Api.Models.Blockchain;
using Drutol.FigureRepository.Shared.Blockchain;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Signer.EIP712;

namespace Drutol.FigureRepository.Api.Infrastructure
{
    public record BlockchainAuthSession(StartAuthenticationRequest Request)
    {
        public Guid SessionGuid { get; } = Guid.NewGuid();
        public DateTime ExpiresAt { get; } = DateTime.UtcNow.Add(TimeSpan.FromMinutes(10));

        public string GetSerializedDataToSign()
        {
            var typedData = GetTypedData();
            typedData.Message = MemberValueFactory.CreateFromMessage(GetMessage());
            return JsonSerializer.Serialize(typedData);
        }

        public TypedData<Domain> GetTypedData()
        {
            return new TypedData<Domain>
            {
                Domain = new Domain
                {
                    Name = "figure.drutol.com",
                    Version = "1",
                    ChainId = Request.ChainId,
                    VerifyingContract = Request.TokenAddress
                },
                Types = MemberDescriptionFactory.GetTypesMemberDescription(typeof(Domain), typeof(AddressOwnerMessage)),
                PrimaryType = nameof(AddressOwnerMessage),
            };
        }

        public AddressOwnerMessage GetMessage() => new AddressOwnerMessage
        {
            OwnerAddress = Request.WalletAddress
        };
    }
}
