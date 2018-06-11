using Newtonsoft.Json;

namespace Nebulas.API.Responses.Admin
{
    public class SignHashResponse
    {
        [JsonProperty("data")]
        public string Data { get; set; }
    }
}
