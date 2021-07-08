namespace MMR.Yaz
{
    /// <summary>
    /// Scheme describing how string lookups for RLE will be performed.
    /// </summary>
    public enum LookupScheme
    {
        /// <summary>
        /// Simple scheme which only performs lookup for current byte.
        /// </summary>
        Simple,

        /// <summary>
        /// Performs lookup for current byte and next byte, and chooses which would be preferable.
        /// </summary>
        LookAhead,
    }
}
