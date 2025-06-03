using GolfCourseWebAPI.Context;
using GolfCourseWebAPI.Models;

namespace GolfCourseWebAPI.Repositories
{
    public class GolfCourseRepository : IGolfCourseRepository
    {
        private readonly GolfCourseContext _context;

        public GolfCourseRepository(GolfCourseContext context)
        {
            _context = context;
        }

        public async Task<int> AddGolfCourse(GolfCourse golfCourse)
        {
            _context.GolfCourses.Add(golfCourse);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteGolfCourse(int id)
        {
            var golfCourse = _context.GolfCourses.FirstOrDefault(gc => gc.Id == id);
            _context.GolfCourses.Remove(golfCourse);
            return await _context.SaveChangesAsync();
        }

        public GolfCourse Get(int id)
        {
            return _context.GolfCourses.Find(id);
        }

        public IEnumerable<GolfCourse> GetAll()
        {
            return _context.GolfCourses.ToList();
        }

        public async Task<int> UpdateGolfCourse(GolfCourse golfCourse)
        {
            _context.GolfCourses.Update(golfCourse);
            return await _context.SaveChangesAsync();
        }
    }
}
