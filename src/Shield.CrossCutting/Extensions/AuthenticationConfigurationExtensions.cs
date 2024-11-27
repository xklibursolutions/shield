using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace XkliburSolutions.Shield.CrossCutting.Extensions;

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
    public static IServiceCollection AddApiCustomAuthentication(
        this IServiceCollection serviceCollection,
        ConfigurationManager configuration)
    {
        serviceCollection
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!))
                };
            });

        return serviceCollection;
    }

    /// <summary>
    /// Adds authentication services with JWT Bearer and cookie support to the specified IServiceCollection.
    /// </summary>
    /// <param name="serviceCollection">The IServiceCollection to add services to.</param>
    /// <param name="configuration">The configuration manager to retrieve JWT settings from.</param>
    /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddWebCustomAuthentication(
        this IServiceCollection serviceCollection,
        ConfigurationManager configuration)
    {
        AuthenticationBuilder authBuilder = serviceCollection
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/Login";
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!))
                };
            });

        return serviceCollection;
    }
}
