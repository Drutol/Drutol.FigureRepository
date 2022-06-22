using System;
using System.Collections.Generic;
using System.Text;

namespace Drutol.FigureRepository.Shared.Blockchain
{
    public record StartAuthenticationRequest(
        int ChainId,
        string WalletAddress,
        string TokenAddress,
        StartAuthenticationRequest.StatusCode Status)
    {
        public enum StatusCode
        {
            Ok,
            TokenAddressNotFound,
            Error = 999,
        }
    }


}
