using System.Windows.Input;

namespace NotesPro.Controls;

public partial class PrimaryButton : ContentView
{
    public PrimaryButton()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(PrimaryButton),
            string.Empty);

    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(
            nameof(Command),
            typeof(ICommand),
            typeof(PrimaryButton));

    public static readonly BindableProperty IsLoadingProperty =
        BindableProperty.Create(
            nameof(IsLoading),
            typeof(bool),
            typeof(PrimaryButton),
            false,
            propertyChanged: OnLoadingChanged);

    public static readonly BindableProperty IsButtonEnabledProperty =
        BindableProperty.Create(
            nameof(IsButtonEnabled),
            typeof(bool),
            typeof(PrimaryButton),
            true);

    private static void OnLoadingChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        var control = (PrimaryButton)bindable;

        control.IsButtonEnabled = !(bool)newValue;
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public bool IsLoading
    {
        get => (bool)GetValue(IsLoadingProperty);
        set => SetValue(IsLoadingProperty, value);
    }

    public bool IsButtonEnabled
    {
        get => (bool)GetValue(IsButtonEnabledProperty);
        set => SetValue(IsButtonEnabledProperty, value);
    }
}