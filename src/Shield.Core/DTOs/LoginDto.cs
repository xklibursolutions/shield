namespace XkliburSolutions.Shield.Core.DTOs;

/// <summary>
/// Represents the data transfer object for login operations.
/// </summary>
public class LoginDto
{
    /// <summary>
    /// Gets or sets the authentication token.
    /// </summary>
    public required string Token { get; set; }
}
