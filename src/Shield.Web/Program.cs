using XkliburSolutions.Shield.CrossCutting.Configuration.Extensions;
using XkliburSolutions.Shield.CrossCutting.ExceptionHandling;
using XkliburSolutions.Shield.CrossCutting.Logging;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add authentication and authorization services.
builder.Services.AddAuthenticationConfiguration(configuration);
builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddRazorPages();

// Use custom logging
builder.UseCustomLogging();

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

// Add authorization middleware to the request pipeline.
app.UseAuthorization();

// Map Razor Pages endpoints.
app.MapRazorPages();

// Run the application.
app.Run();
