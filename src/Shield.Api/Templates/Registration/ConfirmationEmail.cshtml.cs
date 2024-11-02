using Microsoft.AspNetCore.Mvc.RazorPages;

namespace XkliburSolutions.Shield.Api.Templates.Registration;

/// <summary>
/// Represents the model for the confirmation email page.
/// </summary>
public class ConfirmationEmailModel : PageModel
{
    /// <summary>
    /// Gets or sets the user name.
    /// </summary>
    public string UserName { get; set; } = "";

    /// <summary>
    /// Gets or sets the confirmation link.
    /// </summary>
    public string ConfirmationLink { get; set; } = "";

    /// <summary>
    /// Handles GET requests for the confirmation email page.
    /// </summary>
    public void OnGet()
    {
    }
}
