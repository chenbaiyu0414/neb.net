using System.Net.Http;
using System.Threading.Tasks;
using Nebulas.API.Providers;
using Nebulas.API.Responses.Admin;

namespace Nebulas.API
{
    public sealed class NebAdmin
    {
        private readonly IProvider m_provider;

        internal NebAdmin(IProvider provider)
        {
            m_provider = provider;
        }

        /// <summary>
        /// Return the p2p node info.
        /// </summary>
        /// <returns></returns>
        public NodeInfoResponse GetNodeInfo()
        {
            return GetNodeInfoAsync().Result;
        }

        /// <summary>
        /// Return the p2p node info.
        /// </summary>
        /// <returns></returns>
        public Task<NodeInfoResponse> GetNodeInfoAsync()
        {
            return m_provider.SendRequest<NodeInfoResponse>(HttpMethod.Get, "nodeinfo");
        }

        /// <summary>
        /// Return account list.
        /// </summary>
        /// <returns></returns>
        public AccountsResponse GetAccounts()
        {
            return GetAccountsAsync().Result;
        }

        /// <summary>
        /// Return account list.
        /// </summary>
        /// <returns></returns>
        public Task<AccountsResponse> GetAccountsAsync()
        {
            return m_provider.SendRequest<AccountsResponse>(HttpMethod.Get, "accounts");
        }

        /// <summary>
        /// NewAccount create a new account with passphrase.
        /// </summary>
        /// <param name="passphrase">New account passphrase.</param>
        /// <returns></returns>
        public NewAccountResponse NewAccount(string passphrase)
        {
            return NewAccountAsync(passphrase).Result;
        }

        /// <summary>
        /// NewAccount create a new account with passphrase.
        /// </summary>
        /// <param name="passphrase">New account passphrase.</param>
        /// <returns></returns>
        public Task<NewAccountResponse> NewAccountAsync(string passphrase)
        {
            var param = new
            {
                passphrase
            };

            return m_provider.SendRequest<NewAccountResponse>(HttpMethod.Post, "account/new", param);
        }

        /// <summary>
        /// UnlockAccount unlock account with passphrase. After the default unlock time, the account will be locked.
        /// </summary>
        /// <param name="address">UnLock account address.</param>
        /// <param name="passphrase">UnLock account passphrase.</param>
        /// <param name="duration">Unlock accout duration. The unit is ns (10e-9 s).</param>
        /// <returns></returns>
        public UnLockAccountResponse UnLockAccount(string address, string passphrase, string duration = "30000000000")
        {
            return UnLockAccountAsync(address, passphrase, duration).Result;
        }

        /// <summary>
        /// UnlockAccount unlock account with passphrase. After the default unlock time, the account will be locked.
        /// </summary>
        /// <param name="address">UnLock account address.</param>
        /// <param name="passphrase">UnLock account passphrase.</param>
        /// <param name="duration">Unlock accout duration. The unit is ns (10e-9 s).</param>
        /// <returns></returns>
        public Task<UnLockAccountResponse> UnLockAccountAsync(string address, string passphrase, string duration = "30000000000")
        {
            var param = new
            {
                address,
                passphrase,
                duration
            };

            return m_provider.SendRequest<UnLockAccountResponse>(HttpMethod.Post, "account/unlock", param);
        }

        /// <summary>
        /// UnlockAccount unlock account with passphrase. After the default unlock time, the account will be locked.
        /// </summary>
        /// <param name="address">UnLock account address.</param>
        /// <returns></returns>
        public LockAccountResponse LockAccount(string address)
        {
            return LockAccountAsync(address).Result;
        }

        /// <summary>
        /// UnlockAccount unlock account with passphrase. After the default unlock time, the account will be locked.
        /// </summary>
        /// <param name="address">UnLock account address.</param>
        /// <returns></returns>
        public Task<LockAccountResponse> LockAccountAsync(string address)
        {
            var param = new
            {
                address
            };

            return m_provider.SendRequest<LockAccountResponse>(HttpMethod.Post, "account/lock", param);
        }

        /// <summary>
        /// SignTransactionWithPassphrase sign transaction. The transaction's from addrees must be unlocked before sign call.
        /// </summary>
        /// <param name="transaction">this is the same as the SendTransaction parameters.</param>
        /// <param name="passphrase">from account passphrase</param>
        /// <returns></returns>
        public SignTransactionWithPassphraseResponse SignTransactionWithPassphrase(string transaction, string passphrase)
        {
            return SignTransactionWithPassphraseAsync(transaction, passphrase).Result;
        }

        /// <summary>
        /// SignTransactionWithPassphrase sign transaction. The transaction's from addrees must be unlocked before sign call.
        /// </summary>
        /// <param name="transaction">this is the same as the SendTransaction parameters.</param>
        /// <param name="passphrase">from account passphrase</param>
        /// <returns></returns>
        public Task<SignTransactionWithPassphraseResponse> SignTransactionWithPassphraseAsync(string transaction, string passphrase)
        {
            var param = new
            {
                transaction,
                passphrase
            };

            return m_provider.SendRequest<SignTransactionWithPassphraseResponse>(HttpMethod.Post, "sign", param);
        }

        /// <summary>
        /// SendTransactionWithPassphrase send transaction with passphrase.
        /// </summary>
        /// <param name="transaction">this is the same as the SendTransaction parameters.</param>
        /// <param name="passphrase">from account passphrase</param>
        /// <returns></returns>
        public SendTransactionWithPassphraseResponse SendTransactionWithPassphrase(string transaction, string passphrase)
        {
            return SendTransactionWithPassphraseAsync(transaction, passphrase).Result;
        }

