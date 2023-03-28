namespace Monedero.Interfaces
{
    public interface IDialogService
    {
        Task DisplayAlert(string title, string message, string cancel);
        Task<bool> DisplayAlertCofirm(string title, string message, string accepted, string cancel);
        Task ShowLoading();
        Task HideLoading();
    }
}
