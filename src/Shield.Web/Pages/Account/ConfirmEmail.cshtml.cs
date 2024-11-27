using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.FeatureManagement.Mvc;
using XkliburSolutions.Shield.Core.Models.Input;
using XkliburSolutions.Shield.Core.Services;

namespace XkliburSolutions.Shield.Web.Pages.Account;

/// <summary>
/// Page model for confirming a user's account.
/// </summary>
[FeatureGate("Registration")]
public class ConfirmEmailModel(IAuthenticationService authenticationService) : PageModel
{
    /// <summary>
    /// Gets or sets the callback url for email validation.
    /// </summary>
    public string? CallbackUrl { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether a confirm account like should be displayed.
    /// </summary>
    public bool DisplayConfirmAccountLink { get; set; } = false;

    /// <summary>
    /// Handles the GET request to confirm a user's account.
    /// </summary>
    /// <param name="userId">The ID of the user to confirm.</param>
    /// <param name="code">The confirmation code.</param>
    /// <param name="callbackUrl">The validation callback url.</param>
    /// <param name="displayConfirmAccountLink">A value indicating whether the confirm link is displayed.</param>
    /// <returns>A redirect to the login page with a status message.</returns>
    public async Task<IActionResult> OnGetAsync(string? userId, string? code, string? callbackUrl, bool? displayConfirmAccountLink)
    {
        if (displayConfirmAccountLink.HasValue && displayConfirmAccountLink.Value && !string.IsNullOrEmpty(callbackUrl))
        {
            CallbackUrl = callbackUrl;
            DisplayConfirmAccountLink = displayConfirmAccountLink.Value;

            return Page();
        }

        if (userId == null || code == null)
        {
            DisplayConfirmAccountLink = false;

            return Page();
        }

        bool result = await ConfirmAccount(userId, code);

        return RedirectToPage("/Account/Login", new { isAccountConfirmed = result });
    }

    private async Task<bool> ConfirmAccount(string userId, string code)
    {
        ValidateEmailInputModel input = new()
        {
            UserId = userId,
            ValidationCode = code
        };

        return await authenticationService.ValidateEmailAsync(input);
    }
}
