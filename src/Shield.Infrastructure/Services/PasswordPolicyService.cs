using Microsoft.AspNetCore.Identity;

namespace XkliburSolutions.Shield.Infrastructure.Services;

/// <summary>
/// Provides methods for configuring password policies.
/// </summary>
public static class PasswordPolicyService
{
    /// <summary>
    /// Configures the password options with specific security requirements.
    /// </summary>
    /// <param name="options">The PasswordOptions to configure.</param>
    public static void ConfigurePasswordOptions(PasswordOptions options)
    {
        options.RequireDigit = true;
        options.RequiredLength = 12;
        options.RequireNonAlphanumeric = true;
        options.RequireUppercase = true;
        options.RequireLowercase = true;
        options.RequiredUniqueChars = 4;
    }
}
