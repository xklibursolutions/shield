using XkliburSolutions.Shield.Core.Models.Input;
using XkliburSolutions.Shield.Core.Models.Output;

namespace XkliburSolutions.Shield.Core.Services;

/// <summary>
/// Interface for authentication services.
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// Authenticates a user based on the provided login input model.
    /// </summary>
    /// <param name="input">The login input model containing user credentials.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the authentication token if authentication is successful; otherwise, null.</returns>
    Task<LoginOutputModel?> AuthenticateAsync(LoginInputModel input);

    /// <summary>
    /// Registers a new user based on the provided registration input model.
    /// </summary>
    /// <param name="input">The registration input model containing user details.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the registration token if registration is successful; otherwise, null.</returns>
    Task<RegisterOutputModel?> RegisterAsync(RegisterInputModel input);


    /// <summary>
    /// Confirms a user's account using their user ID and confirmation code.
    /// </summary>
    /// <param name="input">The validate email input model containing user id and validation code.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the account confirmation was successful.</returns>
    Task<bool> ValidateEmailAsync(ValidateEmailInputModel input);
}
