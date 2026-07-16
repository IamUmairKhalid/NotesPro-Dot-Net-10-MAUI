using NotesPro.ViewModels;

namespace NotesPro.Views;

public partial class NoteDetailPage : ContentPage
{
    private readonly NoteDetailViewModel _viewModel;

    public NoteDetailPage(NoteDetailViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.LoadNoteDataAsync();
    }
}
