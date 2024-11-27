using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

namespace XkliburSolutions.Shield.CrossCutting.Logging;

/// <summary>
/// Provides extension methods for configuring custom logging.
/// </summary>
public static class LoggingExtensions
{
    /// <summary>
    /// Configures custom logging for the application.
    /// </summary>
    /// <param name="builder">The web application builder.</param>
    /// <param name="configurationSectionName">The name of the configuration section for logging settings.</param>
    /// <returns>The web application builder with custom logging configured.</returns>
    public static WebApplicationBuilder UseCustomLogging(this WebApplicationBuilder builder, string configurationSectionName = "Logging")
    {
        // Retrieve the logging configuration section from the configuration.
        IConfigurationSection configurationSection = builder.Configuration.GetSection(configurationSectionName);

        // Clear existing logging providers.
        builder.Logging.ClearProviders();
        // Add console logging provider.
        builder.Logging.AddConsole();
        // Add debug logging provider.
        builder.Logging.AddDebug();
        // Add logging configuration from the specified configuration section.
        builder.Logging.AddConfiguration(configurationSection);

        return builder;
    }
}
