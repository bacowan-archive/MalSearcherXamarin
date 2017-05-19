using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Objects
{
    /// <summary>
    /// Represents a MAL User. All properties in this class
    /// are XML elements from MAL's anime list REST call.
    /// </summary>
    [DebuggerDisplay("ID: {UserId}, Username: {Username}")]
    public class User : DatabaseObject<User>
    {
        /// <summary>
        /// user_id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// user_name
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// user_watching
        /// </summary>
        public int Watching { get; set; }
        /// <summary>
        /// user_completed
        /// </summary>
        public int Completed { get; set; }
        /// <summary>
        /// user_onhold
        /// </summary>
        public int OnHold { get; set; }
        /// <summary>
        /// user_dropped
        /// </summary>
        public int Dropped { get; set; }
        /// <summary>
        /// user_plantowatch
        /// </summary>
        public int PlanToWatch { get; set; }
        /// <summary>
        /// user_days_spent_watching
        /// </summary>
        public double DaysSpentWatching { get; set; }
        /// <summary>
        /// A collection of all Anime on this User's Anime List
        /// </summary>
        public virtual HashSet<MyAnimeEntry> AnimeList { get; set; } = MyAnimeEntryHashSet.NewHashSet();

        public object[] Key => new object[1] { UserId };

        /// <summary>
        /// Copies properties of the given User to this one.
        /// </summary>
        /// <param name="other">The User to copy from</param>
        public void CopyFrom(User other)
        {
            UserId = other.UserId;
            Username = other.Username;
            Watching = other.Watching;
            Completed = other.Completed;
            OnHold = other.OnHold;
            Dropped = other.Dropped;
            PlanToWatch = other.PlanToWatch;
            DaysSpentWatching = other.DaysSpentWatching;
            AnimeList = other.AnimeList;
        }

        /*public override bool Equals(object obj)
        {
            User other = obj as User;
            if (other == null) return false;
            return UserId.Equals(other.UserId);
        }
        public override int GetHashCode()
        {
            return UserId.GetHashCode();
        }*/

        public override string ToString()
        {
            return Username;
        }
    }
}
