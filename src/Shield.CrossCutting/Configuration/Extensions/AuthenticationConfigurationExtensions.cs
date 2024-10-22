using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace XkliburSolutions.Shield.CrossCutting.Configuration.Extensions;

/// <summary>
/// Provides extension methods for configuring authentication services.
/// </summary>
public static class AuthenticationConfigurationExtensions
{
    /// <summary>
    /// Adds authentication services with JWT Bearer support to the specified IServiceCollection.
    /// </summary>
    /// <param name="serviceCollection">The IServiceCollection to add services to.</param>
    /// <param name="configuration">The configuration manager to retrieve JWT settings from.</param>
    /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddAuthenticationConfiguration(
        this IServiceCollection serviceCollection,
        ConfigurationManager configuration)
    {
        AuthenticationOptions authenticationOptions = new()
        {
            DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme,
            DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme,
            DefaultScheme = JwtBearerDefaults.AuthenticationScheme,
        };

        JwtBearerOptions jwtBearerOptions = new()
        {
            SaveToken = true,
            RequireHttpsMetadata = false,
            TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = configuration["Jwt:Audience"],
                ValidIssuer = configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!))
            },
        };

        return AddAuthenticationConfiguration(serviceCollection, authenticationOptions, jwtBearerOptions);
    }

    /// <summary>
    /// Adds authentication services with the specified options to the IServiceCollection.
    /// </summary>
    /// <param name="serviceCollection">The IServiceCollection to add services to.</param>
    /// <param name="authenticationOptions">The authentication options to use.</param>
    /// <param name="jwtBearerOptions">The JWT Bearer options to use.</param>
    /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddAuthenticationConfiguration(
        this IServiceCollection serviceCollection,
        AuthenticationOptions authenticationOptions,
        JwtBearerOptions jwtBearerOptions)
    {
        serviceCollection
            .AddAuthentication(options => options = authenticationOptions)
            .AddJwtBearer(options => options = jwtBearerOptions);

        return serviceCollection;
    }
}
