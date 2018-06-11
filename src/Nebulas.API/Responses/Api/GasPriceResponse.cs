using Newtonsoft.Json;

namespace Nebulas.API.Responses.Api
{
    public class GasPriceResponse
    {
        [JsonProperty("gas_price")]
        public string GasPrice { get; set; }
    }
}
