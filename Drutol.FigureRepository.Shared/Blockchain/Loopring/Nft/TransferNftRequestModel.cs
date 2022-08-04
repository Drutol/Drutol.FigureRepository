using System.Text.Json.Serialization;

namespace Drutol.FigureRepository.Shared.Blockchain.Loopring.Nft;

public class TransferNftRequestModel
{
    [JsonPropertyName("exchange")]
    public string Exchange { get; set; }

    [JsonPropertyName("fromAccountId")]
    public int FromAccountId { get; set; }

    [JsonPropertyName("fromAddress")]
    public string FromAddress { get; set; }

    [JsonPropertyName("toAccountId")]
    public int ToAccountId { get; set; }

    [JsonPropertyName("toAddress")]
    public string ToAddress { get; set; }

    [JsonPropertyName("token")]
    public Token TokenData { get; set; }

    [JsonPropertyName("maxFee")]
    public MaxFee Fee { get; set; }

    [JsonPropertyName("storageId")]
    public int StorageId { get; set; }

    [JsonPropertyName("payPayeeUpdateAccount")]
    public bool PayPayeeUpdateAccount { get; set; }

    [JsonPropertyName("ecdsaSignature")]
    public string EcdsaSignature { get; set; }

    [JsonPropertyName("memo")]
    public string Memo { get; set; }

    public class MaxFee
    {
        [JsonPropertyName("tokenId")]
        public int TokenId { get; set; }

        [JsonPropertyName("amount")]
        public string Amount { get; set; }
    }

    public class Token
    {
        [JsonPropertyName("tokenId")]
        public int TokenId { get; set; }

        [JsonPropertyName("nftData")]
        public string NftData { get; set; }

        [JsonPropertyName("amount")]
        public string Amount { get; set; }
    }
}