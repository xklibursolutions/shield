using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace XkliburSolutions.Shield.Web.Pages;

/// <summary>
/// Represents the model for the index page.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="IndexModel"/> class.
/// </remarks>
[Authorize]
public class IndexModel() : PageModel
{

    /// <summary>
    /// Handles GET requests.
    /// </summary>
    public IActionResult OnGet()
    {
        return RedirectToPage("/Account/Overview");
    }
}
