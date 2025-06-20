using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GolfApp.Helpers;
using GolfApp.Models;
using GolfApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
                var bookings = await _bookingService.GetUserBookingsAsync();
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

        [RelayCommand]
        private async Task DeleteBookingAsync(Booking booking)
        {
            string userId = await TokenHelper.GetUserId();

            if (Int32.TryParse(userId, out int userIdInt))
            {
                if (booking.CreatedByUserId == userIdInt)
                {
                    var success = await _bookingService.DeleteBookingAsync(booking.Id);

                    if (success)
                    {
                        Bookings.Remove(booking);
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Unable to delete tee time. Reason: You are not the main booker of this tee time", "OK");
                }
            }
        }
    }
}
