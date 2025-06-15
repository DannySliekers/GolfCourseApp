using CommunityToolkit.Mvvm.ComponentModel;
using GolfApp.Models;
using GolfApp.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace GolfApp.ViewModels
{
    public partial class BookingsViewModel : ObservableObject
    {
        public ObservableCollection<Booking> Bookings { get; set; } = new();

        private readonly IBookingService _bookingService;
        
        public BookingsViewModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task InitializeAsync()
        {
            try
            {
                var bookings = await _bookingService.GetAllBookingAsync();
                Bookings.Clear();
                foreach (var booking in bookings)
                {
                    Bookings.Add(booking);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to load bookings: {ex.Message}");
            }
        }
    }
}
