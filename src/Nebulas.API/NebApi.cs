using System.Net.Http;
using System.Threading.Tasks;
using Nebulas.API.Providers;
using Nebulas.API.Responses.Api;

namespace Nebulas.API
{
    public sealed class NebApi
    {
        private readonly IProvider m_provider;

        internal NebApi(IProvider provider)
        {
            m_provider = provider;
        }

        /// <summary>
        /// Return the state of the neb.
        /// </summary>
        /// <returns></returns>
        public NebStateResponse GetNebState()
        {
            return GetNebStateAsync().Result;
        }

        /// <summary>
        /// Return the state of the neb.
        /// </summary>
        /// <returns></returns>
        public async Task<NebStateResponse> GetNebStateAsync()
        {
            return await m_provider.SendRequest<NebStateResponse>(HttpMethod.Get, "nebstate");
        }

        /// <summary>
        /// Return the state of the account. Balance and nonce of the given address will be returned.
        /// </summary>
        /// <param name="address">Hex string of the account addresss.</param>
        /// <param name="height">block account state with height. If not specified, use 0 as tail height.</param>
        /// <returns></returns>
        public AccountStateResponse GetAccountState(string address, int height = 0)
        {
            return GetAccountStateAsync(address, height).Result;
        }

        /// <summary>
        /// Return the state of the account. Balance and nonce of the given address will be returned.
        /// </summary>
        /// <param name="address">Hex string of the account addresss.</param>
        /// <param name="height">block account state with height. If not specified, use 0 as tail height.</param>
        /// <returns></returns>
        public async Task<AccountStateResponse> GetAccountStateAsync(string address, int height = 0)
        {
            var param = new
            {
                address,
                height
            };

            return await m_provider.SendRequest<AccountStateResponse>(HttpMethod.Post, "accountstate", param);
        }

        /// <summary>
        /// Return the latest irreversible block.
        /// </summary>
        /// <returns></returns>
        public LatestIrreversibleBlockResponse GetLatestIrreversibleBlock()
        {
            return GetLatestIrreversibleBlockAsync().Result;
        }

        /// <summary>
        /// Return the latest irreversible block.
        /// </summary>
        /// <returns></returns>
        public async Task<LatestIrreversibleBlockResponse> GetLatestIrreversibleBlockAsync()
        {
            return await m_provider.SendRequest<LatestIrreversibleBlockResponse>(HttpMethod.Get, "lib");
        }

        /// <summary>
        /// Call a smart contract function. The smart contract must have been submited. Method calls are run only on the current node, not broadcast.
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
        public CallResponse Call(string from, string to, string value, int nonce, string gasPrice,
            string gasLimit, string type = null, string contract = null, string binary = null)
        {
            return CallAsync(from, to, value, nonce, gasPrice, gasLimit, type, contract, binary).Result;
        }

        /// <summary>
        /// Call a smart contract function. The smart contract must have been submited. Method calls are run only on the current node, not broadcast.
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
        public async Task<CallResponse> CallAsync(string from, string to, string value, int nonce, string gasPrice,
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
                binary
            };

            return await m_provider.SendRequest<CallResponse>(HttpMethod.Post, "call", param);
        }

        /// <summary>
        /// Submit the signed transaction. The transaction signed value should be return by <see cref="NebAdmin.SignTransactionWithPassphrase"/>.
        /// </summary>
        /// <param name="data">Signed data of transaction</param>
        /// <returns></returns>
        public SendRawTransactionResponse SendRawTransaction(string data)
        {
            return SendRawTransactionAsync(data).Result;
        }

        /// <summary>
        /// Submit the signed transaction. The transaction signed value should be return by <see cref="NebAdmin.SignTransactionWithPassphrase"/>.
        /// </summary>
        /// <param name="data">Signed data of transaction</param>
        /// <returns></returns>
        public async Task<SendRawTransactionResponse> SendRawTransactionAsync(string data)
        {
            var param = new
            {
                data
            };

            return await m_provider.SendRequest<SendRawTransactionResponse>(HttpMethod.Post, "rawtransaction", param);
        }

