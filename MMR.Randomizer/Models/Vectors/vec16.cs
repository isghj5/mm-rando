using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MMR.Randomizer.Models.Vectors
{
    public class vec16
    {
        public short x = new short();
        public short y = new short();
        public short z = new short();

        public vec16() {}
    
        public vec16(int newX, int newY, int newZ)
        {
            x = (short)newX;
            y = (short)newY;
            z = (short)newZ;
        }

    }
}
