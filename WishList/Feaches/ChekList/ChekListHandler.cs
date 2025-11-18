using MediatR;
using Microsoft.EntityFrameworkCore;
using WishList.DbModels;

namespace WishList.Feaches.ChekList
{
    public class ChekListHandler(SysContext db) : IRequestHandler<ChekListResponse, IEnumerable<User>>
    {
        public async Task<IEnumerable<User>> Handle(ChekListResponse request, CancellationToken cancellationToken)
        {
            var account = await db.Users
                .Include(u => u.WishLists)
                .Where(x => x.Id == request.ounerId)
                .Select(u => new User
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Birthday = u.Birthday,
                    Email = u.Email,
                    WishLists = u.WishLists.Select(w => new DbModels.WishList
                    {
                        WishListId = w.WishListId,
                        Gift = w.Gift,
                        Url = w.Url,
                        Price = w.Price,
                        Booked = w.Booked,
                        CreatedAt = w.CreatedAt
                    }).ToList()
                })
                .ToListAsync(cancellationToken);

            return account;
        }
    }
}
