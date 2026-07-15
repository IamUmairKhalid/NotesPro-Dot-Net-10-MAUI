using NotesPro.Data.Repositories.Interfaces;
using NotesPro.Models;
using NotesPro.Services.Interfaces;

namespace NotesPro.Services;

public class DashboardService : IDashboardService
{
    private readonly INoteRepository _noteRepository;

    public DashboardService(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public Task<int> GetTotalNotesAsync()
        => _noteRepository.GetTotalCountAsync();

    public Task<int> GetFavoriteNotesAsync()
        => _noteRepository.GetFavoriteCountAsync();

    public Task<int> GetPinnedNotesAsync()
        => _noteRepository.GetPinnedCountAsync();

    public Task<List<Note>> GetRecentNotesAsync()
        => _noteRepository.GetRecentAsync(5);
}