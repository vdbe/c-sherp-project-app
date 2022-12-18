using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using c_sherp_project_app.Models;
using Microsoft.Maui.Graphics;

namespace c_sherp_project_app;


public partial class PlayPage : ContentPage
{
    PlayPageViewModel vm;

    public PlayPage()
    {
        InitializeComponent();

        BindingContext = vm = new PlayPageViewModel();
    }

    private async void OnRockClicked(object sender, EventArgs e)
    {
        System.Console.WriteLine("Rock Clicked");
        await this.vm.Play(Choice.Rock);
    }

    private async void OnPaperClicked(object sender, EventArgs e)
    {
        System.Console.WriteLine("Paper Clicked");
        await this.vm.Play(Choice.Paper);
    }

    private async void OnScissorsClicked(object sender, EventArgs e)
    {
        await this.vm.Play(Choice.Scissors);
    }

}


public class PlayPageViewModel : ViewModelBase  {
    private uint score = 0;
    public String Score
    {
        get { 

            if (this.LastResult == Result.Loss) {
                return $"You lost, final score: {this.score}";
            }
            else return $"Score: {this.score}";
         }
    }

    // TODO: This binding does not work don't ask me why
    private string color = "White";
    public string TextColor {get {
        return color;
    }
    set {
        color = value;
        OnPropertyChanged();
    }}

    // Because api is slow and can't be bothered to find how to disable button's
    private bool busy = false;

    public Choice? OpponentsLastChoice = null;
    public Choice? YourLastChoice = null;
    public Result? LastResult = null;

    public async Task Play(Choice choice)
    {
        if(this.busy == true)
            return;
        this.busy = true;
        System.Console.WriteLine($"{choice} Clicked");
        
        PlayResult playResult = await App.ApiClient.playRound(choice);

        this.YourLastChoice = choice;

        this.score = playResult.Game.Score;
        System.Console.WriteLine($"Score: {this.Score}");

        this.OpponentsLastChoice = playResult.Choice;
        this.LastResult = playResult.Result;

		if (this.LastResult == Result.Loss) {
            this.TextColor = "Red";
        } else {
            this.TextColor = "White";
        }

        OnPropertyChanged(nameof(this.Score));
        this.busy = false;
    }
}