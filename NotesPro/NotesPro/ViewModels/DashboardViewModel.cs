using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotesPro.Models;
using NotesPro.Services.Interfaces;
using System.Collections.ObjectModel;

namespace NotesPro.ViewModels;

public partial class DashboardViewModel : BaseViewModel
{
    private readonly IDashboardService _dashboardService;
    private readonly INavigationService _navigationService;
    private readonly IDialogService _dialogService;
    private List<Note> _allNotes = new();

    [ObservableProperty]
    private int totalNotes;

    [ObservableProperty]
    private int favoriteNotes;

    [ObservableProperty]
    private int pinnedNotes;

    [ObservableProperty]
    private string greeting = string.Empty;

    [ObservableProperty]
    private string userName = "Developer";

    [ObservableProperty]
    private string todayDate = string.Empty;

    [ObservableProperty]
    private string searchText = string.Empty;

    [ObservableProperty]
    private Note? selectedNote;

    public ObservableCollection<Note> RecentNotes { get; } = new();

    public DashboardViewModel(
        IDashboardService dashboardService,
        INavigationService navigationService,
        IDialogService dialogService)
    {
        _dashboardService = dashboardService;
        _navigationService = navigationService;
        _dialogService = dialogService;

        Title = "Dashboard";
        Greeting = GetGreeting();
        TodayDate = DateTime.Now.ToString("dddd, dd MMMM yyyy");
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
    public async Task LoadAsync()
    {
        if (IsBusy)
            return;

        IsBusy = true;

        try
        {
            TotalNotes = await _dashboardService.GetTotalNotesAsync();

            FavoriteNotes = await _dashboardService.GetFavoriteNotesAsync();

            PinnedNotes = await _dashboardService.GetPinnedNotesAsync();

            _allNotes = await _dashboardService.GetRecentNotesAsync();
            ApplyFilter();
        }
        catch (Exception ex)
        {
            await _dialogService.ShowAlertAsync("Error", $"Failed to load dashboard: {ex.Message}");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }

    partial void OnSearchTextChanged(string value)
    {
        ApplyFilter();
    }

    private void ApplyFilter()
    {
        RecentNotes.Clear();

        IEnumerable<Note> filtered = _allNotes;

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            var cleanQuery = SearchText.Trim().ToLowerInvariant();
            filtered = filtered.Where(note =>
                note.Title.ToLowerInvariant().Contains(cleanQuery) ||
                note.Description.ToLowerInvariant().Contains(cleanQuery));
        }

        foreach (var note in filtered
            .OrderByDescending(note => note.IsPinned)
            .ThenByDescending(note => note.UpdatedOn ?? note.CreatedOn))
        {
            RecentNotes.Add(note);
        }
    }

    private static string GetGreeting()
    {
        var hour = DateTime.Now.Hour;

        if (hour < 12)
            return "Good Morning";

        if (hour < 17)
            return "Good Afternoon";

        return "Good Evening";
    }
}
