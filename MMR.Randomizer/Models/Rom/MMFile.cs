using System.Linq;

namespace MMR.Randomizer.Models.Rom
{
    [System.Diagnostics.DebuggerDisplay("Addr{Addr.ToString(\"X\")}Len{Data.Length}")]
    public class MMFile
    {
        public int Addr;
        public int End;
        public int Cmp_Addr;
        public int Cmp_End;
        public byte[] Data;
        public bool IsCompressed;
        public bool WasEdited;
        public bool IsStatic;

        public MMFile Clone()
        {
            return new MMFile
            {
                Addr = Addr, // decompressed VROM start
                End = End, // decompressed VROM end
                Cmp_Addr = Cmp_Addr,
                Cmp_End = Cmp_End,
                IsCompressed = IsCompressed, // is normally compressed
                WasEdited = WasEdited,
                IsStatic = IsStatic,
                Data = Data?.ToArray()
            };
        }
    }
}
