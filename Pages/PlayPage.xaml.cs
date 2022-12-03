namespace c_sherp_project_app;

public partial class PlayPage : ContentPage
{
	public PlayPage()
	{
		InitializeComponent();
	}

	private void OnRockClicked(object sender, EventArgs e)
	{
		System.Console.WriteLine("Rock Clicked");

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

