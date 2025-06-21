using GolfApp.Models;

namespace GolfApp.Services
{
    public interface IBookingService
    {
        Task<Booking> AddBookingAsync(Booking booking);
        Task<List<Booking>> GetAllBookingAsync();
        Task<List<Booking>> GetUserBookingsAsync();
        Task<List<int>> GetUserIdsForBookingAsync(int bookingId);
        Task<bool> AddLoggedInUserToBookingAsync(int bookingId);
        Task<bool> AddUserToBookingAsync(int bookingId, int userId);
        Task<bool> RemoveUserFromBookingAsync(int bookingId, int userId);
        Task<bool> DeleteBookingAsync(int bookingId);
    }
}
