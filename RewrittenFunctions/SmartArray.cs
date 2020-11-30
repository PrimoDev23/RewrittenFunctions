using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewrittenFunctions
{
    /// <summary>
    /// SmartArray covers some useful performance benefits in terms of arrays
    /// </summary>
    /// <typeparam name="T">Array-type</typeparam>
    public class SmartArray<T> : IDisposable where T : struct
    {
        #region Properties + Fields

        public int Length
        {
            get => sourceArray.Length;
        }

        public T[] sourceArray;

        //Minimum size for better perf with ArrayPool
        private const int minSize = 1000;

        private bool minSizeIgnored = false;

        #endregion Properties + Fields

        #region Constructors

        /// <summary>
        /// Create a new SmartArray
        /// </summary>
        /// <param name="size">Size of the array</param>
        /// <param name="ignoreMinSize">Ignore the minimum size and always use ArrayPool</param>
        public SmartArray(int size, bool ignoreMinSize)
        {
            //If we don't want to ignore minSize and size is smaller than
            if (!ignoreMinSize && size < minSize)
            {
                throw new Exception("Better use array allocation here, else there would be no performance benefit");
            }

            minSizeIgnored = ignoreMinSize;

            sourceArray = ArrayPool<T>.Shared.Rent(size);
        }

        /// <summary>
        /// Create a new SmartArray (throws Exception if size is less than minSize)
        /// </summary>
        /// <param name="size">Size of the array</param>
        public SmartArray(int size)
        {
            //If we don't want to ignore minSize and size is smaller than
            if (size < minSize)
            {
                throw new Exception("Better use array allocation here, else there would be no performance benefit");
            }

            sourceArray = ArrayPool<T>.Shared.Rent(size);
        }

        #endregion Constructors

        #region Indexers

        public T this[int index]
        {
            get => sourceArray[index];
            set => sourceArray[index] = value;
        }

        #endregion Indexers

        #region Methods

        public SmartArray<T> Clone()
        {
            SmartArray<T> clone = new SmartArray<T>(sourceArray.Length, minSizeIgnored);

            for (int i = 0; i < clone.Length; i++)
            {
                clone[i] = sourceArray[i];
            }

            return clone;
        }

        /// <summary>
        /// Check if the SmartArray contains a specific item
        /// </summary>
        /// <param name="item">Item to check for</param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            for (int i = 0; i < sourceArray.Length; i++)
            {
                if (sourceArray[i].Equals(item))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Dispose the sourceArray
        /// </summary>
        public void Dispose()
        {
            ArrayPool<T>.Shared.Return(sourceArray, true);
        }

        /// <summary>
        /// Get the index of a specific item
        /// </summary>
        /// <param name="item">Item to check for</param>
        /// <returns></returns>
        public T FirstOrDefault(T item)
        {
            for (int i = 0; i < sourceArray.Length; i++)
            {
                if (sourceArray[i].Equals(item))
                {
                    return sourceArray[i];
                }
            }
            return default;
        }

        /// <summary>
        /// Get the first matching element or the default value
        /// </summary>
        /// <param name="item">Item to check for</param>
        /// <returns></returns>
        public int IndexOf(T item)
        {
            for (int i = 0; i < sourceArray.Length; i++)
            {
                if (sourceArray[i].Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        #endregion Methods
    }
}