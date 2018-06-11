using Newtonsoft.Json;

namespace Nebulas.API.Responses.Api
{
    public class TransactionReceiptResponse
    {
        [JsonProperty("Hash")]
        public string Hash { get; set; }

        [JsonProperty("chainId")]
        public uint ChainId { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("nonce")]
        public string Nonce { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("gas_price")]
        public string GasPrice { get; set; }

        [JsonProperty("gas_limit")]
        public string GasLimit { get; set; }

        [JsonProperty("contract_address")]
        public string ContractAddress { get; set; }

        [JsonProperty("status")]
        public byte Status { get; set; }

        [JsonProperty("gas_used")]
        public string GasUsed { get; set; }

        [JsonProperty("execute_error")]
        public string ExecuteErr { get; set; }

        [JsonProperty("execute_result")]
        public string ExecuteResult { get; set; }
    }
}
