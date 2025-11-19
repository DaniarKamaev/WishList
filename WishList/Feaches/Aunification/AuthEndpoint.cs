using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WishList.DbModels;
using WishList.Feaches.Authorization;

namespace WishList.Feaches.Aunification
{
    public static class AuthEndpoint
    {
        public static void AuthMap(this IEndpointRouteBuilder app)
        {
            app.MapPost("wishlist/auth", async (
                [FromBody] AuthRequest request,
                IMediator mediator,
                CancellationToken cancellationToken) => 
            {
                try
                {
                    var response = await mediator.Send(request, cancellationToken);
                    return Results.Ok(response);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });
        }
    }
}
