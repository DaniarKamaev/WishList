using DodoPizza.Feaches.Autification;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WishList.DbModels;

namespace WishList.Feaches.Registration
{
    public class RegHandler : IRequestHandler<RegRequest, RegResponse>
    {
        private readonly SysContext _db;

        public RegHandler(SysContext db)
        {
            _db = db;
        }
        public async Task<RegResponse> Handle(RegRequest request, CancellationToken cancellationToken)
        {
            string password = HashCreater.HashPassword(request.password);

            var userDouble = await _db.Users
                .FirstOrDefaultAsync(x => x.UserName == request.userName);
            if (userDouble != null)
                return new RegResponse(0, false, "Такой пользователь уже есть");
            var user = new User
            {
                UserName = request.userName,
                UserPassword = password,
                Birthday = request.birthday,
                Email = request.email,
            };
            await _db.Users.AddAsync(user, cancellationToken);
            await _db.SaveChangesAsync();
            return new RegResponse(user.Id, true, "Пользователь успешно зарегестрирован");
        }
    }
}
