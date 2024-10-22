using Microsoft.AspNetCore.Identity;
using XkliburSolutions.Shield.CrossCutting.Entities;
using XkliburSolutions.Shield.CrossCutting.Security;

namespace XkliburSolutions.Shield.Api.Features.Login;

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
            .Produces(StatusCodes.Status200OK);
    }

    /// <summary>
    /// Handles the login POST request.
    /// </summary>
    /// <param name="model">The login model containing email and password.</param>
    /// <param name="signInManager">The SignInManager to handle sign-in operations.</param>
    /// <param name="userManager">The UserManager to manage user-related operations.</param>
    /// <param name="configuration">The IConfiguration with jwt settings.</param>
    /// <returns>An <see cref="IResult"/> indicating the outcome of the login operation.</returns>
    private static async Task<IResult> LoginPostAsync(
        LoginModel model,
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration)
    {
        // Attempt to sign in the user with the provided email and password
        SignInResult result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

        if (result.Succeeded)
        {
            // If sign-in is successful, find the user by email
            ApplicationUser? user = await userManager.FindByEmailAsync(model.Email);
            // Generate a JWT token for the authenticated user
            string token = new JwtTokenGenerator(configuration)
                .GenerateJwtToken(user!);

            // Return the generated token in the response
            return Results.Ok(new { Token = token }); //TODO: create real response model
        }

        // If sign-in fails, return an unauthorized response
        return Results.Unauthorized();
    }
}
