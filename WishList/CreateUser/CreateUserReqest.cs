using MediatR;
using WishList.CreateList;

namespace WishList.CreateUser
{
    public record CreateUserReqest (
        string UserName,
        string Password,
        DateOnly Birthday,
        string Emaail) : IRequest<CreateUserResponse>;
}
