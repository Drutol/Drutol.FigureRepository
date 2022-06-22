using System;
using System.Collections.Generic;
using System.Text;

namespace Drutol.FigureRepository.Shared.Blockchain
{
    public record FinishAuthenticationResult(FinishAuthenticationResult.StatusCode Status, string JsonToken = default)
    {
        public enum StatusCode
        {
            Ok,
            InvalidSignedData,
            SessionDoesNotExist,
            NotAnOwner,
            Error = 999,
        }
    }
}
