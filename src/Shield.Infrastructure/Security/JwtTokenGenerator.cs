using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace XkliburSolutions.Shield.Infrastructure.Security;

/// <summary>
/// Provides methods for generating JWT tokens.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="JwtTokenGenerator"/> class.
/// </remarks>
/// <param name="configuration">The configuration.</param>
public class JwtTokenGenerator(IConfiguration configuration)
{
    private readonly IConfiguration _configuration = configuration;

    /// <summary>
    /// Generates a JWT token for the specified user.
    /// </summary>
    /// <param name="userName">The user name for whom to generate the token.</param>
    /// <returns>The generated JWT token.</returns>
    public string GenerateJwtToken(string userName)
    {
        Claim[] claims =
        [
            new Claim(JwtRegisteredClaimNames.Sub, userName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        ];

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!));
        SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor1 = new SecurityTokenDescriptor
        {
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(90),
            SigningCredentials = creds
        };

        SecurityToken tokenObject1 = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor1);
        return new JwtSecurityTokenHandler().WriteToken(tokenObject1);
    }
}
