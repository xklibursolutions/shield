using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XkliburSolutions.Shield.CrossCutting.Models;
using XkliburSolutions.Shield.CrossCutting.Services;
using XkliburSolutions.Shield.Infrastructure.Services;

namespace XkliburSolutions.Shield.Web.Pages.Account;

/// <summary>
/// Page model for the login page.
/// </summary>
public class LoginModel(IAuthenticationService authenticationService, IClaimsService claimsService) : PageModel
{
    /// <summary>
    /// Gets or sets the login input model.
    /// </summary>
    [BindProperty]
    public LoginInputModel? Input { get; set; }

    /// <summary>
    /// Gets or sets the return URL.
    /// </summary>
    public string? ReturnUrl { get; set; }

    /// <summary>
    /// Handles GET requests to the login page.
    /// </summary>
    /// <param name="returnUrl">The return URL after a successful login.</param>
    public async Task OnGetAsync(string? returnUrl = null)
    {
        await claimsService.SignOutAsync(HttpContext, CookieAuthenticationDefaults.AuthenticationScheme);
        ReturnUrl = returnUrl;
    }

    /// <summary>
    /// Handles POST requests to the login page.
    /// </summary>
    /// <param name="returnUrl">The return URL after a successful login.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the action result of the login attempt.</returns>
    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (Input != null)
        {
            string? token = await authenticationService.AuthenticateAsync(Input);

            if (token != null)
            {
                await claimsService.SignInAsync(HttpContext, Input.UserName, token, Input.RememberMe);
                return LocalRedirect(returnUrl ?? "/");
            }
        }
        //TODO: Localization
        ModelState.AddModelError(string.Empty, "Invalid login attempt.");

        return Page();
    }
}
