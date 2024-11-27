using XkliburSolutions.Shield.Core.Constants;

namespace XkliburSolutions.Shield.Core.Models.Output;

/// <summary>
/// Represents a standard API response with a status, data, messages, and errors.
/// </summary>
/// <typeparam name="T">The type of the data included in the response.</typeparam>
public class ApiOutputModel<T>
{
    /// <summary>
    /// Gets or sets the status of the API response.
    /// </summary>
    public string Status { get; set; } = ApiResponseStatus.Success;

    /// <summary>
    /// Gets or sets the data included in the API response.
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// Gets or sets the list of messages included in the API response.
    /// </summary>
    public List<string> Messages { get; set; } = [];

    /// <summary>
    /// Gets or sets the list of errors included in the API response.
    /// </summary>
    public List<ApiErrorModel> Errors { get; set; } = [];
}

/// <summary>
/// Provides static methods to create standard API responses.
/// </summary>
public static class ApiOutputModel
{
    /// <summary>
    /// Creates a successful API response with the specified data.
    /// </summary>
    /// <typeparam name="T1">The type of the API output model.</typeparam>
    /// <typeparam name="T2">The type of the data included in the response.</typeparam>
    /// <param name="data">The data to include in the response.</param>
    /// <returns>An instance of <typeparamref name="T1"/> with a success status and the specified data.</returns>
    public static T1 Ok<T1, T2>(T2 data) where T1 : ApiOutputModel<T2>, new()
    {
        return new T1
        {
            Status = ApiResponseStatus.Success,
            Data = data,
        };
    }
}
