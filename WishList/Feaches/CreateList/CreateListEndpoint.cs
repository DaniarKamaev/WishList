using MediatR;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace WishList.Feaches.CreateList
{
    public static class CreateListEndpoint
    {
        public static void CreateEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("wishlist/listcreate", async (
                [FromBody] CreateListReqest reqest,
                CancellationToken cancellationToken,
                IMediator mediator) =>
            {
                try
                {
                    var response = await mediator.Send(reqest, cancellationToken);
                    return Results.Ok(response);
                } catch (ValidationException ex)
                {
                    return Results.BadRequest(ex);
                } catch (Exception ex)
                {
                    return Results.BadRequest(ex);
                }
                
            });

        }
    }
}
