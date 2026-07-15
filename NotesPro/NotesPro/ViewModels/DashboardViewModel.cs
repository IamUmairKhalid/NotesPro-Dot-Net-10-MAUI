using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace NotesPro.ViewModels;

public partial class DashboardViewModel : BaseViewModel
{
    [ObservableProperty]
    private string greeting;

    [ObservableProperty]
    private string userName;

    [ObservableProperty]
    private string todayGoal;

    public DashboardViewModel()
    {
        Title = "Dashboard";

        Greeting = GetGreeting();

        userName = "Developer";

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
    private void Start()
    {
        // Navigation will be implemented in Step 4.
    }
}