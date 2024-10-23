using System.Text.Json;
using System.Text;
using XkliburSolutions.Shield.CrossCutting.Models;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace XkliburSolutions.Shield.Infrastructure.Services;

/// <summary>
/// Implementation of the authentication service interface.
/// </summary>
public class AuthenticationService(HttpClient httpClient) : IAuthenticationService
{
    private readonly HttpClient _httpClient = httpClient;

    /// <summary>
    /// Authenticates a user based on the provided login input model.
    /// </summary>
    /// <param name="input">The login input model containing user credentials.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the authentication token if authentication is successful; otherwise, null.
    /// </returns>
    public async Task<string?> AuthenticateAsync(LoginInputModel input)
    {
        StringContent content = new(
            JsonSerializer.Serialize(input),
            Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync(
            "https://localhost:7064/api/v1/account/login",
            content);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }

        return null;
    }

    /// <summary>
    /// Registers a new user based on the provided registration input model.
    /// </summary>
    /// <param name="input">The registration input model containing user details.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the registration token if registration is successful; otherwise, null.</returns>
    public async Task<HttpStatusCode> RegisterAsync(RegisterInputModel input)
    {
        StringContent content = new(
            JsonSerializer.Serialize(input),
            Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync(
            "https://localhost:7064/api/v1/account/register",
            content);

        return response.StatusCode;
    }
}
