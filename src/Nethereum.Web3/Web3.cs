using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using Common.Logging;
using Nethereum.Contracts.Services;
using Nethereum.JsonRpc.Client;
using Nethereum.RPC;
using Nethereum.RPC.TransactionManagers;
using Nethereum.Signer;
using Nethereum.Util;
using Nethereum.Web3.RPC.Accounts;
using Nethereum.Web3.RPC.Services;

namespace Nethereum.Web3
{
    public enum Flavour
    {
        ETHEREUM,
        QUORUM
    }

    interface IFlavour {
        void InitialiseInnerServices(IClient client, ITransactionManager txManager);
    }

    public class Web3QuorumFlavour : IFlavour, IWeb3Quorum
    {
        public IQuorumChainService Quorum { get; }

        public List<string> PrivateFor { get; private set; }
        public string PrivateFrom { get; private set; }

        public Web3QuorumFlavour(IQuorumChainService _Quorum)
        {
            Quorum = _Quorum ?? throw new Exception("IQuorumChainService _Quorum can not be null\n" +
                "Example ussage:\n" +
                "using Nethereum.Quorum.RPC.bServices \n" +
                "..." +
                "new Web3QuorumFlavour(new QuorumChainService(client));"
            );
        }
        public void InitialiseInnerServices(IClient client, ITransactionManager txManager)
        {
            txManager.DefaultGasPrice = 0;
        }
        //    {

        public void SetPrivateRequestParameters(IClient client, IEnumerable<string> privateFor, string privateFrom = null)
        {
            var list = privateFor.ToList();
            if (privateFor == null || list.Count == 0) throw new ArgumentNullException(nameof(privateFor));
            this.PrivateFor = list;
            this.PrivateFrom = privateFrom;
        //  TODO:(0)
        //  client.OverridingRequestInterceptor = new PrivateForInterceptor(list, privateFrom);
        }
      
        public void ClearPrivateForRequestParameters(IClient client)
        {
           // TODO:(0)
           //  if (client.OverridingRequestInterceptor is PrivateForInterceptor)
           //  {
           //      client.OverridingRequestInterceptor = null;
           //  }
            PrivateFor = null;
              PrivateFrom = null;
          }
    }

    public class Web3 : IWeb3
    {
        private static readonly AddressUtil addressUtil = new AddressUtil();
        private static readonly Sha3Keccack sha3Keccack = new Sha3Keccack();
        private Flavour flavour;
        private Web3QuorumFlavour quorumFlavour;

        public Web3(IClient client, Flavour flavour = Flavour.ETHEREUM )
        {
            this.flavour = flavour;
            Client = client;
            InitialiseInnerServices();
            IntialiseDefaultGasAndGasPrice();
            if (this.flavour == Flavour.QUORUM)
            {
                quorumFlavour = new Web3QuorumFlavour();
            }
        }

        public Web3(IAccount account, IClient client) : this(client)
        {
            TransactionManager = account.TransactionManager;
            TransactionManager.Client = Client;
        }

        public Web3(string url = @"http://localhost:8545/", ILog log = null, AuthenticationHeaderValue authenticationHeader = null)
        {
            IntialiseDefaultRpcClient(url, log, authenticationHeader);
            InitialiseInnerServices();
            IntialiseDefaultGasAndGasPrice();
        }

        public Web3(IAccount account, string url = @"http://localhost:8545/", ILog log = null, AuthenticationHeaderValue authenticationHeader = null) : this(url, log, authenticationHeader)
        {
            TransactionManager = account.TransactionManager;
            TransactionManager.Client = Client;
        }

        public ITransactionManager TransactionManager
        {
            get => Eth.TransactionManager;
            set => Eth.TransactionManager = value;
        }

        public static UnitConversion Convert { get; } = new UnitConversion();

        public static TransactionSigner OfflineTransactionSigner { get; } = new TransactionSigner();

        public IClient Client { get; private set; }

        public IEthApiContractService Eth { get; private set; }
        public IShhApiService Shh { get; private set; }
        public INetApiService Net { get; private set; }
        public IPersonalApiService Personal { get; private set; }

        private void IntialiseDefaultGasAndGasPrice()
        {
            TransactionManager.DefaultGas = Transaction.DEFAULT_GAS_LIMIT;
            TransactionManager.DefaultGasPrice = Transaction.DEFAULT_GAS_PRICE;
        }

        public static string GetAddressFromPrivateKey(string privateKey)
        {
            return EthECKey.GetPublicAddress(privateKey);
        }

        public static bool IsChecksumAddress(string address)
        {
            return addressUtil.IsChecksumAddress(address);
        }

        public static string Sha3(string value)
        {
            return sha3Keccack.CalculateHash(value);
        }

        public static string ToChecksumAddress(string address)
        {
            return addressUtil.ConvertToChecksumAddress(address);
        }

        public static string ToValid20ByteAddress(string address)
        {
            return addressUtil.ConvertToValid20ByteAddress(address);
        }

        protected virtual void InitialiseInnerServices()
        {
            Eth = new EthApiContractService(Client);
            Shh = new ShhApiService(Client);
            Net = new NetApiService(Client);
            Personal = new PersonalApiService(Client);
            if (flavour == Flavour.QUORUM)
            {
                quorumFlavour.InitialiseInnerServices(this.TransactionManager);
            }
        }

        private void IntialiseDefaultRpcClient(string url, ILog log, AuthenticationHeaderValue authenticationHeader)
        {
            Client = new RpcClient(new Uri(url), authenticationHeader, null, null, log);
        }

    }
}