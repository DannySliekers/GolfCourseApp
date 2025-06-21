using GolfCourseWebAPI.Context;
using GolfCourseWebAPI.Models;
using System.Data;

namespace GolfCourseWebAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly GolfCourseContext _context;

        public UserRepository(GolfCourseContext context) 
        {
            _context = context;    
        }

        public User GetUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(user => user.Id == userId);
            user.Hash = string.Empty;
            return user;
        }

        public List<User> GetAllUsers()
        {
            var users = _context.Users.ToList();

            foreach (var user in users)
            {
                user.Hash = string.Empty;
            }

            return users;
        }

        public async  Task<int> SetAvatar(int userId, string avatarUrl)
        {
            var user = _context.Users.FirstOrDefault(user => user.Id == userId);

            if (user != null)
            {
                user.AvatarUrl = avatarUrl;
            }

            _context.Users.Update(user);
            return await _context.SaveChangesAsync();
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
