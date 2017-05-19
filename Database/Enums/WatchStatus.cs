using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    /// <summary>
    /// my_status in a MAL anime response. Since this part of the API is not well documented,
    /// this enum may not be complete. Any values that have not been taken into account will
    /// fall under Other.
    /// </summary>
    public enum WatchStatus
    {
        CurrentlyWatching,
        Completed,
        OnHold,
        Dropped,
        // 5?
        PlanToWatch,
        Other
    }

    public static class WatchStatusExtensions
    {
        private static Dictionary<int, WatchStatus> idToSeriesStatus = new Dictionary<int, WatchStatus>()
        {
            { 1, WatchStatus.CurrentlyWatching },
            { 2, WatchStatus.Completed },
            { 3, WatchStatus.OnHold },
            { 4, WatchStatus.Dropped },
            // 5?
            { 6, WatchStatus.PlanToWatch }
        };

        private static IDictionary<WatchStatus, int> watchStatusToId = idToSeriesStatus.Invert();

        public static WatchStatus IntToWatchStatus(int val)
        {
            return idToSeriesStatus.GetValueOrDefault(val, WatchStatus.Other);
        }

        public static int WatchStatusToInt(WatchStatus status)
        {
            return watchStatusToId.GetValueOrDefault(status, 1);
        }
    }
}
