using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace XkliburSolutions.Shield.CrossCutting.Services;

/// <summary>
/// Implementation of the claims service.
/// </summary>
public class ClaimsService
{
    /// <summary>
    /// Signs in a user by setting the necessary claims in the HTTP context.
    /// </summary>
    /// <param name="httpContext">The HTTP context of the current request.</param>
    /// <param name="userName">The username of the user to sign in.</param>
    /// <param name="token">The authentication token for the user.</param>
    /// <param name="isPersistent">Define if the sign in is persistent.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static async Task SignInAsync(HttpContext httpContext, string userName, string token, bool isPersistent = false)
    {
        List<Claim> claims =
        [
            new(ClaimTypes.Name, userName),
            new("jwt", token)
        ];

        ClaimsIdentity claimsIdentity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        ClaimsPrincipal claimsPrincipal = new(claimsIdentity);

        AuthenticationProperties authProperties = new()
        {
            IsPersistent = isPersistent,
            IssuedUtc = DateTime.UtcNow,
            ExpiresUtc = DateTime.UtcNow.AddMinutes(30),
            AllowRefresh = true,
        };

        await httpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            claimsPrincipal,
            authProperties);
    }

    /// <summary>
    /// Signs out a user by clearing the existing external cookie.
    /// </summary>
    /// <param name="httpContext">The HTTP context of the current request.</param>
    /// <param name="schemes">The authentication schemes to sign out from.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static async Task SignOutAsync(HttpContext httpContext, string? schemes)
    {
        // Clear the existing external cookie to ensure a clean login process
        await httpContext.SignOutAsync(schemes);
    }
}
