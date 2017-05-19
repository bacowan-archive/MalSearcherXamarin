using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    /// <summary>
    /// series_status in a MAL anime response. Since this part of the API is not well documented,
    /// this enum may not be complete. Any values that have not been taken into account will
    /// fall under Other.
    /// </summary>
    public enum SeriesStatus
    {
        CurrentlyAiring,
        FinishedAiring,
        Other
    }

    public static class SeriesStatusExtensions
    {
        private static Dictionary<int, SeriesStatus> idToSeriesStatus = new Dictionary<int, SeriesStatus>()
        {
            { 1, SeriesStatus.CurrentlyAiring },
            { 2, SeriesStatus.FinishedAiring }
        };

        private static Dictionary<string, SeriesStatus> stringToSeriesStatus = new Dictionary<string, SeriesStatus>()
        {
            { "currently airing", SeriesStatus.CurrentlyAiring },
            { "finished airing", SeriesStatus.FinishedAiring }
        };

        private static IDictionary<SeriesStatus, int> seriesStatusToInt = idToSeriesStatus.Invert();

        public static SeriesStatus IntToSeriesStatus(int val)
        {
            return idToSeriesStatus.GetValueOrDefault(val, SeriesStatus.Other);
        }

        public static SeriesStatus StringToSeriesStatus(string val)
        {
            return stringToSeriesStatus.GetValueOrDefault(val.ToLower(), SeriesStatus.Other);
        }
        
        public static int SeriesStatusToInt(SeriesStatus? status)
        {
            return status != null ? seriesStatusToInt.GetValueOrDefault(status.Value, 1) : 1;
        }
    }
}
