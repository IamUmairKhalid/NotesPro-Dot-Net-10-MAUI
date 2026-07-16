namespace NotesPro.Services.Interfaces;

public interface IDialogService
{
    Task ShowAlertAsync(
        string title,
        string message,
        string cancel = "OK");

    Task<bool> ShowConfirmationAsync(
        string title,
        string message,
        string accept = "Delete",
        string cancel = "Cancel");
}
