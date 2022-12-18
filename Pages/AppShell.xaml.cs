namespace c_sherp_project_app;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
	}

    public void Update() {
        Task.Run<Task>(async() => await UpdateAsync());
    }

    public async Task UpdateAsync() {
        await App.ApiClient.getScore();
    }
}
