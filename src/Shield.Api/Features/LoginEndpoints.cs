using Microsoft.AspNetCore.Identity;
using XkliburSolutions.Shield.Core.DTOs;
using XkliburSolutions.Shield.Core.Models.Input;
using XkliburSolutions.Shield.Core.Models.Output;
using XkliburSolutions.Shield.Core.Services;
using XkliburSolutions.Shield.Infrastructure.Services;

namespace XkliburSolutions.Shield.Api.Features;

/// <summary>
/// Provides endpoint mappings for login-related operations.
/// </summary>
public static class LoginEndpoints
{
    /// <summary>
    /// Maps the login endpoints to the specified route builder.
    /// </summary>
    /// <param name="routes">The route builder to which the endpoints will be mapped.</param>
    public static void MapLoginEndpoints(this IEndpointRouteBuilder routes)
    {
        // Map the POST request for login to the LoginPostAsync method
        routes.MapPost("/account/login", LoginPostAsync)
            .MapToApiVersion(1) // Specify API version 1
            .WithName("Login") // Name the endpoint "Login"
            .WithOpenApi() // Enable OpenAPI documentation
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces<LoginOutputModel>(StatusCodes.Status200OK);
    }

    /// <summary>
    /// Handles the login POST request.
    /// </summary>
    /// <param name="model">The login model containing email and password.</param>
    /// <param name="userService">The user service.</param>
    /// <returns>An <see cref="IResult"/> indicating the outcome of the login operation.</returns>
    public static async Task<IResult> LoginPostAsync(LoginInputModel model, IUserService userService)
    {
        (IdentityResult result, string token) = await userService.LoginAsync(model);

        if (result.Succeeded)
        {
            LoginOutputModel apiResponse = ApiOutputModel
                .Ok<LoginOutputModel, LoginDto>(new LoginDto { Token = token });
            return Results.Ok(apiResponse);
        }

        if (result.Errors.Any(e => e.Description == "User not found."))
        {
            return Results.Unauthorized();
        }

        return Results.Unauthorized();
    }
}
