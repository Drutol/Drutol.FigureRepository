using System.Net.Http.Json;
using System.Security.Claims;
using Common.Logging;
using Drutol.FigureRepository.BlazorApp.Interfaces;
using Drutol.FigureRepository.BlazorApp.Interfaces.Http;
using Drutol.FigureRepository.Shared.Admin;
using Drutol.FigureRepository.Shared.Models.Auth;
using Microsoft.AspNetCore.Components.Authorization;

namespace Drutol.FigureRepository.BlazorApp.Infrastructure
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider, IApiAuthenticationStateProvider
    {
        private readonly ILogger<ApiAuthenticationStateProvider> _logger;
        private readonly IApiHttpClient _apiHttpClient;
        public TokenResponse? TokenResponse { get; private set; }

        public ApiAuthenticationStateProvider(
            ILogger<ApiAuthenticationStateProvider> logger,
            IApiHttpClient apiHttpClient)
        {
            _logger = logger;
            _apiHttpClient = apiHttpClient;
        }

        public async Task<bool> Authenticate(string key)
        {
            try
            {
                var response = await _apiHttpClient.Client.PostAsJsonAsync("/api/admin/login", new LoginRequest(key));
                var result = await response.Content.ReadFromJsonAsync<LoginRequestResult>();

                if (result.Success)
                {
                    TokenResponse = result.TokenResponse;
                    // TODO add timer for expiration if ever needed
                    NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to authenticate.");
                return false;
            }
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (TokenResponse != null && TokenResponse.Expiration.Subtract(TimeSpan.FromMinutes(2)) > DateTime.UtcNow)
                return Task.FromResult(BuildState());

            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));
        }

        private AuthenticationState BuildState()
        {
            return new AuthenticationState(
                new ClaimsPrincipal(
                    new ClaimsIdentity(
                        new List<Claim>
                        {
                            new Claim(AdminClaims.AdminClaim, string.Empty)
                        })));
        }
    }
}
