using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;

namespace XkliburSolutions.Shield.CrossCutting.Configuration.Extensions;

/// <summary>
/// Provides extension methods for configuring custom authorization policies in the service collection.
/// </summary>
public static class AuthorizationConfigurationExtensions
{
    /// <summary>
    /// Adds custom authorization policies to the service collection, setting a default policy
    /// that requires authenticated users. If no authentication schemes are provided, it defaults
    /// to using JWT Bearer authentication.
    /// </summary>
    /// <param name="serviceCollection">The service collection to add the authorization policies to.</param>
    /// <param name="authenticationSchemes">The authentication schemes to use for the default policy.</param>
    /// <returns>The updated service collection with the custom authorization policies added.</returns>
    public static IServiceCollection AddCustomAuthorization(
        this IServiceCollection serviceCollection,
        params string[] authenticationSchemes)
    {
        if (authenticationSchemes.Length == 0)
        {
            authenticationSchemes = [JwtBearerDefaults.AuthenticationScheme];
        }

        return serviceCollection.AddAuthorization(options =>
        {
            AuthorizationPolicy defaultPolicy = new AuthorizationPolicyBuilder(authenticationSchemes)
                .RequireAuthenticatedUser()
                .Build();
            options.DefaultPolicy = defaultPolicy;
        });
    }
}
