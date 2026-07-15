using NotesPro.Models;

namespace NotesPro.Data.Repositories.Interfaces;

public interface INoteRepository
{
    Task<List<Note>> GetAllAsync();

    Task<Note?> GetByIdAsync(Guid id);

    Task<int> AddAsync(Note note);

    Task<int> UpdateAsync(Note note);

    Task<int> DeleteAsync(Guid id);
}