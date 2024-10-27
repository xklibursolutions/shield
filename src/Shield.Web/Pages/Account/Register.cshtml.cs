using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XkliburSolutions.Shield.Infrastructure.Services;
using XkliburSolutions.Shield.CrossCutting.DTOs;
using Microsoft.FeatureManagement.Mvc;

namespace XkliburSolutions.Shield.Web.Pages.Account;

/// <summary>
/// Represents the model for the registration page.
/// </summary>
[FeatureGate("Registration")]
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
            RegisterOutputModel? response = await authenticationService.RegisterAsync(Input);

            if (response != null)
            {
                returnUrl ??= "/Account/Login";

                if (response.RequiredConfirmedAccount)
                {
                    if (response.DisplayConfirmAccountLink)
                    {
                        string? callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new {
                            userId = response.UserId!,
                            code = response.ValidationCode!,
                            returnUrl = returnUrl },
                        protocol: Request.Scheme);

                        return RedirectToPage("/Account/ConfirmEmail", new { displayConfirmAccountLink = true, callbackUrl });
                    }
                    else
                    {
                        return RedirectToPage("/Account/ConfirmEmail");
                    }
                }
                else
                {
                    return RedirectToPage("/Account/Login", new { confirmAccount = true });
                }
            }
        }

        //TODO: Localization
        ModelState.AddModelError(string.Empty, "Invalid login attempt.");

        return Page();
    }
}
