using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using XkliburSolutions.Shield.Core.DTOs;
using XkliburSolutions.Shield.Core.Entities;
using XkliburSolutions.Shield.Core.Models.Input;
using XkliburSolutions.Shield.Core.Models.Output;
using XkliburSolutions.Shield.Core.Services;
using XkliburSolutions.Shield.Infrastructure.Services;

namespace XkliburSolutions.Shield.Api.Features;

/// <summary>
/// Provides endpoint mappings for user-related operations.
/// </summary>
public static class UserEndpoints
{
    /// <summary>
    /// Maps the user endpoints to the specified route builder.
    /// </summary>
    /// <param name="routes">The route builder to which the endpoints will be mapped.</param>
    public static void MapUserEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/account/me", GetLoggedUserInfoAsync)
            .MapToApiVersion(1)
            .WithName("GetAccount")
            .WithOpenApi()
            .Produces<CurrentUserOutputModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .RequireAuthorization();
    }

    /// <summary>
    /// Retrieves the logged-in user's information asynchronously.
    /// </summary>
    /// <param name="userManager">The user manager.</param>
    /// <param name="httpContext">The HTTP context.</param>
    /// <param name="userService">The user service.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the operation.</returns>
    [Authorize]
    private static async Task<IResult> GetLoggedUserInfoAsync(
        UserManager<ApplicationUser> userManager,
        HttpContext httpContext,
        IUserService userService)
    {
        string? username = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        UserDto? user = await userService.GetLoggedUserInfoAsync(username!);

        if (user == null)
        {
            return Results.Unauthorized();
        }

        CurrentUserOutputModel apiResponse = ApiOutputModel
            .Ok<CurrentUserOutputModel, UserDto>(user);
        return Results.Ok(apiResponse);
    }
}
