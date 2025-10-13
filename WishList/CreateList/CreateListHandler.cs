using MediatR;
using WishList.DbModels;

namespace WishList.CreateList
{
    public class CreateListHandler(SysContext db) : IRequestHandler<CreateListReqest, CreateListResponse>
    {
        public async Task<CreateListResponse> Handle(CreateListReqest request, CancellationToken cancellationToken)
        {
            var gift = new WishList.DbModels.WishList
            {
                Gift = request.Gift,
                Url = request.URL,
                Booked = 0,
                Price = request.Price,
                UserId = request.UserId, 
                CreatedAt = DateTime.UtcNow
            };

            await db.WishLists.AddAsync(gift);
            await db.SaveChangesAsync(cancellationToken);
            return new CreateListResponse(gift.WishListId, "Id Подарка", gift.UserId, "ID Пользователя");
        }
    }
}
