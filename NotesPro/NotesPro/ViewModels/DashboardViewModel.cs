using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotesPro.Models;
using NotesPro.Services.Interfaces;
using System.Collections.ObjectModel;

namespace NotesPro.ViewModels;

public partial class DashboardViewModel : BaseViewModel
{
    private readonly IDashboardService _dashboardService;

    [ObservableProperty]
    private int totalNotes;

    [ObservableProperty]
    private int favoriteNotes;

    [ObservableProperty]
    private int pinnedNotes;

    public ObservableCollection<Note> RecentNotes { get; } = new();

    public DashboardViewModel(
        IDashboardService dashboardService,
        INavigationService navigationService,
        IDialogService dialogService)
    {
        _dashboardService = dashboardService;

        Title = "Dashboard";
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
}