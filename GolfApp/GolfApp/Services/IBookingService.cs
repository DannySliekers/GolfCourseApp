using GolfApp.Models;

namespace GolfApp.Services
{
    public interface IBookingService
    {
        Task<bool> AddBookingAsync(Booking booking);
        Task<List<Booking>> GetAllBookingAsync();
        Task<List<Booking>> GetUserBookingsAsync();
        Task<List<int>> GetUserIdsForBookingAsync(int bookingId);
        Task<bool> AddUserToBookingAsync(int bookingId);
        Task<bool> DeleteBookingAsync(int bookingId);
    }
}
