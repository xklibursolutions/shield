using Microsoft.AspNetCore.Identity;
using XkliburSolutions.Shield.Core.Enums;

namespace XkliburSolutions.Shield.Core.Entities;

/// <summary>
/// Represents a user in the application with properties and methods specific to the user's identity.
/// This class extends the IdentityUser class, adding application-specific properties and methods
/// that are necessary for the authentication and authorization processes within the application.
/// </summary>
public class ApplicationUser : IdentityUser<Guid>
{
    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Gets or sets the date of birth of the user.
    /// </summary>
    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// Gets or sets the URL of the user's profile picture.
    /// </summary>
    public string? ProfilePictureUrl { get; set; }

    /// <summary>
    /// Gets or sets an alternate email address for the user.
    /// </summary>
    public string? AlternateEmail { get; set; }

    /// <summary>
    /// Gets or sets the status of the user.
    /// </summary>
    public UserStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the addresses of the user.
    /// </summary>
    public List<Address> Addresses { get; set; } = [];
}
