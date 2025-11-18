using MediatR;
using Microsoft.EntityFrameworkCore;
using WishList.DbModels;

namespace WishList.Feaches.DeleteWishList
{
    public class DeleteWishListHandler(SysContext db) : IRequestHandler<DeleteWishListReqest, DeleteWishListResponse>
    {
        public async Task<DeleteWishListResponse> Handle(DeleteWishListReqest request, CancellationToken cancellationToken)
        {
            var wishListExists = await db.WishLists
           .AnyAsync(x => x.WishListId == request.WishListId, cancellationToken);

            if (!wishListExists)
            {
                return new DeleteWishListResponse(false, "Такого элемента нету");
            }

            var elements = await db.WishLists.
                Where(x => x.WishListId == request.WishListId).ToListAsync(cancellationToken);
            foreach (var element in elements)
            {
                db.WishLists.Remove(element);
            }
            await db.SaveChangesAsync(cancellationToken);

            return new DeleteWishListResponse(true, "Успешно Удалено");
        }
    }
}
