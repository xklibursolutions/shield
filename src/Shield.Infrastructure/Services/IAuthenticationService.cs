using System.Net;
using XkliburSolutions.Shield.CrossCutting.DTOs;

namespace XkliburSolutions.Shield.Infrastructure.Services;

/// <summary>
/// Interface for authentication services.
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// Authenticates a user based on the provided login input model.
    /// </summary>
    /// <param name="input">The login input model containing user credentials.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the authentication token if authentication is successful; otherwise, null.</returns>
    Task<string?> AuthenticateAsync(LoginInputModel input);

    /// <summary>
    /// Registers a new user based on the provided registration input model.
    /// </summary>
    /// <param name="input">The registration input model containing user details.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the registration token if registration is successful; otherwise, null.</returns>
    Task<HttpStatusCode> RegisterAsync(RegisterInputModel input);

}
