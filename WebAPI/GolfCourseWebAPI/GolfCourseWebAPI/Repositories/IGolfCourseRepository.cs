using GolfCourseWebAPI.Models;

namespace GolfCourseWebAPI.Repositories
{
    public interface IGolfCourseRepository
    {
        IEnumerable<GolfCourse> GetAll();
        GolfCourse Get(int id);
        Task<int> AddGolfCourse(GolfCourse golfCourse);
        Task<int> AddGolfCourseImage(GolfCourseImage golfCourse);
        Task<int> UpdateGolfCourse(GolfCourse golfCourse);
        Task<int> DeleteGolfCourse(int id);
        List<string> GetImageUrls(int id);
    }
}
