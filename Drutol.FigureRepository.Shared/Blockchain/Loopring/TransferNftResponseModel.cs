using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Drutol.FigureRepository.Shared.Blockchain.Loopring
{
    public interface ITransferNftResponseModel
    {
        public record Fail : ITransferNftResponseModel;

        public record Success : ITransferNftResponseModel
        {
            [JsonPropertyName("hash")]
            public string Hash { get; set; }

            [JsonPropertyName("raw_data")]
            public RawData Data { get; set; }

            public class RawData
            {
                [JsonPropertyName("hash")]
                public string Hash { get; set; }

                [JsonPropertyName("status")]
                public string Status { get; set; }

                [JsonPropertyName("isIdempotent")]
                public bool IsIdempotent { get; set; }
            }
        }
    }
}
