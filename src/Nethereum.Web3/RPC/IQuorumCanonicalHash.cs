using System.Threading.Tasks;
using Nethereum.JsonRpc.Client;

namespace Nethereum.Web3.RPC
{
    public interface IQuorumCanonicalHash
    {
        RpcRequest BuildRequest(long blockNumber, object id = null);
        Task<string> SendRequestAsync(long blockNumber, object id = null);
    }
}
