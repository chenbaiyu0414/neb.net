using System;
using System.Linq;
using System.Security.Cryptography;
using Cryptography.ECDSA;
using Nebulas.API.Utils;

namespace Nebulas.API
{
    public sealed class NebAccount
    {
        public enum AddressType : byte
        {
            Normal = 0x57,
            Contract = 0x58
        }

        public const byte ADDRESS_LENGTH = 0x1A;
        public const byte ADDRESS_PREFIX = 0x19;

        public const int KEY_VERSION_3 = 3;
        public const int KEY_VERSION_CURRENT = 4;

        private byte[] m_privateKey;
        private byte[] m_publicKey;
        private byte[] m_address;

        public static NebAccount NewAccount()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var bytes = new byte[32];
                rng.GetNonZeroBytes(bytes);

                return new NebAccount(bytes);
            }
        }

        public static bool IsValidAddress(string address, AddressType type)
        {
            byte[] addressBytes;

            try
            {
                addressBytes = Base58.Decode(address);
            }
            catch
            {
                return false;
            }

            if (addressBytes.Length != ADDRESS_LENGTH)
            {
                return false;
            }

            if (addressBytes[0] != ADDRESS_PREFIX)
            {
                return false;
            }

            if (addressBytes[1] != (byte)type)
            {
                return false;
            }

            var content = new byte[22];
            Buffer.BlockCopy(addressBytes, 0, content, 0, 22);

            var expectChecksum = new byte[4];
            Buffer.BlockCopy(addressBytes, 22, expectChecksum, 0, 4);

            var actualChecksum = new byte[4];
            Buffer.BlockCopy(Sha3Util.Get256Hash(content), 0, actualChecksum, 0, 4);

            return expectChecksum.SequenceEqual(actualChecksum);
        }

        public static NebAccount FromAddress(string address)
        {
            var acc = new NebAccount();
            if (!IsValidAddress(address, AddressType.Normal))
                throw new ArgumentException("Invalid Account Address");

            acc.m_address = Base58.Decode(address);
            return acc;
        }

        public NebAccount()
        {

        }

        public NebAccount(string privateKey)
        {
            if (privateKey.Length != 64)
                throw new ArgumentOutOfRangeException($"{nameof(privateKey)}'s length must be 64");

            var key = new byte[32];

            for (var i = 0; i < 32; i++)
            {
                key[i] = Convert.ToByte(privateKey.Substring(i * 2, 2), 16);
            }

            SetPrivateKey(key);
        }

        public NebAccount(byte[] privateKeyBuffer)
        {
            if (privateKeyBuffer.Length != 32)
                throw new ArgumentOutOfRangeException(nameof(privateKeyBuffer), $"{nameof(privateKeyBuffer)}'s length must be 32");

            SetPrivateKey(privateKeyBuffer);
        }

        public void SetPrivateKey(byte[] privateKeyBuffer)
        {
            m_privateKey = privateKeyBuffer;
            m_publicKey = null;
            m_address = null;
        }

        public byte[] GetPrivateKey() => m_privateKey;

        public string GetPrivateKeyString() => BitConverter.ToString(GetPrivateKey()).Replace("-", "");

        public byte[] GetPublicKey() => m_publicKey ?? (m_publicKey = CryptoUtil.GetPublicKey(m_privateKey, false));

        public string GetPublicKeyString() => BitConverter.ToString(GetPublicKey()).Replace("-", "");

        public byte[] GetAddress()
        {
            if (m_address != null)
                return m_address;

            var pubKey = GetPublicKey();

            if (pubKey.Length != 64)
            {
                pubKey = CryptoUtil.GetPublicKey(pubKey, false);
            }

            pubKey = ByteUtil.Contact(new byte[] { 0x04 }, pubKey);

            var content = Sha3Util.Get256Hash(pubKey);

            content = Ripemd160Manager.GetHash(content);

            content = ByteUtil.Contact(new[] { ADDRESS_PREFIX, (byte)AddressType.Normal }, content);

            var checksum = new byte[4];
            Buffer.BlockCopy(Sha3Util.Get256Hash(content), 0, checksum, 0, 4);

            m_address = ByteUtil.Contact(content, checksum.ToArray());

            return m_address;
        }

        public string GetAddressString()
        {
            var addr = GetAddress();

            return Base58.Encode(addr);
        }
    }
}
