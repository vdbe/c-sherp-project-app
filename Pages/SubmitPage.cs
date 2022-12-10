using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using c_sherp_project_app.Models;
using Microsoft.Maui.Graphics;

namespace c_sherp_project_app;

public partial class SubmitPage : ContentPage, INotifyPropertyChanged
{

    // Because api is slow and can't be bothered to find how to disable button's
    private bool busy = false;

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public SubmitPage()
    {
        InitializeComponent();

        BindingContext = this;
    }

    async void OnNameEntryCompleted(object sender, EventArgs e)
    {
        System.Console.WriteLine("OnNameEntryCompleted()");

        Game game = App.ApiClient.getCurrentGame();
        if(game == null || game.Active == true || this.busy == true) {
            System.Console.WriteLine(JsonSerializer.Serialize(game));
            System.Console.WriteLine(JsonSerializer.Serialize(this.busy));
            System.Console.WriteLine("~OnNameEntryCompleted() early");
            return;
        }

        this.busy = true;


        string name = ((Entry)sender).Text;
        System.Console.WriteLine(name);

        await App.ApiClient.submitLeaderBoard(name);

        this.busy = false;
        System.Console.WriteLine("~OnNameEntryCompleted()");
    }

}

