namespace GolfCourseWebAPI.Models
{
    public enum UserRole
    {
        Player,
        Manager,
        Admin
    }

    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Hash { get; set; }
        public UserRole Role { get; set; }
        public string? AvatarUrl { get; set; }
    }
}
