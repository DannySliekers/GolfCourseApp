using GolfCourseWebAPI.Context;
using GolfCourseWebAPI.Models;
using System.Diagnostics;

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
            await _context.SaveChangesAsync();

            // Also add the user that created the booking to the booking itself.
            var bookingUsers = new BookingsUsers()
            {
                BookingId = booking.Id,
                UserId = booking.CreatedByUserId
            };

            _context.BookingsUsers.Add(bookingUsers);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddUserToBooking(int bookingId, int userId)
        {
            var userCount = _context.BookingsUsers.Where(x => x.BookingId == bookingId);

            if (userCount.Count() >= 4)
            {
                return 0;
            }

            var bookingsUsers = new BookingsUsers()
            {
                BookingId = bookingId,
                UserId = userId
            };

            _context.BookingsUsers.Add(bookingsUsers);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveUserFromBooking(int bookingId, int userId)
        {
            var bookingUser = _context.BookingsUsers.FirstOrDefault(b => b.UserId == userId && b.BookingId == bookingId);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteBooking(int id)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.Id == id);
            var bookingUsers = _context.BookingsUsers.Where(bu => bu.BookingId == id).ToList();

            _context.BookingsUsers.RemoveRange(bookingUsers);
            await _context.SaveChangesAsync();
            _context.Bookings.Remove(booking);

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

        public IEnumerable<Booking> GetAllUserBookings(int userId)
        {
            var userBookings = _context.BookingsUsers
                .Where(x => x.UserId == userId)
                .ToList();

            List<Booking> correspondingBookings = new();

            foreach (var userBooking in userBookings)
            {
                correspondingBookings.Add(
                    _context.Bookings.First(x => x.Id == userBooking.BookingId)
                );
            }

            return correspondingBookings;
        }

        public IEnumerable<int> GetUserIds(int bookingId)
        {
            return _context.BookingsUsers.Where(x => x.BookingId == bookingId).Select(x => x.UserId);
        }

        public async Task<int> UpdateBooking(Booking booking)
        {
            booking.StartTime = DateTime.SpecifyKind(booking.StartTime.ToUniversalTime(), DateTimeKind.Utc);
            _context.Bookings.Update(booking);
            return await _context.SaveChangesAsync();
        }
    }
}
