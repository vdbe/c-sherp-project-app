using System.Net.Http.Headers;
using System.Net.Http;
using c_sherp_project_app.Models;
using c_sherp_project_app.Storage;
using System.Text.Json;
using System.Text;

namespace c_sherp_project_app.Services;

public class ApiClient {

    private AppDatabase appDatabase;
    private String apiEndpoint = "https://pain.ewood.dev/rps";

    private Game game = null;

    private HttpClient httpClient;

    public ApiClient() {
        this.appDatabase = new AppDatabase();
        this.httpClient = new HttpClient();
        this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<PlayResult> playRound(Choice choice) {
        if (this.game == null) {
            System.Console.WriteLine("Error playRound: game is null");
            await this.getScore();
        }

        // TODO: Clean this up
        if (this.game.Active == false) {
            await this.createGame();
        }

        Play play = new Play() {Choice = choice };
        var stringPayload = JsonSerializer.Serialize(play);
        StringContent content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
        System.Console.WriteLine(JsonSerializer.Serialize(content));

        HttpResponseMessage result = await this.httpClient.PostAsync($"{this.apiEndpoint}/game/{this.game.Guid}/play", content);
        if (!result.IsSuccessStatusCode) {
            // TODO: Handle error
            System.Console.WriteLine(JsonSerializer.Serialize(result.StatusCode));
            System.Console.WriteLine(await result.Content.ReadAsStringAsync());
            System.Console.WriteLine("Error: playRound request failed");
            return null;
        }

        String jsonString = await result.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        PlayResult playResult = JsonSerializer.Deserialize<PlayResult>(jsonString, options);

        this.game = playResult.Game;

        return playResult;
    }

    public async Task<uint> getScore() {
        Guid? gameGuid = await this.getGameGuid();
        System.Console.WriteLine($"getScore(): gameGuid -> {gameGuid}");

        if (gameGuid == null) {

            Game game = await this.createGame();
        } else {
            Game game = await this.getGame(gameGuid);
            this.game = game;
        }

        return this.game.Score;
    }

    private async Task<Game> createGame() {
        System.Console.WriteLine($"createGame()");
        HttpResponseMessage result1 = await this.httpClient.GetAsync($"https://whatthecommit.com/index.txt");
        System.Console.WriteLine($"{this.apiEndpoint}/game");
        HttpResponseMessage result = await this.httpClient.PostAsync($"{this.apiEndpoint}/game", null);
        System.Console.WriteLine($"createGame() request complete");


        if (!result.IsSuccessStatusCode) {
            // TODO: Handle error
            System.Console.WriteLine($"Error: CreateGame request failed -> ${result.StatusCode}");
            return null;
        }

        String jsonString = await result.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        Game game = JsonSerializer.Deserialize<Game>(jsonString, options);

        await this.appDatabase.SetGameIdentifierAsync(new GameIdentifier() {Id = 1, Guid = game.Guid});
        this.game = game;


        System.Console.WriteLine($"createGame(): gameGuid -> {game.Guid}");
        return game;
    }

    public Game getCurrentGame() {
        return this.game;
    }

    public async Task<Game> getGame(Guid? guid) {
        var a = JsonSerializer.Serialize(guid);
        System.Console.WriteLine($"getGame(): gameGuid -> {a}");
        System.Console.WriteLine(guid.HasValue);
        if (guid == null) {
            System.Console.WriteLine("Error: GetGame guid is null");
            return null;
        }

        HttpResponseMessage result = await this.httpClient.GetAsync($"{this.apiEndpoint}/game/{guid}");
        System.Console.WriteLine($"{this.apiEndpoint}/game/{guid}");
        if (!result.IsSuccessStatusCode) {
            // TODO: Handle error
            System.Console.WriteLine("Error: GetGame request failed");
            return null;
        }

        String jsonString = await result.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        Game game = JsonSerializer.Deserialize<Game>(jsonString, options);

        return game;
    }

    public async Task<Guid?> getGameGuid() {
        GameIdentifier gameIdentifier = await this.appDatabase.GetGameIdentifierAsync(1);
        if (gameIdentifier == null) {
            return null;
        }

        return gameIdentifier.Guid;
    }

    public async Task<LeaderBoard> submitLeaderBoard(string name) {
        if (this.game == null) {
            return null;
        }

        LeaderBoardSubmission leaderBoardSubmission = new LeaderBoardSubmission(){Name = name, Game = this.game.Guid };

        var stringPayload = JsonSerializer.Serialize(leaderBoardSubmission);
        StringContent content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
        System.Console.WriteLine(JsonSerializer.Serialize(content));

        HttpResponseMessage result = await this.httpClient.PostAsync($"{this.apiEndpoint}/leaderboard/", content);
        if (!result.IsSuccessStatusCode) {
            // TODO: Handle error
            System.Console.WriteLine("Error: GetGame request failed");
            return null;
        }

        String jsonString = await result.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        LeaderBoard leaderBoard = JsonSerializer.Deserialize<LeaderBoard>(jsonString, options);

        return leaderBoard;
    }

    public async Task<List<LeaderBoard>> getLeaderBoards(int count) {
        HttpResponseMessage result = await this.httpClient.GetAsync($"{this.apiEndpoint}/leaderboard?count={count}");
        if (!result.IsSuccessStatusCode) {
            // TODO: Handle error
            System.Console.WriteLine("Error: GetLeaderBoards request failed");
            return null;
        }

        String jsonString = await result.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        List<LeaderBoard> lbs = JsonSerializer.Deserialize<List<LeaderBoard>>(jsonString, options);

        return lbs;
    }

}