        /// <summary>
        /// SendTransactionWithPassphrase send transaction with passphrase.
        /// </summary>
        /// <param name="transaction">this is the same as the SendTransaction parameters.</param>
        /// <param name="passphrase">from account passphrase</param>
        /// <returns></returns>
        public Task<SendTransactionWithPassphraseResponse> SendTransactionWithPassphraseAsync(string transaction, string passphrase)
        {
            var param = new
            {
                transaction,
                passphrase
            };

            return m_provider.SendRequest< SendTransactionWithPassphraseResponse>(HttpMethod.Post, "transactionWithPassphrase", param);
        }

        /// <summary>
        /// Send the transaction. Parameters from, to, value, nonce, gasPrice and gasLimit are required. If the transaction is to send contract, you must specify the contract.
        /// </summary>
        /// <param name="from">Hex string of the sender account addresss.</param>
        /// <param name="to">Hex string of the receiver account addresss.</param>
        /// <param name="value">Amount of value sending with this transaction. The unit is Wei (10^-18 NAS).</param>
        /// <param name="nonce">Transaction nonce.</param>
        /// <param name="gasPrice">gasPrice sending with this transaction.</param>
        /// <param name="gasLimit">gasLimit sending with this transaction.</param>
        /// <param name="type"> transaction payload type. If the type is specified, the transaction type is determined and the corresponding parameter needs to be passed in, otherwise the transaction type is determined according to the contract and binary data.[optional]</param>
        /// <param name="contract">transaction contract object for deploy/call smart contract. [optional]</param>
        /// <param name="binary">any binary data with a length limit = 64bytes. [optional]</param>
        /// <returns></returns>
        public SendTransactionResponse SendTransaction(string from, string to, string value, int nonce, string gasPrice,
            string gasLimit, string type = null, string contract = null, string binary = null)
        {
            return SendTransactionAsync(from, to, value, nonce, gasPrice, gasLimit, type, contract, binary).Result;
        }

        /// <summary>
        /// Send the transaction. Parameters from, to, value, nonce, gasPrice and gasLimit are required. If the transaction is to send contract, you must specify the contract.
        /// </summary>
        /// <param name="from">Hex string of the sender account addresss.</param>
        /// <param name="to">Hex string of the receiver account addresss.</param>
        /// <param name="value">Amount of value sending with this transaction. The unit is Wei (10^-18 NAS).</param>
        /// <param name="nonce">Transaction nonce.</param>
        /// <param name="gasPrice">gasPrice sending with this transaction.</param>
        /// <param name="gasLimit">gasLimit sending with this transaction.</param>
        /// <param name="type"> transaction payload type. If the type is specified, the transaction type is determined and the corresponding parameter needs to be passed in, otherwise the transaction type is determined according to the contract and binary data.[optional]</param>
        /// <param name="contract">transaction contract object for deploy/call smart contract. [optional]</param>
        /// <param name="binary">any binary data with a length limit = 64bytes. [optional]</param>
        /// <returns></returns>
        public Task<SendTransactionResponse> SendTransactionAsync(string from, string to, string value, int nonce, string gasPrice,
            string gasLimit, string type = null, string contract = null, string binary = null)
        {
            var param = new
            {
                from,
                to,
                value,
                nonce,
                gasPrice,
                gasLimit,
                contract,
                type,
                binary,
            };

            return m_provider.SendRequest<SendTransactionResponse>(HttpMethod.Post, "transaction", param);
        }

        /// <summary>
        /// SignHash sign the hash of a message.
        /// </summary>
        /// <param name="address">Sign address</param>
        /// <param name="hash">A sha3256 hash of the message, base64 encoded.</param>
        /// <param name="alg">Sign algorithm. Default is 1 means SECP256K1</param>
        /// <returns></returns>
        public SignHashResponse SignHash(string address, string hash, int alg = 1)
        {
            return SignHashAsync(address, hash, alg).Result;
        }

        /// <summary>
        /// SignHash sign the hash of a message.
        /// </summary>
        /// <param name="address">Sign address</param>
        /// <param name="hash">A sha3256 hash of the message, base64 encoded.</param>
        /// <param name="alg">Sign algorithm. Default is 1 means SECP256K1</param>
        /// <returns></returns>
        public Task<SignHashResponse> SignHashAsync(string address, string hash, int alg = 1)
        {
            var param = new
            {
                address,
                hash,
                alg
            };

            return m_provider.SendRequest< SignHashResponse>(HttpMethod.Post, "sign/hash", param);
        }

        /// <summary>
        /// StartPprof starts pprof
        /// </summary>
        /// <param name="listen">the address to listen</param>
        /// <returns></returns>
        public StartPprofResponse StartPprof(string listen)
        {
            return StartPprofAsync(listen).Result;
        }

        /// <summary>
        /// StartPprof starts pprof
        /// </summary>
        /// <param name="listen">the address to listen</param>
        /// <returns></returns>
        public Task<StartPprofResponse> StartPprofAsync(string listen)
        {
            var param = new
            {
                listen
            };

            return m_provider.SendRequest<StartPprofResponse>(HttpMethod.Post, "pprof", param);
        }

        /// <summary>
        /// GetConfig return the config current neb is using
        /// </summary>
        /// <returns></returns>
        public ConfigResponse GetConfig()
        {
            return GetConfigAsync().Result;
        }

        /// <summary>
        /// GetConfig return the config current neb is using
        /// </summary>
        /// <returns></returns>
        public Task<ConfigResponse> GetConfigAsync()
        {
            return m_provider.SendRequest<ConfigResponse>(HttpMethod.Get, "getConfig");
        }
    }
}
