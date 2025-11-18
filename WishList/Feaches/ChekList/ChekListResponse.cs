using MediatR;
using WishList.DbModels;
namespace WishList.Feaches.ChekList
{
    public record ChekListResponse(int ounerId) : IRequest<IEnumerable<User>>;
}
