using System.Text.Json.Serialization;

namespace Monedero.Models
{
    public class BalancesResponse
    {
        [JsonPropertyName("numero_tarjeta")]
        public int CardNumber { get; set; }

        [JsonPropertyName("apellido_paterno")]
        public string LastName { get; set; }

        [JsonPropertyName("saldo_monedero")]
        public float Salary { get; set; }

        [JsonPropertyName("ultima_transaccion")]
        public int LastTransaction { get; set; }
    }
}
