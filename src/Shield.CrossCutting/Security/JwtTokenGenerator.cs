using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace XkliburSolutions.Shield.CrossCutting.Security;

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

        JwtSecurityToken token = new(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
