using System.Collections.Generic;

namespace MMRando.Models
{
    public class Spawn
    {
        public Spawn( string Name, ushort Address, string Scene)
        {
            this.Name = Name;
            this.Address = Address;
            this.SceneName = Scene;
        }
        public string Name { get; set; }
        public string SceneName { get; set; }
        public ushort SceneNumber { get; set; }
        public ushort Address { get; set; }
        public List<Spawn> ExitSpawn { get; set; } = new List<Spawn>();
        public string Type { get; set; }
    }
}
