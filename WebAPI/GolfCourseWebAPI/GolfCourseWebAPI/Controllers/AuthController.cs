using GolfCourseWebAPI.Context;
using GolfCourseWebAPI.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LoginRequest = GolfCourseWebAPI.Models.LoginRequest;
using RegisterRequest = GolfCourseWebAPI.Models.RegisterRequest;

namespace GolfCourseWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly GolfCourseContext _context;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger, GolfCourseContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.Username);

            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid username or password." });
            }

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.Hash);

            if (!isPasswordValid)
            {
                return Unauthorized(new { Message = "Invalid username or password." });
            }

            var token = GenerateJwtToken(user.UserName);

            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                _logger.LogInformation("Register request made");

                if (await _context.Users.AnyAsync(u => u.UserName == request.Username))
                {
                    return BadRequest(new { Message = "Username is already taken." });
                }

                if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                {
                    return BadRequest(new { Message = "Email is already in use." });
                }

                var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

                var user = new User
                {
                    UserName = request.Username,
                    Email = request.Email,
                    Hash = passwordHash,
                    Role = UserRole.Player
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "User registered successfully." });
            } 
            catch (Exception e)
            {
                _logger.LogError(e, "Error while processing register request");
                return BadRequest();
            }
        }

        private string GenerateJwtToken(string username)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-very-strong-secret-keyqwqwwqqwqwqw"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "your-issuer",
                audience: "your-audience",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
