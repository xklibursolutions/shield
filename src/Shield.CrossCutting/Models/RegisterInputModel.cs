using System.ComponentModel.DataAnnotations;

namespace XkliburSolutions.Shield.CrossCutting.Models;

/// <summary>
/// Represents the input model for user registration.
/// </summary>
public class RegisterInputModel
{
    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    [Required]
    [Display(Name = "UserName")]
    public required string UserName { get; set; }

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    [EmailAddress]
    [Display(Name = "Email")]
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public required string Password { get; set; }

    /// <summary>
    /// Gets or sets the confirmation password of the user.
    /// </summary>
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public required string ConfirmPassword { get; set; }
}
