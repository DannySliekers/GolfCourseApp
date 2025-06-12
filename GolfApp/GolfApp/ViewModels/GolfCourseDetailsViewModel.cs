using CommunityToolkit.Mvvm.ComponentModel;
using GolfApp.Models;


namespace GolfApp.ViewModels
{
    public class GolfCourseDetailsViewModel : ObservableObject
    {
        public GolfCourse GolfCourse { get; set; }
    }
}
