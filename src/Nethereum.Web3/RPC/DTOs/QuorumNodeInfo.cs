using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Nethereum.Web3.RPC.DTOs
{
    public class QuorumNodeInfo
    {
        [JsonProperty(PropertyName = "blockMakerAccount")]
        public string BlockMakerAccount { get; set; }

        [JsonProperty(PropertyName = "voteAccount")]
        public string VoteAccount { get; set; }

        [JsonProperty(PropertyName = "blockmakestrategy")]
        public QuorumBlockMakeStratregy BlockMakeStratregy { get; set; }

        [JsonProperty(PropertyName = "canCreateBlocks")]
        public bool CanCreateBlocks { get; set; }

        [JsonProperty(PropertyName = "canVote")]
        public bool CanVote { get; set; }
    }
}
