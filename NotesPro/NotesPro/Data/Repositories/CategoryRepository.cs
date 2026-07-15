using NotesPro.Data.Database;
using NotesPro.Data.Repositories.Interfaces;
using NotesPro.Models;

namespace NotesPro.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDatabase _database;

    public CategoryRepository(AppDatabase database)
    {
        _database = database;
    }

    public async Task<List<Category>> GetAllAsync()
    {
        var db = await _database.GetConnectionAsync();

        return await db.Table<Category>()
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        var db = await _database.GetConnectionAsync();

        return await db.Table<Category>()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> AddAsync(Category category)
    {
        var db = await _database.GetConnectionAsync();

        category.CreatedOn = DateTime.UtcNow;

        return await db.InsertAsync(category);
    }

    public async Task<int> UpdateAsync(Category category)
    {
        var db = await _database.GetConnectionAsync();

        category.UpdatedOn = DateTime.UtcNow;

        return await db.UpdateAsync(category);
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        var db = await _database.GetConnectionAsync();

        return await db.DeleteAsync<Category>(id);
    }
}