using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMR.Randomizer.Models.Rom
{
    public class MMSequence
    {
        public int Addr;
        public int Size => Data?.Length ?? 0;
        public byte[] Data;
    }
}
