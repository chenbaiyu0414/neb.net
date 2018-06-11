using Newtonsoft.Json;

namespace Nebulas.API.Responses.Api
{
    public class DynastyResponse
    {
        [JsonProperty("miners")]
        public string[] Miners { get; set; }
    }
}
