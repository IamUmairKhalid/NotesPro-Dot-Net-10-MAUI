using System.Windows.Input;

namespace NotesPro.Controls;

public partial class SearchBarView : ContentView
{
    public SearchBarView()
    {
        InitializeComponent();

        ClearCommand = new Command(() => Text = string.Empty);
    }

    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(SearchBarView),
            string.Empty,
            BindingMode.TwoWay);

    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(
            nameof(Placeholder),
            typeof(string),
            typeof(SearchBarView),
            "Search...");

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public ICommand ClearCommand { get; }
}