using System;
using System.Collections.Generic;
using System.Text;

namespace MMR.Randomizer.Attributes
{
    class FileIDAttribute : Attribute
    {
        public int ID { get; set; }
        public FileIDAttribute(int id)
        {
            ID = id;
        }
    }
}
