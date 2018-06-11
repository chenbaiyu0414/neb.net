using Newtonsoft.Json;

namespace Nebulas.API.Responses.Api
{
    public class NebStateResponse
    {
        [JsonProperty("chain_id")]
        public uint ChainId { get; set; }

        [JsonProperty("tail")]
        public string Tail { get; set; }

        [JsonProperty("lib")]
        public string Lib { get; set; }

        [JsonProperty("height")]
        public string Height { get; set; }

        [JsonProperty("protocol_version")]
        public string ProtocolVersion { get; set; }

        [JsonProperty("synchronized")]
        public bool IsSynchronized { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }
}
