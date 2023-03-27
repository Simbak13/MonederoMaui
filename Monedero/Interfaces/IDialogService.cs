namespace Monedero.Interfaces
{
    public interface IDialogService
    {
        Task DisplayAlert(string title, string message, string cancel);
        Task ShowLoading();
        Task HideLoading();
    }
}
