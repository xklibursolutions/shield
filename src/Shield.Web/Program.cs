using System.Globalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.FeatureManagement;
using XkliburSolutions.Shield.Core.Services;
using XkliburSolutions.Shield.CrossCutting.Extensions;
using XkliburSolutions.Shield.CrossCutting.Logging;
using XkliburSolutions.Shield.CrossCutting.Middleware;
using XkliburSolutions.Shield.CrossCutting.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddRazorPages()
    .AddViewLocalization();
//.AddDataAnnotationsLocalization(options =>
//{
//    options.DataAnnotationLocalizerProvider = (type, factory) =>
//    {
//        AssemblyName assemblyName = new(typeof(SharedResources).GetTypeInfo().Assembly.FullName!);
//        return factory.Create(nameof(SharedResources), assemblyName.Name!);
//    };
//});

builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ClaimsService>();

builder.Services.AddFeatureManagement();

// Add authentication and authorization services.
builder.Services.AddWebCustomAuthentication(configuration);

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
