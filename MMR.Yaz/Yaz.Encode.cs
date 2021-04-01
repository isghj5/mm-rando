using MMR.Yaz.Helpers;
using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace MMR.Yaz
{
    public static partial class Yaz
    {
        /// <summary>
        /// Maximum number of bytes usable in RLE chunk.
        /// </summary>
        public const int MaxCompare = 0xFF + 0x12;

        /// <summary>
        /// Calculate maximum possible length of data if it were encoded. Does not include length of header.
        /// </summary>
        /// <param name="decodedLength">Length of decoded data.</param>
        /// <returns>Maximum possible length of encoded data.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateMaxEncodedLength(int decodedLength)
        {
            // Worst case scenario for encoding: every command byte is 0xFF (direct-copy for all 8 bytes).
            var extra = ((decodedLength % 8) == 0) ? 0 : 1;
            return ((decodedLength / 8) + extra) * 9;
        }

        /// <summary>
        /// Perform encode into a managed destination buffer.
        /// </summary>
        /// <param name="src">Source buffer.</param>
        /// <param name="dst">Destination buffer.</param>
        /// <param name="managed">Whether or not to use managed buffers for state relating to encode operation.</param>
        /// <returns>Length of encoded bytes.</returns>
        public static int Encode(ReadOnlySpan<byte> src, out byte[] dst, bool managed = false)
        {
            dst = new byte[HeaderSize + CalculateMaxEncodedLength(src.Length)];
            return EncodeWithHeader(src, dst, managed);
        }

        /// <summary>
        /// Perform encode into given destination buffer along with a header.
        /// </summary>
        /// <param name="src">Soruce buffer.</param>
        /// <param name="dst">Destination buffer.</param>
        /// <param name="managed">Whether or not to use managed buffers for state relating to encode operation.</param>
        /// <returns>Length of encoded bytes.</returns>
        public static int EncodeWithHeader(ReadOnlySpan<byte> src, Span<byte> dst, bool managed = false)
        {
            // Write Yaz0 header bytes and encode.
            WriteHeader(dst, (uint)src.Length);

            if (managed)
            {
                return EncodeRawManaged(src, dst.Slice(HeaderSize)) + HeaderSize;
            }
            else
            {
                return EncodeRaw(src, dst.Slice(HeaderSize)) + HeaderSize;
            }
        }

        /// <summary>
        /// Perform encode into a managed destination buffer, and copy encoded data into a 16-byte aligned buffer of the minimum length needed.
        /// </summary>
        /// <param name="src">Source buffer.</param>
        /// <returns>Resulting buffer with encoded data.</returns>
        public static byte[] EncodeAndCopy(ReadOnlySpan<byte> src, bool managed = false)
        {
            var written = Encode(src, out var dst, managed);

            // Allocate new buffer with aligned size, and copy data over.
            var result = new byte[AlignTo16(written)];
            dst.AsSpan().Slice(0, written).CopyTo(result);
            return result;
        }

        /// <summary>
        /// Perform raw encode using stack buffers for state relating to encode operation.
        /// </summary>
        /// <param name="src">Source buffer.</param>
        /// <param name="dst">Destination buffer.</param>
        /// <returns>Length of encoded bytes.</returns>
        /// <remarks>Uses at least 17 KiB of stack-allocated space (<c>0x4400</c> bytes).</remarks>
        public static int EncodeRaw(ReadOnlySpan<byte> src, Span<byte> dst)
        {
            Span<int> recents = stackalloc int[0x100];
            Span<int> windowBuf = stackalloc int[0x1000];

            // Call internal to skip bounds checks.
            return EncodeRaw_Internal(src, dst, recents, windowBuf);
        }

        /// <summary>
        /// Perform raw encode using managed buffers for state relating to encode operation, avoiding large stack allocations.
        /// </summary>
        /// <param name="src">Source buffer.</param>
        /// <param name="dst">Destination buffer.</param>
        /// <returns>Length of encoded bytes.</returns>
        public static int EncodeRawManaged(ReadOnlySpan<byte> src, Span<byte> dst)
        {
            Span<int> recents = new int[0x100];
            Span<int> windowBuf = new int[0x1000];

            // Call internal to skip bounds checks.
            return EncodeRaw_Internal(src, dst, recents, windowBuf);
        }

        /// <summary>
        /// Perform raw encode using provided buffers for "Deltas" and "Recents" values with specified <see cref="LookupScheme"/>.
        /// </summary>
        /// <param name="src">Source buffer.</param>
        /// <param name="dst">Destination buffer.</param>
        /// <param name="recents">Provided buffer for storing Recents.</param>
        /// <param name="windowBuf">Provided buffer for storing Deltas.</param>
        /// <returns>Length of encoded bytes.</returns>
        public static int EncodeRaw(ReadOnlySpan<byte> src, Span<byte> dst, Span<int> recents, Span<int> windowBuf, LookupScheme lookupScheme = LookupScheme.LookAhead)
        {
            if (recents.Length < 0x100)
            {
                ThrowArgumentOutOfRangeExceptionForRecents("recents");
            }

            if (windowBuf.Length > 0x1000)
            {
                ThrowArgumentOutOfRangeExceptionForDeltas("windowBuf");
            }

            return EncodeRaw_Internal(src, dst, recents, windowBuf, lookupScheme);
        }

        /// <summary>
        /// Perform raw encode using provided buffers for "Deltas" and "Recents" values with specified <see cref="LookupScheme"/>.
        /// </summary>
        /// <param name="src">Source buffer.</param>
        /// <param name="dst">Destination buffer.</param>
        /// <param name="recents">Provided buffer for storing Recents.</param>
        /// <param name="windowBuf">Provided buffer for storing Deltas.</param>
        /// <param name="lookupScheme">Scheme to use for lookups.</param>
        /// <returns>Length of encoded bytes.</returns>
        public static int EncodeRaw_Internal(
            ReadOnlySpan<byte> src,
            Span<byte> dst,
            Span<int> recents,
            Span<int> windowBuf,
            LookupScheme lookupScheme = LookupScheme.LookAhead)
        {
            // Variables to keep track of indexes for src/dst buffers, and code byte.
            int codeBytePlace = 0, commandBit = 0, dstPlace = 0, srcPlace = 0;

            // Helper for tracking state of "Deltas" revolving buffer.
            var window = new RevolvingBufferTracker<int>(windowBuf);

            // Recent lookup result.
            var result = LookupResult.NoneFound();

            // Whether or not to use LookAhead lookup scheme.
            bool useLookAheadScheme = lookupScheme == LookupScheme.LookAhead;

            while (srcPlace < src.Length)
            {
                if (commandBit == 0)
                {
                    // Reserve code byte location for modifying.
                    codeBytePlace = dstPlace++;
                    dst[codeBytePlace] = 0;
                }

                // Perform lookup if result does not have a valid offset from previous loop.
                if (!result.Found)
                {
                    if (useLookAheadScheme)
                    {
                        // Perform lookup for both current byte and next byte.
                        result = LookupWithAhead(src, srcPlace, ref window, recents);
                    }
                    else
                    {
                        // Perform standard lookup for only the current byte.
                        result = Lookup(src, srcPlace, ref window, recents);
                    }
                }

                // Append to Deltas window and Recents.
                Remember(src, srcPlace, ref window, recents);

                // If found chunk in lookup, encode information about it.
                if (!result.SkipByte && result.Found)
                {
                    // Calculate distance between current src offset and chunk offset.
                    var distance = srcPlace - result.Offset - 1;

                    // Write bytes to indicate RLE copy.
                    // Encodes differently depending on chunk length.
                    if (result.Length < 0x12)
                    {
                        // Encode for chunk size smaller than 0x12. Uses 2 bytes.
                        dst[dstPlace] = (byte)(((result.Length - 2) << 4) | (distance >> 8));
                        dst[dstPlace + 1] = (byte)(distance & 0xFF);
                        dstPlace += 2;
                    }
                    else
                    {
                        // Encode for chunk size 0x12 or larger (up to 0x111). Uses 3 bytes.
                        dst[dstPlace] = (byte)(distance >> 8);
                        dst[dstPlace + 1] = (byte)(distance & 0xFF);
                        dst[dstPlace + 2] = (byte)(result.Length - 0x12);
                        dstPlace += 3;
                    }

                    // Would like to call method WriteRLEIndicator instead of above code, but it causes a performance hit.
                    // dstPlace += WriteRLEIndicator(dst, dstPlace, distance, result.Length);

                    // Go through each matched byte and append to delta window.
                    // Todo: Maybe don't do this if already finished encoding?
                    for (int j = 1; j < result.Length; j++)
                    {
                        Remember(src, srcPlace + j, ref window, recents);
                    }

                    srcPlace += result.Length;
                }
                else
                {
                    // If no result from lookup, set bit for direct copy.
                    dst[codeBytePlace] |= (byte)(1 << (7 - commandBit));
                    dst[dstPlace++] = src[srcPlace++];
                }

                // Update result state.
                result = LookupResult.ClearIfNotSkipped(ref result);

                // Update command bit index.
                commandBit = (commandBit + 1) % 8;
            }

            return dstPlace;
        }

        /// <summary>
        /// Perform lookup for byte string at position.
        /// </summary>
        /// <param name="src">Source buffer.</param>
        /// <param name="position">Position in source buffer.</param>
        /// <param name="window">Deltas window.</param>
        /// <param name="recents">Recents buffer.</param>
        /// <returns></returns>
        static LookupResult Lookup(
            ReadOnlySpan<byte> src,
            int position,
            ref RevolvingBufferTracker<int> window,
            Span<int> recents)
        {
            // Get offset to previous index of current byte value.
            var c = src[position];
            var recent = recents[c] - 1;

            // Check if: recent is 0 (no previous byte with this value), or offset is out-of-range of window.
            if (recent < 0 || window.Length < (position - recent))
            {
                return LookupResult.NoneFound();
            }
            else
            {
                // Start searching at recent position.
                var searchPosition = recent;

                var currentSlice = src.Slice(position, Math.Min(MaxCompare, src.Length - position));
                var windowStartPosition = Math.Max(position - window.Length, 0);
                var result = LookupResult.NoneFound();

                // Follow chain backwards and look for matches.
                while (windowStartPosition <= searchPosition)
                {
                    // Get slice for bytes to compare against currentSlice
                    var searchSlice = src.Slice(searchPosition);

                    // Find number of potential matches in search slice.
                    var potentialMatches = Math.Min(currentSlice.Length, searchSlice.Length);

                    // Set matches count to maximum, will update if loop exits early.
                    var matches = potentialMatches;
                    for (int i = 0; i < potentialMatches; i++)
                    {
                        if (currentSlice[i] != searchSlice[i])
                        {
                            matches = i;
                            break;
                        }
                    }

                    // Update lookup result if found new longest match.
                    if (matches >= 3 && matches >= result.Length)
                    {
                        result = new LookupResult(searchPosition, matches);

                        // If we have found a matching string with the max compare size, return early.
                        if (result.Length == MaxCompare)
                        {
                            break;
                        }
                    }

                    // Get search position relative to window start, and update search position.
                    var windowIndex = searchPosition - windowStartPosition;
                    searchPosition = window.Get(windowIndex);

                    // If next index is less-than 0, no previous instance of this value.
                    if (searchPosition < 0)
                    {
                        break;
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Perform lookup for both current byte and next byte, in order to determine which should be used for RLE.
        /// </summary>
        /// <param name="src">Source buffer.</param>
        /// <param name="position">Position in source buffer.</param>
        /// <param name="window">Deltas window.</param>
        /// <param name="recents">Recents buffer.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static LookupResult LookupWithAhead(
            ReadOnlySpan<byte> src,
            int position,
            ref RevolvingBufferTracker<int> window,
            Span<int> recents)
        {
            var result1 = Lookup(src, position, ref window, recents);
            if (result1.Offset >= 0 && (position + 1) < src.Length)
            {
                // Store recent value for restoring afterwards.
                var prevRecent = recents[src[position]];
                Remember(src, position, ref window, recents);

                // Perform lookup for next byte.
                var result2 = Lookup(src, position + 1, ref window, recents);

                // Restore recent, pop previous delta value.
                recents[src[position]] = prevRecent;
                window.Pop();

                if (result2.Length >= result1.Length + 2)
                {
                    // If result2 is preferred, return with SkipByte set.
                    return result2.WithSkipByte(true);
                }
            }
            return result1;
        }

        /// <summary>
        /// Update Deltas window and Recents for value of current byte.
        /// </summary>
        /// <param name="src">Source buffer.</param>
        /// <param name="position">Position in source buffer.</param>
        /// <param name="window">Deltas window.</param>
        /// <param name="recents">Recents buffer.</param>
        static void Remember(
            ReadOnlySpan<byte> src,
            int position,
            ref RevolvingBufferTracker<int> window,
            Span<int> recents)
        {
            // Updates both Recents and Window.
            var value = src[position];
            var recent = recents[value] - 1;
            recents[value] = position + 1;
            window.Push(recent);
        }

        /// <summary>
        /// Write a Yaz0 header to a destination buffer.
        /// </summary>
        /// <param name="dst">Destination buffer.</param>
        /// <param name="decompressedLength">Length of decompressed data.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void WriteHeader(Span<byte> dst, uint decompressedLength)
        {
            BinaryPrimitives.WriteUInt32BigEndian(dst, Magic);
            BinaryPrimitives.WriteUInt32BigEndian(dst.Slice(4), decompressedLength);
        }

        /// <summary>
        /// Writes bytes for indicating RLE copy. Does not perform bounds checking.
        /// </summary>
        /// <param name="dst">Destination buffer.</param>
        /// <param name="dstPlace">Index into destination buffer.</param>
        /// <param name="distance">Distance value to write.</param>
        /// <param name="length">Length value to write.</param>
        /// <returns>Number of bytes written (either 2 or 3).</returns>
        /// <remarks>Separating this code into its own method causes minor performance hit, even when inlined.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static int WriteRLEIndicator(Span<byte> dst, int dstPlace, int distance, int length)
        {
            // Encodes differently depending on chunk length.
            if (length < 0x12)
            {
                // Encode for chunk size smaller than 0x12. Uses 2 bytes.
                dst[dstPlace] = (byte)(((length - 2) << 4) | (distance >> 8));
                dst[dstPlace + 1] = (byte)(distance & 0xFF);
                // dstPlace += 2;
                return 2;
            }
            else
            {
                // Encode for chunk size 0x12 or larger (up to 0x111). Uses 3 bytes.
                dst[dstPlace] = (byte)(distance >> 8);
                dst[dstPlace + 1] = (byte)(distance & 0xFF);
                dst[dstPlace + 2] = (byte)(length - 0x12);
                // dstPlace += 3;
                return 3;
            }
        }
    }
}
