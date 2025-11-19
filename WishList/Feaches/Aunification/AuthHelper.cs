// AuthorizationHelper.cs
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WishList.DbModels;

namespace WishList.Feaches.Authorization
{
    public static class AuthHelper
    {
        public static int GetCurrentUserId(ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                             ?? user.FindFirst("UserId")?.Value;

            if (int.TryParse(userIdClaim, out int userId))
            {
                return userId;
            }

            throw new UnauthorizedAccessException("User ID not found in token");
        }

        public static async Task<bool> IsWishListOwnerAsync(int wishListId, int userId, SysContext db)
        {
            var wishList = await db.WishLists
                .FirstOrDefaultAsync(w => w.WishListId == wishListId);

            return wishList != null && wishList.UserId == userId;
        }
    }
}
