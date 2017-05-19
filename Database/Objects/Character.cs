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
    /// Represents a character from an Anime
    /// </summary>
    [DebuggerDisplay("ID: {CharacterID}, Name: [{Name}]")]
    public class Character : DatabaseObject<Character>
    {
        /// <summary>
        /// The MAL ID of the character
        /// </summary>
        public int CharacterID { get; set; }
        /// <summary>
        /// The Name of the character
        /// </summary>
        public virtual Name Name { get; set; }
        /// <summary>
        /// Intersection rows of the animes with this character
        /// </summary>
        public HashSet<CharacterAnime> CharacterAnimes { get; set; } = CharacterAnimeHashSet.NewHashSet();
        /// <summary>
        /// The Anime to which the character belongs
        /// </summary>
        public IEnumerable<Anime> Anime => CharacterAnimes.Select(ca => ca.Anime);
        /// <summary>
        /// A description of the character
        /// </summary>
        //public string Description { get; set; }
        /// <summary>
        /// A collection of Voice actors for this character
        /// </summary>
        public virtual HashSet<CharacterActor> Actors { get; set; } = CharacterActorHashSet.NewHashSet();

        public object[] Key => new object[1] { CharacterID };

        public Character() { }

        /// <summary>
        /// Copies properties of the given Character to this one.
        /// </summary>
        /// <param name="other">The Character to copy from</param>
        public void CopyFrom(Character other)
        {
            CharacterID = other.CharacterID;
            Name = other.Name;
            CharacterAnimes = other.CharacterAnimes;
            Actors = other.Actors;
        }

        /*public override bool Equals(object obj)
        {
            Character other = obj as Character;
            if (other == null) return false;
            return CharacterID.Equals(other.CharacterID);
        }
        public override int GetHashCode()
        {
            return CharacterID.GetHashCode();
        }*/

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}
