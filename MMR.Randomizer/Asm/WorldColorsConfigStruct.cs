using Be.IO;
using MMR.Common.Extensions;
using MMR.Randomizer.Extensions;
using System.Drawing;
using System.IO;

namespace MMR.Randomizer.Asm
{
    /// <summary>
    /// World Colors configuration structure.
    /// </summary>
    public struct WorldColorsConfigStruct : IAsmConfigStruct
    {
        public uint Version;
        public Color[] Colors;

        /// <summary>
        /// Convert to bytes.
        /// </summary>
        /// <returns>Bytes</returns>
        public byte[] ToBytes()
        {
            using (var memoryStream = new MemoryStream())
            using (var writer = new BeBinaryWriter(memoryStream))
            {
                writer.WriteUInt32(this.Version);

                foreach (var color in this.Colors)
                {
                    writer.WriteBytes(color.ToBytesRGB(0));
                }
                return memoryStream.ToArray();
            }
        }
    }
}
