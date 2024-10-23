using Microsoft.AspNetCore.Http;

namespace XkliburSolutions.Shield.CrossCutting.Services;

/// <summary>
/// Interface for claims services.
/// </summary>
public interface IClaimsService
{
    /// <summary>
    /// Signs in a user by setting the necessary claims in the HTTP context.
    /// </summary>
    /// <param name="httpContext">The HTTP context of the current request.</param>
    /// <param name="userName">The username of the user to sign in.</param>
    /// <param name="token">The authentication token for the user.</param>
    /// <param name="isPersistent">Define if the sign in is persistent.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SignInAsync(HttpContext httpContext, string userName, string token, bool isPersistent = false);

    /// <summary>
    /// Signs out a user by clearing the existing external cookie.
    /// </summary>
    /// <param name="httpContext">The HTTP context of the current request.</param>
    /// <param name="schemes">The authentication schemes to sign out from.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SignOutAsync(HttpContext httpContext, string? schemes);
}
