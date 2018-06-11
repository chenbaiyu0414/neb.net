using Newtonsoft.Json;

namespace Nebulas.API.Responses.Admin
{
    public class StartPprofResponse
    {
        [JsonProperty("result")]
        public bool Result { get; set; }
    }
}
