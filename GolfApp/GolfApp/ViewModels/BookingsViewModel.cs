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
        public ObservableCollection<DisplayedBooking> DisplayedBookings { get; set; } = new();
        private List<Booking> bookings = [];

        private readonly IBookingService _bookingService;
        private readonly IUserService _userService;
        private readonly IGolfCourseService _golfCourseService;
        
        public BookingsViewModel(IBookingService bookingService, IUserService userService, IGolfCourseService golfCourseService)
        {
            _bookingService = bookingService;
            _userService = userService;
            _golfCourseService = golfCourseService;
        }

        public async Task InitializeAsync()
        {
            try
            {
                bookings = await _bookingService.GetUserBookingsAsync();
                DisplayedBookings.Clear();
                
                foreach (var booking in bookings)
                {
                    GolfCourse golfcourse = await _golfCourseService.GetGolfCourseById(booking.GolfCourseId);
                    User mainBooker = await _userService.GetUserById(booking.CreatedByUserId);

                    var displayedBooking = new DisplayedBooking()
                    {
                        Id = booking.Id,
                        GolfCourseName = golfcourse.Name,
                        MainBooker = mainBooker.UserName,
                        StartTime = booking.StartTime
                    };

                    DisplayedBookings.Add(displayedBooking);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to load bookings: {ex.Message}");
            }
        }

        [RelayCommand]
        private async Task DeleteBookingAsync(DisplayedBooking displayedBooking)
        {
            string userId = await TokenHelper.GetUserId();
            var booking = bookings.FirstOrDefault(x => x.Id == displayedBooking.Id);

            if (Int32.TryParse(userId, out int userIdInt))
            {
                if (booking.CreatedByUserId == userIdInt)
                {
                    var success = await _bookingService.DeleteBookingAsync(booking.Id);

                    if (success)
                    {
                        DisplayedBookings.Remove(displayedBooking);
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Unable to delete tee time. Reason: You are not the main booker of this tee time", "OK");
                }
            }
        }

        [RelayCommand]
        private async Task DeleteUserFromBookingAsync(DisplayedBooking displayedBooking)
        {
            string userId = await TokenHelper.GetUserId();
            var booking = bookings.FirstOrDefault(x => x.Id == displayedBooking.Id);

            if (Int32.TryParse(userId, out int userIdInt))
            {
                if (booking.CreatedByUserId != userIdInt)
                {
                    var success = await _bookingService.RemoveUserFromBookingAsync(booking.Id, userIdInt);

                    if (success)
                    {
                        DisplayedBookings.Remove(displayedBooking);
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Unable to remove yourself from tee time. Reason: You are the main booker of this tee time", "OK");
                }
            }
        }
    }
}
