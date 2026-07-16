using SQLite;
using System.Threading.Tasks;

namespace NotesPro.Data.Database;

public class AppDatabase
{
    private SQLiteAsyncConnection? _database;
    private readonly TaskCompletionSource _initCompletionSource = new(TaskCreationOptions.RunContinuationsAsynchronously);

    public Task DatabaseReady => _initCompletionSource.Task;

    public void MarkInitialized()
    {
        _initCompletionSource.TrySetResult();
    }

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