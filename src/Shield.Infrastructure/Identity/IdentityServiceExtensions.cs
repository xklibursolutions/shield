using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using XkliburSolutions.Shield.CrossCutting.Entities;
using XkliburSolutions.Shield.CrossCutting.Security;
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
    /// <returns>The IServiceCollection with the added identity services.</returns>
    public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
    {
        // Configure identity services with custom password options
        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            PasswordPolicyService.ConfigurePasswordOptions(options.Password);
        })
        .AddEntityFrameworkStores<ApplicationDbContext>() // Use Entity Framework for storing identity data
        .AddDefaultTokenProviders(); // Add default token providers for password reset, email confirmation, etc.

        return services;
    }
}
