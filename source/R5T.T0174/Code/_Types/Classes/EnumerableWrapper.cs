using System;
using System.Collections;
using System.Collections.Generic;

using R5T.T0142;


namespace R5T.T0174
{
    /// <summary>
    /// A simple wrapper around an instance of the <see cref="IEnumerable{T}"/> type.
    /// </summary>
    [UtilityTypeMarker]
    public class EnumerableWrapper<T> : IEnumerable<T>
    {
        private IEnumerable<T> Enumerable { get; }


        public EnumerableWrapper(IEnumerable<T> enumerable)
        {
            this.Enumerable = enumerable;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.Enumerable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Enumerable.GetEnumerator();
        }
    }
}
