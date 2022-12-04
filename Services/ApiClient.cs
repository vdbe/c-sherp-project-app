using System.Text.Json;
using c_sherp_project_app.Models;
using c_sherp_project_app.Storage;

namespace c_sherp_project_app.Services;

public class ApiClient {

    private AppDatabase appDatabase;

    public ApiClient() {
        this.appDatabase = new AppDatabase();
    }

    public async Task<Guid?> getGameGuid() {
        GameIdentifier gameIdentifier = await this.appDatabase.GetGameIdentifierAsync(0);
        if (gameIdentifier == null) {
            return null;
        }

        return gameIdentifier.Guid;
    }

}