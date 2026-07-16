namespace NotesPro.Controls;

public partial class StatisticCard : ContentView
{
    public StatisticCard()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(StatisticCard),
            string.Empty);

    public static readonly BindableProperty ValueProperty =
        BindableProperty.Create(
            nameof(Value),
            typeof(int),
            typeof(StatisticCard),
            0);

    public static readonly BindableProperty IconProperty =
        BindableProperty.Create(
            nameof(Icon),
            typeof(string),
            typeof(StatisticCard),
            string.Empty,
            propertyChanged: OnIconChanged);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public int Value
    {
        get => (int)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public string Icon
    {
        get => (string)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public bool IsNotesIcon => Icon.Equals("Notes", StringComparison.OrdinalIgnoreCase);

    public bool IsFavoritesIcon => Icon.Equals("Favorites", StringComparison.OrdinalIgnoreCase);

    public bool IsPinnedIcon => Icon.Equals("Pinned", StringComparison.OrdinalIgnoreCase);

    private static void OnIconChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not StatisticCard card)
            return;

        card.OnPropertyChanged(nameof(IsNotesIcon));
        card.OnPropertyChanged(nameof(IsFavoritesIcon));
        card.OnPropertyChanged(nameof(IsPinnedIcon));
    }
}
