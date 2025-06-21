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

namespace GolfApp.ViewModels
{
    public partial class ManageViewModel : ObservableObject
    {
        public ObservableCollection<GolfCourse> ManagedGolfCourses { get; set; } = new();
        public ObservableCollection<Booking> ManagedBookings { get; set; } = new();
        private readonly IGolfCourseService _golfCourseService;
        private readonly IBookingService _bookingService;

        public ManageViewModel(IGolfCourseService golfCourseService, IBookingService bookingService)
        {
            _golfCourseService = golfCourseService;
            _bookingService = bookingService;
        }

        [RelayCommand]
        private async Task AddGolfCourseAsync()
        {
            await Shell.Current.GoToAsync("AddGolfCourse");
        }

        [RelayCommand]
        private async Task DeleteGolfCourseAsync(GolfCourse golfCourse)
        {
            bool success = await _golfCourseService.DeleteGolfCourseAsync(golfCourse.Id);
            
            if (success)
            {
                ManagedGolfCourses.Remove(golfCourse);
            }
        }

        [RelayCommand]
        private async Task DeleteBookingAsync(Booking booking)
        {
            bool success = await _bookingService.DeleteBookingAsync(booking.Id);
            
            if (success)
            {
                ManagedBookings.Remove(booking);
            }
        }

        [RelayCommand]
        private async Task EditGolfCourseAsync(GolfCourse golfCourse)
        {
            await Shell.Current.GoToAsync("EditGolfCourse",
                new Dictionary<string, object>
                {
                    { "GolfCourse", golfCourse }
                });
        }

        public async Task GetManagedGolfCourses()
        {
            List<GolfCourse> courses = await _golfCourseService.GetManagedGolfCoursesAsync();
            ManagedGolfCourses.Clear();

            foreach (var course in courses)
            {
                ManagedGolfCourses.Add(course);
            }
        }

        public async Task GetManagedBookings()
        {
            foreach (var golfCourse in ManagedGolfCourses)
            {
                var bookings = await _bookingService.GetGolfCourseBookingsAsync(golfCourse.Id);

                foreach (var booking in bookings)
                {
                    ManagedBookings.Add(booking);
                }
            }
        }
    }
}
