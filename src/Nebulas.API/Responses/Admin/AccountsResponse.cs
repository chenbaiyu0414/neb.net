using Newtonsoft.Json;

namespace Nebulas.API.Responses.Admin
{
    public class AccountsResponse
    {
        [JsonProperty("addresses")]
        public string Addresses { get; set; }
    }
}
