using System.ComponentModel.DataAnnotations;

namespace XkliburSolutions.Shield.Core.Models.Input;

/// <summary>
/// Represents the input model for user login.
/// </summary>
public class LoginInputModel
{
    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    [Required]
    [Display(Name = "Email or username")]
    public string? UserName { get; set; }

    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string? Password { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user wants to be remembered on the device.
    /// </summary>
    [Display(Name = "Remember me")]
    public bool RememberMe { get; set; } = false;
}
