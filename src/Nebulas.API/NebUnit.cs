using Nebulas.API.Utils;

namespace Nebulas.API
{
    public static class NebUnit
    {
        public enum Unit : ulong
        {
            None = 0,
            Wei = 1,
            Kwei = 1000,
            Mwei = 1000000,
            Gwei = 1000000000,
            Nas = 1000000000000000000
        }

        public static decimal ToBasic(string number, Unit unit)
        {
            return Util.ToDecimal(number) * (ulong)unit;
        }

        public static decimal FromBasic(string number, Unit unit)
        {
            return Util.ToDecimal(number) / (ulong)unit;
        }

        public static decimal NasToBasic(string number)
        {
            return ToBasic(number, Unit.Nas);
        }
    }
}
