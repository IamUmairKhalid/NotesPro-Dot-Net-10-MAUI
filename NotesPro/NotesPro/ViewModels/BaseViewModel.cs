using CommunityToolkit.Mvvm.ComponentModel;

namespace NotesPro.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private bool isRefreshing;

    [ObservableProperty]
    private string title = string.Empty;
}
