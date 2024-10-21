using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using XkliburSolutions.Shield.Api.Configuration.Extensions;
using XkliburSolutions.Shield.Api.Features.Ping;
using XkliburSolutions.Shield.CrossCutting.Configuration.Extensions;
using XkliburSolutions.Shield.CrossCutting.ExceptionHandling;
using XkliburSolutions.Shield.Infrastructure.Identity;
using XkliburSolutions.Shield.Infrastructure.Repositories;
using XkliburSolutions.Shield.CrossCutting.Security;
using XkliburSolutions.Shield.CrossCutting.Logging;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Configure the database context to use SQLite with the connection string from the configuration.
builder.Services
    .AddDbContext<ApplicationDbContext>(options => options
        .UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity services with custom password options and Entity Framework stores.
builder.Services
    .AddIdentity<ApplicationUser, ApplicationRole>(options =>
    {
        PasswordPolicyService.ConfigurePasswordOptions(options.Password);
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Add services for API endpoint exploration.
builder.Services.AddEndpointsApiExplorer();

// Add authentication and authorization services.
builder.Services.AddAuthenticationConfiguration(configuration);
builder.Services.AddAuthorization();

// Add API versioning and Swagger configuration services.
builder.Services.AddApiVersioningConfiguration();
builder.Services.AddSwaggerConfiguration();

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
}

// Enable HTTPS redirection.
app.UseHttpsRedirection();

// Enable authentication and authorization middleware.
app.UseAuthentication();
app.UseAuthorization();

app.MapPingEndpoints();

// Run the application.
app.Run();
