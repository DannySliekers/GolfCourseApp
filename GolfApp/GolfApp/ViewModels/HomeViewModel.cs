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
        private readonly IGolfCourseService _golfCourseService;

        public HomeViewModel(IGolfCourseService golfCourseService)
        {
            _golfCourseService = golfCourseService;
        }

        public async Task InitializeAsync()
        {
            try
            {
                var courses = await _golfCourseService.GetAllGolfCoursesAsync();
                GolfCourses.Clear();
                foreach (var course in courses)
                {
                    GolfCourses.Add(course);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to load golf courses: {ex.Message}");
            }
        }
    }
}
