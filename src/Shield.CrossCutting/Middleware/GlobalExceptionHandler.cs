using Microsoft.Extensions.Logging;
using XkliburSolutions.Shield.CrossCutting.Logging;

namespace XkliburSolutions.Shield.CrossCutting.Middleware;

/// <summary>
/// Provides a global exception handler for logging unhandled exceptions.
/// </summary>
public static class GlobalExceptionHandler
{
    /// <summary>
    /// Handles the specified exception by logging it.
    /// </summary>
    /// <param name="ex">The exception to handle.</param>
    /// <param name="logger">The logger to use for logging the exception.</param>
    public static void Handle(Exception ex, ILogger logger)
    {
        switch (ex)
        {
            default:
                // Log the unhandled exception.
                logger.LogUnhandledException(ex);
                break;
        }
    }
}
