using GolfCourseWebAPI.Models;

namespace GolfCourseWebAPI.Repositories
{
    public interface IUserRepository
    {
        Task<int> SetRole(int userId, UserRole role);
        User GetUser(int userId);
        Task<int> SetAvatar(int userId, string avatarUrl);
    }
}
