using MediatR;
using System;

namespace WishList.Feaches.CreateList
{
    public record CreateListReqest(
    string Gift,
    string URL,
    int Price,
    int UserId = 0) : IRequest<CreateListResponse>;
}
