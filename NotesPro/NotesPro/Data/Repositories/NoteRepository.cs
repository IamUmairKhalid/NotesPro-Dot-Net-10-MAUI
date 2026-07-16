using NotesPro.Data.Database;
using NotesPro.Data.Repositories.Interfaces;
using NotesPro.Models;

namespace NotesPro.Data.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly AppDatabase _database;

    public NoteRepository(AppDatabase database)
    {
        _database = database;
    }

    public async Task<List<Note>> GetAllAsync()
    {
        await _database.DatabaseReady;
        var db = await _database.GetConnectionAsync();

        var notes = await db.Table<Note>()
            .Where(x => !x.IsDeleted)
            .ToListAsync();

        return notes
            .OrderByDescending(x => x.IsPinned)
            .ThenByDescending(x => x.UpdatedOn ?? x.CreatedOn)
            .ToList();
    }

    public async Task<Note?> GetByIdAsync(Guid id)
    {
        await _database.DatabaseReady;
        var db = await _database.GetConnectionAsync();

        return await db.Table<Note>()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> AddAsync(Note note)
    {
        await _database.DatabaseReady;
        var db = await _database.GetConnectionAsync();

        note.CreatedOn = DateTime.UtcNow;

        return await db.InsertAsync(note);
    }

    public async Task<int> UpdateAsync(Note note, bool updateTimestamp = true)
    {
        await _database.DatabaseReady;
        var db = await _database.GetConnectionAsync();

        if (updateTimestamp)
            note.UpdatedOn = DateTime.UtcNow;

        return await db.UpdateAsync(note);
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        await _database.DatabaseReady;
        var db = await _database.GetConnectionAsync();

        var note = await GetByIdAsync(id);

        if (note == null)
            return 0;

        note.IsDeleted = true;

        note.UpdatedOn = DateTime.UtcNow;

        return await db.UpdateAsync(note);
    }

    public async Task<int> GetTotalCountAsync()
    {
        await _database.DatabaseReady;
        var db = await _database.GetConnectionAsync();

        return await db.Table<Note>()
            .Where(x => !x.IsDeleted)
            .CountAsync();
    }

    public async Task<int> GetFavoriteCountAsync()
    {
        await _database.DatabaseReady;
        var db = await _database.GetConnectionAsync();

        return await db.Table<Note>()
            .Where(x => x.IsFavorite && !x.IsDeleted)
            .CountAsync();
    }

    public async Task<int> GetPinnedCountAsync()
    {
        await _database.DatabaseReady;
        var db = await _database.GetConnectionAsync();

        return await db.Table<Note>()
            .Where(x => x.IsPinned && !x.IsDeleted)
            .CountAsync();
    }

    public async Task<List<Note>> GetRecentAsync(int count)
    {
        await _database.DatabaseReady;
        var db = await _database.GetConnectionAsync();

        var notes = await db.Table<Note>()
            .Where(x => !x.IsDeleted)
            .ToListAsync();

        return notes
            .OrderByDescending(x => x.IsPinned)
            .ThenByDescending(x => x.UpdatedOn ?? x.CreatedOn)
            .Take(count)
            .ToList();
    }
}
