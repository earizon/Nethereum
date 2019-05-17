using Nethereum.RPC.TransactionManagers;
using System;
using System.Collections.Generic;
using System.Text;
using Nethereum.RPC.NonceServices;

namespace Nethereum.Web3.RPC.Accounts
{
    public interface IAccount
    {
        string Address { get; }
        ITransactionManager TransactionManager { get; }

        INonceService NonceService { get; set; }
    }
}
