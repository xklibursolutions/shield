namespace XkliburSolutions.Shield.Api.Features;

/// <summary>
/// Provides extension methods for mapping ping-related endpoints.
/// </summary>
public static class PingEndpoints
{
    /// <summary>
    /// Maps ping-related endpoints.
    /// </summary>
    /// <param name="routes">The IEndpointRouteBuilder to add routes to.</param>
    public static void MapPingEndpoints(this IEndpointRouteBuilder routes)
    {
        // Map a simple endpoint that responds with "pong" to a GET request at "/ping".
        routes.MapGet("/ping", Pong)
            .MapToApiVersion(1)
            .WithName("Ping") // Name the endpoint "Ping".
            .WithOpenApi()   // Include this endpoint in the OpenAPI documentation.
            .Produces(StatusCodes.Status200OK);
    }

    /// <summary>
    /// Handles the GET request to the "/ping" endpoint.
    /// </summary>
    /// <returns>An IResult containing the response "pong".</returns>
    private static IResult Pong() => Results.Ok("pong");
}
