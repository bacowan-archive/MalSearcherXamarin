using Database.Objects.Intersections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Objects
{
    /// <summary>
    /// Represents a single Anime in a User's list from MAL. All properties in this class
    /// except for the character list are XML elements from MAL's anime list REST call.
    /// </summary>
    [DebuggerDisplay("ID: {AnimeID}, Title: {Title}")]
    public class Anime : DatabaseObject<Anime>
    {
        /// <summary>
        /// series_animedb_id
        /// </summary>
        public int AnimeID { get; set; }
        /// <summary>
        /// series_title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// series_type
        /// </summary>
        public SeriesType? Type
        {
            get { return SeriesTypeExtensions.IntToSeriesType(IntSeriesType); }
            set { IntSeriesType = SeriesTypeExtensions.SeriesTypeToInt(value); }
        }
        public int IntSeriesType { get; set; }
        /// <summary>
        /// series_synonyms
        /// </summary>
        public string Synonyms { get; set; }
        /// <summary>
        /// series_episodes
        /// </summary>
        public int? Episodes { get; set; }
        /// <summary>
        /// series_status
        /// </summary>
        public SeriesStatus? Status
        {
            get { return SeriesStatusExtensions.IntToSeriesStatus(IntSeriesStatus); }
            set { IntSeriesStatus = SeriesStatusExtensions.SeriesStatusToInt(value); }
        }
        public int IntSeriesStatus { get; set; }
        /// <summary>
        /// series_start
        /// </summary>
        public DateTime? Start { get; set; }
        /// <summary>
        /// series_end
        /// </summary>
        public DateTime? End { get; set; }
        /// <summary>
        /// series_image
        /// </summary>
        public string ImageURL { get; set; }
        /// <summary>
        /// The synopsis of the anime (only available from the search api)
        /// </summary>
        public string Synopsis { get; set; }
        /// <summary>
        /// Intersection rows of the characters with this anime
        /// </summary>
        public virtual HashSet<CharacterAnime> CharacterAnimes { get; internal set; } = CharacterAnimeHashSet.NewHashSet();
        /// <summary>
        /// The Characters in this anime
        /// </summary>
        public IEnumerable<Character> Characters => CharacterAnimes.Select(ca => ca.Character);

        public object[] Key => new object[1] { AnimeID };

        /// <summary>
        /// Adds values from the given anime to this one
        /// </summary>
        /// <param name="anime">The anime to join with this one</param>
        public void CopyFrom(Anime anime)
        {
            AnimeID = anime.AnimeID;
            Title = anime.Title ?? Title;
            Type = anime.Type ?? Type;
            Synonyms = anime.Synonyms ?? Synonyms;
            Episodes = anime.Episodes ?? Episodes;
            Status = anime.Status ?? Status;
            Start = anime.Start ?? Start;
            End = anime.End ?? End;
            ImageURL = anime.ImageURL ?? ImageURL;
            Synopsis = anime.Synopsis ?? Synopsis;
        }

        /*public override bool Equals(object obj)
        {
            Anime other = obj as Anime;
            if (other == null) return false;
            return AnimeID.Equals(other.AnimeID);
        }

        public override int GetHashCode()
        {
            return AnimeID.GetHashCode();
        }*/

        public override string ToString()
        {
            return Title;
        }
    }
}
