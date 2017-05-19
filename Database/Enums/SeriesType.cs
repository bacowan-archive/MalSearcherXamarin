using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    /// <summary>
    /// series_type in a MAL anime response. Since this part of the API is not well documented,
    /// this enum may not be complete. Any values that have not been taken into account will
    /// fall under Other.
    /// </summary>
    public enum SeriesType
    {
        TV,
        OVA,
        Movie,
        Special,
        ONA,
        Music,
        Other
    }

    public static class SeriesTypeExtensions
    {
        private static IDictionary<int, SeriesType> idToSeriesType = new Dictionary<int, SeriesType>()
        {
            { 1, SeriesType.TV },
            { 2, SeriesType.OVA },
            { 3, SeriesType.Movie },
            { 4, SeriesType.Special },
            { 5, SeriesType.ONA },
            { 6, SeriesType.Music }
        };

        private static IDictionary<string, SeriesType> stringToSeriesType = new Dictionary<string, SeriesType>()
        {
            { "tv", SeriesType.TV },
            { "ova", SeriesType.OVA },
            { "movie", SeriesType.Movie },
            { "special", SeriesType.Special },
            { "ona", SeriesType.ONA },
            { "music", SeriesType.Music }
        };

        private static IDictionary<SeriesType, int> seriesTypeToId = idToSeriesType.Invert();

        public static SeriesType IntToSeriesType(int val)
        {
            return idToSeriesType.GetValueOrDefault(val, SeriesType.Other);
        }

        public static SeriesType StringToSeriesType(string val)
        {
            return stringToSeriesType.GetValueOrDefault(val.ToLower(), SeriesType.Other);
        }

        public static int SeriesTypeToInt(SeriesType? seriesType)
        {
            return seriesType != null ? seriesTypeToId.GetValueOrDefault(seriesType.Value, 1) : 1;
        }
    }
}
