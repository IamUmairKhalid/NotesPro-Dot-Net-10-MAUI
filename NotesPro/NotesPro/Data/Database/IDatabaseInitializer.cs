namespace NotesPro.Data.Database;

public interface IDatabaseInitializer
{
    Task InitializeAsync();
}