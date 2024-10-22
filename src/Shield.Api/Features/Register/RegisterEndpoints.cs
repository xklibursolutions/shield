using Microsoft.AspNetCore.Identity;
using XkliburSolutions.Shield.CrossCutting.Entities;

namespace XkliburSolutions.Shield.Api.Features.Register;

/// <summary>
/// Provides extension methods for mapping register endpoints.
/// </summary>
public static class RegisterEndpoints
{
    /// <summary>
    /// Maps the register endpoints.
    /// </summary>
    /// <param name="routes">The endpoint route builder.</param>
    public static void MapRegisterEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapPost("/account/register", RegisterPost)
            .MapToApiVersion(1)
            .WithName("Register")
            .WithOpenApi()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status200OK);
    }

    /// <summary>
    /// Handles the registration POST request.
    /// </summary>
    /// <param name="model">The registration model.</param>
    /// <param name="userManager">The user manager.</param>
    /// <returns>A task that represents the completion of the registration request.</returns>
    private static async Task<IResult> RegisterPost(
        RegisterModel model,
        UserManager<ApplicationUser> userManager)
    {
        //TODO: Change model - should be bigger
        ApplicationUser user = new() { UserName = model.Email, Email = model.Email };
        IdentityResult result = await userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            // TODO: Localize and log and create final response
            return Results.Ok(new { Message = "User registered successfully" });
        }

        //TODO: Add logs
        return Results.BadRequest(result.Errors);
    }
}
