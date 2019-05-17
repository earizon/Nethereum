using Nethereum.JsonRpc.Client;
using Nethereum.RPC.Infrastructure;
using Nethereum.Web3.RPC;

namespace Nethereum.Quorum.RPC
{
    public class QuorumResumeBlockMaker : GenericRpcRequestResponseHandlerNoParam<object>, IQuorumResumeBlockMaker
    {
        public QuorumResumeBlockMaker(IClient client) : base(client, ApiMethods.quorum_resumeBlockMaker.ToString())
        {
        }
    }

}