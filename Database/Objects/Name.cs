using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Objects
{
    /// <summary>
    /// A Character's or Person's name, represented in different languages
    /// </summary>
    [DebuggerDisplay("English: {English}, Japanese: {Japanese}")]
    public class Name : DatabaseObject<Name>
    {
        public int NameId { get; set; }
        /// <summary>
        /// Name in the Roman characterset
        /// </summary>
        public string English { get; set; }
        /// <summary>
        /// Name in Japanese
        /// </summary>
        public string Japanese { get; set; }

        public object[] Key => new object[1] { NameId };

        /// <summary>
        /// Copies properties of the given Name to this one.
        /// </summary>
        /// <param name="other">The Name to copy from</param>
        public void CopyFrom(Name other)
        {
            NameId = other.NameId;
            English = other.English;
            Japanese = other.Japanese;
        }

        public override string ToString()
        {
            return English ?? Japanese;
        }

        /*public override bool Equals(object obj)
        {
            Name other = obj as Name;
            if (other == null) return false;
            return NameId.Equals(other.NameId);
        }

        public override int GetHashCode()
        {
            return NameId.GetHashCode();
        }*/
    }

    public static class NameHashSet
    {
        public static NameComparer Comparer = new NameComparer();

        public static HashSet<Name> NewHashSet()
        {
            return new HashSet<Name>(Comparer);
        }

        public class NameComparer : IEqualityComparer<Name>
        {
            public bool Equals(Name x, Name y)
            {
                if (x?.NameId == null || y?.NameId == null) return false;
                return x.NameId.Equals(y.NameId);
            }

            public int GetHashCode(Name obj)
            {
                return obj.NameId.GetHashCode();
            }
        }
    }
}
