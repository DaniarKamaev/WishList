using MediatR;
using System;

namespace WishList.CreateList
{
    public record CreateListReqest(
        string Gift,
        string URL,
        int Price,
        int UserId) : IRequest<CreateListResponse>;
}
