using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XkliburSolutions.Shield.CrossCutting.Services;

namespace XkliburSolutions.Shield.Web.Pages.Account;

/// <summary>
/// Represents the model for the logout page.
/// </summary>
public class LogoutModel() : PageModel
{
    /// <summary>
    /// Handles GET requests to log out the user.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task<IActionResult> OnGetAsync()
    {
        await ClaimsService.SignOutAsync(HttpContext, CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToPage("/Account/Login", new { logout = true });
    }
}
