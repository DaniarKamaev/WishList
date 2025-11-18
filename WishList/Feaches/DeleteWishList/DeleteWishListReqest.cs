using MediatR;

namespace WishList.Feaches.DeleteWishList
{
    public record DeleteWishListReqest(
        int WishListId) : IRequest<DeleteWishListResponse>;
}
