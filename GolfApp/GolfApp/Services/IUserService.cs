using GolfApp.Models;

namespace GolfApp.Services
{
    public interface IUserService
    {
        Task<User> GetLoggedInUser();
        Task<bool> SetUserAvatar(string avatarUrl);
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(int userId);
    }
}
