namespace GolfApp.Models
{
    public sealed class DisplayedBooking
    {
        public int Id { get; set; }
        public string GolfCourseName { get; set; }
        public string MainBooker { get; set; }
        public DateTime StartTime { get; set; }
    }
}
