using MediatR;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace WishList.Feaches.Booked
{
    public static class BookedEndpoint
    {
        public static void BookedEndpointMap(this IEndpointRouteBuilder app)
        {
            app.MapPost("wishlist/booked", async (
                [FromBody] BookedReqest reqest,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                try
                {
                    var response = await mediator.Send(reqest, cancellationToken);
                    return Results.Ok(response);
                }
                catch (ValidationException ex)
                {
                    return Results.BadRequest(ex);
                }
                catch (UnauthorizedAccessException)
                {
                    return Results.Unauthorized();
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex);
                }
                
            })
            .RequireAuthorization()
            .WithName("UpdateBookingStatus")
            .WithTags("WishLists")
            .WithSummary("Обновление статуса бронирования")
            .WithDescription("Изменяет статус бронирования подарка (0 - свободен, 1 - забронирован)")
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Обновление статуса бронирования"
            });
        }
    }
}
