using System;
using System.Collections.Generic;
using System.Text;
using Drutol.FigureRepository.Shared.Models.Auth;

namespace Drutol.FigureRepository.Shared.Blockchain;

public record FinishAuthenticationRequest(Guid SessionGuid, string SignedDataHash);

public record FinishAuthenticationResult(FinishAuthenticationResult.StatusCode Status, TokenResponse TokenResponse = default)
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