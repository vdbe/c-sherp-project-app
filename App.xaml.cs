using c_sherp_project_app.Storage;
using c_sherp_project_app.Models;

namespace c_sherp_project_app;

public partial class App : Application
{

	public static AppDatabase Database { get; private set;}
	public App(AppDatabase database)
	{
		InitializeComponent();

		MainPage = new AppShell();
		Database = database;

		try {
			// TODO: fix
			ApiIdentifier apiIdentifier = new ApiIdentifier{Id = 1, Identifier =  ""};
			App.Database.SetApiIdentifierAsync(apiIdentifier).RunSynchronously();
		} catch { }
	}
}
