using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using NotesPro.ViewModels;
using NotesPro.Views;
using NotesPro.Services;
using NotesPro.Services.Interfaces;
using NotesPro.Data.Database;

namespace NotesPro;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<AppShell>();

        builder.Services.AddSingleton<DashboardPage>();
        builder.Services.AddSingleton<DashboardViewModel>();

        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<IDialogService, DialogService>();

        builder.Services.AddSingleton<AppDatabase>();
        builder.Services.AddSingleton<IDatabaseInitializer, DatabaseInitializer>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}