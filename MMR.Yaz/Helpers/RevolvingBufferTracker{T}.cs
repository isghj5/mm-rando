using System;
using System.Runtime.CompilerServices;

namespace MMR.Yaz.Helpers
{
    /// <summary>
    /// Contains state used for tracking revolving buffer.
    /// </summary>
    /// <typeparam name="T">Type of element in buffer.</typeparam>
    ref struct RevolvingBufferTracker<T>
    {
        /// <summary>
        /// Amount of items currently being used.
        /// </summary>
        public int Amount { readonly get; private set; }

        /// <summary>
        /// Origin index of buffer.
        /// </summary>
        public int Origin { readonly get; private set; }

        /// <summary>
        /// Underlying buffer.
        /// </summary>
        public readonly Span<T> Buffer;

        /// <summary>
        /// Get full length of buffer.
        /// </summary>
        public readonly int Length => Buffer.Length;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RevolvingBufferTracker(Span<T> buffer)
        {
            this.Amount = 0;
            this.Origin = 0;
            this.Buffer = buffer;
        }

        /// <summary>
        /// Get an element at the specified index in a revolving buffer.
        /// </summary>
        /// <param name="index">Index.</param>
        /// <returns>Element at specified index.</returns>
        /// <remarks>Does not check if the gotten byte is in bounds.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly T Get(int index)
        {
            var position = this.Origin + index;
            if (position < this.Buffer.Length)
            {
                return this.Buffer[position];
            }
            else
            {
                return this.Buffer[position - this.Buffer.Length];
            }
        }

        /// <summary>
        /// Put a given element into a revolving buffer at the specified index.
        /// </summary>
        /// <param name="index">Index.</param>
        /// <param name="item">Item to put.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Put(int index, T item)
        {
            var position = this.Origin + index;
            if (position < this.Buffer.Length)
            {
                this.Buffer[position] = item;
            }
            else
            {
                this.Buffer[position - this.Buffer.Length] = item;
            }
        }

        /// <summary>
        /// Push an item to the "top" of the revolving buffer.
        /// </summary>
        /// <param name="item">Item to push.</param>
        public void Push(T item)
        {
            if (this.Amount < this.Buffer.Length)
            {
                // Buffer is not yet full, overwrite unused item.
                Put(this.Amount, item);
                this.Amount++;
            }
            else
            {
                // Buffer is full, advance origin and overwrite last byte.
                Put(0, item);
                this.Origin = (this.Origin + 1) < this.Buffer.Length ? (this.Origin + 1) : 0;
            }
        }

        /// <summary>
        /// Pop an item from the "top" of the revolving buffer.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Pop()
        {
            if (this.Amount != 0)
            {
                this.Amount--;
            }
        }
    }
}
