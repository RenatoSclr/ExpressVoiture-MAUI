using ExpressVoiture.MAUI.Services;
using ExpressVoiture.MAUI.Services.Interface;
using ExpressVoiture.MAUI.ViewModels;
using ExpressVoiture.MAUI.Views;
using Microsoft.Extensions.Logging;

namespace ExpressVoiture.MAUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.AddHttpClient<IHomeService, HomeService>(client =>
        {
			#if ANDROID || IOS
				client.BaseAddress = new Uri("https://10.0.2.2:7167/");
			#else
				client.BaseAddress = new Uri("https://localhost:7167/");
			#endif
		}).ConfigurePrimaryHttpMessageHandler(() =>
        {
            return new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
        });

        builder.Services.AddHttpClient<IAuthService, AuthService>(client =>
        {
            #if ANDROID || IOS
                    client.BaseAddress = new Uri("https://10.0.2.2:7167/");
            #else
				    client.BaseAddress = new Uri("https://localhost:7167/");
            #endif
        }).ConfigurePrimaryHttpMessageHandler(() =>
        {
            return new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
        });


        builder.Services.AddHttpClient<IVehicleAdminService, VehicleAdminService>(client =>
        {
            #if ANDROID || IOS
                    client.BaseAddress = new Uri("https://10.0.2.2:7167/");
            #else
				    client.BaseAddress = new Uri("https://localhost:7167/");
            #endif
        }).ConfigurePrimaryHttpMessageHandler(() =>
        {
            return new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
        });

        builder.Services.AddSingleton<IUserStateService, UserStateService>();

        builder.Services.AddTransient<VehicleAdminListViewModel>();
        builder.Services.AddTransient<AdminVehicleListPage>();

        builder.Services.AddTransient<VehicleDetailsViewModel>();
        builder.Services.AddTransient<VehicleDetailsPage>();

        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<LoginPage>();

        builder.Services.AddTransient<VehicleListViewModel>();
        builder.Services.AddTransient<VehicleListPage>();

        builder.Services.AddTransient<AddVehiclePage>();
        builder.Services.AddTransient<AddVehicleViewModel>();

        builder.Services.AddTransient<UpdateVehicleViewModel>();
        builder.Services.AddTransient<UpdateVehiclePage>();


#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
