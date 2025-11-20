using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WishList.Feaches.ChekList
{
    public static class ChekListEndpoint
    {
        public static void ChekListMap(this IEndpointRouteBuilder app)
        {
            app.MapGet("wishlist/chek/{id}", async (
                [FromRoute] int id,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var response = new ChekListResponse(id);
                var user = await mediator.Send(response);
                return Results.Ok(user);
            })
            .WithName("GetUserWishLists")
            .WithTags("WishLists")
            .WithSummary("Получение вишлистов пользователя")
            .WithDescription("Возвращает все вишлисты указанного пользователя")
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Получение вишлистов пользователя"
            }); ;
        }
    }
}
