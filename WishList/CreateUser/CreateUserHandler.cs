using MediatR;
using WishList.DbModels;
namespace WishList.CreateUser
{
    public class CreateUserHandler(SysContext db) : IRequestHandler<CreateUserReqest, CreateUserResponse>
    {
        public async Task<CreateUserResponse> Handle(CreateUserReqest request, CancellationToken cancellationToken)
        {
            var psaawordHash = request.Password.GetHashCode();
            var user = new User
            {
                UserName = request.UserName,
                UserPassword = $"{psaawordHash}",
                Email = request.Emaail,
                Birthday = request.Birthday,
            };
            await db.Users.AddAsync(user, cancellationToken);
            await db.SaveChangesAsync();
            return new CreateUserResponse(user.Id, $"Ваш Id {user.UserPassword}");
        }
    }
}
