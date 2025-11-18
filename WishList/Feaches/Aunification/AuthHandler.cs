using DodoPizza.Feaches.Autification;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WishList.DbModels;

namespace WishList.Feaches.Aunification
{
    public class AuthHandler : IRequestHandler<AuthRequest, AuthResponse>
    {
        private readonly SysContext _db;
        private readonly IConfiguration _configuration;

        public AuthHandler(SysContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public async Task<AuthResponse> Handle(AuthRequest request, CancellationToken cancellationToken)
        {
            var user = await _db.Users
        .FirstOrDefaultAsync(x => x.UserName == request.userName, cancellationToken);

            if (user == null)
                return new AuthResponse(null, false, "Неверный логин");

            bool isPasswordValid = HashCreater.VerifyPassword(request.password, user.UserPassword);

            if (isPasswordValid)
            {
                var token = GenerateJwtToken(user);
                return new AuthResponse(token, true, "Аутентификация успешно пройдена");
            }

            return new AuthResponse(null, false, "Неверный пароль");
        }
        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("UserId", user.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpireMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
