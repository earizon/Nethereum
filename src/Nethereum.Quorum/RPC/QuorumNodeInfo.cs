using Nethereum.JsonRpc.Client;
using Nethereum.RPC.Infrastructure;
using Nethereum.Web3.RPC;

namespace Nethereum.Quorum.RPC
{
    public class QuorumNodeInfo : GenericRpcRequestResponseHandlerNoParam<Web3.RPC.DTOs.QuorumNodeInfo>, IQuorumNodeInfo
    {
        public QuorumNodeInfo(IClient client) : base(client, ApiMethods.quorum_nodeInfo.ToString())
        {
        }
    }
}