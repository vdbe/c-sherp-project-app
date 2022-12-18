using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using c_sherp_project_app.Models;
using Microsoft.Maui.Graphics;

namespace c_sherp_project_app;

public partial class SubmitPage : ContentPage
{
    private bool busy = false;

    SubmitPageViewModel vm;
    public SubmitPage()
    {
        InitializeComponent();

        BindingContext = vm = new SubmitPageViewModel();
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

public class SubmitPageViewModel : ViewModelBase {

}