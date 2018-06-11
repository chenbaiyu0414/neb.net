using Newtonsoft.Json;

namespace Nebulas.API.Responses.Api
{
    public class EventsByHashResponse
    {
        [JsonProperty("events")]
        public NebEvents[] Events { get; set; }
    }

    public class NebEvents
    {
        [JsonProperty("topic")]
        public string Topic { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }
    }
}
