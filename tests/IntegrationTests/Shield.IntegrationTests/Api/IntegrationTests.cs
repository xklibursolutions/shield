using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using XkliburSolutions.Shield.Core.Models.Input;
using XkliburSolutions.Shield.Core.Models.Output;

namespace Shield.IntegrationTests.Api;

public class IntegrationTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private async Task<LoginOutputModel?> LoginAsync(string userName, string password)
    {
        LoginInputModel loginRequest = new()
        {
            UserName = userName,
            Password = password,
            RememberMe = true,
        };

        StringContent loginContent = new(
            JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");
        HttpResponseMessage loginResponse =
            await _client.PostAsync("/api/v1/account/login", loginContent);
        loginResponse.EnsureSuccessStatusCode();
        string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<LoginOutputModel>(loginResponseBody, _options);
    }

    private async Task<RegisterOutputModel?> RegisterAsync(string userName, string email, string password)
    {
        RegisterInputModel registerRequest = new()
        {
            UserName = userName,
            Email = email,
            Password = password,
        };

        StringContent registerContent = new(
            JsonSerializer.Serialize(registerRequest), Encoding.UTF8, "application/json");
        HttpResponseMessage registerResponse =
            await _client.PostAsync("/api/v1/account/register", registerContent);
        registerResponse.EnsureSuccessStatusCode();
        string registerResponseBody = await registerResponse.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<RegisterOutputModel>(registerResponseBody, _options);
    }

    private async Task ValidateEmailAsync(string userId, string validationCode)
    {
        ValidateEmailInputModel validateEmailRequest = new()
        {
            UserId = userId,
            ValidationCode = validationCode,
        };

        StringContent validateEmailContent = new(
            JsonSerializer.Serialize(validateEmailRequest), Encoding.UTF8, "application/json");
        HttpResponseMessage validateEmailResponse =
            await _client.PostAsync("/api/v1/account/validate", validateEmailContent);
        validateEmailResponse.EnsureSuccessStatusCode();
    }

    private async Task<HttpResponseMessage> GetUserInfoAsync(string token)
    {
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
        return await _client.GetAsync("/api/v1/account/me");
    }

    [Fact]
    public async Task RegisterAndGetUserInfo_ShouldReturnOk()
    {
        // Act - Register
        RegisterOutputModel? registerResult =
            await RegisterAsync("newuser", "opencode@xklibursolutions.io", "Password123!");

        // Assert - Register
        Assert.NotNull(registerResult);
        Assert.NotNull(registerResult!.Data);

        // Act - Validate Email
        await ValidateEmailAsync(
            registerResult.Data.UserId!, registerResult.Data.ValidationCode!);

        // Act - Login
        LoginOutputModel? loginResult = await LoginAsync("newuser", "Password123!");

        // Assert - Login
        Assert.NotNull(loginResult);
        Assert.NotNull(loginResult!.Data);
        Assert.NotNull(loginResult.Data!.Token);

        // Act - Get Status
        HttpResponseMessage statusResponse =
            await GetUserInfoAsync(loginResult.Data.Token);

        // Assert - Get Status
        Assert.Equal(System.Net.HttpStatusCode.OK, statusResponse.StatusCode);
    }
}
