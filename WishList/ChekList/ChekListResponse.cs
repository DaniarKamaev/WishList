using MediatR;
using WishList.DbModels;
namespace WishList.ChekList
{
    public record ChekListResponse(int ounerId) : IRequest<IEnumerable<User>>;
}
