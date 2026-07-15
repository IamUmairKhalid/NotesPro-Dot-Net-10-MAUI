namespace NotesPro;

using NotesPro.Data.Database;

public partial class App : Application
{
    private readonly AppShell _shell;
    private readonly IDatabaseInitializer _databaseInitializer;

    public App(
        AppShell shell,
        IDatabaseInitializer databaseInitializer)
    {
        InitializeComponent();

        _shell = shell;
        _databaseInitializer = databaseInitializer;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(_shell);

        window.Created += async (_, _) =>
        {
            await _databaseInitializer.InitializeAsync();
        };

        return window;
    }
}