using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace XkliburSolutions.Shield.Web.Pages.Account;

/// <summary>
/// Represents the security page of the user profile.
/// </summary>
[Authorize]
public class SecurityModel : PageModel
{
    /// <summary>
    /// Handles GET requests to the security page.
    /// </summary>
    public void OnGet()
    {
        // This method is called on GET requests.
    }
}
