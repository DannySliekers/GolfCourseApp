using GolfCourseWebAPI.Models;

namespace GolfCourseWebAPI.Repositories
{
    public interface IUserRepository
    {
        Task<int> SetRole(int userId, UserRole role);
    }
}
