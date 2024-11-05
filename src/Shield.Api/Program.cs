using System.Globalization;
using Asp.Versioning;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.FeatureManagement;
using XkliburSolutions.Shield.Api.Configuration.Extensions;
using XkliburSolutions.Shield.Api.Features.Login;
using XkliburSolutions.Shield.Api.Features.Ping;
using XkliburSolutions.Shield.Api.Features.Register;
using XkliburSolutions.Shield.CrossCutting.Configuration;
using XkliburSolutions.Shield.CrossCutting.Configuration.Extensions;
using XkliburSolutions.Shield.CrossCutting.ExceptionHandling;
using XkliburSolutions.Shield.CrossCutting.Logging;
using XkliburSolutions.Shield.CrossCutting.Services;
using XkliburSolutions.Shield.Infrastructure.Identity;
using XkliburSolutions.Shield.Infrastructure.Repositories;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

IConfigurationSection appSettingsSection = configuration.GetSection("ApplicationSettings");
ApplicationSettings applicationSettings = appSettingsSection.Get<ApplicationSettings>()!;

builder.Services.Configure<ApplicationSettings>(appSettingsSection);
builder.Services.Configure<RegistrationSettings>(
    configuration.GetSection("ApplicationSettings:RegistrationSettings"));

// Configure the database context to use SQLite with the connection string from the configuration.
builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddSingleton<IStringLocalizerFactory, ResourceManagerStringLocalizerFactory>();
builder.Services.AddSingleton(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));

// Configure Identity services with custom password options and Entity Framework stores.
builder.Services.AddCustomIdentity(applicationSettings.RegistrationSettings!);

// Add services for API endpoint exploration.
builder.Services.AddEndpointsApiExplorer();

// Add authentication and authorization services.
builder.Services.AddCustomAuthentication(configuration);
builder.Services.AddCustomAuthorization();

// Add API versioning and Swagger configuration services.
builder.Services.AddApiVersioningConfiguration();
builder.Services.AddSwaggerConfiguration();

builder.Services.AddFeatureManagement();

builder.Services.AddCommunicationService(configuration);

builder.Services.AddSingleton<TemplateService>();

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
    new( "en" ),
    new( "fr" ),
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

// Run the application.
app.Run();
