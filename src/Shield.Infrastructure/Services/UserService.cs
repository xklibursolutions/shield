using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using XkliburSolutions.Shield.Core.DTOs;
using XkliburSolutions.Shield.Core.Entities;
using XkliburSolutions.Shield.Core.Enums;
using XkliburSolutions.Shield.Core.Models.Input;
using XkliburSolutions.Shield.Core.Services;
using XkliburSolutions.Shield.Infrastructure.Configurations;
using XkliburSolutions.Shield.Infrastructure.Resources;
using XkliburSolutions.Shield.Infrastructure.Security;

namespace XkliburSolutions.Shield.Infrastructure.Services;

/// <summary>
/// Provides services related to user management, including registration and email validation.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="UserService"/> class.
/// </remarks>
/// <param name="userManager">The user manager.</param>
/// <param name="applicationSettings">The application settings.</param>
/// <param name="communicationService">The communication service.</param>
/// <param name="localizer">The string localizer.</param>
/// <param name="templateService">The template service.</param>
/// <param name="signInManager">The sign in manager.</param>
/// <param name="configuration">The configuration options.</param>
public class UserService(
    UserManager<ApplicationUser> userManager,
    IOptions<ApplicationSettings> applicationSettings,
    ICommunicationService communicationService,
    IStringLocalizer<UserServiceResource> localizer,
    TemplateService templateService,
    SignInManager<ApplicationUser> signInManager,
    IConfiguration configuration) : IUserService
{
    /// <inheritdoc/>
    public async Task<IdentityResult> RegisterUserAsync(RegisterInputModel model)
    {
        ApplicationUser user = new()
        {
            UserName = model.UserName,
            Email = model.Email,
            Status = UserStatus.Active
        };

        IdentityResult result = await userManager.CreateAsync(user, model.Password!);

        return result;
    }

    /// <inheritdoc/>
    public bool RequireConfirmedAccount()
    {
        return userManager.Options.SignIn.RequireConfirmedAccount;
    }

    /// <inheritdoc/>
    public async Task<RegisterDto> GenerateConfirmationEmailAsync(RegisterInputModel model)
    {
        ApplicationUser? user = await userManager.FindByNameAsync(model.UserName!);
        string userId = await userManager.GetUserIdAsync(user!);
        string code = await userManager.GenerateEmailConfirmationTokenAsync(user!);
        string encodedCode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        RegisterDto outputDto = new()
        {
            UserId = userId,
            ValidationCode = encodedCode,
            RequiredConfirmedAccount = true,
            DisplayConfirmAccountLink = false
        };

        if (userManager.SupportsUserEmail && !string.IsNullOrEmpty(user!.Email))
        {
            string from = applicationSettings.Value.FromEmailAddress;
            string webAppBaseUrl = applicationSettings.Value.WebAppBaseUrl!;

            UriBuilder builder = new(webAppBaseUrl)
            {
                Path = "/Account/ConfirmEmail",
                Query = $"userId={userId}&code={encodedCode}"
            };

            ConfirmEmailDto confirmationEmailModel = new()
            {
                UserName = user.UserName!,
                ConfirmationLink = builder.ToString()
            };

            string emailBody = await templateService.RenderTemplateAsync<ConfirmEmailDto>(
                "/Registration/ConfirmationEmail",
                confirmationEmailModel);

            communicationService.SendEmail(
                localizer["RegisterEndpoints.Email.Subject"],
                emailBody,
                from,
                user.Email);
        }

#if DEBUG
        outputDto.DisplayConfirmAccountLink = true;
#endif

        return outputDto;
    }

    /// <inheritdoc/>
    public async Task<IdentityResult> ValidateEmailAsync(ValidateEmailInputModel model)
    {
        ApplicationUser? user = await userManager.FindByIdAsync(model.UserId);
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        }

        string code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.ValidationCode));
        IdentityResult result = await userManager.ConfirmEmailAsync(user, code);

        return result;
    }

    /// <inheritdoc/>
    public async Task<(IdentityResult Result, string Token)> LoginAsync(LoginInputModel model)
    {
        ApplicationUser? user = await userManager.FindByNameAsync(model.UserName!);

        if (user == null)
        {
            return (IdentityResult.Failed(new IdentityError { Description = "User not found." }), string.Empty);
        }

        SignInResult result = await signInManager.PasswordSignInAsync(
            user.UserName!,
            model.Password!,
            model.RememberMe,
            lockoutOnFailure: true);

        if (result.Succeeded && !result.IsLockedOut)
        {
            string token = new JwtTokenGenerator(configuration)
                .GenerateJwtToken(user.UserName!);

            return (IdentityResult.Success, token);
        }

        return (IdentityResult.Failed(new IdentityError { Description = "Invalid login attempt." }), string.Empty);
    }

    /// <inheritdoc/>
    public async Task<UserDto?> GetLoggedUserInfoAsync(string username)
    {
        if (username == null)
        {
            return null;
        }

        ApplicationUser? user = await userManager.FindByNameAsync(username);
        if (user == null)
        {
            return null;
        }

        return new UserDto
        {
            UserName = user.UserName,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            DateOfBirth = user.DateOfBirth,
            ProfilePictureUrl = user.ProfilePictureUrl
        };
    }
}
