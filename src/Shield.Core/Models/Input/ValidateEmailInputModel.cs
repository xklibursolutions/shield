namespace XkliburSolutions.Shield.Core.Models.Input;

/// <summary>
/// Represents the input model for validating an email.
/// </summary>
public class ValidateEmailInputModel
{
    /// <summary>
    /// Gets or sets the user ID of the newly registered user, if available.
    /// </summary>
    public required string UserId { get; set; }

    /// <summary>
    /// Gets or sets the validation code for the registration, if applicable.
    /// </summary>
    public required string ValidationCode { get; set; }
}
