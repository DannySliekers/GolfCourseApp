namespace GolfApp.Models
{
    public sealed class Booking
    {
        public int Id { get; set; }
        public int GolfCourseId { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime StartTime { get; set; }
    }
}
