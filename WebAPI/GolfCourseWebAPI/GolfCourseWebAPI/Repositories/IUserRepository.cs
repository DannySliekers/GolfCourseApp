using GolfCourseWebAPI.Models;

namespace GolfCourseWebAPI.Repositories
{
    public interface IUserRepository
    {
        Task<int> SetRole(int userId, UserRole role);
        User GetUser(int userId);
        List<User> GetAllUsers();
        Task<int> SetAvatar(int userId, string avatarUrl);
    }
}
