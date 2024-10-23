using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace XkliburSolutions.Shield.Web.Pages;

/// <summary>
/// Represents the model for the index page.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="IndexModel"/> class.
/// </remarks>
/// <param name="logger">The logger instance.</param>
[Authorize]
public class IndexModel(ILogger<IndexModel> logger) : PageModel
{
    private readonly ILogger<IndexModel> _logger = logger;

    /// <summary>
    /// Handles GET requests.
    /// </summary>
    public void OnGet()
    {
        // This method is called on GET requests.
    }
}
