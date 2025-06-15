using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GolfApp.Models;
using GolfApp.Services;
using System.Windows.Input;

namespace GolfApp.ViewModels
{
    public partial class AddGolfCourseViewModel : ObservableObject
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public TimeSpan FirstTeeTime { get; set; }
        public TimeSpan LastTeeTime { get; set; }

        [ObservableProperty]
        private int teeTimeIntervalMinutes;

        public ICommand AddGolfCourse { get; set; }

        private readonly IGolfCourseService _golfCourseService;

        public AddGolfCourseViewModel(IGolfCourseService golfCourseService)
        {
            _golfCourseService = golfCourseService;
        }

        [RelayCommand]
        private async Task AddGolfCourseAsync()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                await Shell.Current.DisplayAlert("Validation Error", "Name is required.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(Description))
            {
                await Shell.Current.DisplayAlert("Validation Error", "Description is required.", "OK");
                return;
            }

            if (LastTeeTime <= FirstTeeTime)
            {
                await Shell.Current.DisplayAlert("Validation Error", "Last booking time must be after start time.", "OK");
                return;
            }

            if (TeeTimeIntervalMinutes <= 0)
            {
                await Shell.Current.DisplayAlert("Validation Error", "Interval must be greater than 0.", "OK");
                return;
            }

            var newCourse = new GolfCourse
            {
                Name = Name,
                Description = Description,
                BookingStartTime = TimeOnly.FromTimeSpan(FirstTeeTime),
                BookingLastStartTime = TimeOnly.FromTimeSpan(LastTeeTime),
                StartTimeIntervalMinutes = TeeTimeIntervalMinutes,
                OwnerId = 11,
                Longitude = 0,
                Latitude = 0
            };

            var success = await _golfCourseService.AddGolfCourseAsync(newCourse);

            if (success)
            {
                await Shell.Current.DisplayAlert("Success", "Golf course added successfully.", "OK");
                // Optionally clear fields or navigate
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Failed to add golf course.", "OK");
            }
        }
    }
}
