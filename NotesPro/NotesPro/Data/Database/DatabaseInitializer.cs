using NotesPro.Data.Seed;
using NotesPro.Models;
using NotesPro.Services.Interfaces;

namespace NotesPro.Data.Database;

public class DatabaseInitializer : IDatabaseInitializer
{
    private readonly AppDatabase _database;
    private readonly IDataSeeder _dataSeeder;

    public DatabaseInitializer(
        AppDatabase database,
        IDataSeeder dataSeeder)
    {
        _database = database;
        _dataSeeder = dataSeeder;
    }

    public async Task InitializeAsync()
    {
        var connection = await _database.GetConnectionAsync();

        await connection.CreateTableAsync<Category>();

        await connection.CreateTableAsync<Note>();

        await _dataSeeder.SeedAsync();
    }
}