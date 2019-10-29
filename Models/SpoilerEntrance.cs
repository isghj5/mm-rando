using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMRando.Models
{
    public class SpoilerEntrance
    {
        public SpoilerEntrance(string OriginalEntrance, string ShuffledEntrance)
        {
            this.OriginalEntrance = OriginalEntrance;
            this.ShuffledEntrance = ShuffledEntrance;
        }
        public string OriginalEntrance { get; }
        public string ShuffledEntrance { get; }
    }
}
