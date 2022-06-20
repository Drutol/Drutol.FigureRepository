using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Drutol.FigureRepository.Api.Models.Blockchain
{
    [Struct("Owner")]
    public class AddressOwnerMessage
    {
        [Parameter("address", "ownerAddress", 1)]
        public string OwnerAddress { get; set; }
    }
}
