using MediatR;

namespace WishList.Feaches.Update
{
    public record UpdateRequest(
        int WishListId,
        string Gift,
        string URL,
        int Price) : IRequest<UpdateResponse>;
}
