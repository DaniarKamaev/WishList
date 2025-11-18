using MediatR;

namespace WishList.Feaches.CreateUser
{
    public record CreateUserReqest (
        string UserName,
        string Password,
        DateOnly Birthday,
        string Emaail) : IRequest<CreateUserResponse>;
}
