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

    public async Task<bool> ShowConfirmationAsync(
        string title,
        string message,
        string accept = "Delete",
        string cancel = "Cancel")
    {
        return await Shell.Current.DisplayAlert(
            title,
            message,
            accept,
            cancel);
    }
}
