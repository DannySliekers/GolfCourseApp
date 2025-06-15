using GolfApp.Pages;

namespace GolfApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("GolfCourseDetails", typeof(GolfCourseDetailsPage));
        Routing.RegisterRoute("AddGolfCourse", typeof(AddGolfCoursePage));
        Routing.RegisterRoute("BookTeeTime", typeof(BookTeeTimePage));
    }
}
