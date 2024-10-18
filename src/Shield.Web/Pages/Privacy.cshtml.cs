using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace XkliburSolutions.Shield.Web.Pages;

/// <summary>
/// Represents the model for the privacy page.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="PrivacyModel"/> class.
/// </remarks>
/// <param name="logger">The logger instance.</param>
public class PrivacyModel(ILogger<PrivacyModel> logger) : PageModel
{
    private readonly ILogger<PrivacyModel> _logger = logger;

    /// <summary>
    /// Handles GET requests.
    /// </summary>
    public void OnGet()
    {
        // This method is called on GET requests.
    }
}
