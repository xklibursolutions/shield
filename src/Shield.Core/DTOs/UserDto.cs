namespace XkliburSolutions.Shield.Core.DTOs;

/// <summary>
/// Represents a data transfer object for user information.
/// </summary>
public class UserDto
{
    /// <summary>
    /// Gets or sets the username.
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// Gets or sets the email address.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the first name.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Gets or sets the date of birth.
    /// </summary>
    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// Gets or sets the profile picture URL.
    /// </summary>
    public string? ProfilePictureUrl { get; set; }
}
