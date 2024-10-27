using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using XkliburSolutions.Shield.CrossCutting.Configuration.Extensions;
using XkliburSolutions.Shield.CrossCutting.ExceptionHandling;
using XkliburSolutions.Shield.CrossCutting.Logging;
using XkliburSolutions.Shield.CrossCutting.Services;
using XkliburSolutions.Shield.Infrastructure.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IClaimsService, ClaimsService>();

builder.Services.AddFeatureManagement();

// Add authentication and authorization services.
builder.Services.AddCustomAuthentication(
    configuration,
    new()
    {
        DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme,
        DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme,
        DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme,
    },
    new()
    {
        LoginPath = "/Account/Login",
        AccessDeniedPath = "/Account/Login"
    });

builder.Services.AddCustomAuthorization(
    CookieAuthenticationDefaults.AuthenticationScheme);

// Use custom logging
builder.UseCustomLogging();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events = new CookieAuthenticationEvents
    {
        OnRedirectToLogin = ctx =>
        {
            if (ctx.Request.Path.StartsWithSegments("/api"))
            {
                ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
            else
            {
                ctx.Response.Redirect(ctx.RedirectUri);
            }
            return Task.CompletedTask;
        },
        OnRedirectToAccessDenied = ctx =>
        {
            if (ctx.Request.Path.StartsWithSegments("/api"))
            {
                ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
            }
            else
            {
                ctx.Response.Redirect(ctx.RedirectUri);
            }
            return Task.CompletedTask;
        }
    };
});


WebApplication app = builder.Build();

// Log that the application has started
ILogger<Program> logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.ApplicationStarted();

// Enable the exception handling middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // Use the exception handler middleware to handle exceptions by redirecting to the "/Error" page.
    app.UseExceptionHandler("/Error");
    // Use HTTP Strict Transport Security (HSTS) with a default value of 30 days.
    // This can be adjusted for production scenarios. For more information, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Redirect HTTP requests to HTTPS.
app.UseHttpsRedirection();

// Serve static files from the wwwroot folder.
app.UseStaticFiles();

// Add routing middleware to the request pipeline.
app.UseRouting();

app.UseAuthentication();
// Add authorization middleware to the request pipeline.
app.UseAuthorization();

// Map Razor Pages endpoints.
app.MapRazorPages();

// Run the application.
app.Run();
