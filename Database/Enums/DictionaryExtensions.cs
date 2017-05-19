using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public static class DictionaryExtensions
    {
        // from http://stackoverflow.com/questions/2601477/dictionary-returning-a-default-value-if-the-key-does-not-exist
        public static TValue GetValueOrDefault<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key,
            TValue defaultValue)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : defaultValue;
        }

        public static IDictionary<TValue, TKey> Invert<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary)
        {
            return dictionary.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);
        }
    }
}
