using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GolfApp.Helpers;
using GolfApp.Models;
using GolfApp.Pages;
using GolfApp.Services;
using System.Collections.ObjectModel;

namespace GolfApp.ViewModels
{
    public partial class AddGolfCourseViewModel : ObservableObject
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public TimeSpan FirstTeeTime { get; set; }
        public TimeSpan LastTeeTime { get; set; }
        public ObservableCollection<SelectedImage> SelectedImages { get; set; } = new();
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public int AmountOfHoles { get; set; }
        public int AmountOfCourses { get; set; }
        public string Address { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; }
        public decimal? Price { get; set; }

        [ObservableProperty]
        private int teeTimeIntervalMinutes;

        private readonly IGolfCourseService _golfCourseService;
        private readonly IUploadService _uploadService;

        public AddGolfCourseViewModel(IGolfCourseService golfCourseService, IUploadService uploadService)
        {
            _golfCourseService = golfCourseService;
            _uploadService = uploadService;
        }

        [RelayCommand]
        private async Task AddImageAsync()
        {
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Select an image",
                    FileTypes = FilePickerFileType.Images
                });

                if (result != null)
                {
                    using var stream = await result.OpenReadAsync();
                    using var memoryStream = new MemoryStream();
                    await stream.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;

                    // Now pass a function that re-opens a stream from the byte array
                    var imageBytes = memoryStream.ToArray();

                    var image = new SelectedImage
                    {
                        ImageSource = ImageSource.FromStream(() => new MemoryStream(imageBytes)),
                        ImageBytes = imageBytes,
                        FileName = result.FileName
                    };

                    SelectedImages.Add(image);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Unable to pick image: {ex.Message}", "OK");
            }
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

            if (string.IsNullOrWhiteSpace(Address))
            {
                await Shell.Current.DisplayAlert("Validation Error", "Address is required.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                await Shell.Current.DisplayAlert("Validation Error", "Email is required.", "OK");
                return;
            }

            if (AmountOfHoles <= 0)
            {
                await Shell.Current.DisplayAlert("Validation Error", "Enter a valid amount of holes.", "OK");
                return;
            }

            if (AmountOfCourses <= 0)
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

            if (SelectedImages.Count < 1)
            {
                await Shell.Current.DisplayAlert("Validation Error", "Add at least 1 image.", "OK");
                return;
            }

            if (Latitude == null || Longitude == null)
            {
                await Shell.Current.DisplayAlert("Validation Error", "Please set a golf course location.", "OK");
                return;
            }

            string userId = await TokenHelper.GetUserId();

            if (!int.TryParse(userId, out int parsedUserId))
            {
                await Shell.Current.DisplayAlert("Error", "Invalid user ID.", "OK");
                return;
            }

            var newCourse = new GolfCourse
            {
                Name = Name,
                Description = Description,
                BookingStartTime = TimeOnly.FromTimeSpan(FirstTeeTime),
                BookingLastStartTime = TimeOnly.FromTimeSpan(LastTeeTime),
                StartTimeIntervalMinutes = TeeTimeIntervalMinutes,
                OwnerId = parsedUserId,
                Longitude = (double) Longitude,
                Latitude = (double) Latitude,
                AmountOfHoles = AmountOfHoles,
                AmountOfCourses = AmountOfCourses,
                Address = Address,
                Phone = Phone,
                Email = Email,
                Price = Price
            };

            int golfCourseId = await _golfCourseService.AddGolfCourseAsync(newCourse);

            if (golfCourseId != 0)
            {
                List<string> urls = await UploadImagesAsync();

                foreach (var url in urls)
                {
                    string localHostUrl = UrlHelpers.TransformToLocalHost(url);
                    await _golfCourseService.AddImageToGolfCourseAsync(golfCourseId, localHostUrl);
                }

                await Shell.Current.DisplayAlert("Success", "Golf course added successfully.", "OK");
                await Shell.Current.GoToAsync("//MainPage");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Failed to add golf course.", "OK");
            }
        }

        [RelayCommand]
        private async Task SetLocationAsync()
        {
            await Shell.Current.Navigation.PushAsync(new SelectLocationPage(location =>
            {
                Latitude = location.Latitude;
                Longitude = location.Longitude;
            }));
        }

        private async Task<List<string>> UploadImagesAsync()
        {
            List<string> urls = [];

            foreach (SelectedImage image in SelectedImages)
            {
                using var stream = new MemoryStream(image.ImageBytes);
                string url = await _uploadService.UploadFileAsync(stream, image.FileName);
                urls.Add(url);
            }

            return urls;
        }
    }
}
