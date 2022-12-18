using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using c_sherp_project_app.Models;
using Microsoft.Maui.Graphics;

namespace c_sherp_project_app;

public partial class LeaderBoardPage : ContentPage, INotifyPropertyChanged
{

    LeaderBoardPageViewModel vm;
    public LeaderBoardPage()
    {
        InitializeComponent();

        BindingContext = vm = new LeaderBoardPageViewModel();

        this.vm.GetLeaderboard(100);
    }

}

public class LeaderBoardPageViewModel : ViewModelBase {

    // Because api is slow and can't be bothered to find how to disable button's
    private bool busy = false;

    private List<LeaderBoard> lbs;
    public List<LeaderBoard> LeaderBoards {
        get { return lbs; }
        set
        {
            lbs = value;
            OnPropertyChanged();
        }

    }

    public List<LeaderBoard> GetLeaderboard(int count) {
        System.Console.WriteLine("GetLeaderBoard()");
        Task<List<LeaderBoard>> lbs = Task.Run<List<LeaderBoard>>(async() => await GetLeaderBoardsAsync(count));
        List<LeaderBoard> leaderBoards = lbs.Result;
        this.LeaderBoards = leaderBoards;

        System.Console.WriteLine(JsonSerializer.Serialize(this.LeaderBoards));

        System.Console.WriteLine("~GetLeaderBoard()");
        return leaderBoards;
    }

    public async Task<List<LeaderBoard>> GetLeaderBoardsAsync(int count) {
        return await App.ApiClient.getLeaderBoards(count);
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