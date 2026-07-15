using SQLite;

namespace NotesPro.Data.Database;

public class AppDatabase
{
    private SQLiteAsyncConnection? _database;

    public async Task<SQLiteAsyncConnection> GetConnectionAsync()
    {
        if (_database != null)
            return _database;

        var databasePath = Path.Combine(
            FileSystem.AppDataDirectory,
            DatabaseConstants.DatabaseName);

        _database = new SQLiteAsyncConnection(
            databasePath,
            DatabaseConstants.Flags);

        return _database;
    }
}