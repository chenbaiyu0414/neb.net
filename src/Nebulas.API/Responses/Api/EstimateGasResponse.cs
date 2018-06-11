using Newtonsoft.Json;

namespace Nebulas.API.Responses.Api
{
    public class EstimateGasResponse
    {
        [JsonProperty("gas")]
        public string Gas { get; set; }

        [JsonProperty("err")]
        public string Err { get; set; }
    }
}
