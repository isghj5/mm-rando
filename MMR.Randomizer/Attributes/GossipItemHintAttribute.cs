using System;
using System.Linq;

namespace MMR.Randomizer.Attributes
{
    public class GossipItemHintAttribute : Attribute
    {
        public string[] Values { get; }

        public GossipItemHintAttribute(string value, params string[] values)
        {
            var list = values.ToList();
            list.Add(value);
            Values = list.ToArray();
        }
    }
}
