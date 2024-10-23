using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
    /// <param name="authenticationOptions">Optional authentication options to use. Defaults to JWT Bearer if not provided.</param>
    /// <param name="cookieAuthenticationOptions">Optional cookie authentication options to use.</param>
    /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddCustomAuthentication(
        this IServiceCollection serviceCollection,
        ConfigurationManager configuration,
        AuthenticationOptions? authenticationOptions = null,
        CookieAuthenticationOptions? cookieAuthenticationOptions = null)
    {
        authenticationOptions ??= new()
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
            }
        };

        return AddCustomAuthentication(serviceCollection, authenticationOptions, jwtBearerOptions, cookieAuthenticationOptions);
    }

    /// <summary>
    /// Adds authentication services with the specified options to the IServiceCollection.
    /// </summary>
    /// <param name="serviceCollection">The IServiceCollection to add services to.</param>
    /// <param name="authenticationOptions">The authentication options to use.</param>
    /// <param name="jwtBearerOptions">Optional JWT Bearer options to use.</param>
    /// <param name="cookieAuthenticationOptions">Optional cookie authentication options to use.</param>
    /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddCustomAuthentication(
        this IServiceCollection serviceCollection,
        AuthenticationOptions authenticationOptions,
        JwtBearerOptions? jwtBearerOptions,
        CookieAuthenticationOptions? cookieAuthenticationOptions)
    {
        AuthenticationBuilder authBuilder = serviceCollection
    .AddAuthentication(options => options = authenticationOptions);

        if (cookieAuthenticationOptions != null)
        {
            authBuilder.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options = cookieAuthenticationOptions;
            });
        }

        if (jwtBearerOptions != null)
        {
            authBuilder.AddJwtBearer(options => options = jwtBearerOptions);
        }

        return serviceCollection;
    }
}
