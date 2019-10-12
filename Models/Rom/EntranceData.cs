using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MMRando.Models
{
    public class Exit
    {
        public Exit(Exit x) {
            if( x!= null)
            {
                this.RegionName = x.RegionName;
                this.ExitName = x.ExitName;
                this.ExitIndex = x.ExitIndex;
                this.SpawnName = x.SpawnName;
            }
        }
        public string RegionName { get; set; }
        public string ExitName { get; set; }
        public int ExitIndex { get; set; }
        public string SpawnName { get; set; }
    }

    public class Spawn
    {
        public string SpawnName { get; private set; }
        public string RegionName { get; private set; }
        public byte SpawnIndex { get; private set; }
        public Spawn(string SpawnName, string RegionName, byte SpawnIndex)
        {
            this.SpawnName = SpawnName;
            this.RegionName = RegionName;
            this.SpawnIndex = SpawnIndex;
        }
    }

    public class Entrance
    {
        public string EntranceName { get; private set; }
        public string SpawnName { get; private set; }
        public string ExitName { get; private set; }
        public string ReturnSpawnName { get; set; }
        public string ReturnExitName { get; set; }
        //public string Leaving { get; set; }
        //public string Returning { get; set; }
        public string Type { get; private set; }
        public Entrance( string EntranceName, string SpawnName, string ExitName, string Type)
        {
            this.EntranceName = EntranceName;
            this.SpawnName = SpawnName;
            this.ExitName = ExitName;
            this.Type = Type;
        }
    }

    public class Region
    {
        public string RegionName { get; }
        public ushort SceneId { get; }
        public ushort ExternalSceneId { get; private set; }
        public List<string> Spawns { get; private set; }
        public List<string> Exits { get; private set; }
    }

    public class EntranceData
    {
        public List<Region> regions;
        public List<Exit> exits;
        public List<Spawn> spawns;
        public List<Entrance> entrances;
        public Dictionary<string, string> internalSceneSwitches;
        public EntranceData(EntranceData Copy)
        {
            if(Copy != null)
            {
                this.regions = new List<Region>();
                foreach (Region r in Copy.regions)
                {
                    this.regions.Add(r);
                }
                this.exits = new List<Exit>();
                foreach (Exit x in Copy.exits)
                {
                    this.exits.Add(new Exit(x));
                }
                this.spawns = new List<Spawn>();
                foreach (Spawn s in Copy.spawns)
                {
                    this.spawns.Add(s);
                }
                this.entrances = new List<Entrance>();
                foreach (Entrance e in Copy.entrances)
                {
                    this.entrances.Add(e);
                }
            }
        }
    }
}
