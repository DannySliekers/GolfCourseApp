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
        private readonly IGolfCourseService _golfCourseService;

        public ManageViewModel(IGolfCourseService golfCourseService)
        {
            _golfCourseService = golfCourseService;
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
    }
}
