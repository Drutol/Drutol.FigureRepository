using Drutol.FigureRepository.Shared.Models.Auth;
using Microsoft.AspNetCore.Components.Authorization;

namespace Drutol.FigureRepository.BlazorApp.Interfaces;

public interface IApiAuthenticationStateProvider
{
    TokenResponse? TokenResponse { get; }
    Task<bool> Authenticate(string key);
    event AuthenticationStateChangedHandler? AuthenticationStateChanged;
}