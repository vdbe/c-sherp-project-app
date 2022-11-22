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
        //var result = await Database.CreateTableAsync<ApiIdentifier>();
        await _conn.CreateTableAsync<ApiIdentifier>();
    }

    public async Task<int> SetApiIdentifierAsync(ApiIdentifier apiIdentifier)
    {
        await Init();
        if (apiIdentifier.Id != 0)
            return await _conn.UpdateAsync(apiIdentifier);
        else
            return await _conn.InsertAsync(apiIdentifier);
    }

    #nullable enable
    public async Task<ApiIdentifier?> GetApiIdentifierAsync(int id)
    {
        await Init();
        var search =  _conn.Table<ApiIdentifier>().Where(i => i.Id == id);
        int count = await search.CountAsync();

        if( count == 0) {
            System.Console.WriteLine("Return null");
            return new ApiIdentifier{Id = 0, Identifier = ""};
        }

        System.Console.WriteLine("Return object");
        return await search.FirstAsync();
    }
    #nullable disable

    public async Task<int> DeleteApiIdentifierAsync(ApiIdentifier apiIdentifier)
    {
        await Init();
        return await _conn.DeleteAsync(apiIdentifier);
    }

    public async Task<List<ApiIdentifier>> GetAllIdentifiers()
        {
            // Init then retrieve a list of Person objects from the database into a list
            try
            {
                await Init();
                return await _conn.Table<ApiIdentifier>().ToListAsync();
            }
            catch (Exception ex)
            {
            }

            return new List<ApiIdentifier>();
        }
}
