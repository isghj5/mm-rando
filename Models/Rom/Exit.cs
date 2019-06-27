using System.Collections.Generic;

namespace MMRando.Models
{
    public class Exit
    {
        public Exit() { }
        public Exit( string Name, ushort Address, string Scene)
        {
            this.SpawnName = Name;
            this.SpawnAddress = Address;
            this.SceneName = Scene;
        }
        public int ID { get; set; }
        public string SpawnName { get; set; }
        public ushort SpawnAddress { get; set; }
        public string SpawnAddressString { get; set; }
        public string SpawnType { get; set; }
        public string SceneName { get; set; }
        public ushort SceneId { get; set; }
        public int ExitIndex { get; set; }
        public Exit ExitSpawn { get; set; }
        public int ExitId { get; set; }
    }
}
