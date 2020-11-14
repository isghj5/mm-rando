using MMR.Randomizer.Utils.Mzxrules;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

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

        /// <summary>
        /// Get image data for the 4 stray fairy icons.
        /// </summary>
        /// <returns>Array of image bytes.</returns>
        public static byte[][] GetStrayFairyIcons()
        {
            // Stray Fairy icons start at: 0xA0A000 + 0x1B80
            RomUtils.CheckCompressed(11);
            var fileData = RomData.MMFileList[11].Data;

            // Extract HUD icon for Stray Fairy icons.
            var fairies = new List<byte[]>(4);
            for (int i = 0; i < 4; i++)
            {
                var imageData = new byte[0xC00];
                var offset = 0x1B80 + (imageData.Length * i);
                Buffer.BlockCopy(fileData, offset, imageData, 0, imageData.Length);
                fairies.Add(imageData);
            }

            return fairies.ToArray();
        }

        /// <summary>
        /// Get image data for the Clock Town stray fairy icon by modifying an existing icon.
        /// </summary>
        /// <returns>Array of image bytes.</returns>
        public static byte[] GetClockTownStrayFairyIcon()
        {
            var fairies = GetStrayFairyIcons();
            // Use Snowhead Stray Fairy icon as base icon.
            var source = fairies[1];
            using (var image = Image.LoadPixelData<Rgba32>(source, 32, 24))
            {
                // Shift hue to affect color.
                image.Mutate(o => o.Hue(-80f));
                var result = MemoryMarshal.AsBytes(image.GetPixelSpan()).ToArray();
                if (source.Length != result.Length)
                {
                    throw new Exception($"Invalid image data size for Stray Fairy icon: {result.Length} != {source.Length}");
                }
                return result;
            }
        }
    }
}
