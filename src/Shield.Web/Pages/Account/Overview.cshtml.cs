using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace XkliburSolutions.Shield.Web.Pages.Account;

/// <summary>
/// Represents the model for the account overview page, requiring authorization.
/// </summary>
[Authorize]
public class OverviewModel() : PageModel
{
    /// <summary>
    /// Handles GET requests to the account overview page.
    /// </summary>
    public void OnGet()
    {
        //var user = httpContextAccessor.HttpContext.User;
    }
}
