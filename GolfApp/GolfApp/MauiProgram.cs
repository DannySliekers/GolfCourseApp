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
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
        builder.Services.AddHttpClient<IAuthService, AuthService>(client => client.BaseAddress = new Uri("https://10.0.2.2:7129"))
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                {
                    if (cert != null && cert.Issuer.Equals("CN=localhost"))
                        return true;

                    return errors == System.Net.Security.SslPolicyErrors.None;
                };
                return handler;
            });
#else
    builder.Services.AddHttpClient<IAuthService, AuthService>();
#endif


        return builder.Build();
	}

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<LoginViewModel>();
        mauiAppBuilder.Services.AddSingleton<RegisterViewModel>();
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<RegisterPage>();
        mauiAppBuilder.Services.AddSingleton<LoginPage>();

        return mauiAppBuilder;
    }
}
