
using Monedero.Helpers;
using Monedero.Utils;
using Monedero.Views;
using System.Globalization;

namespace Monedero;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        // Registrar el servicio de diálogo
        //var dialogService = new DialogService();
        //DependencyService.RegisterSingleton<IDialogService>(dialogService);

        //	MainPage = new AppShell();
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(BorderLessEntry), (handler, view) =>
        {
#if __ANDROID__

           handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);

#elif __IOS__

            handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
            handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif

        });

        var cultureInfo = new CultureInfo("es-MX");
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

        if (Preferences.ContainsKey(GlobalKey.BALANCE))
        {

            MainPage = new DetailsPage();
        }
        else
        {
            MainPage = new HomePage();
        }



      //  MainPage = new HomePage();
	}
}
