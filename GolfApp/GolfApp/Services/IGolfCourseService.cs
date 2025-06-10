using GolfApp.Models;

namespace GolfApp.Services
{
    public interface IGolfCourseService
    {
        Task<List<GolfCourse>> GetAllGolfCoursesAsync();
    }
}
