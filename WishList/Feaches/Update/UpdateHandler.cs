using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WishList.DbModels;

namespace WishList.Feaches.Update
{
    public class UpdateHandler : IRequestHandler<UpdateRequest, UpdateResponse>
    {
        private readonly SysContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateHandler(SysContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<UpdateResponse> Handle(UpdateRequest request, CancellationToken cancellationToken)
        {
            int userId = GetCurrentUserId();
            if (userId == 0)
                return new UpdateResponse(false, "Вы не авторизированы");

            var user = await _db.WishLists
                .FirstOrDefaultAsync(x => x.WishListId == request.WishListId, cancellationToken);
            if (user == null)
                return new UpdateResponse(false, "Такого пользователя нету");
            if (user.UserId != userId)
                return new UpdateResponse(false, "Недостаточно прав");

            user.Url = request.URL;
            user.Price = request.Price;
            user.Gift = request.Gift;

            await _db.SaveChangesAsync(cancellationToken);
            return new UpdateResponse(true, "Успешно обновлено");

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
