using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMRando.Models
{
    public class SpoilerEntrance
    {
        public ushort OriginalAddress { get; }
        public ushort ShuffledAddress { get; }
        public string OriginalEntrance { get; }
        public string ShuffledEntrance { get; }
    }
}
