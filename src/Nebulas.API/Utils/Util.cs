using System;

namespace Nebulas.API.Utils
{
    internal static class Util
    {
        public static decimal ToDecimal(string number)
        {
            var radix = 10;

            if (number.ToLower().StartsWith("0x"))
            {
                radix = 16;
            }

            return Convert.ToDecimal(Convert.ToUInt64(number, radix));
        }
    }
}
