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

    public ObservableCollection<Note> RecentNotes { get; } = new();

    public DashboardViewModel(
        IDashboardService dashboardService,
        INavigationService navigationService,
        IDialogService dialogService)
    {
        _dashboardService = dashboardService;
        _navigationService = navigationService;

        Title = "Dashboard";
        Greeting = GetGreeting();
        TodayDate = DateTime.Now.ToString("dddd, dd MMMM yyyy");
    }

    [RelayCommand]
    private async Task SelectNoteAsync(Note note)
    {
        if (note == null)
            return;

        await _navigationService.GoToAsync($"NoteDetailPage?NoteId={note.Id}");
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

            RecentNotes.Clear();

            var notes = await _dashboardService.GetRecentNotesAsync();

            foreach (var note in notes)
                RecentNotes.Add(note);
        }
        finally
        {
            IsBusy = false;
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