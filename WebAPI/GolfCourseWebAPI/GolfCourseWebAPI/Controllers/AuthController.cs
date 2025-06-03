using GolfCourseWebAPI.Context;
using GolfCourseWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegisterRequest = GolfCourseWebAPI.Models.RegisterRequest;

namespace GolfCourseWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly GolfCourseContext _context;

        public AuthController(GolfCourseContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (await _context.users.AnyAsync(u => u.user_name == request.Username))
            {
                return BadRequest(new { Message = "Username is already taken." });
            }

            if (await _context.users.AnyAsync(u => u.email == request.Email))
            {
                return BadRequest(new { Message = "Email is already in use." });
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                user_name = request.Username,
                email = request.Email,
                hash = passwordHash,
                role = UserRole.Player
            };

            _context.users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "User registered successfully." });
        }
    }
}
