using Newtonsoft.Json;

namespace Nebulas.API.Responses.Admin
{
    public class NewAccountResponse
    {
        [JsonProperty("address")]
        public string Address { get; set; }
    }
}
