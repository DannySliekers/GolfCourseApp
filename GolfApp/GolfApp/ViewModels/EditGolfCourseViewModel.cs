using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GolfApp.Helpers;
using GolfApp.Models;
using GolfApp.Services;

namespace GolfApp.ViewModels
{
    public partial class EditGolfCourseViewModel : ObservableObject
    {
        [ObservableProperty]
        private TimeSpan firstTeeTime;
        [ObservableProperty]
        private TimeSpan lastTeeTime;
        [ObservableProperty]
        private int teeTimeIntervalMinutes;
        [ObservableProperty]
        private GolfCourse golfCourse;
        private readonly IGolfCourseService _golfCourseService;

        public EditGolfCourseViewModel(IGolfCourseService golfCourseService)
        {
            _golfCourseService = golfCourseService;
        }

        [RelayCommand]
        private async Task EditGolfCourseAsync()
        {
            if (string.IsNullOrWhiteSpace(GolfCourse.Name))
            {
                await Shell.Current.DisplayAlert("Validation Error", "Name is required.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(GolfCourse.Description))
            {
                await Shell.Current.DisplayAlert("Validation Error", "Description is required.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(GolfCourse.Address))
            {
                await Shell.Current.DisplayAlert("Validation Error", "Address is required.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(GolfCourse.Email))
            {
                await Shell.Current.DisplayAlert("Validation Error", "Email is required.", "OK");
                return;
            }

            if (GolfCourse.AmountOfHoles <= 0)
            {
                await Shell.Current.DisplayAlert("Validation Error", "Enter a valid amount of holes.", "OK");
                return;
            }

            if (GolfCourse.AmountOfCourses <= 0)
            {
                await Shell.Current.DisplayAlert("Validation Error", "Enter a valid amount of courses.", "OK");
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

            //if (GolfCourse.SelectedImages.Count < 1)
            //{
            //    await Shell.Current.DisplayAlert("Validation Error", "Add at least 1 image.", "OK");
            //    return;
            //}

            string userId = await TokenHelper.GetUserId();

            if (!int.TryParse(userId, out int parsedUserId))
            {
                await Shell.Current.DisplayAlert("Error", "Invalid user ID.", "OK");
                return;
            }

            GolfCourse.StartTimeIntervalMinutes = TeeTimeIntervalMinutes;
            GolfCourse.BookingStartTime = TimeOnly.FromTimeSpan(FirstTeeTime);
            GolfCourse.BookingLastStartTime = TimeOnly.FromTimeSpan(LastTeeTime);

            bool success = await _golfCourseService.EditGolfCourseAsync(GolfCourse);

            if (success)
            {
                //List<string> urls = await UploadImagesAsync();

                //foreach (var url in urls)
                //{
                //    string localHostUrl = UrlHelpers.TransformToLocalHost(url);
                //    await _golfCourseService.AddImageToGolfCourseAsync(golfCourseId, localHostUrl);
                //}

                await Shell.Current.DisplayAlert("Success", "Golf course edited successfully.", "OK");
                await Shell.Current.GoToAsync("//Manage");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Failed to edit golf course.", "OK");
            }
        }
    }
}
