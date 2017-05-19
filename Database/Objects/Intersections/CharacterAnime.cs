using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Objects.Intersections
{
    /// <summary>
    /// Anime/Character intersection table
    /// </summary>
    [DebuggerDisplay("ID: [{CharacterId}, {AnimeId}], Character: [{Character}], Anime: [{Anime}]")]
    public class CharacterAnime : DatabaseObject<CharacterAnime>
    {
        /// <summary>
        /// Character
        /// </summary>
        public virtual Character Character { get; set; }
        /// <summary>
        /// The character's id
        /// </summary>
        public virtual int CharacterId { get; set; }
        /// <summary>
        /// Anime
        /// </summary>
        public virtual Anime Anime { get; set; }
        /// <summary>
        /// The Anime's id
        /// </summary>
        public virtual int AnimeId { get; set; }

        public object[] Key => new object[2] { CharacterId, AnimeId };

        /// <summary>
        /// Copies properties of the given CharacterAnime to this one.
        /// </summary>
        /// <param name="other">The CharacterAnime to copy from</param>
        public void CopyFrom(CharacterAnime other)
        {
            Character = other.Character;
            CharacterId = other.CharacterId;
            Anime = other.Anime;
            AnimeId = other.AnimeId;
        }

        /*public override bool Equals(object obj)
        {
            CharacterAnime other = obj as CharacterAnime;
            if (other == null || Character == null || Anime == null) return false;
            return Character.Equals(other.Character) && Anime.Equals(other.Anime);
        }

        public override int GetHashCode()
        {
            return new Tuple<int?, int?>(Anime?.AnimeID, Character?.CharacterID).GetHashCode();
        }*/

        public override string ToString()
        {
            return Character.ToString() + ": " + Anime.ToString();
        }
    }

    public static class CharacterAnimeHashSet
    {
        public static CharacterAnimeComparer Comparer = new CharacterAnimeComparer();

        public static HashSet<CharacterAnime> NewHashSet()
        {
            return new HashSet<CharacterAnime>(Comparer);
        }

        public class CharacterAnimeComparer : IEqualityComparer<CharacterAnime>
        {
            public bool Equals(CharacterAnime x, CharacterAnime y)
            {
                if (x?.Character == null || x?.Anime == null || y?.Character == null || y?.Anime == null) return false;
                return x.Character.Equals(y.Character) && x.Anime.Equals(y.Anime);
            }

            public int GetHashCode(CharacterAnime obj)
            {
                return new Tuple<int?, int?>(obj?.Anime?.AnimeID, obj?.Character?.CharacterID).GetHashCode();
            }
        }
    }
}
