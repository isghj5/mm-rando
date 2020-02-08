using System;

namespace MMR.Randomizer.Attributes.Entrance
{
    public class ExitAddressAttribute : Attribute
    {
        public int Address { get; private set; }
        public ExitAddressAttribute(int address)
        {
            Address = address;
        }

        public enum BaseAddress
        {
            SongOfSoaring = 0xF587AC,
        }
    }
}
