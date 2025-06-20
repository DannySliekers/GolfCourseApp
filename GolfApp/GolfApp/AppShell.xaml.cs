using GolfApp.Pages;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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

    public async Task AddManageTabAsync()
    {
        var token = await SecureStorage.GetAsync("jwt");

        if (!string.IsNullOrEmpty(token))
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var role = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role || c.Type == "role")?.Value;

            if (role != "Player")
            {
                var tabBar = Items.OfType<TabBar>().FirstOrDefault();
                if (tabBar != null && !tabBar.Items.Any(i => i.Title == "Manage"))
                {
                    var manageTab = new ShellContent
                    {
                        Title = "Manage",
                        Icon = "manage.png",
                        Route = "Manage",
                        ContentTemplate = new DataTemplate(typeof(ManagePage))
                    };

                    tabBar.Items.Add(manageTab);
                }
            }
        }
    }
}
