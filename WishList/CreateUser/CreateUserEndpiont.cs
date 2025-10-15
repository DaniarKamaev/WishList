using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WishList.CreateUser
{
    public static class CreateUserEndpiont
    {
        public static void CreateUserMap(this IEndpointRouteBuilder app)
        {
            
        app.MapPost("wishlist/usercreate", async (
            [FromBody] CreateUserReqest reqest,
            IMediator mediator,
            CancellationToken cancellationToken) =>
            {
               var result = await mediator.Send(reqest, cancellationToken);
               return Results.Ok(result);
            });
        }
    }
}
