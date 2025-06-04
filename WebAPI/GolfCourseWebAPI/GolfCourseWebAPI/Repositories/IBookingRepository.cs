using GolfCourseWebAPI.Models;

namespace GolfCourseWebAPI.Repositories
{
    public interface IBookingRepository
    {
        IEnumerable<Booking> GetAll();
        Booking Get(int id);
        Task<int> AddBooking(Booking booking);
        Task<int> UpdateBooking(Booking booking);
        Task<int> DeleteBooking(int id);
    }
}
