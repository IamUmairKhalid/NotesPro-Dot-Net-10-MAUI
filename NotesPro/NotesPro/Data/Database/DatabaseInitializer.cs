using NotesPro.Models;

namespace NotesPro.Data.Database;

public class DatabaseInitializer : IDatabaseInitializer
{
    private readonly AppDatabase _database;

    public DatabaseInitializer(AppDatabase database)
    {
        _database = database;
    }

    public async Task InitializeAsync()
    {
        var connection = await _database.GetConnectionAsync();

        await connection.CreateTableAsync<Category>();

        await connection.CreateTableAsync<Note>();
    }
}