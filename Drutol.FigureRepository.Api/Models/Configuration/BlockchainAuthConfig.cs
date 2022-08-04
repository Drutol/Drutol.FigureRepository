namespace Drutol.FigureRepository.Api.Models.Configuration;

public class BlockchainAuthConfig
{
    public string JwtSigningKey { get; set; }
    public string RpcUrl { get; set; }
    public string LoopringApiUrl { get; set; }
    public string LoopringExchangeAddress { get; set; }
    public int LoopringChainId { get; set; }

    public int SourceAccountId { get; set; }
    public string SourceAccountAddress { get; set; }
    public string SourceAccountL2Key { get; set; }
    public string SourceAccountL1Key { get; set; }
}