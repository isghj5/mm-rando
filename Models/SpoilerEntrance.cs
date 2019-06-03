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
        public SpoilerEntrance(Spawn Original, Spawn Shuffled, bool WasPlaced)
        {
            OriginalAddress = Original.Address;
            OriginalEntrance = Original.Name;
            if(Shuffled == null)
            {
                ShuffledAddress = 0xFFFF;
                ShuffledEntrance = "NULL";
            }
            else
            {
                ShuffledAddress = Shuffled.Address;
                ShuffledEntrance = Shuffled.Name + (WasPlaced ? "" : " (Not Placed)");
            }
        }
    }
}
