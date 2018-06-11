using Newtonsoft.Json;

namespace Nebulas.API.Responses.Admin
{
    public class UnLockAccountResponse
    {
        [JsonProperty("result")]
        public bool Result { get; set; }
    }
}
