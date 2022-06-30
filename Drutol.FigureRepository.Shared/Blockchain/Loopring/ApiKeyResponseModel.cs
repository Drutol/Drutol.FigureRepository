using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Drutol.FigureRepository.Shared.Blockchain.Loopring
{
    public interface IApiKeyResponseModel
    {
        public record Fail : IApiKeyResponseModel;

        public record Success : IApiKeyResponseModel
        {
            [JsonPropertyName("apiKey")]
            public string ApiKey { get; init; }
        }
    }
}
