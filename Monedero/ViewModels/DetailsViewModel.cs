using CommunityToolkit.Maui.Alerts;
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
    public class DetailsViewModel :BasicViewModel
    {
        public string LastName { get; set; }
        public string Salary { get; set; }
        public int CardNumber { get; set; }

        public bool IsBusy { get; set; }
        public bool IsEnable { get; set; }
        public DateTime LastTransaction { get; set; }
        public ICommand GoBackCommand { get; private set; }
        public ICommand SubmitCommand { get; private set; }

        private readonly IApiService _apiService;
        private readonly IDialogService _dialogService;
        public DetailsViewModel()
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

            Task.Run(async () => await GetCurrenUser());
            Task.Run(async () => await GetCardSalayAsync());


            SubmitCommand = new Command
            (async () => await GetCardSalayAsync());

            GoBackCommand = new Command
            (async () => await GoBackAsync());
        }

        private async Task GoBackAsync()
        {
    

            IsBusy = true;
            IsEnable = false;

            Preferences.Remove(GlobalKey.BALANCE);
            Preferences.Clear();
            Preferences.Set(GlobalKey.START_PAGE, GlobalKey.HOME);
            await App.Current.MainPage.Navigation.PushModalAsync(new HomePage());

            IsBusy = false;
            IsEnable = true;
        }

        private async Task GetCardSalayAsync()
        {
            if (await ValidateFields()) return;



            if (!NetworkValidation.IsNetworkActive())
            {
                await _dialogService.DisplayAlert( "Network Connection", GlobalMessages.NETWOKR_MESSAGE, "Continuar");

                return;
            }

            IsBusy = true;
            IsEnable = false;


            try
            {
                BalancesResponse response = await _apiService.GetCardSalary(CardNumber.ToString(), LastName);

                if (response != null)
                {
                    string json = JsonConvert.SerializeObject(response);
                    Preferences.Set(GlobalKey.BALANCE, json);
                    await GetCurrenUser();

                    var snack = Snackbar.Make("Consulta generada con éxito.");
                   await snack.Show();
                }
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    await _dialogService.DisplayAlert("Alerta", GlobalMessages.NOT_FOUND, "Continuar");
                }
                else if (ex.StatusCode == HttpStatusCode.BadRequest)
                {
                    await _dialogService.DisplayAlert("Error 400", GlobalMessages.BAD_REQUEST, "Salir");
                }
                else if (ex.StatusCode == HttpStatusCode.InternalServerError)

                {
                    await _dialogService.DisplayAlert("Error 500", GlobalMessages.INTERNAL_SERVER_ERROR, "Salir");
                }
                else
                {
                    await _dialogService.DisplayAlert("Error 500", GlobalMessages.INTERNAL_SERVER_ERROR, "Salir");
                }

            }
            catch (Exception ex)
            {
                await _dialogService.DisplayAlert("Alerta", GlobalMessages.INTERNAL_SERVER_ERROR, "Continuar");
            }
            finally
            {
                IsBusy = false;
                IsEnable = true;
            }


        }


        private async Task GetCurrenUser()
        {
            if (Preferences.ContainsKey(GlobalKey.BALANCE))
            {


                string json = Preferences.Get(GlobalKey.BALANCE, string.Empty);
                BalancesResponse item = JsonConvert.DeserializeObject<BalancesResponse>(json);
                LastName = item.LastName;
                Salary = item.Salary.ToString();//item.Salary.ToString("N", new System.Globalization.CultureInfo("es-Mx"));
                CardNumber = item.CardNumber;
                DateTime dateTime = DateTimeOffset.FromUnixTimeSeconds(item.LastTransaction).UtcDateTime;
                LastTransaction = dateTime;
            }
            else
            {
                await App.Current.MainPage.Navigation.PushModalAsync(new MainPage());
            }

        }

        private async Task<bool> ValidateFields()
        {

            if (string.IsNullOrEmpty(CardNumber.ToString()) ||
                string.IsNullOrWhiteSpace(CardNumber.ToString()))
            {
                return true;
            }

            if (string.IsNullOrEmpty(LastName) ||
                string.IsNullOrWhiteSpace(LastName))
            {
                return true;
            }
            return false;
        }


    }
}
