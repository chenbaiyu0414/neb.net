//This source was modified from https://github.com/Zumisha/SHA-3

using System;
using System.Linq;

namespace Nebulas.API.Utils
{
    internal class Sha3Util
    {
        private const byte StateWidth = 64;
        private const byte StateWidthInBytes = 8;
        private const byte RoundsNumber = 24;

        private static readonly ushort[] RateInBytesArray = { 144, 136, 104, 72 };
        private static readonly ushort[] SecurityArray = { 224, 256, 384, 512 };

        private static readonly byte[,] Offset =
        {
            {0, 36, 3, 41, 18},
            {1, 44, 10, 45, 2},
            {62, 6, 43, 15, 61},
            {28, 55, 25, 21, 56},
            {27, 20, 39, 8, 14}
        };

        private static readonly ulong[] Rc =
        {
            0x0000000000000001,
            0x0000000000008082,
            0x800000000000808A,
            0x8000000080008000,
            0x000000000000808B,
            0x0000000080000001,
            0x8000000080008081,
            0x8000000000008009,
            0x000000000000008A,
            0x0000000000000088,
            0x0000000080008009,
            0x000000008000000A,
            0x000000008000808B,
            0x800000000000008B,
            0x8000000000008089,
            0x8000000000008003,
            0x8000000000008002,
            0x8000000000000080,
            0x000000000000800A,
            0x800000008000000A,
            0x8000000080008081,
            0x8000000000008080,
            0x0000000080000001,
            0x8000000080008008
        };

        private static readonly ulong[,] B = new ulong[5, 5];
        private static readonly ulong[] C = new ulong[5];
        private static readonly ulong[] D = new ulong[5];

        private static byte[] Keccak(ushort security, byte[] messageB)
        {
            var constantIndex = Array.FindIndex(SecurityArray, sc => sc == security);

            if (constantIndex == -1) 
                return new byte[] { 0x00 };

            var rateInBytes = RateInBytesArray[constantIndex];
            var s = new ulong[5, 5];

            for (byte i = 0; i < 5; ++i)
                for (byte j = 0; j < 5; ++j)
                    s[i, j] = 0;

            var k = 0;

            while (k <= messageB.Length - rateInBytes)
            {
                State_Change(s, SubArray(messageB, k, rateInBytes), rateInBytes);
                k += rateInBytes;
            }

            Last_block_proc(s, SubArray(messageB, k, messageB.Length - k), rateInBytes);
            return Squeezing(s, rateInBytes).Take(security / 8).ToArray();
        }

        private static void Last_block_proc(ulong[,] state, byte[] messageB, ushort rateInBytes)
        {
            var delta = (ushort)(rateInBytes - messageB.Length);
            var padMessage = new byte[rateInBytes];
            var pos = 0;

            foreach (var t in messageB)
            {
                padMessage[pos] = t;
                ++pos;
            }

            if (delta == 1)
            {
                padMessage[pos] = 0x86;
            }
            else
            {
                padMessage[pos] = 0x06;
                delta -= 2;
                while (delta > 0)
                {
                    padMessage[pos + delta] = 0x00;
                    --delta;
                }

                padMessage[padMessage.Length - 1] = 0x80;
            }

            State_Change(state, padMessage, rateInBytes);
        }

        private static void State_Change(ulong[,] state, byte[] message, ushort rateInBytes)
        {
            for (byte ib = 0; ib < 5; ++ib)
            {
                for (byte jb = 0; jb < 5; ++jb)
                {
                    var pos = ib + jb * 5;

                    if (pos < rateInBytes / StateWidthInBytes)
                    {
                        pos *= StateWidthInBytes;
                        for (byte i = 0; i < StateWidthInBytes; ++i)
                        {
                            state[ib, jb] ^= (ulong) message[pos + i] << (i * 8);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            for (byte i = 0; i < RoundsNumber; i++)
                state = Round(state, Rc[i]);
        }

        private static byte[] SubArray(byte[] array, int position, int length)
        {
            var subArray = new byte[length];

            for (var i = 0; i < length; ++i)
            {
                subArray[i] = array[i + position];
            }

            return subArray;
        }

        private static byte[] Squeezing(ulong[,] state, ushort rateInBytes)
        {
            var hash = new byte[rateInBytes];
            var cur = 0;

            for (byte jb = 0; jb < 5; ++jb)
            {
                for (byte ib = 0; ib < 5; ++ib)
                {
                    var pos = ib + jb * 5;
                    if (pos < rateInBytes / StateWidthInBytes)
                    {
                        var temp = BitConverter.GetBytes(state[ib, jb]);
                        for (var j = 0; j < 8; ++j)
                        {
                            hash[cur + j] = temp[j];
                        }

                        cur += 8;
                    }
                }
            }

            return hash;
        }

        private static ulong[,] Round(ulong[,] a, ulong rcI)
        {
            byte i, j;

            for (i = 0; i < 5; i++)
                C[i] = a[i, 0] ^ a[i, 1] ^ a[i, 2] ^ a[i, 3] ^ a[i, 4];
            for (i = 0; i < 5; i++)
                D[i] = C[(i + 4) % 5] ^ Rotate(C[(i + 1) % 5], 1, StateWidth);
            for (i = 0; i < 5; i++)
                for (j = 0; j < 5; j++)
                    a[i, j] = a[i, j] ^ D[i];

            for (i = 0; i < 5; i++)
                for (j = 0; j < 5; j++)
                    B[j, (2 * i + 3 * j) % 5] = Rotate(a[i, j], Offset[i, j], StateWidth);

            for (i = 0; i < 5; i++)
                for (j = 0; j < 5; j++)
                    a[i, j] = B[i, j] ^ ((~B[(i + 1) % 5, j]) & B[(i + 2) % 5, j]);

            a[0, 0] = a[0, 0] ^ rcI;

            return a;
        }

        private static ulong Rotate(ulong x, byte n, byte w)
        {
            return (x << (n % w)) | (x >> (w - n % w));
        }

        public static byte[] Get256Hash(byte[] message)
        {
            return Keccak(256, message);
        }
    }
}
