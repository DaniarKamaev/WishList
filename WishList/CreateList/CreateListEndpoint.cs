using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace WishList.CreateList
{
    public static class CreateListEndpoint
    {
        public static void CreateEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("wishlist/create", async (
                [FromBody] CreateListReqest reqest,
                CancellationToken cancellationToken,
                IMediator mediator) =>
            {
                var response = await mediator.Send(reqest, cancellationToken);
                return Results.Ok(response);
            });

        }
    }
}
