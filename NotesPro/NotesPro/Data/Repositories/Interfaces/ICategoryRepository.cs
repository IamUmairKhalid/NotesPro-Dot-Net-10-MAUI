using NotesPro.Models;

namespace NotesPro.Data.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync();

    Task<Category?> GetByIdAsync(Guid id);

    Task<int> AddAsync(Category category);

    Task<int> UpdateAsync(Category category);

    Task<int> DeleteAsync(Guid id);
}