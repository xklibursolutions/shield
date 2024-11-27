using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using XkliburSolutions.Shield.Core.Models.Output;

namespace Shield.IntegrationTests.Api.Me;

public class AccountIntegrationTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task LoginAndGetStatus_ShouldReturnOk()
    {
        // Arrange
        var loginRequest = new
        {
            userName = "admin",
            password = "admin",
            rememberMe = true
        };

        var loginContent = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");

        // Act - Login
        var loginResponse = await _client.PostAsync("/api/v1/account/login", loginContent);
        loginResponse.EnsureSuccessStatusCode();
        var loginResponseBody = await loginResponse.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var loginResult = JsonSerializer.Deserialize<LoginOutputModel>(loginResponseBody, options);

        // Assert - Login
        Assert.NotNull(loginResult);
        Assert.NotNull(loginResult.Data);
        Assert.NotNull(loginResult.Data!.Token);

        // Act - Get Status
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResult.Data.Token);
        var statusResponse = await _client.GetAsync("/api/v1/account/me");
        statusResponse.EnsureSuccessStatusCode();

        // Assert - Get Status
        Assert.Equal(System.Net.HttpStatusCode.OK, statusResponse.StatusCode);
    }
}
