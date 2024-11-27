namespace XkliburSolutions.Shield.Core.Models;

/// <summary>
/// Represents an error model for API responses, containing an error code and a message.
/// </summary>
public class ApiErrorModel
{
    /// <summary>
    /// Gets or sets the error code.
    /// </summary>
    public required int Code { get; set; }

    /// <summary>
    /// Gets or sets the error message.
    /// </summary>
    public required string Message { get; set; }
}
