using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace XkliburSolutions.Shield.Infrastructure.Repositories;

/// <summary>
/// Provides extension methods for configuring the database context.
/// </summary>
public static class DatabaseContextExtensions
{
    /// <summary>
    /// Configures the database context to use SQLite with the connection string from the configuration.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The service collection with the database context configured.</returns>
    public static IServiceCollection AddCustomDatabaseContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection")!;

        return AddCustomDatabaseContext(services, connectionString);
    }

    /// <summary>
    /// Configures the database context to use SQLite with the specified connection string.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="connectionString">The connection string for the database.</param>
    /// <returns>The service collection with the database context configured.</returns>
    public static IServiceCollection AddCustomDatabaseContext(
        this IServiceCollection services,
        string connectionString)
    {
        // Configure the database context to use SQLite with the connection string from the configuration.
        services
            .AddDbContext<ApplicationDbContext>(options => options
                .UseSqlite(connectionString));

        return services;
    }
}
