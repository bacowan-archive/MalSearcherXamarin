using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Objects
{
    /// <summary>
    /// Represents the language of an anime dub
    /// </summary>
    [DebuggerDisplay("{LanguageID}")]
    public class Language : DatabaseObject<Language>
    {
        public Language() { }

        public Language(string id)
        {
            this.LanguageID = id;
        }

        /// <summary>
        /// The name of the language
        /// </summary>
        public string LanguageID { get; set; }

        public object[] Key => new object[1] { LanguageID };

        /// <summary>
        /// Copies properties of the given Language to this one.
        /// </summary>
        /// <param name="other">The Language to copy from</param>
        public void CopyFrom(Language other)
        {
            LanguageID = other.LanguageID;
        }

        /// <summary>
        /// String representation of the language. Same as the LanguageID.
        /// </summary>
        /// <returns>As String representation of the language.</returns>
        public override string ToString()
        {
            return LanguageID;
        }

        /*public override bool Equals(object obj)
        {
            Language other = obj as Language;
            if (other == null) return false;
            return LanguageID.Equals(other.LanguageID);
        }
        public override int GetHashCode()
        {
            return LanguageID.GetHashCode();
        }*/
    }

    public static class LanguageHashSet
    {
        public static LanguageComparer Comparer = new LanguageComparer();

        public static HashSet<Language> NewHashSet()
        {
            return new HashSet<Language>(Comparer);
        }

        public class LanguageComparer : IEqualityComparer<Language>
        {
            public bool Equals(Language x, Language y)
            {
                if (x?.LanguageID == null || y?.LanguageID == null) return false;
                return x.LanguageID.Equals(y.LanguageID);
            }

            public int GetHashCode(Language obj)
            {
                return obj.LanguageID.GetHashCode();
            }
        }
    }
}
