using GolfApp.Models;

namespace GolfApp.Services
{
    public interface IBookingService
    {
        Task<bool> AddBookingAsync(Booking booking);
        Task<List<Booking>> GetAllBookingAsync();
    }
}
