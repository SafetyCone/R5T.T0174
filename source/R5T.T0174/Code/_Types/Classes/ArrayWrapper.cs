using System;
using System.Collections;
using System.Collections.Generic;


using R5T.T0142;


namespace R5T.T0174
{
    /// <summary>
    /// A simple wrapper around an instance of the <see cref="IDictionary{TKey, TValue}"/> type.
    /// </summary>
    [UtilityTypeMarker]
    public class ArrayWrapper<T> : IList<T>, ICollection<T>, IEnumerable<T>, IReadOnlyList<T>, IReadOnlyCollection<T>
    {
        #region Static

        public static implicit operator T[](ArrayWrapper<T> distinctArray)
        {
            return distinctArray.Array;
        }

        #endregion


        private T[] Array { get; }

        public int Count => ((ICollection<T>)Array).Count;
        public bool IsReadOnly => ((ICollection<T>)Array).IsReadOnly;

        public T this[int index]
        {
            get => ((IList<T>)Array)[index];
            set => ((IList<T>)Array)[index] = value;
        }


        public ArrayWrapper(T[] array)
        {
            this.Array = array;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)Array).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Array.GetEnumerator();
        }

        public void Add(T item)
        {
            ((ICollection<T>)Array).Add(item);
        }

        public void Clear()
        {
            ((ICollection<T>)Array).Clear();
        }

        public bool Contains(T item)
        {
            return ((ICollection<T>)Array).Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ((ICollection<T>)Array).CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return ((ICollection<T>)Array).Remove(item);
        }

        public int IndexOf(T item)
        {
            return ((IList<T>)Array).IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            ((IList<T>)Array).Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ((IList<T>)Array).RemoveAt(index);
        }
    }
}
