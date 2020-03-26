using MMR.Randomizer.Utils.Mzxrules;
using System;
using System.Collections.Generic;

namespace MMR.Randomizer.Utils
{
    public static class ImageUtils
    {
        public static List<int> GetIconIndices(byte[] hash)
        {
            var numberOfIcons = 5;
            var value = Endian.ConvertInt32(BitConverter.ToInt32(hash, 0));
            var indices = new List<int>();
            for (var i = 0; i < numberOfIcons; i++)
            {
                indices.Add((value >> 26) & 0x3F);
                value <<= 6;
            }
            return indices;
        }
    }
}
