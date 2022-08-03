using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Api.Models.Configuration;
using Drutol.FigureRepository.Shared.Blockchain;
using Drutol.FigureRepository.Shared.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Drutol.FigureRepository.Api.Infrastructure;

public class DownloadTokenGenerator : IDownloadTokenGenerator
{
    private readonly IOptions<BlockchainAuthConfig> _configuration;

    public DownloadTokenGenerator(IOptions<BlockchainAuthConfig> configuration)
    {
        _configuration = configuration;
    }

    public string GenerateDownloadTokenForFigure(Figure figure)
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration.Value.JwtSigningKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
            Claims = new Dictionary<string, object>
            {
                {BlockChainAuthClaims.FigureOwnerClaim, figure.Guid}
            }
        };

        var jwt = jwtHandler.CreateToken(tokenDescriptor);
        return jwtHandler.WriteToken(jwt);
    }
}