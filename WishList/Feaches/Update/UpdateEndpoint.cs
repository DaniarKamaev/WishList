using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WishList.Feaches.Update
{
    public static class UpdateEndpoint
    {
        public static void UpdateMap(this IEndpointRouteBuilder app)
        {
            app.MapPut("wishlist/update", async (
                [FromBody] UpdateRequest request,
                IMediator mediator,
                CancellationToken token) => 
            {
                try
                {
                    var response = await mediator.Send(request, token);
                    return Results.Ok(response);
                }
                catch (UnauthorizedAccessException)
                {
                    return Results.Unauthorized();
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            })
            .RequireAuthorization()
            .WithName("UpdateWishList")
            .WithTags("WishLists")
            .WithSummary("Обновление вишлиста")
            .WithDescription("Обновляет информацию о подарке в вишлисте (только для владельца)")
            .Produces<UpdateResponse>(StatusCodes.Status200OK, "application/json")
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Обновление вишлиста",
                Description = "Позволяет владельцу изменить название подарка, ссылку или цену"
            });
        }
    }
}
