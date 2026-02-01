using System;

namespace MMR.Randomizer.Attributes
{
    public class ExitNameAttribute : Attribute
    {
        public string Name { get; private set; }

        public ExitNameAttribute(string name)
        {
            Name = name;
        }
    }
}
