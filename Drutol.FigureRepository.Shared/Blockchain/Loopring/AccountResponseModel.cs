using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Drutol.FigureRepository.Shared.Blockchain.Loopring
{
    public interface IAccountResponseModel
    {
        public record Fail : IAccountResponseModel;

        public record Success : IAccountResponseModel
        {
            [JsonPropertyName("accountId")] public int AccountId { get; init; }
            [JsonPropertyName("owner")] public string Owner { get; init; }
            [JsonPropertyName("frozen")] public bool Frozen { get; init; }
            [JsonPropertyName("publicKey")] public PublicKeys PublicKey { get; init; }
            [JsonPropertyName("tags")] public string Tags { get; init; }
            [JsonPropertyName("nonce")] public int Nonce { get; init; }
            [JsonPropertyName("keyNonce")] public int KeyNonce { get; init; }
            [JsonPropertyName("keySeed")] public string KeySeed { get; init; }

            public class PublicKeys
            {
                [JsonPropertyName("x")] public string X { get; init; }
                [JsonPropertyName("y")] public string Y { get; init; }
            }
        }
    }
}
