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

        [HttpPost("set-avatar")]
        public async Task<IActionResult> SetAvatar(int userId, string avatarUrl)
        {
            if (string.IsNullOrEmpty(avatarUrl))
            {
                return BadRequest("Invalid avatarUrl value.");
            }

            var result = await _userRepository.SetAvatar(userId, avatarUrl);

            if (result == 0)
            {
                return NotFound($"User with ID {userId} not found.");
            }

            return Ok(new { message = "Avatar URL updated successfully." });
        }

        [HttpGet("{id}")]
        public UserResponse GetUser(int id)
        {
            var user = _userRepository.GetUser(id);
            var userResponse = new UserResponse()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                AvatarUrl = user.AvatarUrl
            };

            return userResponse;
        }

        [HttpGet]
        public List<UserResponse> GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();
            List<UserResponse> userResponses = [];

            foreach (var user in users)
            {
                var userResponse = new UserResponse()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    AvatarUrl = user.AvatarUrl
                };

                userResponses.Add(userResponse);
            }

            return userResponses;
        }
    }
}
