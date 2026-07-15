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
        var db = await _database.GetConnectionAsync();

        return await db.Table<Note>()
            .Where(x => !x.IsDeleted)
            .OrderByDescending(x => x.IsPinned)
            .ThenByDescending(x => x.CreatedOn)
            .ToListAsync();
    }

    public async Task<Note?> GetByIdAsync(Guid id)
    {
        var db = await _database.GetConnectionAsync();

        return await db.Table<Note>()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> AddAsync(Note note)
    {
        var db = await _database.GetConnectionAsync();

        note.CreatedOn = DateTime.UtcNow;

        return await db.InsertAsync(note);
    }

    public async Task<int> UpdateAsync(Note note)
    {
        var db = await _database.GetConnectionAsync();

        note.UpdatedOn = DateTime.UtcNow;

        return await db.UpdateAsync(note);
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        var db = await _database.GetConnectionAsync();

        var note = await GetByIdAsync(id);

        if (note == null)
            return 0;

        note.IsDeleted = true;

        note.UpdatedOn = DateTime.UtcNow;

        return await db.UpdateAsync(note);
    }
}