using NotesPro.Models;

namespace NotesPro.Services.Interfaces;

public interface IDashboardService
{
    Task<int> GetTotalNotesAsync();

    Task<int> GetFavoriteNotesAsync();

    Task<int> GetPinnedNotesAsync();

    Task<List<Note>> GetRecentNotesAsync();
}