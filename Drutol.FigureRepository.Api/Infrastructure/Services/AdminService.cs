using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Api.Models.Configuration;
using Drutol.FigureRepository.Shared.Admin;
using Drutol.FigureRepository.Shared.Blockchain;
using Drutol.FigureRepository.Shared.Models.Auth;
using Functional.Maybe;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Drutol.FigureRepository.Api.Infrastructure.Services
{
    public class AdminService : IAdminService
    {
        private readonly IOptions<AdminConfiguration> _config;
        private readonly byte[] _key;

        public AdminService(
            IOptions<AdminConfiguration> config,
            IOptions<JwtConfiguration> jwtConfig)
        {
            _config = config;

            _key = Encoding.ASCII.GetBytes(jwtConfig.Value.JwtSigningKey);
        }

        public Maybe<TokenResponse> Authenticate(string key)
        {
            if (key == _config.Value.AdminKey)
            {
                var jwtHandler = new JwtSecurityTokenHandler();
                var expiration = DateTime.UtcNow.AddHours(1);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Expires = expiration,
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(_key),
                        SecurityAlgorithms.HmacSha256Signature),
                    Claims = new Dictionary<string, object>
                    {
                        { AdminClaims.AdminClaim, null }
                    }
                };

                var jwt = jwtHandler.CreateToken(tokenDescriptor);
                var token = jwtHandler.WriteToken(jwt);
                return new TokenResponse(token, expiration).ToMaybe();
            }

            return Maybe<TokenResponse>.Nothing;
        }
    }
}
