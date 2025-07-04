﻿namespace GolfCourseWebAPI.Models
{
    public class GolfCourse
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public TimeOnly BookingStartTime { get; set; }
        public TimeOnly BookingLastStartTime { get; set; }
        public int StartTimeIntervalMinutes { get; set; }
        public int AmountOfHoles { get; set; }
        public int AmountOfCourses { get; set; }
        public string Address { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; }
        public decimal? Price { get; set; }
    }
}
