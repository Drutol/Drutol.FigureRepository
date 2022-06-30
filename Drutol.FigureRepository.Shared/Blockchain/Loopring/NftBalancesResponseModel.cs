using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Drutol.FigureRepository.Shared.Blockchain.Loopring
{
    public interface INftBalancesResponseModel
    {
        public record Fail : INftBalancesResponseModel;

        public record Success : INftBalancesResponseModel
        {
            [JsonPropertyName("totalNum")] public int TotalNum { get; init; }
            [JsonPropertyName("data")] public List<Data> NftData { get; init; }

            public class Data
            {
                [JsonPropertyName("id")] public int Id { get; init; }
                [JsonPropertyName("accountId")] public int AccountId { get; init; }
                [JsonPropertyName("tokenId")] public int TokenId { get; init; }
                [JsonPropertyName("nftData")] public string NftData { get; init; }
                [JsonPropertyName("tokenAddress")] public string TokenAddress { get; init; }
                [JsonPropertyName("nftId")] public string NftId { get; init; }
                [JsonPropertyName("nftType")] public string NftType { get; init; }
                [JsonPropertyName("total")] public string Total { get; init; }
                [JsonPropertyName("locked")] public string Locked { get; init; }
                [JsonPropertyName("pending")] public PendingTransactions Pending { get; init; }
                [JsonPropertyName("deploymentStatus")] public string DeploymentStatus { get; init; }
                [JsonPropertyName("isCounterFactualNFT")] public bool IsCounterFactualNFT { get; init; }

                public class PendingTransactions
                {
                    [JsonPropertyName("withdraw")] public string Withdraw { get; init; }
                    [JsonPropertyName("deposit")] public string Deposit { get; init; }
                }
            }
        }
    }

    
}
