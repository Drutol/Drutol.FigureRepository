using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Drutol.FigureRepository.Shared.Blockchain.Loopring;

public interface IGetStorageIdResponseModel
{
    public record Fail : LoopringFailResponseModel, IGetStorageIdResponseModel;

    public record Success : IGetStorageIdResponseModel
    {
        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }

        [JsonPropertyName("offchainId")]
        public int OffchainId { get; set; }
    }
}