using GolfCourseWebAPI.Context;
using GolfCourseWebAPI.Models;

namespace GolfCourseWebAPI.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly GolfCourseContext _context;

        public BookingRepository(GolfCourseContext context)
        {
            _context = context;
        }

        public async Task<int> AddBooking(Booking booking)
        {
             booking.StartTime = DateTime.SpecifyKind(booking.StartTime.ToUniversalTime(), DateTimeKind.Utc);
            _context.Bookings.Add(booking);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteBooking(int id)
        {
            var Booking = _context.Bookings.FirstOrDefault(b => b.Id == id);
            _context.Bookings.Remove(Booking);
            return await _context.SaveChangesAsync();
        }

        public Booking Get(int id)
        {
            return _context.Bookings.Find(id);
        }

        public IEnumerable<Booking> GetAll()
        {
            return _context.Bookings.ToList();
        }

        public async Task<int> UpdateBooking(Booking booking)
        {
            booking.StartTime = DateTime.SpecifyKind(booking.StartTime.ToUniversalTime(), DateTimeKind.Utc);
            _context.Bookings.Update(booking);
            return await _context.SaveChangesAsync();
        }
    }
}
