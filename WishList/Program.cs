using Microsoft.EntityFrameworkCore;
using WishList.DbModels;
using WishList.Feaches.Aunification;
using WishList.Feaches.Booked;
using WishList.Feaches.ChekList;
using WishList.Feaches.CreateList;
using WishList.Feaches.CreateUser;
using WishList.Feaches.DeleteWishList;
using WishList.Feaches.Registration;

var builder = WebApplication.CreateBuilder(args);

// Регистрация сервисов
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Регистрация DbContext с повторными попытками
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SysContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
        mySqlOptions => mySqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null)
    ));

// Регистрация MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();

app.CreateEndpoint();
app.CreateUserMap();
app.ChekListMap();
app.BookedEndpointMap();
app.DeleteWishListMap();
app.AuthMap();
app.RegMap();

// Конфигурация pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


app.Run("http://0.0.0.0:80");