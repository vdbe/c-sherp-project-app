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

        Game game = App.ApiClient.getCurrentGame();
        if(game == null || game.Active == true || this.busy == true) {
            return;
        }

        this.busy = true;


        string name = ((Entry)sender).Text;

        await App.ApiClient.submitLeaderBoard(name);

        this.busy = false;
    }

}

public class SubmitPageViewModel : ViewModelBase {

}