using Newtonsoft.Json;

namespace Nebulas.API.Responses.Api
{
    public class AccountStateResponse
    {
        [JsonProperty("balance")]
        public string Balance { get; set; }

        [JsonProperty("nonce")]
        public ulong Nonce { get; set; }

        [JsonProperty("type")]
        public NebAccount.AddressType Type { get; set; }
    }
}
