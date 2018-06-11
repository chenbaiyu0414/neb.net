using Newtonsoft.Json;

namespace Nebulas.API.Responses.Admin
{
    public class LockAccountResponse
    {
        [JsonProperty("result")]
        public bool Result { get; set; }
    }
}
