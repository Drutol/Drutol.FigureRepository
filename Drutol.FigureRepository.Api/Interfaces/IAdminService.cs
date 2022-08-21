using Drutol.FigureRepository.Shared.Models.Auth;
using Functional.Maybe;

namespace Drutol.FigureRepository.Api.Interfaces;

public interface IAdminService
{
    Maybe<TokenResponse> Authenticate(string key);
}