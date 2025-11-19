using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WishList.DbModels;

namespace WishList.Feaches.DeleteWishList
{
    public class DeleteWishListHandler : IRequestHandler<DeleteWishListReqest, DeleteWishListResponse>
    {
        private readonly SysContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteWishListHandler(SysContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<DeleteWishListResponse> Handle(DeleteWishListReqest request, CancellationToken cancellationToken)
        {
            var userId = GetCurrentUserId();
            if (userId == 0)
            {
                throw new UnauthorizedAccessException("Пользователь нн авторизован");
            }
            var wishList = await _db.WishLists
            .FirstOrDefaultAsync(x => x.WishListId == request.WishListId, cancellationToken);

            if (wishList == null)
            {
                return new DeleteWishListResponse(false, "Такого элемента нет");
            }
            if (wishList.UserId != userId)
            {
                return new DeleteWishListResponse(false, "Недостаточно прав для удаления этого вишлиста");
            }

            _db.WishLists.Remove(wishList);
            await _db.SaveChangesAsync(cancellationToken);

            return new DeleteWishListResponse(true, "Успешно Удалено");
        }
        private int GetCurrentUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null)
                return 0;

            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                             ?? user.FindFirst("UserId")?.Value;

            if (int.TryParse(userIdClaim, out int userId))
            {
                return userId;
            }

            return 0;
        }
    }
}
