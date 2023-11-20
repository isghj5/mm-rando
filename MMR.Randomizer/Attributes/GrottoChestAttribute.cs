using System;

namespace MMR.Randomizer.Attributes
{
    public class GrottoChestAttribute : Attribute
    {
        public int[] Addresses { get; private set; }

        public GrottoChestAttribute(params int[] addresses)
        {
            Addresses = addresses;
        }
    }
}
