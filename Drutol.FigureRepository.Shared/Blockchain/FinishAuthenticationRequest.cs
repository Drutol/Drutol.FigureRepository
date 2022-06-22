using System;
using System.Collections.Generic;
using System.Text;

namespace Drutol.FigureRepository.Shared.Blockchain
{
    public record FinishAuthenticationRequest(Guid SessionGuid, string SignedDataHash);
}
