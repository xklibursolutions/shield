using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace XkliburSolutions.Shield.Api.Configuration.Extensions;

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
    public static IServiceCollection AddApiVersioningConfiguration(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version"));
        }).AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return serviceCollection;
    }
}
