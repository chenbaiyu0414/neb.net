using Newtonsoft.Json;

namespace Nebulas.API.Responses.Api
{
    public class CallResponse
    {
        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("execute_err")]
        public string ExecuteErr { get; set; }

        [JsonProperty("estimate_gas")]
        public string EstimateGas { get; set; }
    }
}
