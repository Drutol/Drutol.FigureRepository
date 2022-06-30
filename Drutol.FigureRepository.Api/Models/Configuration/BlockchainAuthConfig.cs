namespace Drutol.FigureRepository.Api.Models.Configuration
{
    public class BlockchainAuthConfig
    {
        public string JwtSigningKey { get; set; }
        public string RpcUrl { get; set; }
        public string LoopringApiUrl { get; set; }
        public string LoopringExchangeAddress { get; set; }
    }
}
