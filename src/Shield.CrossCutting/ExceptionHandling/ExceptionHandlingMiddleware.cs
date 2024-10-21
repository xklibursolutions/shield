using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using XkliburSolutions.Shield.CrossCutting.Logging;

namespace XkliburSolutions.Shield.CrossCutting.ExceptionHandling;

/// <summary>
/// Middleware for handling exceptions globally in the application.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ExceptionHandlingMiddleware"/> class.
/// </remarks>
/// <param name="next">The next middleware in the pipeline.</param>
/// <param name="logger">The logger to log exceptions.</param>
public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

    /// <summary>
    /// Invokes the middleware to handle the HTTP request.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>A task that represents the completion of request processing.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogUnhandledException(ex, context.TraceIdentifier);
            await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// Handles the exception by setting the response status code and content.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <param name="exception">The exception that occurred.</param>
    /// <returns>A task that represents the completion of exception handling.</returns>
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        // TODO: Add localization here
        var response = new { message = "An error occurred while processing your request." };
        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
