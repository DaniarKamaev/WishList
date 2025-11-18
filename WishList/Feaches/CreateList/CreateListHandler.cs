using MediatR;
using FluentValidation;
using WishList.DbModels;

namespace WishList.Feaches.CreateList
{
    public class CreateListHandler(SysContext db) : IRequestHandler<CreateListReqest, CreateListResponse>
    {
        public async Task<CreateListResponse> Handle(CreateListReqest request, CancellationToken cancellationToken)
        {
            var validator = new CreateValidation();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var gift = new DbModels.WishList
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
