﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using XkliburSolutions.Shield.CrossCutting.Configuration;
using XkliburSolutions.Shield.Infrastructure.Services;

namespace XkliburSolutions.Shield.Api.Configuration.Extensions;

/// <summary>
/// Provides extension methods for configuring communication services.
/// </summary>
public static class CommunicationServiceConfigurationExtensions
{
    private const string AzureCommunicationServiceSection = "ApplicationSettings:AzureCommunicationService";

    /// <summary>
    /// Adds and configures the communication service to the service collection.
    /// </summary>
    /// <param name="services">The service collection to add the communication service to.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddCommunicationService(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Configures the AzureCommunicationService settings from the application configuration.
        services.Configure<AzureCommunicationService>(configuration.GetSection(AzureCommunicationServiceSection));

        // Registers the AzureCommunicationEmailService as a singleton implementation of ICommunicationService.
        services.AddSingleton<ICommunicationService, AzureCommunicationEmailService>();

        return services;
    }
}
