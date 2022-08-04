using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Drutol.FigureRepository.Shared.Blockchain.Loopring;

public interface IGetOffchainFeeResponseModel
{
    public record Fail : LoopringFailResponseModel, IGetOffchainFeeResponseModel;

    public record Success : IGetOffchainFeeResponseModel
    {
        [JsonPropertyName("gasPrice")]
        public string GasPrice { get; set; }

        [JsonPropertyName("fees")]
        public List<Fee> Fees { get; set; }

        public class Fee
        {
            [JsonPropertyName("token")]
            public string Token { get; set; }

            [JsonPropertyName("tokenId")]
            public int TokenId { get; set; }

            [JsonPropertyName("fee")]
            public string RequiredFee { get; set; }

            [JsonPropertyName("discount")]
            public int Discount { get; set; }
        }
    }
}