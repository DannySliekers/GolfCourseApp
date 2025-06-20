using GolfCourseWebAPI.Models;

namespace GolfCourseWebAPI.Repositories
{
    public interface IBookingRepository
    {
        IEnumerable<Booking> GetAll();
        IEnumerable<Booking> GetAllUserBookings(int userId);
        Booking Get(int id);
        IEnumerable<int> GetUserIds(int bookingId);
        Task<int> AddBooking(Booking booking);
        Task<int> AddUserToBooking(int bookingId, int userId);
        Task<int> UpdateBooking(Booking booking);
        Task<int> DeleteBooking(int id);
        Task<int> RemoveUserFromBooking(int bookingId, int userId);
    }
}
