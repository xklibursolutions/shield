using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XkliburSolutions.Shield.Core.Models.Input;
using XkliburSolutions.Shield.Core.Models.Output;
using XkliburSolutions.Shield.Core.Services;
using XkliburSolutions.Shield.CrossCutting.Services;

namespace XkliburSolutions.Shield.Web.Pages.Account;

/// <summary>
/// Page model for the login page.
/// </summary>
public class LoginModel(IAuthenticationService authenticationService) : PageModel
{
    /// <summary>
    /// Gets or sets the logged out state.
    /// </summary>
    public bool? IsLoggedOut { get; set; } = null;

    /// <summary>
    /// Gets or sets whether the account is confirmed or not.
    /// </summary>
    public bool? IsAccountConfirmed { get; set; } = null;

    /// <summary>
    /// Gets or sets the login input model.
    /// </summary>
    [BindProperty]
    public LoginInputModel Input { get; set; } = new();

    /// <summary>
    /// Gets or sets the return URL.
    /// </summary>
    public string? ReturnUrl { get; set; }

    /// <summary>
    /// Handles GET requests to the login page.
    /// </summary>
    /// <param name="isAccountConfirmed">Gets or sets a value indicating whether the account is confirmed.</param>
    /// <param name="logout">The logout action is checked.</param>
    /// <param name="returnUrl">The return URL after a successful login.</param>
    public void OnGetAsync(bool? isAccountConfirmed = null, bool? logout = null, string? returnUrl = null)
    {
        if (logout.HasValue && logout.Value)
        {
            IsLoggedOut = true;
        }

        if (isAccountConfirmed.HasValue)
        {
            IsAccountConfirmed = isAccountConfirmed.Value;
        }

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
            LoginOutputModel? output = await authenticationService.AuthenticateAsync(Input);

            if (output != null && output.Data != null)
            {
                await ClaimsService.SignInAsync(HttpContext, Input.UserName!, output.Data.Token, Input.RememberMe);
                return LocalRedirect(returnUrl ?? "/");
            }
        }
        //TODO: Localization
        ModelState.AddModelError(string.Empty, "Invalid login attempt.");

        return Page();
    }
}
