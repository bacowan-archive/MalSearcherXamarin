using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Objects.Intersections
{
    /// <summary>
    /// Intersection table between Characters and Voice Actors for different languages.
    /// </summary>
    [DebuggerDisplay("ID: [{CharacterId}, {ActorId}], Character: [{Character}], Actor: [{Actor}], Language: [{Language}]")]
    public class CharacterActor : DatabaseObject<CharacterActor>
    {
        /// <summary>
        /// The character
        /// </summary>
        public virtual Character Character { get; set; }
        /// <summary>
        /// The character's id
        /// </summary>
        public virtual int CharacterId { get; set; }
        /// <summary>
        /// The actor who plays the character
        /// </summary>
        public virtual VoiceActor Actor { get; set; }
        /// <summary>
        /// The actor's id
        /// </summary>
        public virtual int ActorId { get; set; }
        /// <summary>
        /// The language for which the actor plays the character
        /// </summary>
        public virtual Language Language { get; set; }
        /// <summary>
        /// The Language's id
        /// </summary>
        public virtual string LanguageId { get; set; }

        public object[] Key => new object[2] { CharacterId, ActorId };

        /// <summary>
        /// Copies properties of the given CharacterActor to this one.
        /// </summary>
        /// <param name="other">The CharacterActor to copy from</param>
        public void CopyFrom(CharacterActor other)
        {
            Character = other.Character;
            CharacterId = other.CharacterId;
            Actor = other.Actor;
            ActorId = other.ActorId;
            Language = other.Language;
            LanguageId = other.LanguageId;
        }

        /*public override bool Equals(object obj)
        {
            CharacterActor other = obj as CharacterActor;
            if (other == null || Character == null || Actor == null) return false;
            return Character.Equals(other.Character) && Actor.Equals(other.Actor);
        }

        public override int GetHashCode()
        {
            return new Tuple<int?, int?>(Actor?.VoiceActorID, Character?.CharacterID).GetHashCode();
        }*/

        public override string ToString()
        {
            return Character.ToString() + ": " + Actor.ToString();
        }
    }

    public static class CharacterActorCollectionExtensions
    {
        public static IEnumerable<VoiceActor> GetActorsForCharacter(this ICollection<CharacterActor> collection, Character character)
        {
            return collection.Where(ca => ca.Character == character).Select(ca => ca.Actor);
        }
    }

    public static class CharacterActorHashSet
    {
        public static CharacterActorComparer Comparer = new CharacterActorComparer();

        public static HashSet<CharacterActor> NewHashSet()
        {
            return new HashSet<CharacterActor>(Comparer);
        }

        public class CharacterActorComparer : IEqualityComparer<CharacterActor>
        {
            public bool Equals(CharacterActor x, CharacterActor y)
            {
                if (x?.Character == null || x?.Actor == null || x?.Language == null 
                    || y?.Character == null || y?.Actor == null || y?.Language == null) return false;
                return x.Character.Equals(y.Character) && x.Actor.Equals(y.Actor) && x.Language.Equals(y.Language);
            }

            public int GetHashCode(CharacterActor obj)
            {
                return new Tuple<int?, int?, string>(obj?.Actor?.VoiceActorID, obj?.Character?.CharacterID, obj?.Language?.LanguageID).GetHashCode();
            }
        }
    }
}
