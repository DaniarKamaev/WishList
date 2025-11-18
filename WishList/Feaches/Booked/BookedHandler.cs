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

         var wishListExists = await db.WishLists
            .AnyAsync(x => x.WishListId == request.id, cancellationToken);

            if (!wishListExists)
            {
                return new BookedResponse(false, "Такого элемента нету");
            }

            var elements = await db.WishLists.
                Where(x => x.WishListId == request.id).ToListAsync(cancellationToken);
            foreach (var element in elements) 
            {
                element.Booked = 1;
            }
            await db.SaveChangesAsync(cancellationToken);
            return new BookedResponse(true, "Успешно забронированн");
        }
    }
}
