using Monedero.Views;

namespace Monedero.Controls;

public partial class ToolBarCustom : ContentView
{
	public ToolBarCustom()
	{
		InitializeComponent();
	}

    public string ToolBarTitle
    {
        get => (string)GetValue(ToolbarTitleProperty);
        set => SetValue(ToolbarTitleProperty, value);
    }

    public static readonly BindableProperty ToolbarTitleProperty = BindableProperty.Create(
        nameof(ToolBarTitle),
        typeof(string),
        typeof(ToolBarCustom),
        default(string),
        defaultBindingMode: BindingMode.OneWay);

    private async void OnShowDialogClicked(object sender, EventArgs e)
    {
        await MauiPopup.PopupAction.DisplayPopup(new PopupPage());
    }

}