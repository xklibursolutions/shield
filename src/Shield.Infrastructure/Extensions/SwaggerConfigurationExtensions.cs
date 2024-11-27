using System.Reflection;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace XkliburSolutions.Shield.Infrastructure.Extensions;

/// <summary>
/// Provides extension methods for configuring Swagger generation with API versioning support.
/// </summary>
public static class SwaggerConfigurationExtensions
{
    /// <summary>
    /// Adds Swagger generation services with versioning support to the specified IServiceCollection.
    /// </summary>
    /// <param name="serviceCollection">The IServiceCollection to add services to.</param>
    /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen(c =>
        {
            IApiVersionDescriptionProvider provider = serviceCollection.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

            foreach (ApiVersionDescription apiDescription in provider.ApiVersionDescriptions)
            {
                c.SwaggerDoc(apiDescription.GroupName, new OpenApiInfo
                {
                    Title = "Identity API",
                    Version = apiDescription.ApiVersion.ToString(),
                    Description = "Identity API ensures secure user authentication, facilitates user registration, and offers features for managing user accounts effectively. Developers can integrate this API into their applications to handle user-related tasks seamlessly."
                });
            }

            string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

        });

        return serviceCollection;
    }
}
