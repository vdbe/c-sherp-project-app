using c_sherp_project_app.Services;
namespace c_sherp_project_app;

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
				fonts.AddFont("fa_solid.ttf", "FontAwesome");
			});

		builder.Services.AddSingleton<ApiClient>(s => ActivatorUtilities.CreateInstance<ApiClient>(s));


		return builder.Build();
	}
}
