namespace NotesPro.Services.Interfaces;

public interface INavigationService
{
    Task GoToAsync(string route);

    Task GoBackAsync();
}