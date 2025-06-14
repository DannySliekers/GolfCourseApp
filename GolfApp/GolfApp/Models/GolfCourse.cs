﻿namespace GolfApp.Models
{
    public sealed class GolfCourse
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
        public ImageSource FirstImage { get; set; }
    }
}
