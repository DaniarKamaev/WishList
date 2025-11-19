using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WishList.DbModels;

namespace WishList.Feaches.Booked
{
    public class BookedHandler(SysContext db) : IRequestHandler<BookedReqest, BookedResponse>
    {
        public async Task<BookedResponse> Handle(BookedReqest request, CancellationToken cancellationToken)
        {
            var validator = new BookedValidation();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BookedResponse(false, "Неконкретные данные");
            }
            var wishListExists = await db.WishLists
            .FirstOrDefaultAsync(x => x.WishListId == request.id, cancellationToken);

            if (wishListExists == null)
            {
                return new BookedResponse(false, "Такого элемента нету");
            }
            wishListExists.Booked = request.booked;
            
            await db.SaveChangesAsync(cancellationToken);
            return new BookedResponse(true, $"Статус успешно обновлен на {request.booked}");
        }
    }
}
