using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WishList.Feaches.Aunification;
using WishList.Feaches.Authorization;
using WishList.Feaches.CreateList;

public static class CreateListEndpoint
{
    public static void CreateEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("wishlist/create", async (
            [FromBody] CreateListReqest request,
            IMediator mediator,
            ClaimsPrincipal user,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var currentUserId = AuthHelper.GetCurrentUserId(user);
                var createRequest = request with { UserId = currentUserId };
                var response = await mediator.Send(createRequest, cancellationToken);
                return Results.Ok(response);
            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                return Results.BadRequest(new { message = "Ошибки валидации", errors });
            }
            catch (UnauthorizedAccessException)
            {
                return Results.Unauthorized();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }).RequireAuthorization();
    }
}