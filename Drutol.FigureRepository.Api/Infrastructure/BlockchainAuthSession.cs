using Drutol.FigureRepository.Api.Models.Blockchain;
using Drutol.FigureRepository.Shared.Blockchain;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Signer.EIP712;

namespace Drutol.FigureRepository.Api.Infrastructure
{
    public record BlockchainAuthSession(StartAuthenticationRequest Request)
    {
        public DateTime ExpiresAt { get; } = DateTime.UtcNow.Add(TimeSpan.FromMinutes(10));

        public TypedData<Domain> GetDataToSign()
        {
            return new TypedData<Domain>
            {
                Domain = new Domain
                {
                    Name = "Drutol Figure Repository",
                    Version = "1",
                    ChainId = Request.ChainId,
                    VerifyingContract = Request.TokenAddress
                },
                Types = MemberDescriptionFactory.GetTypesMemberDescription(typeof(Domain), typeof(AddressOwnerMessage)),
                PrimaryType = nameof(AddressOwnerMessage),
                Message = new MemberValue[]
                {
                    new MemberValue
                    {
                        TypeName = 
                    }
                }
            };
        }
    }
}
