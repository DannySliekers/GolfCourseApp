using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GolfApp.Models;
using GolfApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GolfApp.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        public ObservableCollection<GolfCourse> GolfCourses { get; set; } = new();
        [ObservableProperty]
        private bool showAddButton;
        public ImageSource FirstImage { get; set; }
        private readonly IGolfCourseService _golfCourseService;
        private readonly IImageService _imageService;

        public HomeViewModel(IGolfCourseService golfCourseService, IImageService imageService)
        {
            _golfCourseService = golfCourseService;
            _imageService = imageService;
        }

        [RelayCommand]
        private async Task BookTeeTimeAsync(object sender)
        {
            if (sender is GolfCourse golfCourse)
            {
                await Shell.Current.GoToAsync("BookTeeTime",
                new Dictionary<string, object>
                {
                    { "GolfCourse", golfCourse }
                });
            }
        }

        public async Task InitializeAsync()
        {
            await (Shell.Current as AppShell)?.AddManageTabAsync();

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

        public async Task LoadUserRoleAndSetVisibility()
        {
            var token = await SecureStorage.GetAsync("jwt");

            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                var role = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role || c.Type == "role")?.Value;

                ShowAddButton = role != "Player";
            }
            else
            {
                ShowAddButton = false;
            }
        }
    }
}
