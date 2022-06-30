using System;
using System.Collections.Generic;
using System.Text;

namespace Drutol.FigureRepository.Shared.Blockchain
{
    public record FinishAuthenticationRequest(Guid SessionGuid, string SignedDataHash);

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
