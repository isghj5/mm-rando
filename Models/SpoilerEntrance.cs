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
        public SpoilerEntrance(Exit Original, Exit Shuffled, bool WasPlaced)
        {
            OriginalAddress = Original.SpawnAddress;
            OriginalEntrance = Original.SpawnName;
            if(Shuffled == null)
            {
                ShuffledAddress = 0xFFFF;
                ShuffledEntrance = "NULL";
            }
            else
            {
                ShuffledAddress = Shuffled.SpawnAddress;
                ShuffledEntrance = Shuffled.SpawnName + (WasPlaced ? "" : " (Not Placed)");
            }
        }
    }
}
