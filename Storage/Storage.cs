namespace c_sherp_project_app.Storage;

using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using c_sherp_project_app.Models;

public static class Constants
{
    public const string DatabaseFilename = "AppSQLite.db3";

    public const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

    public static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
}

public class AppDatabase
{
    private SQLiteAsyncConnection _conn;

    public AppDatabase()
    {
    }

    async Task Init()
    {
        if (_conn is not null)
            return;

        _conn = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        //var result = await Database.CreateTableAsync<GameIdentifier>();
        await _conn.CreateTableAsync<GameIdentifier>();
    }

    public async Task<int> SetGameIdentifierAsync(GameIdentifier gameIdentifier)
    {
        await Init();
        if (gameIdentifier.Id != 0)
            return await _conn.InsertOrReplaceAsync(gameIdentifier);
        else
            return await _conn.InsertAsync(gameIdentifier);
    }

    public async Task<GameIdentifier> GetGameIdentifierAsync(int id)
    {
        await Init();
        var search =  _conn.Table<GameIdentifier>().Where(i => i.Id == id);
        int count = await search.CountAsync();

        if( count == 0) {
            return null;
        }

        return await search.FirstAsync();
    }

    public async Task<int> DeleteGameIdentifierAsync(GameIdentifier gameIdentifier)
    {
        await Init();
        return await _conn.DeleteAsync(gameIdentifier);
    }
}
