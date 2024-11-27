using Microsoft.Extensions.Configuration;
using Moq;
using XkliburSolutions.Shield.Infrastructure.Security;
using System.IdentityModel.Tokens.Jwt;

namespace Shield.Infrastructure.Tests.Security;

public class JwtTokenGeneratorTests
{
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly JwtTokenGenerator _jwtTokenGenerator;

    public JwtTokenGeneratorTests()
    {
        _configurationMock = new Mock<IConfiguration>();

        // Setup the configuration mock to return specific values
        _configurationMock.Setup(config => config["Jwt:Secret"])
            .Returns("your-256-bit-secret-need-to-be-very-long");
        _configurationMock.Setup(config => config["Jwt:Issuer"])
            .Returns("your-issuer");
        _configurationMock.Setup(config => config["Jwt:Audience"])
            .Returns("your-audience");

        _jwtTokenGenerator = new JwtTokenGenerator(_configurationMock.Object);
    }

    [Fact]
    public void GenerateJwtToken_ShouldReturnToken()
    {
        // Arrange
        string userName = "testuser";

        // Act
        string token = _jwtTokenGenerator.GenerateJwtToken(userName);

        // Assert
        Assert.NotNull(token);
        Assert.IsType<string>(token);
    }

    [Fact]
    public void GenerateJwtToken_ShouldContainUserName()
    {
        // Arrange
        string userName = "testuser";

        // Act
        string token = _jwtTokenGenerator.GenerateJwtToken(userName);

        // Assert
        JwtSecurityTokenHandler handler = new();
        JwtSecurityToken jwtToken = handler.ReadJwtToken(token);

        Assert.Contains(
            jwtToken.Claims,
            claim => claim.Type == JwtRegisteredClaimNames.Sub && claim.Value == userName);
    }
}
