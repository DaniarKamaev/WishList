using MediatR;

namespace WishList.Feaches.Registration
{
    public record RegRequest(
        string userName,
        string password,
        DateOnly birthday,
        string email) : IRequest<RegResponse>;
}
