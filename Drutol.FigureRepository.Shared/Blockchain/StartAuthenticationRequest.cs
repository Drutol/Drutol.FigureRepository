using System;
using System.Collections.Generic;
using System.Text;

namespace Drutol.FigureRepository.Shared.Blockchain
{
    public class StartAuthenticationRequest
    {
        public int ChainId { get; set; }
        public string WalletAddress { get; set; }
        public string TokenAddress { get; set; }
    }
}