        /// <summary>
        /// Get block header info by the block hash.
        /// </summary>
        /// <param name="hash">Hex string of transaction hash.</param>
        /// <param name="isFull">If true it returns the full transaction objects, if false only the hashes of the transactions.</param>
        /// <returns></returns>
        public LatestIrreversibleBlockResponse GetBlockByHash(string hash, bool isFull = false)
        {
            return GetBlockByHashAsync(hash, isFull).Result;
        }

        /// <summary>
        /// Get block header info by the block hash.
        /// </summary>
        /// <param name="hash">Hex string of transaction hash.</param>
        /// <param name="isFull">If true it returns the full transaction objects, if false only the hashes of the transactions.</param>
        /// <returns></returns>
        public async Task<LatestIrreversibleBlockResponse> GetBlockByHashAsync(string hash, bool isFull = false)
        {
            var param = new
            {
                hash,
                full_fill_transaction = isFull
            };

            return await m_provider.SendRequest<LatestIrreversibleBlockResponse>(HttpMethod.Post, "getBlockByHash", param);
        }

        /// <summary>
        /// Get block header info by the block hash.
        /// </summary>
        /// <param name="height">Height of transaction hash.</param>
        /// <param name="isFull">If true it returns the full transaction objects, if false only the hashes of the transactions.</param>
        /// <returns></returns>
        public LatestIrreversibleBlockResponse GetBlockByHeight(int height, bool isFull = false)
        {
            return GetBlockByHeightAsync(height, isFull).Result;
        }

        /// <summary>
        /// Get block header info by the block hash.
        /// </summary>
        /// <param name="height">Height of transaction hash.</param>
        /// <param name="isFull">If true it returns the full transaction objects, if false only the hashes of the transactions.</param>
        /// <returns></returns>
        public async Task<LatestIrreversibleBlockResponse> GetBlockByHeightAsync(int height, bool isFull = false)
        {
            var param = new
            {
                height,
                full_fill_transaction = isFull
            };

            return await m_provider.SendRequest<LatestIrreversibleBlockResponse>(HttpMethod.Post, "getBlockByHeight", param);
        }

        /// <summary>
        /// Get transactionReceipt info by tansaction hash. If the transaction not submit or only submit and not packaged on chain, it will reurn not found error.
        /// </summary>
        /// <param name="hash">Hex string of transaction hash.</param>
        /// <returns></returns>
        public TransactionReceiptResponse GetTransactionReceipt(string hash)
        {
            return GetTransactionReceiptAsync(hash).Result;
        }

        /// <summary>
        /// Get transactionReceipt info by tansaction hash. If the transaction not submit or only submit and not packaged on chain, it will reurn not found error.
        /// </summary>
        /// <param name="hash">Hex string of transaction hash.</param>
        /// <returns></returns>
        public async Task<TransactionReceiptResponse> GetTransactionReceiptAsync(string hash)
        {
            var param = new
            {
                hash
            };

            return await m_provider.SendRequest<TransactionReceiptResponse>(HttpMethod.Post, "getTransactionReceipt", param);
        }


        /// <summary>
        /// Get transactionReceipt info by contract address. If contract not exists or packaged on chain, a not found error will be returned.
        /// </summary>
        /// <param name="address">Hex string of contract account address.</param>
        /// <returns></returns>
        public TransactionReceiptResponse GetTransactionByContract(string address)
        {
            return GetTransactionByContractAsync(address).Result;
        }

        /// <summary>
        /// Get transactionReceipt info by contract address. If contract not exists or packaged on chain, a not found error will be returned.
        /// </summary>
        /// <param name="address">Hex string of contract account address.</param>
        /// <returns></returns>
        public async Task<TransactionReceiptResponse> GetTransactionByContractAsync(string address)
        {
            var param = new
            {
                address
            };

            return await m_provider.SendRequest< TransactionReceiptResponse>(HttpMethod.Post, "getTransactionByContract", param);
        }

