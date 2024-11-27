using System.Globalization;
using Asp.Versioning;
using Microsoft.AspNetCore.Localization;
using XkliburSolutions.Shield.Infrastructure.Extensions;
using XkliburSolutions.Shield.Api.Features;
using XkliburSolutions.Shield.Infrastructure.Data;
using XkliburSolutions.Shield.Infrastructure;
using XkliburSolutions.Shield.CrossCutting.Middleware;
using XkliburSolutions.Shield.CrossCutting.Logging;
using XkliburSolutions.Shield.Infrastructure.Services;
using XkliburSolutions.Shield.Core.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddInfrastructure(configuration);
// Use custom logging
builder.UseCustomLogging();

WebApplication app = builder.Build();

// Log that the application has started
ILogger<Program> logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.ApplicationStarted();

// Enable the exception handling middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline for development environment.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (IServiceScope scope = app.Services.CreateScope())
    {
        ApplicationDbContext db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
    }
}

List<CultureInfo> supportedCultures =
[
    new("en"),
    new("fr"),
];
RequestLocalizationOptions options = new()
{
    DefaultRequestCulture = new RequestCulture(supportedCultures[0]),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures,
};

app.UseRequestLocalization(options);

// Enable HTTPS redirection.
app.UseHttpsRedirection();

// Enable authentication and authorization middleware.
app.UseAuthentication();
app.UseAuthorization();

RouteGroupBuilder apiVersionGroupV1 = app.MapApiVersionGroup(new ApiVersion(1));
apiVersionGroupV1.MapPingEndpoints();
apiVersionGroupV1.MapRegisterEndpoints();
apiVersionGroupV1.MapLoginEndpoints();
apiVersionGroupV1.MapUserEndpoints();

// Run the application.
app.Run();

/// <summary>
/// Expose the Program for unit and integration testing.
/// </summary>
public partial class Program { }
