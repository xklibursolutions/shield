namespace XkliburSolutions.Shield.CrossCutting.Models;

/// <summary>
/// Represents the input model for user login.
/// </summary>
public class LoginInputModel
{
    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    public required string UserName { get; set; }

    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    public required string Password { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user wants to be remembered on the device.
    /// </summary>
    public required bool RememberMe { get; set; } = false;
}
