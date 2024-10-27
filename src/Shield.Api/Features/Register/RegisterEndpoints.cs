using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.FeatureManagement.Mvc;
using XkliburSolutions.Shield.CrossCutting.DTOs;
using XkliburSolutions.Shield.Domain.Entities;
using XkliburSolutions.Shield.Domain.Enums;

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

        routes.MapPost("/account/validate", ValidateEmailPost)
            .MapToApiVersion(1)
            .WithName("Validate")
            .WithOpenApi()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);
    }

    /// <summary>
    /// Handles the registration POST request.
    /// </summary>
    /// <param name="model">The registration model.</param>
    /// <param name="userManager">The user manager.</param>
    /// <returns>A task that represents the completion of the registration request.</returns>
    [FeatureGate("Registration")]
    private static async Task<IResult> RegisterPost(
        RegisterInputModel model,
        UserManager<ApplicationUser> userManager)
    {
        ApplicationUser user = new()
        {
            UserName = model.UserName,
            Email = model.Email,
            Status = UserStatus.Active
        };

        IdentityResult result = await userManager.CreateAsync(user, model.Password!);

        if (result.Succeeded)
        {
            bool requireConfirmedAccount = userManager.Options.SignIn.RequireConfirmedAccount;

            if (requireConfirmedAccount)
            {
                string userId = await userManager.GetUserIdAsync(user);
                string code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                string encodedCode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                RegisterOutputModel output = new RegisterOutputModel
                {
                    UserId = userId,
                    ValidationCode = encodedCode,
                    RequiredConfirmedAccount = true,
                    DisplayConfirmAccountLink = false
                };

                if (userManager.SupportsUserEmail)
                {
                    //TODO: Add send email mechanism and fallback to link

                    // TODO: Localize and log and create final response
                    //return Results.Ok();
                }

                output.DisplayConfirmAccountLink = true;
                return Results.Ok(output);
            }
            else
            {
                // TODO: Localize and log and create final response
                return Results.Ok(new RegisterOutputModel
                {
                    RequiredConfirmedAccount = false,
                    DisplayConfirmAccountLink = false
                });
            }
        }

        //TODO: Add logs
        return Results.BadRequest(result.Errors);
    }

    private static async Task<IResult> ValidateEmailPost(
        ValidateEmailInputModel model,
        UserManager<ApplicationUser> userManager)
    {
        // Find the user by their ID.
        ApplicationUser? user = await userManager.FindByIdAsync(model.UserId);
        if (user == null)
        {
            // If the user is not found, return a NotFound result.
            // TODO: Add logs
            return Results.NotFound();
        }

        // Decode the confirmation code.
        string code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.ValidationCode));
        //TODO: Transfer the below to an api call
        // Confirm the user's email.
        IdentityResult result = await userManager.ConfirmEmailAsync(user, code);

        return result.Succeeded ? Results.Ok() : Results.BadRequest();
    }
}
