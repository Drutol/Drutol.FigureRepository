using System.Text.Json.Serialization;

namespace Drutol.FigureRepository.Shared.Blockchain.Loopring.Nft;

public interface ITransferNftResponseModel
{
    public record Fail : LoopringFailResponseModel, ITransferNftResponseModel;

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