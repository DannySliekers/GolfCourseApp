using GolfCourseWebAPI.Context;
using GolfCourseWebAPI.Models;
using GolfCourseWebAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GolfCourseWebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("set-role")]
        public async Task<IActionResult> SetRole(int userId, UserRole role)
        {
            if (!Enum.IsDefined(typeof(UserRole), role))
            {
                return BadRequest("Invalid role value.");
            }

            var result = await _userRepository.SetRole(userId, role);

            if (result == 0)
            {
                return NotFound($"User with ID {userId} not found.");
            }

            return Ok(new { message = "Role updated successfully." });
        }
    }
}
