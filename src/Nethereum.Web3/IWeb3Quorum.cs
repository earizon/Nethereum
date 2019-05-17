using System.Collections.Generic;
using Nethereum.JsonRpc.Client;
using Nethereum.Web3.RPC.Services;

namespace Nethereum.Web3
{
    public interface IWeb3Quorum
    {
        List<string> PrivateFor { get; }
        string PrivateFrom { get; }
        IQuorumChainService Quorum { get; }

        void ClearPrivateForRequestParameters(IClient client);
        void SetPrivateRequestParameters(IClient client, IEnumerable<string> privateFor, string privateFrom = null);
    }
}
