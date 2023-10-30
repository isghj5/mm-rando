using System;

namespace MMR.Randomizer.Attributes
{
    public class RegionNameAttribute : Attribute
    {
        public string Name { get; }
        public string Prepositsion { get; }

        public RegionNameAttribute(string name, string preposition = null)
        {
            Name = name;
            Prepositsion = preposition;
        }
    }
}
