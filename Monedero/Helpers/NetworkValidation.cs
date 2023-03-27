namespace Monedero.Helpers
{
    public class NetworkValidation
    {

        public static bool IsNetworkActive()
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                return true;
            }
            return false;
        }

    }
}
