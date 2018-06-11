using Newtonsoft.Json;

namespace Nebulas.API.Responses.Api
{
    public class SendRawTransactionResponse
    {
        [JsonProperty("txhash")]
        public string TxHash { get; set; }

        [JsonProperty("contract_address")]
        public string ContractAddress { get; set; }
    }
}
