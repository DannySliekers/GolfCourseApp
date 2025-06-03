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
        public int id { get; set; }
        public string email { get; set; }
        public string user_name { get; set; }
        public string hash { get; set; }
        public UserRole role { get; set; }
        public string? avatar_url { get; set; }
    }
}
