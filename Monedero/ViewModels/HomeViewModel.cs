using Google.Android.Material.Dialog;
using Monedero.Helpers;
using Monedero.Interfaces;
using Monedero.Models;
using Monedero.Services;
using Monedero.Utils;
using Monedero.Views;
using Newtonsoft.Json;
using PropertyChanged;
using Refit;
using System.Net;
using System.Windows.Input;

namespace Monedero.ViewModels
{

    [AddINotifyPropertyChangedInterface]
    public class HomeViewModel : BasicViewModel
    {
        public string CardNumber { get; set; }
        public string LastName { get; set; }

        public bool IsBusy { get; set; }
        public bool IsEnable{ get; set; }

        public ICommand SubmitCommand { get; private set; }

        private readonly IDialogService _dialogService;
        private readonly IApiService _apiService;


        public HomeViewModel()
        {
            _dialogService = new DialogService();
            IsEnable = true;
            //TODO Cambiar por la url con certificado
            //Esta es la implementacion correcta
            //_apiService = RestService
            //  .For<IApiService>(GlobalKey.HOST);

            //Este es solo de prueba por que el certifica SSL no esta funcionando
            _apiService = RestService
               .For<IApiService>(SSLCertificationValidation.DisableSslCerfication());


            SubmitCommand = new Command
           (async () => await GetCardSalayAsync());
          
        }
        private async Task GetCardSalayAsync()
        {
            

            if (await ValidateFields()) return;

            CardNumber = CardNumber.Trim();
            LastName = LastName.Trim();

            if (!NetworkValidation.IsNetworkActive())
            {
                await _dialogService.DisplayAlert("Network Connection", GlobalMessages.NETWOKR_MESSAGE, "Continuar");

                return;
            }
            IsBusy = true;
            IsEnable = false;



            try
            {

                BalancesResponse response = await _apiService.GetCardSalary(CardNumber, LastName);


                if (response != null)
                {
                    string json = JsonConvert.SerializeObject(response);
                    Preferences.Set(GlobalKey.BALANCE, json);
                    Preferences.Set(GlobalKey.START_PAGE, GlobalKey.DETAILS);
                    await App.Current.MainPage.Navigation.PushModalAsync(new DetailsPage());
                }
                else
                {
                    await _dialogService.DisplayAlert( "Error 500", GlobalMessages.INTERNAL_SERVER_ERROR, "Salir");
                    IsBusy = false;
                    IsEnable = true;
                    return;
                }
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    await _dialogService.DisplayAlert( "Alerta", GlobalMessages.NOT_FOUND, "Continuar");
                }
                else if (ex.StatusCode == HttpStatusCode.BadRequest)
                {
                    await _dialogService.DisplayAlert( "Error 400", GlobalMessages.BAD_REQUEST, "Salir");
                }
                else if (ex.StatusCode == HttpStatusCode.InternalServerError)

                {
                    await _dialogService.DisplayAlert( "Error 500", GlobalMessages.INTERNAL_SERVER_ERROR, "Salir");
                }
                else
                {
                    await _dialogService.DisplayAlert( "Error 500", GlobalMessages.INTERNAL_SERVER_ERROR, "Salir");
                }

            }
            catch (Exception ex)
            {
                await _dialogService.DisplayAlert( "Alerta", GlobalMessages.INTERNAL_SERVER_ERROR, "Continuar");
            }
            finally
            {
                IsBusy = false;
                IsEnable = true;
            }
        }


        private async Task<bool> ValidateFields()
        {

            if (string.IsNullOrEmpty(CardNumber) ||
                string.IsNullOrWhiteSpace(CardNumber))
            {

                await _dialogService.DisplayAlert( "Alerta", "El número de tarjeta es requerido", "Continuar");
                return true;
            }

            int _cardNumber;
            if (!int.TryParse(CardNumber, out _cardNumber))
            {
                await _dialogService.DisplayAlert("Alerta", "El número de tarjeta ingresado no es valido", "Continuar");
                return true;
            }

            if (string.IsNullOrEmpty(LastName) ||
                string.IsNullOrWhiteSpace(LastName))
            {

                await _dialogService.DisplayAlert( "Alerta", "El Apellido es requerido", "Continuar");
                return true;
            }

            if (LastName.Length < 3 || LastName.Length > 20)
            {
                await _dialogService.DisplayAlert( "Alerta", "El Apellido debe tener al menos 3 caracteres", "Continuar");
                return true;
            }


            return false;
        }

    }
}
