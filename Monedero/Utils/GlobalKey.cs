namespace Monedero.Utils
{
    class GlobalKey
    {
        public static readonly string START_PAGE = "StartPage";
        public static readonly string GOOGLE_PLAYSTORE_URL = "https://play.google.com/store/apps/details?id=com.mesquitestudio.smart&hl=es&gl=US&pli=1";
        public static readonly string SMART_WEBSITE = "https://www.supermercadossmart.com";
        public static readonly string HOME = "HomePage";
        public static readonly string DETAILS = "DetailsPage";
        public static readonly string BALANCE = "Balace";
        // public static readonly string HOST = "https://monedero.tiendassmart.com:40499/index.php";//Test
        public static readonly string HOST = "https://monedero.tiendassmart.com:40500/index.php";//Production
        public const string END_POINT_GET_BALANCE = "/monederos/{no_tarjeta}/{apellido}";

    }
}
