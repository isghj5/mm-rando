using System;
using System.Linq;

namespace MMR.Randomizer.Attributes
{
    public class GossipLocationHintAttribute : Attribute
    {
        public string[] Values { get; }

        public GossipLocationHintAttribute(string value, params string[] values)
        {
            var list = values.ToList();
            list.Add(value);
            Values = list.ToArray();
        }
    }
}
