namespace XkliburSolutions.Shield.Core.DTOs;

/// <summary>
/// Represents the data transfer object for email confirmation details.
/// </summary>
public class ConfirmEmailDto
{
    /// <summary>
    /// Gets or sets the user name.
    /// </summary>
    public string UserName { get; set; } = "";

    /// <summary>
    /// Gets or sets the confirmation link.
    /// </summary>
    public string ConfirmationLink { get; set; } = "";
}
