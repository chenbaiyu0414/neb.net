using System;
using System.Numerics;
using Google.Protobuf;
using Nebulas.API.Utils;
using Newtonsoft.Json;

namespace Nebulas.API
{
    public class NebTransaction
    {
        private const int ALG_SECP256K1 = 1;

        private static readonly DateTime StartTime = new DateTime(1970, 1, 1);

        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
        };

        private readonly uint m_chainId;
        private readonly NebAccount m_fromAccount;
        private readonly NebAccount m_toAccount;
        private readonly BigInteger m_value;
        private readonly ulong m_nonce;
        private readonly long m_timestamp;
        private readonly BigInteger m_gasPrice;
        private readonly BigInteger m_gasLimit;
        private readonly NebContract m_contract;
        private readonly Corepb.Data m_data;

        public NebTransaction(uint chainId, NebAccount fromAccount, NebAccount toAccount, string value, ulong nonce,
            string gasPrice, string gasLimit, NebContract contract = null)
        {
            m_chainId = chainId;
            m_fromAccount = fromAccount;
            m_toAccount = toAccount;
            m_value = BigInteger.Parse(value);
            m_nonce = nonce;
#if DEBUG
            m_timestamp = 0;
#else
            m_timestamp = GetTimeStamp(DateTime.Now);
#endif
            m_gasPrice = BigInteger.Parse(gasPrice);
            m_gasLimit = BigInteger.Parse(gasLimit);
            m_contract = contract ?? NebContract.DefaultContract;

            if (m_gasPrice <= 0)
            {
                m_gasPrice = new BigInteger(1000000);
            }

            if (m_gasLimit <= 0)
            {
                m_gasLimit = new BigInteger(20000);
            }

            m_data = new Corepb.Data();

            switch (m_contract.Content)
            {
                case NebContract.BinaryContent _:
                    m_data.Type = "binary";
                    m_data.Payload = ByteString.CopyFrom(((NebContract.BinaryContent) m_contract.Content).Data);
                    break;
                case NebContract.DeployContent _:
                    m_data.Type = "deploy";
                    m_data.Payload =
                        ByteString.CopyFromUtf8(JsonConvert.SerializeObject(m_contract.Content, JsonSettings));
                    break;
                case NebContract.CallContent _:
                    m_data.Type = "call";
                    m_data.Payload =
                        ByteString.CopyFromUtf8(JsonConvert.SerializeObject(m_contract.Content, JsonSettings));
                    break;
                default:
                    throw new Exception();
            }
        }

        public byte[] GetTransactionHash()
        {
            return Sha3Util.Get256Hash(ByteUtil.Contact(
                m_fromAccount.GetAddress(),
                m_toAccount.GetAddress(),
                CryptoUtil.PadToBigEndian(m_value, 128),
                CryptoUtil.PadToBigEndian(m_nonce, 64),
                CryptoUtil.PadToBigEndian(m_timestamp, 64),
                m_data.ToByteArray(),
                CryptoUtil.PadToBigEndian(m_chainId, 32),
                CryptoUtil.PadToBigEndian(m_gasPrice, 128),
                CryptoUtil.PadToBigEndian(m_gasLimit, 128)
            ));
        }

        public byte[] SignTransaction()
        {
            if (m_fromAccount.GetPrivateKey() != null)
            {
                var hash = GetTransactionHash();
                //var alg = ALG_SECP256K1;
                return CryptoUtil.Sign(hash, m_fromAccount.GetPrivateKey());
            }

            return null;
        }

        private long GetTimeStamp(DateTime time)
        {
            return (long) ((time - StartTime).TotalMilliseconds / 1000);
        }
    }
}
