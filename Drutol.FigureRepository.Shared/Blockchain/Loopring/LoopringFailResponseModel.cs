using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Drutol.FigureRepository.Shared.Blockchain.Loopring;

public abstract record LoopringFailResponseModel
{
    [JsonPropertyName("resultInfo")] public ResultInfo Info { get; set; }

    public class ResultInfo
    {
        [JsonPropertyName("code")] public int Code { get; set; }

        [JsonPropertyName("message")] public string Message { get; set; }
    }
}