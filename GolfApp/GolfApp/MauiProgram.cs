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
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        return builder.Build();
	}

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<LoginViewModel>();
        mauiAppBuilder.Services.AddSingleton<RegisterViewModel>();
        mauiAppBuilder.Services.AddSingleton<HomeViewModel>();
        mauiAppBuilder.Services.AddTransient<GolfCourseDetailsViewModel>();
        mauiAppBuilder.Services.AddTransient<AddGolfCourseViewModel>();
        mauiAppBuilder.Services.AddTransient<BookTeeTimeViewModel>();
        mauiAppBuilder.Services.AddSingleton<BookingsViewModel>();
        mauiAppBuilder.Services.AddSingleton<UserProfileViewModel>();
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<RegisterPage>();
        mauiAppBuilder.Services.AddSingleton<LoginPage>();
        mauiAppBuilder.Services.AddSingleton<MainPage>();
        mauiAppBuilder.Services.AddTransient<GolfCourseDetailsPage>();
        mauiAppBuilder.Services.AddTransient<AddGolfCoursePage>();
        mauiAppBuilder.Services.AddTransient<BookTeeTimePage>();
        mauiAppBuilder.Services.AddSingleton<BookingsPage>();
        mauiAppBuilder.Services.AddSingleton<UserProfilePage>();
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
