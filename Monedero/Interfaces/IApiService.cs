using Monedero.Models;
using Monedero.Utils;
using Refit;

namespace Monedero.Interfaces
{
    public interface IApiService
    {

        [Get(GlobalKey.END_POINT_GET_BALANCE)]
        Task<BalancesResponse> GetCardSalary(string no_tarjeta, string apellido);

    }
}
