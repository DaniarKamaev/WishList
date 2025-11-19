using FluentValidation;
using MediatR;
using System.Security.Claims;
using WishList.DbModels;
using WishList.Feaches.CreateList;

public class CreateListHandler : IRequestHandler<CreateListReqest, CreateListResponse>
{
    private readonly SysContext _db;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateListHandler(SysContext db, IHttpContextAccessor httpContextAccessor)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<CreateListResponse> Handle(CreateListReqest request, CancellationToken cancellationToken)
    {
        var validator = new CreateValidation();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var userId = GetCurrentUserId();

        if (userId == 0)
        {
            throw new UnauthorizedAccessException("Пользователь нн авторизован");
        }

        var gift = new WishList.DbModels.WishList
        {
            Gift = request.Gift,
            Url = request.URL,
            Booked = 0,
            Price = request.Price,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        await _db.WishLists.AddAsync(gift, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        return new CreateListResponse(gift.WishListId, "Id Подарка", gift.UserId, "ID Пользователя");
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