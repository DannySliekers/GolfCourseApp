using CommunityToolkit.Mvvm.ComponentModel;
using GolfApp.Models;
using GolfApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfApp.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        public ObservableCollection<GolfCourse> GolfCourses { get; set; } = new();
        public ImageSource FirstImage { get; set; }
        private readonly IGolfCourseService _golfCourseService;
        private readonly IImageService _imageService;

        public HomeViewModel(IGolfCourseService golfCourseService, IImageService imageService)
        {
            _golfCourseService = golfCourseService;
            _imageService = imageService;
        }

        public async Task InitializeAsync()
        {
            try
            {
                var courses = await _golfCourseService.GetAllGolfCoursesAsync();
                GolfCourses.Clear();
                foreach (var course in courses)
                {
                    await LoadFirstImageAsync(course);
                    GolfCourses.Add(course);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to load golf courses: {ex.Message}");
            }
        }

        public async Task LoadFirstImageAsync(GolfCourse golfCourse)
        {
            var imageSources = await _imageService.GetImagesAsync(golfCourse.Id);
            golfCourse.FirstImage = imageSources.FirstOrDefault();
        }
    }
}
