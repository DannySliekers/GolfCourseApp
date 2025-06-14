using CommunityToolkit.Mvvm.ComponentModel;
using GolfApp.Models;
using GolfApp.Services;
using System.Collections.ObjectModel;


namespace GolfApp.ViewModels
{
    public class GolfCourseDetailsViewModel : ObservableObject
    {
        public GolfCourse GolfCourse { get; set; }
        public ObservableCollection<ImageSource> Images { get; set; } = new();

        private readonly IImageService _imageService;
        
        public GolfCourseDetailsViewModel(IImageService imageService)
        {
            _imageService = imageService;
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
