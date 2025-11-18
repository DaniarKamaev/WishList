using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WishList.Feaches.DeleteWishList
{
    public static class DeleteWishListEndpoint
    {
        public static void DeleteWishListMap(this IEndpointRouteBuilder app)
        {
            app.MapDelete("wishList/delete", async(
                [FromBody] DeleteWishListReqest reqest,
                IMediator mediator,
                CancellationToken cancellationToken) => 
            {
                var response = await mediator.Send(reqest, cancellationToken);
                return Results.Ok(response);
            });
        }
    }
}
