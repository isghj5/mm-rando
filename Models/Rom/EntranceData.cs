using System;
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
        public ushort SpawnAddress { get; set; }
        public string SpawnType { get; set; }
        public string SceneName { get; set; } // phase this out for region name
        public ushort SceneId { get; set; }
        public Exit ExitSpawn { get; set; }

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
        public Spawn Spawn { get; private set; }
        public Exit Exit { get; private set; }
        public string SpawnName { get; private set; }
        public string ExitName { get; private set; }
        public string Leaving { get; set; }
        public string Returning { get; set; }
        public string Type { get; private set; }
        public Entrance( string EntranceName, Spawn Spawn, Exit Exit, string Type)
        {
            this.EntranceName = EntranceName;
            this.Spawn = Spawn;
            this.Exit = Exit;
            if (Spawn != null) this.SpawnName = Spawn.SpawnName;
            if (Exit != null) this.ExitName = Exit.SpawnName;
            this.Type = Type;
        }
    }

    public class Region
    {
        public string RegionName { get; }
        public uint SceneId { get; }
        public uint ExternalSceneId { get; private set; }
        public List<string> Spawns { get; private set; }
        public List<string> Exits { get; private set; }
        public Region(string RegionName, uint SceneId)
        {
            this.RegionName = RegionName;
            this.SceneId = SceneId;
            ExternalSceneId = 0xFFFF;
            Spawns = new List<string>();
            Exits = new List<string>();
        }
        public List<Entrance> AddExits(List<Exit> oldExits)
        {
            Spawn spawn;
            List<Entrance> addedEntrances = new List<Entrance>();
            byte s;
            foreach (Exit x in oldExits)
            {
                if (x.RegionName.Equals(RegionName))
                {
                    if( x.ExitSpawn != null)
                    {
                        s = (byte)((x.ExitSpawn.SpawnAddress & 0x1F0) >> 4);
                        if (ExternalSceneId == 0xFFFF)
                        {
                            ExternalSceneId = (uint)((x.ExitSpawn.SpawnAddress & 0xFE00) >> 9);
                        }
                        spawn = new Spawn(x.ExitSpawn.SpawnName, this.RegionName, s);
                        addedEntrances.Add(new Entrance(x.SpawnName, spawn, x, x.SpawnType));
                        Spawns.Add($"{spawn.SpawnName} [{x.ExitSpawn.SpawnAddress.ToString("X4")}]");
                        Exits.Add(x.ExitName);
                    }
                }
            }
            return addedEntrances;
        }
    }

    public class EntranceData
    {
        public List<Region> regions;
        public List<Exit> exits;
        public List<Spawn> spawns;
        public List<Entrance> entrances;
        public Dictionary<string, string> internalSceneSwitches;
    }
}
