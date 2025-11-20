using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WishList.DbModels;
using WishList.Feaches.Authorization;

namespace WishList.Feaches.DeleteWishList
{
    public static class DeleteWishListEndpoint
    {
        public static void DeleteWishListMap(this IEndpointRouteBuilder app)
        {
            app.MapDelete("wishlist/delete/{wishListId}", async (
                int wishListId,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                try
                {
                    var request = new DeleteWishListReqest(wishListId);
                    var response = await mediator.Send(request, cancellationToken);
                    return Results.Ok(response);
                }
                catch (UnauthorizedAccessException)
                {
                    return Results.Unauthorized();
                }
                catch (Exception ex)
                {
                    return Results.BadRequest("Ошибка: " + ex.Message);
                }
            })
            .RequireAuthorization()
            .WithName("DeleteWishList")
            .WithTags("WishLists")
            .WithSummary("Удаление вишлиста")
            .WithDescription("Удаляет вишлист по ID (только владелец)")
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Удаление вишлиста"
            });
        }
    }
}
