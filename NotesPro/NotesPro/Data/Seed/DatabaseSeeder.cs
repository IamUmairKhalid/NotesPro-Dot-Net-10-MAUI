using NotesPro.Data.Repositories.Interfaces;
using NotesPro.Models;
using NotesPro.Services.Interfaces;

namespace NotesPro.Data.Seed;

public class DatabaseSeeder : IDataSeeder
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly INoteRepository _noteRepository;

    public DatabaseSeeder(
        ICategoryRepository categoryRepository,
        INoteRepository noteRepository)
    {
        _categoryRepository = categoryRepository;
        _noteRepository = noteRepository;
    }

    public async Task SeedAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();

        if (categories.Count > 0)
            return;

        var personal = new Category
        {
            Name = "Personal",
            Color = "#4F46E5",
            Icon = "person"
        };

        var work = new Category
        {
            Name = "Work",
            Color = "#10B981",
            Icon = "briefcase"
        };

        var learning = new Category
        {
            Name = "Learning",
            Color = "#F59E0B",
            Icon = "book"
        };

        await _categoryRepository.AddAsync(personal);
        await _categoryRepository.AddAsync(work);
        await _categoryRepository.AddAsync(learning);

        await _noteRepository.AddAsync(new Note
        {
            CategoryId = learning.Id,
            Title = "Welcome to NotesPro",
            Description = "This application was created with .NET MAUI and SQLite.",
            IsPinned = true
        });

        await _noteRepository.AddAsync(new Note
        {
            CategoryId = work.Id,
            Title = "Project Roadmap",
            Description = "Complete Dashboard, Notes, Categories and API Sync."
        });

        await _noteRepository.AddAsync(new Note
        {
            CategoryId = personal.Id,
            Title = "Shopping",
            Description = "Milk\nBread\nEggs"
        });
    }
}