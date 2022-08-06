using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drutol.FigureRepository.Shared.Models.Auth;

public record TokenResponse(string JwtToken, DateTime Expiration);