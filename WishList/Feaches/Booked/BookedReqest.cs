using MediatR;

namespace WishList.Feaches.Booked
{
    public record BookedReqest(
        int id,
        sbyte booked
        ) : IRequest<BookedResponse>;
}
