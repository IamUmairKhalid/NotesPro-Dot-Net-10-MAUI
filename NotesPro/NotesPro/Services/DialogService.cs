using NotesPro.Services.Interfaces;

namespace NotesPro.Services;

public class DialogService : IDialogService
{
    public async Task ShowAlertAsync(
        string title,
        string message,
        string cancel = "OK")
    {
        await Shell.Current.DisplayAlert(
            title,
            message,
            cancel);
    }
}