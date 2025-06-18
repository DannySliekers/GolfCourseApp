using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GolfApp.Models;
using GolfApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<TimeOnly> availableTeeTimes;

        private readonly IBookingService _bookingService;

        public BookTeeTimeViewModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task GenerateTeeTimes(TimeOnly start, TimeOnly end, int intervalMinutes)
        {
            var list = new ObservableCollection<TimeOnly>();
            var time = start;
            List<Booking> bookings = await _bookingService.GetAllBookingAsync();
            


            while (time <= end)
            {
                list.Add(time);
                time = time.AddMinutes(intervalMinutes);
            }

            AvailableTeeTimes = list;
        }

        [RelayCommand]
        private async Task BookTimeAsync(TimeOnly time)
        {
            var fullDate = SelectedDate + time.ToTimeSpan();
            fullDate = DateTime.SpecifyKind(fullDate, DateTimeKind.Unspecified);

            var booking = new Booking()
            {
                GolfCourseId = GolfCourse.Id,
                StartTime = fullDate,
                CreatedByUserId = 11
            };

            var success = await _bookingService.AddBookingAsync(booking);

            if (success)
            {
                await Shell.Current.DisplayAlert("Success", "Booking added successfully.", "OK");
                // Optionally clear fields or navigate
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Failed to add booking.", "OK");
            }
        }
    }
}
