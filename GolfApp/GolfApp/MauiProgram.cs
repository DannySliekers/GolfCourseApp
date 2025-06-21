using GolfApp.Pages;
using GolfApp.Services;
using GolfApp.ViewModels;

namespace GolfApp;
public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .RegisterViews()
            .RegisterViewModels()
            .RegisterServices()
            .UseMauiMaps()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        return builder.Build();
	}

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<LoginViewModel>();
        mauiAppBuilder.Services.AddTransient<RegisterViewModel>();
        mauiAppBuilder.Services.AddTransient<HomeViewModel>();
        mauiAppBuilder.Services.AddTransient<GolfCourseDetailsViewModel>();
        mauiAppBuilder.Services.AddTransient<AddGolfCourseViewModel>();
        mauiAppBuilder.Services.AddTransient<EditGolfCourseViewModel>();
        mauiAppBuilder.Services.AddTransient<BookTeeTimeViewModel>();
        mauiAppBuilder.Services.AddTransient<BookingsViewModel>();
        mauiAppBuilder.Services.AddTransient<UserProfileViewModel>();
        mauiAppBuilder.Services.AddTransient<MapViewModel>();
        mauiAppBuilder.Services.AddTransient<ManageViewModel>();
        mauiAppBuilder.Services.AddTransient<BookTeeTimeDetailsViewModel>();
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<RegisterPage>();
        mauiAppBuilder.Services.AddTransient<LoginPage>();
        mauiAppBuilder.Services.AddTransient<MainPage>();
        mauiAppBuilder.Services.AddTransient<GolfCourseDetailsPage>();
        mauiAppBuilder.Services.AddTransient<AddGolfCoursePage>();
        mauiAppBuilder.Services.AddTransient<EditGolfCoursePage>();
        mauiAppBuilder.Services.AddTransient<BookTeeTimePage>();
        mauiAppBuilder.Services.AddTransient<BookingsPage>();
        mauiAppBuilder.Services.AddTransient<UserProfilePage>();
        mauiAppBuilder.Services.AddTransient<MapPage>();
        mauiAppBuilder.Services.AddTransient<ManagePage>();
        mauiAppBuilder.Services.AddTransient<BookTeeTimeDetailsPage>();
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
    {
        const string baseUrl = "https://10.0.2.2:7129";

        mauiAppBuilder.Services.AddCustomHttpClient<IAuthService, AuthService>(baseUrl);
        mauiAppBuilder.Services.AddCustomHttpClient<IGolfCourseService, GolfCourseService>(baseUrl);
        mauiAppBuilder.Services.AddCustomHttpClient<IImageService, ImageService>(baseUrl);
        mauiAppBuilder.Services.AddCustomHttpClient<IBookingService, BookingService>(baseUrl);
        mauiAppBuilder.Services.AddCustomHttpClient<IUserService, UserService>(baseUrl);
        mauiAppBuilder.Services.AddCustomHttpClient<IUploadService, UploadService>(baseUrl);

        return mauiAppBuilder;
    }
}
