using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using XkliburSolutions.Shield.CrossCutting.Configuration;
using XkliburSolutions.Shield.CrossCutting.Security;
using XkliburSolutions.Shield.Domain.Entities;
using XkliburSolutions.Shield.Infrastructure.Repositories;

namespace XkliburSolutions.Shield.Infrastructure.Identity;

/// <summary>
/// Provides extension methods for configuring identity services.
/// </summary>
public static class IdentityServiceExtensions
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
