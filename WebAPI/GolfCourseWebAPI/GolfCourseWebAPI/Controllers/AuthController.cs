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
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger, GolfCourseContext context)
        {
            _context = context;
            _logger = logger;
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
    }
}
