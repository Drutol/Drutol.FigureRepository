using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Api.Models.Configuration;
using Drutol.FigureRepository.Shared.Blockchain;
using Drutol.FigureRepository.Shared.Models.Auth;
using Drutol.FigureRepository.Shared.Models.Figure;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Drutol.FigureRepository.Api.Infrastructure.Downloads;

public class DownloadTokenManager : IDownloadTokenManager
{
    private readonly ILogger<DownloadTokenManager> _logger;
    private readonly IOptions<JwtConfiguration> _configuration;
    private readonly JwtSecurityTokenHandler _validator;
    private readonly TokenValidationParameters _validationParameters;

    private readonly byte[] _key;

    public DownloadTokenManager(
        ILogger<DownloadTokenManager> logger,
        IOptions<JwtConfiguration> configuration)
    {
        _logger = logger;
        _configuration = configuration;

        _key = Encoding.ASCII.GetBytes(_configuration.Value.JwtSigningKey);;
        _validator = new JwtSecurityTokenHandler();
        _validationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(_key),
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidateAudience = false,
            ValidateIssuer = false
        };
    }

    public TokenResponse GenerateDownloadTokenForFigure(Figure figure)
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
                {FigureClaims.FigureOwnerClaim, figure.Guid.ToString("N")}
            }
        };

        var jwt = jwtHandler.CreateToken(tokenDescriptor);
        return new(jwtHandler.WriteToken(jwt), expiration);
    }

    public bool ValidateToken(Guid figureGuid, string token)
    {
        try
        {
            var principal = _validator.ValidateToken(token, _validationParameters, out _);
            return principal.HasClaim(FigureClaims.FigureOwnerClaim, figureGuid.ToString("N"));
        }
        catch (Exception e)
        {
            return false;
        }
    }
}