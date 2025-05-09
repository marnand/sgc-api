using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using sgc.Domain.Entities;
using sgc.Domain.Entities.Handlers;
using sgc.Domain.Interfaces.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace sgc.Infra.Security;

public class TokenHandlers(IOptions<TokenSettings> options) : ITokenHandlers
{
    private readonly TokenSettings _tokenSettings = options.Value;

    public string GenerateToken(string userId, string email, RoleEnum role)
    {
        var claims = new[]
        {
            new Claim("collaborator_id", userId),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, role.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new JwtSecurityToken(
            _tokenSettings.Issuer,
            _tokenSettings.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(_tokenSettings.ExpirationInMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

    /// <summary>
    /// Decodifica e valida o token JWT e retorna o ID e o Nome do usuário.
    /// </summary>
    /// <param name="token">Token JWT.</param>
    /// <returns>Um objeto contendo o ID e o Nome do usuário.</returns>
    /// <exception cref="SecurityTokenException">Se o token for inválido ou não puder ser decodificado.</exception>
    public ResultData<(string CollaboratorId, string CollaboratorEmail)> GetUserFromToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_tokenSettings.Secret);

            // Valida o token
            var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _tokenSettings.Issuer,
                ValidAudience = _tokenSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            }, out SecurityToken validatedToken);

            // Extrai as claims do token
            var collaboratorId = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "collaborator_id")?.Value;
            var collaboratorEmail = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(collaboratorId) || string.IsNullOrEmpty(collaboratorEmail))
            {
                var errorMessage = "As informações do usuário não foram encontradas no token.";
                return ResultData<(string CollaboratorId, string CollaboratorEmail)>.Failure(errorMessage, HttpStatusCode.Unauthorized);
            }

            return ResultData<(string CollaboratorId, string CollaboratorEmail)>.Success((collaboratorId, collaboratorEmail));
        }
        catch
        {
            var errorMessage = "O token é inválido ou não pôde ser decodificado.";
            return ResultData<(string CollaboratorId, string CollaboratorEmail)>.Failure(errorMessage, HttpStatusCode.Unauthorized);
        }
    }
}
