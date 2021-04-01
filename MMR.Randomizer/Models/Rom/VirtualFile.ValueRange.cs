using System;

namespace MMR.Randomizer.Models.Rom
{
    public readonly partial struct VirtualFile
    {
        /// <summary>
        /// Range type with underlying <see cref="uint"/> values.
        /// </summary>
        public readonly ref struct ValueRange
        {
            /// <summary>
            /// Range start value.
            /// </summary>
            public readonly uint Start;

            /// <summary>
            /// Range end value.
            /// </summary>
            public readonly uint End;

            /// <summary>
            /// Whether or not this range is empty.
            /// </summary>
            public readonly bool Empty => Start == End;

            /// <summary>
            /// Get the length of this range.
            /// </summary>
            public readonly uint Length => End - Start;

            /// <summary>
            /// Construct with start and end values.
            /// </summary>
            /// <param name="start">Range start value.</param>
            /// <param name="end">Range end value.</param>
            /// <exception cref="ArgumentOutOfRangeException"></exception>
            public ValueRange(uint start, uint end)
            {
                if (end < start)
                {
                    throw new ArgumentOutOfRangeException("end", "End value must be equal to or greater than start value.");
                }
                Start = start;
                End = end;
            }
        }
    }
}
