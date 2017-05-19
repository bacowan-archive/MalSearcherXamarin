using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Objects
{
    /// <summary>
    /// Represents personalized values that a User has given an Anime in a User's list from MAL.
    /// (such as score and watch status). All properties in this class are XML elements from MAL's
    /// anime list REST call.
    /// </summary>
    [DebuggerDisplay("ID: [{AnimeId}, {UserId}], Anime: [{Anime}], User: [{User}]")]
    public class MyAnimeEntry : DatabaseObject<MyAnimeEntry>
    {
        /// <summary>
        /// The id of the anime to which this entry belongs
        /// </summary>
        public int AnimeId { get; set; }
        /// <summary>
        /// The id of the user who's entry this is
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// The User who's entry this is
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// The Anime to which this entry belongs
        /// </summary>
        public virtual Anime Anime { get; set; }
        /// <summary>
        /// my_id
        /// </summary>
        public int MyIdValue { get; set; }
        /// <summary>
        /// my_watched_episodes
        /// </summary>
        public int WatchedEpisodes { get; set; }
        /// <summary>
        /// my_start_date
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// my_end_date
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// my_score
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// my_status
        /// </summary>
        public WatchStatus Status
        {
            get { return WatchStatusExtensions.IntToWatchStatus(IntStatus); }
            set { IntStatus = WatchStatusExtensions.WatchStatusToInt(value); }
        }
        public int IntStatus { get; set; }
        /// <summary>
        /// my_rewatching_ep
        /// </summary>
        public int RewatchedEpisodes { get; set; }
        /// <summary>
        /// my_rewatching
        /// </summary>
        public RewatchValue RewatchValue
        {
            get { return RewatchValueExtensions.IntToRewatchValue(IntRewatchValue); }
            set { IntRewatchValue = RewatchValueExtensions.RewatchValueToInt(value); }
        }
        public int IntRewatchValue { get; set; }
        /// <summary>
        /// my_last_updated
        /// </summary>
        public long LastUpdated { get; set; }
        // tags?

        public object[] Key => new object[2] { AnimeId, UserId };

        /// <summary>
        /// Copies properties of the given MyAnimeEntry to this one.
        /// </summary>
        /// <param name="other">The MyAnimeEntry to copy from</param>
        public void CopyFrom(MyAnimeEntry other)
        {
            AnimeId = other.AnimeId;
            UserId = other.UserId;
            Anime = other.Anime;
            User = other.User;
            MyIdValue = other.MyIdValue;
            WatchedEpisodes = other.WatchedEpisodes;
            StartDate = other.StartDate;
            EndDate = other.EndDate;
            Score = other.Score;
            Status = other.Status;
            RewatchedEpisodes = other.RewatchedEpisodes;
            RewatchValue = other.RewatchValue;
            LastUpdated = other.LastUpdated;
        }

        /*public override bool Equals(object obj)
        {
            MyAnimeEntry other = obj as MyAnimeEntry;
            if (other == null || Anime == null || User == null) return false;
            return Anime.Equals(other.Anime) && User.Equals(other.User);
        }

        public override int GetHashCode()
        {
            return new Tuple<int?, int?>(Anime?.AnimeID ?? 0, User?.UserId ?? 0).GetHashCode();
        }*/

        public override string ToString()
        {
            return Anime.ToString();
        }
    }

    public static class MyAnimeEntryHashSet
    {
        public static MyAnimeEntryComparer Comparer = new MyAnimeEntryComparer();

        public static HashSet<MyAnimeEntry> NewHashSet()
        {
            return new HashSet<MyAnimeEntry>(Comparer);
        }

        public class MyAnimeEntryComparer : IEqualityComparer<MyAnimeEntry>
        {
            public bool Equals(MyAnimeEntry x, MyAnimeEntry y)
            {
                if (x?.Anime == null || y?.Anime == null || x.User == null || y.User == null) return false;
                return x.Anime.Equals(y.Anime) && x.User.Equals(y.User);
            }

            public int GetHashCode(MyAnimeEntry obj)
            {
                return new Tuple<int?, int?>(obj?.Anime?.AnimeID, obj?.User?.UserId).GetHashCode();
            }
        }
    }
}
