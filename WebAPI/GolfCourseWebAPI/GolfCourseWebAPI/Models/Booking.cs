namespace GolfCourseWebAPI.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int GolfCourseId { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime StartTime { get; set; }
    }
}
