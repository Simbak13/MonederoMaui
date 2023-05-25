using MauiPopup.Views;
using Monedero.Helpers;

namespace Monedero.Views;

public partial class PopupPage : BasePopupPage
{
	public PopupPage()
	{
		InitializeComponent();
        InitScreenResolution();
	}

    private void InitScreenResolution()
    {
       //FrameContainer.Margin = new Thickness(ScreenHelper.GetAdjustedSize(15), ScreenHelper.GetAdjustedSize(70));
        ImageFigure.HeightRequest = ScreenHelper.GetAdjustedSize(30);
        ImageFigure.WidthRequest = ScreenHelper.GetAdjustedSize(150);

        if (Device.RuntimePlatform == Device.Android)
        {
            ImageFigure.WidthRequest = ScreenHelper.GetAdjustedSize(150);
        }
        if (Device.RuntimePlatform == Device.iOS)
        {
            ImageFigure.WidthRequest = ScreenHelper.GetAdjustedSize(175);
        }
    }
}