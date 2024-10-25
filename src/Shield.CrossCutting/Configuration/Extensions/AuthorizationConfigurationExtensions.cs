using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using XkliburSolutions.Shield.CrossCutting.Security;

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

            AddPolicies(options);
        });
    }

    /// <summary>
    /// Adds authorization policies to the specified <see cref="AuthorizationOptions"/>.
    /// </summary>
    /// <param name="options">The <see cref="AuthorizationOptions"/> to add policies to.</param>
    public static void AddPolicies(AuthorizationOptions options)
    {
        options.AddPolicy(Policies.UserManagement.Create,
                policy => policy.RequireClaim(Permissions.ClaimType, Policies.UserManagement.Create));
        options.AddPolicy(Policies.UserManagement.Read,
            policy => policy.RequireClaim(Permissions.ClaimType, Policies.UserManagement.Read));
        options.AddPolicy(Policies.UserManagement.Update,
            policy => policy.RequireClaim(Permissions.ClaimType, Policies.UserManagement.Update));
        options.AddPolicy(Policies.UserManagement.Delete,
            policy => policy.RequireClaim(Permissions.ClaimType, Policies.UserManagement.Delete));
        options.AddPolicy(Policies.UserManagement.ManageRoles,
            policy => policy.RequireClaim(Permissions.ClaimType, Policies.UserManagement.ManageRoles));
        options.AddPolicy(Policies.UserManagement.ManageClaims,
            policy => policy.RequireClaim(Permissions.ClaimType, Policies.UserManagement.ManageClaims));
        options.AddPolicy(Policies.UserManagement.Lock,
            policy => policy.RequireClaim(Permissions.ClaimType, Policies.UserManagement.Lock));
        options.AddPolicy(Policies.UserManagement.Unlock,
            policy => policy.RequireClaim(Permissions.ClaimType, Policies.UserManagement.Unlock));
        options.AddPolicy(Policies.UserManagement.ResetPassword,
            policy => policy.RequireClaim(Permissions.ClaimType, Policies.UserManagement.ResetPassword));

        options.AddPolicy(Policies.RoleManagement.Create,
            policy => policy.RequireClaim(Permissions.ClaimType, Policies.RoleManagement.Create));
        options.AddPolicy(Policies.RoleManagement.Read,
            policy => policy.RequireClaim(Permissions.ClaimType, Policies.RoleManagement.Read));
        options.AddPolicy(Policies.RoleManagement.Update,
            policy => policy.RequireClaim(Permissions.ClaimType, Policies.RoleManagement.Update));
        options.AddPolicy(Policies.RoleManagement.Delete,
            policy => policy.RequireClaim(Permissions.ClaimType, Policies.RoleManagement.Delete));
        options.AddPolicy(Policies.RoleManagement.ManageClaims,
            policy => policy.RequireClaim(Permissions.ClaimType, Policies.RoleManagement.ManageClaims));

        options.AddPolicy(Policies.Security.EnableTwoFactorAuthentication,
            policy => policy.RequireClaim(Permissions.ClaimType, Policies.Security.EnableTwoFactorAuthentication));
        options.AddPolicy(Policies.Security.DisableTwoFactorAuthentication,
            policy => policy.RequireClaim(Permissions.ClaimType, Policies.Security.DisableTwoFactorAuthentication));
        options.AddPolicy(Policies.Security.ViewLoginHistory,
            policy => policy.RequireClaim(Permissions.ClaimType, Policies.Security.ViewLoginHistory));

        options.AddPolicy(Policies.AccessControl.Grant,
            policy => policy.RequireClaim(Permissions.ClaimType, Policies.AccessControl.Grant));
        options.AddPolicy(Policies.AccessControl.Revoke,
            policy => policy.RequireClaim(Permissions.ClaimType, Policies.AccessControl.Revoke));
        options.AddPolicy(Policies.AccessControl.ViewLogs,
            policy => policy.RequireClaim(Permissions.ClaimType, Policies.AccessControl.ViewLogs));

        options.AddPolicy(Policies.ApplicationSettings.View,
            policy => policy.RequireClaim(Permissions.ClaimType, Policies.ApplicationSettings.View));
        options.AddPolicy(Policies.ApplicationSettings.Update,
            policy => policy.RequireClaim(Permissions.ClaimType, Policies.ApplicationSettings.Update));
        options.AddPolicy(Policies.ApplicationSettings.ManageAPIKeys,
            policy => policy.RequireClaim(Permissions.ClaimType, Policies.ApplicationSettings.ManageAPIKeys));

        options.AddPolicy(Policies.Analytics.View,
            policy => policy.RequireClaim(Permissions.ClaimType, Policies.Analytics.View));
        options.AddPolicy(Policies.Analytics.Generate,
            policy => policy.RequireClaim(Permissions.ClaimType, Policies.Analytics.Generate));
        options.AddPolicy(Policies.Analytics.Export,
            policy => policy.RequireClaim(Permissions.ClaimType, Policies.Analytics.Export));
    }
}
