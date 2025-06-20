using GolfApp.Models;

namespace GolfApp.Services
{
    public interface IGolfCourseService
    {
        Task<List<GolfCourse>> GetAllGolfCoursesAsync();
        Task<int> AddGolfCourseAsync(GolfCourse course);
        Task<bool> AddImageToGolfCourseAsync(int id, string imageUrl);
        Task<bool> DeleteGolfCourseAsync(int id);
        Task<bool> EditGolfCourseAsync(GolfCourse course);
        Task<List<GolfCourse>> GetManagedGolfCoursesAsync();
    }
}
