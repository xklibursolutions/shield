using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.FeatureManagement;
using XkliburSolutions.Shield.Core.Interfaces;
using XkliburSolutions.Shield.Infrastructure.Configurations;
using XkliburSolutions.Shield.Infrastructure.Data;
using XkliburSolutions.Shield.Infrastructure.Extensions;
using XkliburSolutions.Shield.Infrastructure.Identity;
using XkliburSolutions.Shield.Infrastructure.Repositories;
using XkliburSolutions.Shield.Infrastructure.Services;
using XkliburSolutions.Shield.CrossCutting.Extensions;

namespace XkliburSolutions.Shield.Infrastructure;

/// <summary>
/// Provides extension methods for configuring infrastructure services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds infrastructure services to the specified IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <param name="configuration">The ConfigurationManager containing application settings.</param>
    /// <returns>The IServiceCollection with added services.</returns>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        IConfigurationSection appSettingsSection = configuration.GetSection("ApplicationSettings");
        ApplicationSettings applicationSettings = appSettingsSection.Get<ApplicationSettings>()!;

        services.Configure<ApplicationSettings>(appSettingsSection);
        services.Configure<RegistrationSettings>(
            configuration.GetSection("ApplicationSettings:RegistrationSettings"));

        // Configure the database context to use SQLite with the connection string from the configuration.
        services.AddDbContext<ApplicationDbContext>();

        services.AddLocalization(options => options.ResourcesPath = "Resources");

        services.AddSingleton<IStringLocalizerFactory, ResourceManagerStringLocalizerFactory>();
        services.AddSingleton(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));

        // Configure Identity services with custom password options and Entity Framework stores.
        services.AddCustomIdentity(applicationSettings.RegistrationSettings!);

        // Add services for API endpoint exploration.
        services.AddEndpointsApiExplorer();

        // Add authentication and authorization services.
        services.AddApiCustomAuthentication(configuration);
        services.AddCustomAuthorization();

        // Add API versioning and Swagger configuration services.
        services.AddApiVersioningConfiguration();
        services.AddSwaggerConfiguration();

        services.AddFeatureManagement();

        services.AddCommunicationService(configuration);

        services.AddSingleton<TemplateService>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<UserService>();

        return services;
    }
}
