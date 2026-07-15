namespace NotesPro.Controls;

public partial class NoteCard : ContentView
{
    public NoteCard()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(NoteCard), string.Empty);

    public static readonly BindableProperty DescriptionProperty =
        BindableProperty.Create(nameof(Description), typeof(string), typeof(NoteCard), string.Empty);

    public static readonly BindableProperty DateProperty =
        BindableProperty.Create(nameof(Date), typeof(string), typeof(NoteCard), string.Empty);

    public static readonly BindableProperty IsPinnedProperty =
        BindableProperty.Create(nameof(IsPinned), typeof(bool), typeof(NoteCard), false);

    public static readonly BindableProperty IsFavoriteProperty =
        BindableProperty.Create(nameof(IsFavorite), typeof(bool), typeof(NoteCard), false);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public string Date
    {
        get => (string)GetValue(DateProperty);
        set => SetValue(DateProperty, value);
    }

    public bool IsPinned
    {
        get => (bool)GetValue(IsPinnedProperty);
        set => SetValue(IsPinnedProperty, value);
    }

    public bool IsFavorite
    {
        get => (bool)GetValue(IsFavoriteProperty);
        set => SetValue(IsFavoriteProperty, value);
    }
}