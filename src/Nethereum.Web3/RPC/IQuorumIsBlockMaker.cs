using System.Threading.Tasks;
using Nethereum.JsonRpc.Client;

namespace Nethereum.Web3.RPC
{
    public interface IQuorumIsBlockMaker
    {
        RpcRequest BuildRequest(string address, object id = null);
        Task<bool> SendRequestAsync(string address, object id = null);
    }
}
