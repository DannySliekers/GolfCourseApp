using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GolfApp.Models;
using GolfApp.Services;
using System.Collections.ObjectModel;


namespace GolfApp.ViewModels
{
    public partial class GolfCourseDetailsViewModel : ObservableObject
    {
        [ObservableProperty]
        private GolfCourse golfCourse;
        public ObservableCollection<ImageSource> Images { get; set; } = new();

        private readonly IImageService _imageService;
        
        public GolfCourseDetailsViewModel(IImageService imageService)
        {
            _imageService = imageService;
        }

        [RelayCommand]
        private async Task BookTeeTimeAsync(object sender)
        {
            await Shell.Current.GoToAsync("BookTeeTime",
                new Dictionary<string, object>
                {
                    { "GolfCourse", GolfCourse }
                }
            );
        }

        public async Task LoadImagesAsync(int courseId)
        {
            var imageSources = await _imageService.GetImagesAsync(courseId);
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Images.Clear();
                foreach (var img in imageSources)
                {
                    Images.Add(img);
                }
            });
        }
    }
}
