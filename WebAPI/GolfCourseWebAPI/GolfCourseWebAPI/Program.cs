using GolfCourseWebAPI.Context;
using GolfCourseWebAPI.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers();
builder.Services.AddDbContext<GolfCourseContext>();
builder.Services.AddScoped<IGolfCourseRepository, GolfCourseRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "your-issuer",
            ValidAudience = "your-audience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-very-strong-secret-keyqwqwwqqwqwqw"))
        };
    });
var app = builder.Build();


app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles();
app.Run();
