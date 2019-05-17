using Nethereum.RPC.Eth.DTOs;
using Newtonsoft.Json;

namespace Nethereum.Web3.RPC.DTOs
{
    public class QuorumPrivateTransactionInput : TransactionInput
    {
        public QuorumPrivateTransactionInput()
        {
        }

        public QuorumPrivateTransactionInput(TransactionInput transaction, string[] privateFor, string privateFrom)
        {
            PrivateFrom = privateFrom;
            PrivateFor = privateFor;
            From = transaction.From;
            Gas = transaction.Gas;
            GasPrice = transaction.GasPrice;
            Nonce = transaction.Nonce;
            To = transaction.To;
            Data = transaction.Data;
            Value = transaction.Value;
        }

        [JsonProperty(PropertyName = "privateFrom")]
        public string PrivateFrom { get; set; }

        [JsonProperty(PropertyName = "privateFor")]
        public string[] PrivateFor { get; set; }
    }
}