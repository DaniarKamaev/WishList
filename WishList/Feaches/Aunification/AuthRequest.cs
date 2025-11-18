using MediatR;

namespace WishList.Feaches.Aunification
{
    public record AuthRequest(
        string userName,
        string password) : IRequest<AuthResponse>;
}
