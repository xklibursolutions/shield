using System.Text.Json;
using System.Text;
using System.Net.Http.Json;
using XkliburSolutions.Shield.Core.Models.Input;
using XkliburSolutions.Shield.Core.Models.Output;
using XkliburSolutions.Shield.Core.Services;

namespace XkliburSolutions.Shield.CrossCutting.Services;

/// <summary>
/// Implementation of the authentication service interface.
/// </summary>
public class AuthenticationService(HttpClient httpClient) : IAuthenticationService
{
    private readonly HttpClient _httpClient = httpClient;

    /// <inheritdoc/>
    public async Task<LoginOutputModel?> AuthenticateAsync(LoginInputModel input)
    {
        StringContent content = new(
            JsonSerializer.Serialize(input),
            Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync(
            "https://localhost:7064/api/v1/account/login",
            content);

        if (response.IsSuccessStatusCode && response.Content != null)
        {
            return await response.Content.ReadFromJsonAsync<LoginOutputModel>();
        }

        return null;
    }

    /// <inheritdoc/>
    public async Task<bool> ValidateEmailAsync(ValidateEmailInputModel input)
    {
        StringContent content = new(
            JsonSerializer.Serialize(input),
            Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync(
            "https://localhost:7064/api/v1/account/validate",
            content);

        return response.IsSuccessStatusCode;
    }

    /// <inheritdoc/>
    public async Task<RegisterOutputModel?> RegisterAsync(RegisterInputModel input)
    {
        StringContent content = new(
            JsonSerializer.Serialize(input),
            Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync(
            "https://localhost:7064/api/v1/account/register",
            content);

        if (response.IsSuccessStatusCode && response.Content != null)
        {
            return await response.Content.ReadFromJsonAsync<RegisterOutputModel>();
        }

        return null;
    }
}
