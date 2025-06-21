using System.Text.Json.Serialization;

namespace GolfApp.Models
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
        public int AmountOfHoles { get; set; }
        public int AmountOfCourses { get; set; }
        public string Address { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; }
        public decimal? Price { get; set; }
        [JsonIgnore]
        public ImageSource FirstImage { get; set; }
    }
}