        ///// <summary>
        ///// Return the subscribed events of transaction & block. The request is a keep-alive connection.
        ///// </summary>
        ///// <param name="topicList">repeated event topic name, string array.</param>
        ///// <returns></returns>
        //public async Task<Stream> Subscribe(params string[] topicList)
        //{
        //    var param = new
        //    {
        //        topics = topicList
        //    };

        //    return await SendRequestWithStreamResponse(HttpMethod.Post, "subscribe", param);
        //}

        /// <summary>
        /// Return current gasPrice.
        /// </summary>
        /// <returns></returns>
        public GasPriceResponse GetGasPrice()
        {
            return GetGasPriceAsync().Result;
        }

        /// <summary>
        /// Return current gasPrice.
        /// </summary>
        /// <returns></returns>
        public async Task<GasPriceResponse> GetGasPriceAsync()
        {
            return await m_provider.SendRequest<GasPriceResponse>(HttpMethod.Get, "getGasPrice");
        }

        /// <summary>
        /// Return the estimate gas of transaction.
        /// </summary>
        /// <param name="from">Hex string of the sender account addresss.</param>
        /// <param name="to">Hex string of the receiver account addresss.</param>
        /// <param name="value">Amount of value sending with this transaction. The unit is Wei (10^-18 NAS).</param>
        /// <param name="nonce">Transaction nonce.</param>
        /// <param name="gasPrice">gasPrice sending with this transaction.</param>
        /// <param name="gasLimit">gasLimit sending with this transaction.</param>
        /// <returns></returns>
        public EstimateGasResponse GetEstimateGas(string from, string to, string value, int nonce, string gasPrice, string gasLimit)
        {
            return GetEstimateGasAsync(from, to, value, nonce, gasPrice, gasLimit).Result;
        }

        /// <summary>
        /// Return the estimate gas of transaction.
        /// </summary>
        /// <param name="from">Hex string of the sender account addresss.</param>
        /// <param name="to">Hex string of the receiver account addresss.</param>
        /// <param name="value">Amount of value sending with this transaction. The unit is Wei (10^-18 NAS).</param>
        /// <param name="nonce">Transaction nonce.</param>
        /// <param name="gasPrice">gasPrice sending with this transaction.</param>
        /// <param name="gasLimit">gasLimit sending with this transaction.</param>
        /// <returns></returns>
        public async Task<EstimateGasResponse> GetEstimateGasAsync(string from, string to, string value, int nonce, string gasPrice, string gasLimit)
        {
            var param = new
            {
                from,
                to,
                value,
                nonce,
                gasPrice,
                gasLimit
            };

            return await m_provider.SendRequest<EstimateGasResponse>(HttpMethod.Post, "estimateGas", param);
        }

        /// <summary>
        /// Return the events list of transaction.
        /// </summary>
        /// <param name="hash">Hex string of transaction hash.</param>
        /// <returns></returns>
        public EventsByHashResponse GetEventsByHash(string hash)
        {
            return GetEventsByHashAsync(hash).Result;
        }

        /// <summary>
        /// Return the events list of transaction.
        /// </summary>
        /// <param name="hash">Hex string of transaction hash.</param>
        /// <returns></returns>
        public async Task<EventsByHashResponse> GetEventsByHashAsync(string hash)
        {
            var param = new
            {
                hash
            };

            return await m_provider.SendRequest<EventsByHashResponse>(HttpMethod.Post, "getEventsByHash", param);
        }

        /// <summary>
        /// GetDynasty get dpos dynasty.
        /// </summary>
        /// <param name="height">block height</param>
        /// <returns></returns>
        public DynastyResponse GetDynasty(int height)
        {
            return GetDynastyAsync(height).Result;
        }

        /// <summary>
        /// GetDynasty get dpos dynasty.
        /// </summary>
        /// <param name="height">block height</param>
        /// <returns></returns>
        public async Task<DynastyResponse> GetDynastyAsync(int height)
        {
            var param = new
            {
                height
            };

            return await m_provider.SendRequest<DynastyResponse>(HttpMethod.Post, "dynasty", param);
        }
    }
}
