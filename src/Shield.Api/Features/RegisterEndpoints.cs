using Microsoft.AspNetCore.Identity;
using Microsoft.FeatureManagement.Mvc;
using XkliburSolutions.Shield.Core.DTOs;
using XkliburSolutions.Shield.Core.Models.Input;
using XkliburSolutions.Shield.Core.Models.Output;
using XkliburSolutions.Shield.Infrastructure.Services;

namespace XkliburSolutions.Shield.Api.Features;

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
            .Produces<RegisterOutputModel>(StatusCodes.Status200OK);

        routes.MapPost("/account/validate", ValidateEmailPost)
            .MapToApiVersion(1)
            .WithName("Validate")
            .WithOpenApi()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);
    }

    /// <summary>
    /// Handles the registration of a new user.
    /// </summary>
    /// <param name="model">The registration input model.</param>
    /// <param name="userService">The user service.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the registration process.</returns>
    [FeatureGate("Registration")]
    private static async Task<IResult> RegisterPost(
        RegisterInputModel model,
        UserService userService)
    {
        IdentityResult result = await userService.RegisterUserAsync(model);

        if (result.Succeeded)
        {
            bool requireConfirmedAccount = userService.RequireConfirmedAccount();

            if (requireConfirmedAccount)
            {
                RegisterDto outputDto = await userService.GenerateConfirmationEmailAsync(model);

                RegisterOutputModel apiResponse = ApiOutputModel
                    .Ok<RegisterOutputModel, RegisterDto>(outputDto);

                return Results.Ok(apiResponse);
            }
            else
            {
                RegisterOutputModel apiResponse = ApiOutputModel
                    .Ok<RegisterOutputModel, RegisterDto>(
                    new RegisterDto {
                        RequiredConfirmedAccount = false,
                        DisplayConfirmAccountLink = false
                    });
                return Results.Ok(apiResponse);
            }
        }

        return Results.BadRequest(result.Errors);
    }

    /// <summary>
    /// Handles the validation of a user's email.
    /// </summary>
    /// <param name="model">The email validation input model.</param>
    /// <param name="userService">The user service.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the email validation process.</returns>
    [FeatureGate("Registration")]
    private static async Task<IResult> ValidateEmailPost(
        ValidateEmailInputModel model,
        UserService userService)
    {
        IdentityResult result = await userService.ValidateEmailAsync(model);

        if (result.Succeeded)
        {

            return Results.Ok();
        }

        if (result.Errors.Any(e => e.Description == "User not found."))
        {
            return Results.NotFound();
        }

        return Results.BadRequest(result.Errors);
    }
}
