using CommunityToolkit.Mvvm.ComponentModel;
using GolfApp.Models;
using GolfApp.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Maps;
using System.Collections.ObjectModel;

namespace GolfApp.ViewModels
{
    [ObservableObject]
    public partial class MapViewModel
    {
        public ObservableCollection<Pin> Pins { get; } = new ObservableCollection<Pin>();

        private readonly IGolfCourseService _golfCourseService;
        
        public MapViewModel(IGolfCourseService golfCourseService)
        {
            _golfCourseService = golfCourseService;
        }

        public async Task GeneratePins()
        {
            List<GolfCourse> golfCourses = await _golfCourseService.GetAllGolfCoursesAsync();

            foreach (var golfCourse in golfCourses)
            {
                Pins.Add(new Pin
                {
                    Label = $"{golfCourse.Name}",
                    Address = $"Insert address",
                    Location = new Location(golfCourse.Latitude, golfCourse.Longitude),
                    Type = PinType.Place
                });
            }
        }
    }
}
