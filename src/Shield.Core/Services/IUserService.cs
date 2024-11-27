using Microsoft.AspNetCore.Identity;
using XkliburSolutions.Shield.Core.DTOs;
using XkliburSolutions.Shield.Core.Models.Input;

namespace XkliburSolutions.Shield.Core.Services;

/// <summary>
/// Provides services related to user management, including registration and email validation.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Registers a new user asynchronously.
    /// </summary>
    /// <param name="model">The registration input model.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the identity result of the registration process.</returns>
    Task<IdentityResult> RegisterUserAsync(RegisterInputModel model);

    /// <summary>
    /// Determines whether the application requires confirmed accounts.
    /// </summary>
    /// <returns><c>true</c> if confirmed accounts are required; otherwise, <c>false</c>.</returns>
    bool RequireConfirmedAccount();

    /// <summary>
    /// Generates a confirmation email for the specified user.
    /// </summary>
    /// <param name="model">The registration input model.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the registration output model.</returns>
    Task<RegisterDto> GenerateConfirmationEmailAsync(RegisterInputModel model);

    /// <summary>
    /// Validates a user's email asynchronously.
    /// </summary>
    /// <param name="model">The email validation input model.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the identity result of the email validation process.</returns>
    Task<IdentityResult> ValidateEmailAsync(ValidateEmailInputModel model);

    /// <summary>
    /// Attempts to log in a user asynchronously and generates a JWT token if successful.
    /// </summary>
    /// <param name="model">The login input model containing the user's credentials.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a tuple with the identity result and the generated JWT token.</returns>
    Task<(IdentityResult Result, string Token)> LoginAsync(LoginInputModel model);

    /// <summary>
    /// Asynchronously retrieves the logged-in user's information based on their username.
    /// </summary>
    /// <param name="username">The username of the logged-in user.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the user data transfer object or null if the user is not found.</returns>
    Task<UserDto?> GetLoggedUserInfoAsync(string username);
}
