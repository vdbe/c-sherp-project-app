using c_sherp_project_app.Storage;
using c_sherp_project_app.Models;
using c_sherp_project_app.Services;

namespace c_sherp_project_app;

public partial class App : Application
{

	public static ApiClient ApiClient { get; private set;}
	public App( ApiClient apiClient)
	{
		InitializeComponent();

		MainPage = new AppShell();
		ApiClient = apiClient;
	}
}
