using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotesPro.Models;
using NotesPro.Services.Interfaces;
using NotesPro.Data.Repositories.Interfaces;

namespace NotesPro.ViewModels;

[QueryProperty(nameof(NoteIdString), "NoteId")]
public partial class NoteDetailViewModel : BaseViewModel
{
    private readonly INoteRepository _noteRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly INavigationService _navigationService;
    private readonly IDialogService _dialogService;
    private Note? _currentNote;

    [ObservableProperty]
    private string noteIdString = string.Empty;

    [ObservableProperty]
    private string noteTitle = string.Empty;

    [ObservableProperty]
    private string noteDescription = string.Empty;

    [ObservableProperty]
    private bool isPinned;

    [ObservableProperty]
    private bool isFavorite;

    [ObservableProperty]
    private Category? selectedCategory;

    [ObservableProperty]
    private bool isEditMode;

    public ObservableCollection<Category> Categories { get; } = new();

    public NoteDetailViewModel(
        INoteRepository noteRepository,
        ICategoryRepository categoryRepository,
        INavigationService navigationService,
        IDialogService dialogService)
    {
        _noteRepository = noteRepository;
        _categoryRepository = categoryRepository;
        _navigationService = navigationService;
        _dialogService = dialogService;
        Title = "New Note";
    }

    [RelayCommand]
    public async Task LoadNoteDataAsync()
    {
        if (IsBusy)
            return;

        IsBusy = true;

        try
        {
            Categories.Clear();
            var categoriesList = await _categoryRepository.GetAllAsync();
            foreach (var category in categoriesList)
            {
                Categories.Add(category);
            }

            if (!string.IsNullOrEmpty(NoteIdString) && Guid.TryParse(NoteIdString, out Guid guid))
            {
                _currentNote = await _noteRepository.GetByIdAsync(guid);
                if (_currentNote != null)
                {
                    Title = "Edit Note";
                    IsEditMode = true;
                    NoteTitle = _currentNote.Title;
                    NoteDescription = _currentNote.Description;
                    IsPinned = _currentNote.IsPinned;
                    IsFavorite = _currentNote.IsFavorite;
                    SelectedCategory = Categories.FirstOrDefault(c => c.Id == _currentNote.CategoryId);
                }
            }
            else
            {
                Title = "New Note";
                IsEditMode = false;
                NoteTitle = string.Empty;
                NoteDescription = string.Empty;
                IsPinned = false;
                IsFavorite = false;
                SelectedCategory = Categories.FirstOrDefault();
            }
        }
        catch (Exception ex)
        {
            await _dialogService.ShowAlertAsync("Error", $"Failed to load note details: {ex.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(NoteTitle))
        {
            await _dialogService.ShowAlertAsync("Validation", "Title cannot be empty.");
            return;
        }

        if (SelectedCategory == null)
        {
            await _dialogService.ShowAlertAsync("Validation", "Please select a category.");
            return;
        }

        IsBusy = true;

        try
        {
            if (IsEditMode && _currentNote != null)
            {
                _currentNote.Title = NoteTitle.Trim();
                _currentNote.Description = NoteDescription?.Trim() ?? string.Empty;
                _currentNote.CategoryId = SelectedCategory.Id;
                _currentNote.IsPinned = IsPinned;
                _currentNote.IsFavorite = IsFavorite;

                await _noteRepository.UpdateAsync(_currentNote);
            }
            else
            {
                var newNote = new Note
                {
                    Title = NoteTitle.Trim(),
                    Description = NoteDescription?.Trim() ?? string.Empty,
                    CategoryId = SelectedCategory.Id,
                    IsPinned = IsPinned,
                    IsFavorite = IsFavorite
                };

                await _noteRepository.AddAsync(newNote);
            }

            await _navigationService.GoBackAsync();
        }
        catch (Exception ex)
        {
            await _dialogService.ShowAlertAsync("Error", $"Failed to save note: {ex.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (_currentNote == null)
            return;

        var shouldDelete = await _dialogService.ShowConfirmationAsync(
            "Delete note?",
            $"Delete \"{_currentNote.Title}\"? This cannot be undone.");

        if (!shouldDelete)
            return;

        IsBusy = true;
        try
        {
            await _noteRepository.DeleteAsync(_currentNote.Id);
            await _navigationService.GoBackAsync();
        }
        catch (Exception ex)
        {
            await _dialogService.ShowAlertAsync("Error", $"Failed to delete note: {ex.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task TogglePinAsync()
    {
        IsPinned = !IsPinned;

        if (IsEditMode && _currentNote != null)
        {
            _currentNote.IsPinned = IsPinned;
            await _noteRepository.UpdateAsync(_currentNote, updateTimestamp: false);
        }
    }

    [RelayCommand]
    private async Task ToggleFavoriteAsync()
    {
        IsFavorite = !IsFavorite;

        if (IsEditMode && _currentNote != null)
        {
            _currentNote.IsFavorite = IsFavorite;
            await _noteRepository.UpdateAsync(_currentNote, updateTimestamp: false);
        }
    }

    [RelayCommand]
    private void SelectCategory(Category category)
    {
        SelectedCategory = category;
    }

    [RelayCommand]
    private async Task CancelAsync()
    {
        await _navigationService.GoBackAsync();
    }
}
