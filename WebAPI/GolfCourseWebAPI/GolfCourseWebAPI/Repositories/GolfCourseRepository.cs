using GolfCourseWebAPI.Context;
using GolfCourseWebAPI.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<int> AddGolfCourseImage(GolfCourseImage golfCourseImage)
        {
            var golfCourse = await _context.GolfCourses
                .FirstOrDefaultAsync(x => x.Id == golfCourseImage.GolfCourseId);

            if (golfCourse == null)
            {
                return -1;
            }

            _context.GolfCourseImages.Add(golfCourseImage);
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

        public IEnumerable<GolfCourse> GetByOwnerId(int ownerId)
        {
            return _context.GolfCourses
                           .Where(gc => gc.OwnerId == ownerId)
                           .ToList();
        }

        public List<string> GetImageUrls(int id)
        {
            return _context.GolfCourseImages.Where(x => x.GolfCourseId == id).Select(image => image.Url).ToList();
        }

        public async Task<int> UpdateGolfCourse(GolfCourse golfCourse)
        {
            _context.GolfCourses.Update(golfCourse);
            return await _context.SaveChangesAsync();
        }
    }
}
