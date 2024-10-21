using Microsoft.Extensions.Logging;

namespace XkliburSolutions.Shield.CrossCutting.Logging;

/// <summary>
/// Provides pre-defined log messages for the application.
/// </summary>
public static class LogMessages
{
    private static readonly Action<ILogger, Exception?> s_applicationStarted;
    private static readonly Action<ILogger, string, Exception?> s_unhandledExceptionWithRequestId;
    private static readonly Action<ILogger, Exception?> s_unhandledException;

    /// <summary>
    /// Initializes static members of the <see cref="LogMessages"/> class.
    /// </summary>
    static LogMessages()
    {
        s_applicationStarted = LoggerMessage.Define(
            LogLevel.Information,
            new EventId(1, nameof(ApplicationStarted)),
            "Application started");
        s_unhandledExceptionWithRequestId = LoggerMessage.Define<string>(
            LogLevel.Error,
            new EventId(2, nameof(LogUnhandledException)),
            "An error occurred while processing request {RequestId}.");
        s_unhandledException = LoggerMessage.Define(
            LogLevel.Error,
            new EventId(3, nameof(LogUnhandledException)),
            "An error occurred while processing a request.");
    }

    /// <summary>
    /// Logs an information message indicating that the application has started.
    /// </summary>
    /// <param name="logger">The logger to log the message.</param>
    public static void ApplicationStarted(this ILogger logger)
    {
        s_applicationStarted(logger, null);
    }

    /// <summary>
    /// Logs a generic error message along with the request ID.
    /// </summary>
    /// <param name="logger">The logger to use for logging the error.</param>
    /// <param name="requestId">The ID of the request associated with the error.</param>
    /// <param name="exception">The exception that occurred.</param>
    public static void LogUnhandledException(this ILogger logger, Exception? exception, string? requestId = null)
    {
        if (string.IsNullOrEmpty(requestId))
        {
            s_unhandledException(logger, exception);
        }
        else
        {
            s_unhandledExceptionWithRequestId(logger, requestId, exception);
        }
        
    }
}

