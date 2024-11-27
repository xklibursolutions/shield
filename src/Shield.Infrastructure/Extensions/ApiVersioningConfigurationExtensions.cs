using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace XkliburSolutions.Shield.Infrastructure.Extensions;

/// <summary>
/// Provides extension methods for configuring API versioning.
/// </summary>
public static class ApiVersioningConfigurationExtensions
{
    /// <summary>
    /// Adds API versioning services to the specified IServiceCollection.
    /// </summary>
    /// <param name="serviceCollection">The IServiceCollection to add services to.</param>
    /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddApiVersioningConfiguration(
        this IServiceCollection serviceCollection)
    {
        serviceCollection.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version"));
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return serviceCollection;
    }

    /// <summary>
    /// Configures API versioning for the specified route group.
    /// </summary>
    /// <param name="app">The web application.</param>
    /// <param name="version">The API version.</param>
    /// <returns>The route group builder with API versioning configured.</returns>
    public static RouteGroupBuilder MapApiVersionGroup(
        this WebApplication app,
        ApiVersion version)
    {
        ApiVersionSet apiVersionSet = app.NewApiVersionSet()
            .HasApiVersion(version)
            .ReportApiVersions()
            .Build();

        RouteGroupBuilder apiVersionGroup = app
            .MapGroup("api/v{version:apiVersion}")
            .WithApiVersionSet(apiVersionSet)
            .WithOpenApi();

        return apiVersionGroup;
    }
}
