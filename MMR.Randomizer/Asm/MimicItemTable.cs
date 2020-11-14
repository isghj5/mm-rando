using MMR.Randomizer.Models;
using MMR.Randomizer.Utils;
using System.Collections.Generic;

namespace MMR.Randomizer.Asm
{
    public class MimicItemTable
    {
        public ItemGraphic[] Overrides;

        public int Length => Overrides.Length;

        public MimicItemTable(int length)
        {
            this.Overrides = new ItemGraphic[length];
        }

        public ItemGraphic this[int key] {
            get {
                return this.Overrides[key];
            }
            set {
                this.Overrides[key] = value;
            }
        }

        /// <summary>
        /// Build table <see cref="byte"/> array.
        /// </summary>
        /// <returns>Table <see cref="byte"/> array.</returns>
        public byte[] Build()
        {
            var bytes = new byte[this.Overrides.Length * 4];
            for (int i = 0; i < this.Overrides.Length; i++)
            {
                var entry = this.Overrides[i];
                var offset = i * 4;
                ReadWriteUtils.Arr_WriteU16(bytes, offset, entry.GraphicId);
                ReadWriteUtils.Arr_WriteU16(bytes, offset + 2, entry.ObjectId);
            }
            return bytes;
        }

        /// <summary>
        /// Update table with entries.
        /// </summary>
        /// <param name="graphics">Mapping of index to graphics</param>
        public void Update(Dictionary<int, ItemGraphic> graphics)
        {
            foreach (var kvp in graphics)
            {
                this[kvp.Key] = kvp.Value;
            }
        }
    }
}
