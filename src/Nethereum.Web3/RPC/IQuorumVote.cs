using System.Threading.Tasks;
using Nethereum.JsonRpc.Client;

namespace Nethereum.Web3.RPC
{
    public interface IQuorumVote
    {
        RpcRequest BuildRequest(string hash, object id = null);
        Task<string> SendRequestAsync(string hash, object id = null);
    }
}
