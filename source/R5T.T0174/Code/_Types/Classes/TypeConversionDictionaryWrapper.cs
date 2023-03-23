using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using R5T.T0142;
using R5T.T0177;


namespace R5T.T0174
{
    /// <summary>
    /// A simple wrapper around an instance of the <see cref="IDictionary{TKey, TValue}"/> type.
    /// </summary>
    [UtilityTypeMarker]
    public class TypeConversionDictionaryWrapper<TKeyOuter, TValueOuter, TKeyInner, TValueInner> : IDictionary<TKeyOuter, TValueOuter>
    {
        private IDictionary<TKeyInner, TValueInner> InnerDictionary { get; }
        private IDictionary<TKeyOuter, TValueOuter> OuterDictionary { get; }
        private IConverter<TKeyInner, TKeyOuter> KeyConverter { get; }
        public IConverter<TValueInner, TValueOuter> ValueConverter { get; }


        public TypeConversionDictionaryWrapper(
            IDictionary<TKeyInner, TValueInner> innerDictionary,
            IConverter<TKeyInner, TKeyOuter> keyConverter,
            IConverter<TValueInner, TValueOuter> valueConverter)
        {
            this.InnerDictionary = innerDictionary;
            this.KeyConverter = keyConverter;
            this.ValueConverter = valueConverter;

            this.OuterDictionary = new Dictionary<TKeyOuter, TValueOuter>(
                this.InnerDictionary
                    .Select(x => new KeyValuePair<TKeyOuter, TValueOuter>(
                        this.KeyConverter.From(x.Key),
                        this.ValueConverter.From(x.Value))));
        }

        public TValueOuter this[TKeyOuter key]
        {
            get
            {
                return this.OuterDictionary[key];
            }
            set
            {
                // Do inner first in case of errors.
                var innerKey = this.KeyConverter.From(key);
                var innerValue = this.ValueConverter.From(value);

                this.InnerDictionary[innerKey] = innerValue;

                // Now do outer.
                this.OuterDictionary[key] = value;
            }
        }

        public ICollection<TKeyOuter> Keys => this.OuterDictionary.Keys;
        public ICollection<TValueOuter> Values => this.OuterDictionary.Values;
        public int Count => this.InnerDictionary.Count;
        public bool IsReadOnly => this.InnerDictionary.IsReadOnly;


        public void Add(TKeyOuter key, TValueOuter value)
        {
            // Do inner first in case of errors.
            var innerKey = this.KeyConverter.From(key);
            var innerValue = this.ValueConverter.From(value);

            this.InnerDictionary.Add(innerKey, innerValue);

            // Now do outer.
            this.OuterDictionary.Add(key, value);
        }

        public void Add(KeyValuePair<TKeyOuter, TValueOuter> item)
        {
            this.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            this.InnerDictionary.Clear();
            this.OuterDictionary.Clear();
        }

        public bool Contains(KeyValuePair<TKeyOuter, TValueOuter> item)
        {
            return this.OuterDictionary.Contains(item);
        }

        public bool ContainsKey(TKeyOuter key)
        {
            return this.OuterDictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<TKeyOuter, TValueOuter>[] array, int arrayIndex)
        {
            this.OuterDictionary.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<TKeyOuter, TValueOuter>> GetEnumerator()
        {
            return this.OuterDictionary.GetEnumerator();
        }

        public bool Remove(TKeyOuter key)
        {
            // Do inner first in case of errors.
            var innerKey = this.KeyConverter.From(key);

            var output = this.InnerDictionary.Remove(innerKey);

            // Now do outer.
            this.OuterDictionary.Remove(key);

            // Return value from the inner dictionary for precision.
            return output;
        }

        public bool Remove(KeyValuePair<TKeyOuter, TValueOuter> item)
        {
            return this.Remove(item.Key);
        }

        public bool TryGetValue(TKeyOuter key, out TValueOuter value)
        {
            var innerKey = this.KeyConverter.From(key);

            var output = this.InnerDictionary.TryGetValue(innerKey, out var innerValue);

            value = output
                ? this.ValueConverter.From(innerValue)
                : default
                ;

            return output;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.InnerDictionary.GetEnumerator();
        }
    }
}
