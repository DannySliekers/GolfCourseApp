using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GolfApp.Helpers;
using GolfApp.Models;
using GolfApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GolfApp.ViewModels
{
    public partial class BookTeeTimeViewModel : ObservableObject
    {
        [ObservableProperty]
        private DateTime selectedDate = DateTime.Today;

        public DateTime MaxSelectableDate => DateTime.Today.AddMonths(1);
        public GolfCourse GolfCourse { get; set; }

        [ObservableProperty]
        private ObservableCollection<DisplayedTeeTime> availableTeeTimes;

        private readonly IBookingService _bookingService;
        
        public BookTeeTimeViewModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        partial void OnSelectedDateChanged(DateTime oldValue, DateTime newValue)
        {
            _ = GenerateTeeTimes(GolfCourse.BookingStartTime, GolfCourse.BookingLastStartTime, GolfCourse.StartTimeIntervalMinutes);
        }

        public async Task GenerateTeeTimes(TimeOnly start, TimeOnly end, int intervalMinutes)
        {
            var list = new ObservableCollection<DisplayedTeeTime>();
            var time = start;
            List<Booking> bookings = await _bookingService.GetAllBookingAsync();

            TimeZoneInfo amsterdamTimeZone;

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                amsterdamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
                
            }
            else
            {
                amsterdamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Amsterdam");
            }

            while (time <= end)
            {
                var displayBooking = new DisplayedTeeTime();
                var correspondingBooking = bookings
                    .Where(x => x.StartTime.Date == selectedDate.Date &&
                                TimeOnly.FromDateTime(TimeZoneInfo.ConvertTimeFromUtc(x.StartTime, amsterdamTimeZone)) == time)
                    .FirstOrDefault();

                if (correspondingBooking != null)
                {
                    displayBooking.BookingId = correspondingBooking.Id;

                    var users = await _bookingService.GetUserIdsForBookingAsync(correspondingBooking.Id);
                    displayBooking.UserCount = users.Count;

                }

                displayBooking.Time = time;

                list.Add(displayBooking);
                time = time.AddMinutes(intervalMinutes);
            }
            
            list.Add(new DisplayedTeeTime(){ BookingId = 2, Time = TimeOnly.MaxValue});
            AvailableTeeTimes = list;
            Debug.WriteLine($"[iOS] AvailableTeeTimes: {AvailableTeeTimes?.Count}");
        }

        [RelayCommand]
        private async Task BookTimeAsync(DisplayedTeeTime booking)
        {
            var fullDate = SelectedDate + booking.Time.ToTimeSpan();
            fullDate = DateTime.SpecifyKind(fullDate, DateTimeKind.Unspecified);
            string userIdToken = await TokenHelper.GetUserId();

            if (!Int32.TryParse(userIdToken, out int userId))
            {
                await Shell.Current.DisplayAlert("Error", "Failed getting User ID.", "OK");
            }

            var newBooking = new Booking()
            {
                Id = booking.BookingId,
                GolfCourseId = GolfCourse.Id,
                StartTime = fullDate,
                CreatedByUserId = userId
            };
            await Shell.Current.GoToAsync("BookTeeTimeDetails",
                new Dictionary<string, object>
                {
                    { "Booking", newBooking }
                }
            );
        }
    }
}
