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
    /// Represents a Voice Actor or Actress
    /// </summary>
    [DebuggerDisplay("ID: {VoiceActorID}, Name: [{Name}]")]
    public class VoiceActor : DatabaseObject<VoiceActor>
    {
        /// <summary>
        /// MAL ID for the Voice Actor
        /// </summary>
        public int VoiceActorID { get; set; }
        /// <summary>
        /// Primary name of the Voice Actor
        /// </summary>
        public virtual Name Name { get; set; }
        /// <summary>
        /// A collection of Characters that this Voice Actor voices
        /// </summary>
        public virtual HashSet<CharacterActor> Characters { get; set; } = CharacterActorHashSet.NewHashSet();

        public object[] Key => new object[1] { VoiceActorID };

        /// <summary>
        /// Copies properties of the given VoiceActor to this one.
        /// </summary>
        /// <param name="other">The VoiceActor to copy from</param>
        public void CopyFrom(VoiceActor other)
        {
            VoiceActorID = other.VoiceActorID;
            Name = other.Name;
            Characters = other.Characters;
        }

        /*public override bool Equals(object obj)
        {
            VoiceActor other = obj as VoiceActor;
            if (other == null) return false;
            return VoiceActorID.Equals(other.VoiceActorID);
        }

        public override int GetHashCode()
        {
            return VoiceActorID.GetHashCode();
        }*/

        public override string ToString()
        {
            return Name.ToString();
        }
    }

    public static class VoiceActorHashSet
    {
        public static VoiceActorComparer Comparer = new VoiceActorComparer();

        public static HashSet<VoiceActor> NewHashSet()
        {
            return new HashSet<VoiceActor>();
        }

        public class VoiceActorComparer : IEqualityComparer<VoiceActor>
        {
            public bool Equals(VoiceActor x, VoiceActor y)
            {
                if (x == null || y == null) return false;
                return x.VoiceActorID.Equals(y.VoiceActorID);
            }

            public int GetHashCode(VoiceActor obj)
            {
                return obj.VoiceActorID.GetHashCode();
            }
        }
    }
}
