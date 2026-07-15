namespace NotesPro;

public partial class App : Application
{
    public App(AppShell shell)
    {
        InitializeComponent();

        Windows[0].Page = shell;
    }
}