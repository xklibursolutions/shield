using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XkliburSolutions.Shield.Infrastructure.Services;
using System.Net;
using XkliburSolutions.Shield.CrossCutting.DTOs;

namespace XkliburSolutions.Shield.Web.Pages.Account;

/// <summary>
/// Represents the model for the registration page.
/// </summary>
public class RegisterModel(IAuthenticationService authenticationService) : PageModel
{
    /// <summary>
    /// Gets or sets the register input model.
    /// </summary>
    [BindProperty]
    public RegisterInputModel Input { get; set; } = new();

    /// <summary>
    /// Gets or sets the return URL.
    /// </summary>
    public string? ReturnUrl { get; set; } = null;

    /// <summary>
    /// Handles GET requests to the registration page.
    /// </summary>
    /// <param name="returnUrl">The return URL.</param>
    public void OnGet(string? returnUrl = null)
    {
        ReturnUrl = returnUrl;
    }

    /// <summary>
    /// Handles POST requests to the registration page.
    /// </summary>
    /// <param name="returnUrl">The return URL.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the action.</returns>
    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (Input != null)
        {
            HttpStatusCode response = await authenticationService.RegisterAsync(Input);

            if (response == HttpStatusCode.OK)
            {
                return LocalRedirect(returnUrl ?? "/Account/Login");
            }
        }

        //TODO: Localization
        ModelState.AddModelError(string.Empty, "Invalid login attempt.");

        return Page();
    }
}
