using System;
using System.Linq;

namespace Nebulas.API.Utils
{
    internal class ByteUtil
    {
        public static byte[] Contact(params byte[][] buffers)
        {
            var length = buffers.Sum(buffer => buffer.Length);
            var finalBuffer = new byte[length];
            var index = 0;

            foreach (var buffer in buffers)
            {
                Buffer.BlockCopy(buffer, 0, finalBuffer, index, buffer.Length);
                index += buffer.Length;
            }

            return finalBuffer;
        }
    }
}
