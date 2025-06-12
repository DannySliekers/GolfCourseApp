using GolfApp.Pages;

namespace GolfApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("GolfCourseDetails", typeof(GolfCourseDetailsPage));
    }
}
