using Newtonsoft.Json;

namespace Nebulas.API.Responses.Admin
{
    public class SendTransactionResponse
    {
        [JsonProperty("txhash")]
        public string TxHash { get; set; }

        [JsonProperty("contract_address")]
        public string ContractAddress { get; set; }
    }
}
