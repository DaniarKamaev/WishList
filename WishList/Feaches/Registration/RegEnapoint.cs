using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WishList.Feaches.Registration
{
    public static class RegEnapoint
    {
        public static void RegMap(this IEndpointRouteBuilder app)
        {
            app.MapPost("wishlist/reg", async (
                [FromBody] RegRequest request,
                IMediator mediator,
                CancellationToken token) =>
            {
                try
                {
                    var response = await mediator.Send(request, token);
                    return Results.Ok(response);
                }
                catch (Exception ex)
                { 
                    return Results.BadRequest(ex.Message);
                }
            })
            .WithName("RegisterUser")
            .WithTags("Authentication")
            .WithSummary("Регистрация нового пользователя")
            .WithDescription("Создает нового пользователя в системе")
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Регистрация пользователя"
            }); ;
        }
    }
}
