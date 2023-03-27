using Monedero.Controls;
using Monedero.Interfaces;

namespace Monedero.Services
{
    public class DialogService : IDialogService
    {
        public async Task DisplayAlert(string title, string message, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public async Task ShowLoading()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new LodingDialog());
        }

        public async Task HideLoading()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
