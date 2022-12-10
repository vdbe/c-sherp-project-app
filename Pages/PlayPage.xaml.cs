using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using c_sherp_project_app.Models;
using Microsoft.Maui.Graphics;

namespace c_sherp_project_app;

public partial class PlayPage : ContentPage, INotifyPropertyChanged
{
    private uint score = 0;
    public uint Score
    {
        get { return score; }
        set
        {
            score = value;
            OnPropertyChanged();
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

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public PlayPage()
    {
        InitializeComponent();

        BindingContext = this;
    }

    private async void OnRockClicked(object sender, EventArgs e)
    {
        System.Console.WriteLine("Rock Clicked");
        await this.Play(Choice.Rock);
    }

    private async void OnPaperClicked(object sender, EventArgs e)
    {
        System.Console.WriteLine("Paper Clicked");
        await this.Play(Choice.Paper);
    }

    private async void OnScissorsClicked(object sender, EventArgs e)
    {
        await this.Play(Choice.Scissors);
    }

    private async Task Play(Choice choice)
    {
        if(this.busy == true)
            return;
        this.busy = true;
        System.Console.WriteLine($"{choice} Clicked");
        
        PlayResult playResult = await App.ApiClient.playRound(choice);

        this.YourLastChoice = choice;

        this.Score = playResult.Game.Score;
        this.OpponentsLastChoice = playResult.Choice;
        this.LastResult = playResult.Result;

		if (this.LastResult == Result.Loss) {
            this.TextColor = "Red";
        } else {
            this.TextColor = "White";
        }

        this.busy = false;
    }
}

