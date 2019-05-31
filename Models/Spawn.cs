using System.Collections.Generic;

namespace MMRando.Models
{
    public class Spawn
    {
        public Spawn( string Name, ushort Address, string Scene)
        {
            this.Name = Name;
            this.Address = Address;
            this.Scene = Scene;
        }
        public string Name { get; set; }
        public string Scene { get; set; }
        public ushort Address { get; set; }
        public List<Spawn> Exit { get; set; } = new List<Spawn>();
        public string Type { get; set; }
    }
}
