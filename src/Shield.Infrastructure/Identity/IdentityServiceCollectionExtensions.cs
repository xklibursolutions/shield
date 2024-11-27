using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using XkliburSolutions.Shield.Core.Entities;
using XkliburSolutions.Shield.Infrastructure.Configurations;
using XkliburSolutions.Shield.Infrastructure.Data;
using XkliburSolutions.Shield.Infrastructure.Services;

namespace XkliburSolutions.Shield.Infrastructure.Identity;

/// <summary>
/// Provides extension methods for configuring identity services.
/// </summary>
public static class IdentityServiceCollectionExtensions
{
    /// <summary>
    /// Adds custom identity services to the specified IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to which the identity services will be added.</param>
    /// <param name="registrationSettings">The registration settings options.</param>
    /// <returns>The IServiceCollection with the added identity services.</returns>
    public static IServiceCollection AddCustomIdentity(
        this IServiceCollection services,
        RegistrationSettings registrationSettings)
    {
        // Configure identity services with custom password options
        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            PasswordPolicyService.ConfigurePasswordOptions(options.Password);
            options.User.RequireUniqueEmail = true;

            //TODO: Must be customizable
            options.SignIn.RequireConfirmedAccount = registrationSettings.RequireConfirmation;
            options.SignIn.RequireConfirmedEmail = registrationSettings.RequireConfirmation;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>() // Use Entity Framework for storing identity data
        .AddDefaultTokenProviders(); // Add default token providers for password reset, email confirmation, etc.

        return services;
    }
}
