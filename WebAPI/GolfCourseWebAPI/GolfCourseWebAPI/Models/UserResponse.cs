﻿namespace GolfCourseWebAPI.Models
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string? AvatarUrl { get; set; }
    }
}
