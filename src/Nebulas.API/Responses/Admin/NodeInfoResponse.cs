using Newtonsoft.Json;

namespace Nebulas.API.Responses.Admin
{
    public class NodeInfoResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("chain_id")]
        public uint ChainId { get; set; }

        [JsonProperty("coinbase")]
        public string Coinbase { get; set; }

        [JsonProperty("peer_count")]
        public uint PeerCount { get; set; }

        [JsonProperty("synchronized")]
        public bool IsSynchronized { get; set; }

        [JsonProperty("bucket_size")]
        public uint BucketSize { get; set; }

        [JsonProperty("protocol_version")]
        public string ProtocolVersion { get; set; }

        [JsonProperty("route_table")]
        public RouteResponse[] RouteTable { get; set; }

        public class RouteResponse
        {
            [JsonProperty("id")]
            public string Id { get; set;}

            [JsonProperty("address")]
            public string[] Address { get; set; }
        }
    }
}
