using System.Collections.Generic;
using System.Diagnostics;

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
        public string SpawnName { get; set; }
        public ushort SpawnAddress { get; set; }
        public string SpawnType { get; set; }
        public string RegionName { get; set; }
        public string SceneName { get; set; } // phase this out for region name
        public ushort SceneId { get; set; }
        public int ExitIndex { get; set; }
        public Exit ExitSpawn { get; set; }
    }
    public class Entrance
    {
        public string Leaving { get; private set; }
        public string Returning { get; private set; }
        public string Type { get; private set; }
        public Entrance( string Leaving, string Returning, string Type)
        {
            this.Leaving = Leaving;
            this.Returning = Returning;
            this.Type = Type;
        }
        public void Print()
        {
            Debug.WriteLine($"{Leaving} -> {Returning} [{Type}]");
        }
    }

    public class Region
    {
        public string RegionName { get; }
        public uint SceneId { get; }
        private List<Exit> Exits { get; }
        public Region(string RegionName, uint SceneId)
        {
            this.RegionName = RegionName;
            this.SceneId = SceneId;
        }
    }

    public class EntranceData
    {
        public List<Region> regions;
        public List<Exit> exits;
        public List<Entrance> entrances;
    }
}
