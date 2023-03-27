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

        var cultureInfo = new CultureInfo("es-MX");
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;


        MainPage = new HomePage();
	}
}
