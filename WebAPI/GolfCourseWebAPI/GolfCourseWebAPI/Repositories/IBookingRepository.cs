using GolfCourseWebAPI.Models;

namespace GolfCourseWebAPI.Repositories
{
    public interface IBookingRepository
    {
        IEnumerable<Booking> GetAll();
        Booking Get(int id);
        IEnumerable<int> GetUserIds(int bookingId);
        Task<int> AddBooking(Booking booking);
        Task<int> AddUserToBooking(int bookingId, int userId);
        Task<int> UpdateBooking(Booking booking);
        Task<int> DeleteBooking(int id);
    }
}
