namespace GolfCourseWebAPI.Models
{
    public class GolfCourseImage
    {
        public int Id { get; set; }
        public int GolfCourseId { get; set; }
        public string Url { get; set; }
    }
}
