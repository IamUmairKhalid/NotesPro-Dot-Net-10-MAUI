using NotesPro.ViewModels;
using NotesPro.Models;

namespace NotesPro.Views;

public partial class NotesPage : ContentPage
{
    private readonly NotesViewModel _viewModel;

    public NotesPage(NotesViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;

        _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.LoadAsync();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        _viewModel.SearchText = string.Empty;
    }

    private void OnNoteCardTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is Note note)
            _viewModel.SelectNoteCommand.Execute(note);
    }

    private void OnDeleteSwipeInvoked(object sender, EventArgs e)
    {
        if (TryGetSwipeNote(sender, out var note))
            _viewModel.DeleteNoteCommand.Execute(note);
    }

    private void OnPinSwipeInvoked(object sender, EventArgs e)
    {
        if (TryGetSwipeNote(sender, out var note))
            _viewModel.TogglePinCommand.Execute(note);
    }

    private void OnFavoriteSwipeInvoked(object sender, EventArgs e)
    {
        if (TryGetSwipeNote(sender, out var note))
            _viewModel.ToggleFavoriteCommand.Execute(note);
    }

    private static bool TryGetSwipeNote(object sender, out Note note)
    {
        note = null!;

        if (sender is not SwipeItem swipeItem)
            return false;

        if (swipeItem.CommandParameter is Note parameterNote)
        {
            note = parameterNote;
            return true;
        }

        if (swipeItem.BindingContext is Note contextNote)
        {
            note = contextNote;
            return true;
        }

        if (swipeItem.Parent is SwipeItems { Parent: SwipeView { BindingContext: Note parentNote } })
        {
            note = parentNote;
            return true;
        }

        return false;
    }
}
