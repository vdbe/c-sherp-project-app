using System.Text.Json;

namespace c_sherp_project_app;

public partial class PlayPage : ContentPage
{
	public PlayPage()
	{
		InitializeComponent();
	}

	private async void OnRockClicked(object sender, EventArgs e)
	{
		System.Console.WriteLine("Rock Clicked");
		System.Console.WriteLine(await App.ApiClient.getGameGuid());
		

	}

	private void OnPaperClicked(object sender, EventArgs e)
	{
		System.Console.WriteLine("Paper Clicked");
	}

	private void OnScissorsClicked(object sender, EventArgs e)
	{
		System.Console.WriteLine("Scissors Clicked");
	}
}

