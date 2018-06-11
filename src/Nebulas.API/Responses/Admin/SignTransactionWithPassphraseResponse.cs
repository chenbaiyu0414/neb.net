using Newtonsoft.Json;

namespace Nebulas.API.Responses.Admin
{
    public class SignTransactionWithPassphraseResponse
    {
        [JsonProperty("data")]
        public string Data { get; set; }
    }
}
