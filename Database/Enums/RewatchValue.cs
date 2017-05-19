using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    /// <summary>
    /// my_rewatching in a MAL anime response. Since this part of the API is not well documented,
    /// this enum may not be complete. Any values that have not been taken into account will
    /// fall under Other.
    /// </summary>
    public enum RewatchValue
    {
        None,
        VeryLow,
        Low,
        Medium,
        High,
        VeryHigh,
        Other
    }

    public static class RewatchValueExtensions
    {
        private static Dictionary<int, RewatchValue> idToRewatchValue = new Dictionary<int, RewatchValue>()
        {
            { 0, RewatchValue.None },
            { 1, RewatchValue.VeryLow },
            { 2, RewatchValue.Low },
            { 3, RewatchValue.Medium },
            { 4, RewatchValue.High },
            { 6, RewatchValue.VeryHigh }
        };

        private static IDictionary<RewatchValue, int> rewatchValueToId = idToRewatchValue.Invert();

        public static RewatchValue IntToRewatchValue(int val)
        {
            return idToRewatchValue.GetValueOrDefault(val, RewatchValue.Other);
        }

        public static int RewatchValueToInt(RewatchValue val)
        {
            return rewatchValueToId.GetValueOrDefault(val, 0);
        }
    }
}
