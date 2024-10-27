namespace XkliburSolutions.Shield.CrossCutting.DTOs;

/// <summary>
/// Represents the output model for a registration process.
/// </summary>
public class RegisterOutputModel
{
    /// <summary>
    /// Gets or sets the user ID of the newly registered user, if available.
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// Gets or sets the validation code for the registration, if applicable.
    /// </summary>
    public string? ValidationCode { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether a confirmation is required.
    /// </summary>
    public bool RequiredConfirmedAccount { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether a confirmation link should be displayed.
    /// </summary>
    public bool DisplayConfirmAccountLink { get; set; }
}
