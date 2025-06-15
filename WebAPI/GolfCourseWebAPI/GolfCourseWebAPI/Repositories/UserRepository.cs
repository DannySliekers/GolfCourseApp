using GolfCourseWebAPI.Context;
using GolfCourseWebAPI.Models;

namespace GolfCourseWebAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly GolfCourseContext _context;

        public UserRepository(GolfCourseContext context) 
        {
            _context = context;    
        }

        public async Task<int> SetRole(int userId, UserRole role)
        {
            var user = _context.Users.FirstOrDefault(user => user.Id == userId);
            
            if (user != null)
            {
                user.Role = role;
            }

            _context.Users.Update(user);
            return await _context.SaveChangesAsync();
        }
    }
}
