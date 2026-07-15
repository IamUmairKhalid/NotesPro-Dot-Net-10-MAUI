using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotesPro.Services.Interfaces;

namespace NotesPro.ViewModels;

public partial class DashboardViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;
    private readonly IDialogService _dialogService;

    [ObservableProperty]
    private string greeting;

    [ObservableProperty]
    private string userName;

    [ObservableProperty]
    private string todayGoal;

    public DashboardViewModel(
        INavigationService navigationService,
        IDialogService dialogService)
    {
        _navigationService = navigationService;
        _dialogService = dialogService;

        Title = "Dashboard";

        Greeting = GetGreeting();

        UserName = "Developer";

        TodayGoal = ".NET MAUI Production App";
    }

    private static string GetGreeting()
    {
        var hour = DateTime.Now.Hour;

        return hour switch
        {
            < 12 => "Good Morning",
            < 17 => "Good Afternoon",
            _ => "Good Evening"
        };
    }

    [RelayCommand]
    private async Task StartAsync()
    {
        await _dialogService.ShowAlertAsync(
            "NotesPro",
            "Architecture is ready.");
    }
}