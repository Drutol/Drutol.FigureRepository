using System;
using System.Collections.Generic;
using System.Text;

namespace Drutol.FigureRepository.Shared.Blockchain
{
    public record StartAuthenticationResult(Guid SessionGuid, string DataToSign, StartAuthenticationResult.StatusCode Status)
    {
        public enum StatusCode
        {
            Ok,
            TokenAddressNotFound,
            Error = 999,
        }
    }
}
