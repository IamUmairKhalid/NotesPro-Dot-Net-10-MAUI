using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotesPro.Models;
using NotesPro.Services.Interfaces;
using NotesPro.Data.Repositories.Interfaces;

namespace NotesPro.ViewModels;

public partial class NotesViewModel : BaseViewModel
{
    private readonly INoteRepository _noteRepository;
    private readonly INavigationService _navigationService;
    private readonly IDialogService _dialogService;
    private List<Note> _allNotes = new();

    [ObservableProperty]
    private int totalNotes;

    [ObservableProperty]
    private string searchText = string.Empty;

    [ObservableProperty]
    private Note? selectedNote;

    public ObservableCollection<Note> Notes { get; } = new();

    public NotesViewModel(
        INoteRepository noteRepository,
        INavigationService navigationService,
        IDialogService dialogService)
    {
        _noteRepository = noteRepository;
        _navigationService = navigationService;
        _dialogService = dialogService;
        Title = "Notes";
    }

    [RelayCommand]
    public async Task LoadAsync()
    {
        if (IsBusy)
            return;

        IsBusy = true;

        try
        {
            _allNotes = await _noteRepository.GetAllAsync();
            ApplyFilter();
        }
        catch (Exception ex)
        {
            await _dialogService.ShowAlertAsync("Error", $"Failed to load notes: {ex.Message}");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    private async Task AddNoteAsync()
    {
        await _navigationService.GoToAsync("NoteDetailPage");
    }

    [RelayCommand]
    private async Task SelectNoteAsync(Note note)
    {
        if (note == null)
            return;

        try
        {
            await _navigationService.GoToAsync($"NoteDetailPage?NoteId={note.Id}");
        }
        catch (Exception ex)
        {
            await _dialogService.ShowAlertAsync("Navigation Error", $"Unable to open note: {ex.Message}");
        }
    }

    partial void OnSelectedNoteChanged(Note? value)
    {
        if (value == null)
            return;

        SelectedNote = null;
        SelectNoteCommand.Execute(value);
    }

    [RelayCommand]
    private async Task TogglePinAsync(Note note)
    {
        if (note == null)
            return;

        note.IsPinned = !note.IsPinned;
        await _noteRepository.UpdateAsync(note, updateTimestamp: false);
        await LoadAsync();
    }

    [RelayCommand]
    private async Task ToggleFavoriteAsync(Note note)
    {
        if (note == null)
            return;

        note.IsFavorite = !note.IsFavorite;
        await _noteRepository.UpdateAsync(note, updateTimestamp: false);
        await LoadAsync();
    }

    [RelayCommand]
    private async Task DeleteNoteAsync(Note note)
    {
        if (note == null)
            return;

        var shouldDelete = await _dialogService.ShowConfirmationAsync(
            "Delete note?",
            $"Delete \"{note.Title}\"? This cannot be undone.");

        if (!shouldDelete)
            return;

        await _noteRepository.DeleteAsync(note.Id);
        await LoadAsync();
    }

    partial void OnSearchTextChanged(string value)
    {
        ApplyFilter();
    }

    private void ApplyFilter()
    {
        Notes.Clear();

        IEnumerable<Note> filtered = _allNotes;

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            var cleanQuery = SearchText.Trim().ToLowerInvariant();
            filtered = filtered.Where(n => 
                (n.Title != null && n.Title.ToLowerInvariant().Contains(cleanQuery)) || 
                (n.Description != null && n.Description.ToLowerInvariant().Contains(cleanQuery)));
        }

        foreach (var note in filtered
            .OrderByDescending(n => n.IsPinned)
            .ThenByDescending(n => n.UpdatedOn ?? n.CreatedOn))
        {
            Notes.Add(note);
        }

        TotalNotes = Notes.Count;
    }
}
