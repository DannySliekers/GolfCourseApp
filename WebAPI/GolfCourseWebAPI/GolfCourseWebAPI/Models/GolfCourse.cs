namespace GolfCourseWebAPI.Models
{
    public class GolfCourse
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime BookingStartTime { get; set; }
        public DateTime BookingLastStartTime { get; set; }
        public int StartTimeIntervalMinutes { get; set; }
    }
}
