using System;

namespace MMR.Randomizer.Attributes
{
    public class FileIDAttribute : Attribute
    {
        public int ID { get; set; }
        public FileIDAttribute(int id)
        {
            ID = id;
        }
    }
}
