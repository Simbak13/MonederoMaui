using Monedero.Utils;

namespace Monedero.Helpers
{
    public class SSLCertificationValidation
    {
        /// <summary>
        ///Esto no es recomendable solo se implementa por que hay problemas 
        ///con el servidor por cause del certificado SSL
        /// </summary>
        public static HttpClient DisableSslCerfication()
        {
            var insecureHandler = new HttpClientHandler
            {
                // SslProtocols = SslProtocols.Tls | SslProtocols.Ssl2 | SslProtocols.Ssl3|SslProtocols.Tls11 | SslProtocols.Tls12,

                ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicy) => true
            };

            var client = new HttpClient(insecureHandler)
            {
                BaseAddress = new Uri(GlobalKey.HOST)
            };

            return client;

        }
    }
}
