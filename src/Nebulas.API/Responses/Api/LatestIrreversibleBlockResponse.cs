using Newtonsoft.Json;

namespace Nebulas.API.Responses.Api
{
    public class LatestIrreversibleBlockResponse
    {
        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("parent_hash")]
        public string ParentHash { get; set; }

        [JsonProperty("height")]
        public string Height { get; set; }

        [JsonProperty("nonce")]
        public string Nonce { get; set; }

        [JsonProperty("coinbase")]
        public string Coinbase { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("chain_id")]
        public uint ChainId { get; set; }

        [JsonProperty("state_root")]
        public string StateRoot { get; set; }

        [JsonProperty("txs_root")]
        public string TxsRoot { get; set; }

        [JsonProperty("events_root")]
        public string EventsRoot { get; set; }

        [JsonProperty("consensus_root")]
        public InternalConsensusRoot ConsensusRoot { get; set; }

        [JsonProperty("miner")]
        public string Miner { get; set; }

        [JsonProperty("is_finality")]
        public bool IsFinality { get; set; }

        [JsonProperty("transactions")]
        public TransactionReceiptResponse Transactions { get; set; }

        public class InternalConsensusRoot
        {
            [JsonProperty("timestamp")]
            public string Timestamp { get; set; }

            [JsonProperty("proposer")]
            public string Proposer { get; set; }

            [JsonProperty("dynasty_root")]
            public string DynastyRoot { get; set; }
        }
    }
}
