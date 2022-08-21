using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drutol.FigureRepository.Shared.Models.Auth;

namespace Drutol.FigureRepository.Shared.Admin
{
    public record LoginRequest(string Key);

    public record LoginResult(bool Success, TokenResponse TokenResponse = null);
}
