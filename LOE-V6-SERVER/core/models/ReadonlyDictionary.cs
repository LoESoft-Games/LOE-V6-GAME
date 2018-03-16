#region

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace core.artic
{
    //http://stackoverflow.com/questions/678379/is-there-a-read-only-generic-dictionary-available-in-net
    public class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private IDictionary<TKey, TValue> _dictionary { get; set; }

        public ReadOnlyDictionary()
        {
            _dictionary = new Dictionary<TKey, TValue>();
        }

        public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionary)
        {
            _dictionary = dictionary;
        }

        void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            throw ReadOnlyException();
        }

        public bool ContainsKey(TKey key) => _dictionary.ContainsKey(key);

        public ICollection<TKey> Keys => _dictionary.Keys;

        bool IDictionary<TKey, TValue>.Remove(TKey key)
        {
            throw ReadOnlyException();
        }

        public bool TryGetValue(TKey key, out TValue value) => _dictionary.TryGetValue(key, out value);

        public ICollection<TValue> Values => _dictionary.Values;

        public TValue this[TKey key] => _dictionary[key];

        TValue IDictionary<TKey, TValue>.this[TKey key]
        {
            get
            {
                return this[key];
            }
            set
            {
                throw ReadOnlyException();
            }
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            throw ReadOnlyException();
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Clear()
        {
            throw ReadOnlyException();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item) => _dictionary.Contains(item);

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _dictionary.CopyTo(array, arrayIndex);
        }

        public int Count => _dictionary.Count;

        public bool IsReadOnly => true;

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            throw ReadOnlyException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _dictionary.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private static Exception ReadOnlyException() => new NotSupportedException("This dictionary is read-only");
    }
}