


using MauiPopup;
using Monedero.Interfaces;
using Monedero.Services;
using Monedero.Utils;
using PropertyChanged;
using System.Windows.Input;

namespace Monedero.ViewModels
{
    [AddINotifyPropertyChangedInterface]

    public class PopupViewModel : BasicViewModel
    {
        public ICommand DisponseCommand { get; private set; }
        public ICommand SharedCommand { get; private set; }
        public ICommand BrowserCommand { get; private set; }
        public ICommand MailCommand { get; private set; }
        private readonly IDialogService _dialogService;

        public PopupViewModel()
        {
            _dialogService = new DialogService();

            DisponseCommand = new Command
      (async () => await DisponseAsync());

            SharedCommand = new Command
          (async () => await SharedAsync());

            BrowserCommand = new Command
           (async () => await BrowserAsync());

            MailCommand = new Command
          (async () => await MailAsync());
        }

        private async Task DisponseAsync()
        {
            try
            {
                // await PopupNavigation.PopAsync();
                await PopupAction.ClosePopup(true);

            }
            catch (Exception)
            {
                await _dialogService.DisplayAlert("ERROR APLICATIVO", GlobalMessages.FAIL_APP_MESSAGE, "Salir");
                Environment.Exit(0);
            }
        }

        private async Task SharedAsync()
        {




            var result = await _dialogService.DisplayAlertCofirm("Shared", GlobalMessages.SHARED_MESSAGE, "SI", "NO");

            if (!result) return;

            try
            {
                var text = GlobalKey.GOOGLE_PLAYSTORE_URL;
                await Share.RequestAsync(new ShareTextRequest { Text = text, Title = "Shared" });
                await DisponseAsync();
            }
            catch (Exception ex)
            {
                await _dialogService.DisplayAlert("Error 502", GlobalMessages.MAIL_MESSAGE_FAIL, "Salir");

            }
        }

        private async Task BrowserAsync()
        {

            var result = await _dialogService.DisplayAlertCofirm("Browser", GlobalMessages.BROWSER_MESSAGE, "SI", "NO");

            if (!result) return;


            try
            {
                var url = GlobalKey.SMART_WEBSITE;
                await Launcher.OpenAsync(url);
                await DisponseAsync();
            }
            catch (Exception ex)
            {
                await _dialogService.DisplayAlert("Error 502", GlobalMessages.BROWSER_MESSAGE_FAIL, "Salir");
            }

        }

        private async Task MailAsync()
        {


            var result = await _dialogService.DisplayAlertCofirm("Email", GlobalMessages.MAIL_MESSAGE, "SI", "NO");

            if (!result) return;

            try
            {
                var message = new EmailMessage
                {
                    Subject = "Monedero Electrónico Smart",
                    Body = "Quería compartir esto contigo...",
                    To = new List<string> { "monedero@s-martmx.com" }
                };

                await Email.ComposeAsync(message);
                await DisponseAsync();
            }
            catch (Exception ex)
            {
                await _dialogService.DisplayAlert("Error 502", GlobalMessages.MAIL_MESSAGE_FAIL, "Salir");
            }

        }
    }
}
