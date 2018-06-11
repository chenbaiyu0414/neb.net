using System;
using System.Linq;
using System.Numerics;
using Cryptography.ECDSA;

namespace Nebulas.API.Utils
{
    internal static class CryptoUtil
    {
        public static byte[] GetPublicKey(byte[] privateKey,bool compressed)
        {
            var originalkeyBytes = Secp256K1Manager.GetPublicKey(privateKey, compressed);
            var validKeyBytes = new byte[originalkeyBytes.Length - 1];
            Buffer.BlockCopy(originalkeyBytes, 1, validKeyBytes, 0, validKeyBytes.Length);

            return validKeyBytes;
        }

        public static byte[] Sign(byte[] msgHash, byte[] privateKey)
        {
            var signature = Secp256K1Manager.SignCompact(msgHash, privateKey, out var recoveryId);
            return ByteUtil.Contact(signature, new[] {Convert.ToByte(recoveryId)});
        }

        public static byte[] PadToBigEndian(BigInteger number, int digit)
        {
            var value = BitConverter.IsLittleEndian ? number.ToByteArray().Reverse().ToArray() : number.ToByteArray();

            var buff = new byte[digit / 8];

            for (var i = 0; i < value.Length; i++) {
                var start = buff.Length - value.Length + i;
                if ( start >= 0) {
                    buff[start] = value[i];
                }
            }
            return buff;
        }
    }
}
