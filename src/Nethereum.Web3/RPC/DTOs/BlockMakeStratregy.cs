using Newtonsoft.Json;

namespace Nethereum.Web3.RPC.DTOs
{
    public class QuorumBlockMakeStratregy

    {
        [JsonProperty(PropertyName = "maxblocktime")]
        public int MaxBlockTime { get; set; }

        [JsonProperty(PropertyName = "minblocktime")]
        public int MinBlockTime { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}