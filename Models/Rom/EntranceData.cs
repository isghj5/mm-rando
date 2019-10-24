using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MMRando.Models
{
    public class Exit
    {
        public Exit() { }
        public Exit(string ExitName, string RegionName, byte ExitIndex, string SpawnName)
        {
            this.RegionName = RegionName;
            this.ExitName = ExitName;
            this.ExitIndex = ExitIndex;
            this.SpawnName = SpawnName;
        }
        public Exit(Exit x) {
            if( x != null)
            {
                this.RegionName = x.RegionName;
                this.ExitName = x.ExitName;
                this.ExitIndex = x.ExitIndex;
                this.SpawnName = x.SpawnName;
            }
        }
        public string RegionName { get; set; }
        public string ExitName { get; set; }
        public byte ExitIndex { get; set; }
        public string SpawnName { get; set; }
    }

    public class Spawn
    {
        public string SpawnName { get; set; }
        public string RegionName { get; set; }
        public byte SpawnIndex { get; set; }
        public Spawn(string SpawnName, string RegionName, byte SpawnIndex)
        {
            this.SpawnName = SpawnName;
            this.RegionName = RegionName;
            this.SpawnIndex = SpawnIndex;
        }
    }

    public class Entrance
    {
        public string EntranceName { get; set; }
        public string SpawnName { get; set; }
        public string ExitName { get; set; }
        public string ReturnSpawnName { get; set; }
        public string ReturnExitName { get; set; }
        public string Type { get; set; }
        public List<string> Properties { get; set; }
        public Entrance( string EntranceName, string SpawnName, string ReturnSpawnName, string Type)
        {
            this.EntranceName = EntranceName;
            this.SpawnName = SpawnName;
            this.ExitName = SpawnName;
            this.ReturnSpawnName = ReturnSpawnName;
            this.ReturnExitName = ReturnSpawnName;
            this.Type = Type;
            this.Properties = new List<string>();
        }
    }

    public class Region
    {
        public string RegionName { get; set; }
        public ushort SceneId { get; set; }
        public ushort ExternalSceneId { get; set; }
        public List<string> Spawns { get; set; }
        public List<string> Exits { get; set; }
        public Region(string RegionName, ushort SceneId, ushort ExternalSceneId)
        {
            this.RegionName = RegionName;
            this.SceneId = SceneId;
            this.ExternalSceneId = ExternalSceneId;
            Spawns = new List<string>();
            Exits = new List<string>();
        }
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

        public void RenameSpawn(string oldSpawnName, string newSpawnName)
        {
            if(spawns.Find(s => oldSpawnName.Equals(s.SpawnName)) != null)
            {
                foreach (Spawn spawn in spawns.FindAll(s => oldSpawnName.Equals(s.SpawnName)))
                {
                    spawn.SpawnName = newSpawnName;
                }
                foreach (Exit exit in exits.FindAll(x => oldSpawnName.Equals(x.SpawnName)))
                {
                    exit.SpawnName = newSpawnName;
                }
                foreach (Entrance ent in entrances.FindAll(e => oldSpawnName.Equals(e.SpawnName)))
                {
                    ent.SpawnName = newSpawnName;
                }
                foreach (Entrance ent in entrances.FindAll(e => oldSpawnName.Equals(e.ReturnSpawnName)))
                {
                    ent.ReturnSpawnName = newSpawnName;
                }
                foreach (Region region in regions)
                {
                    int i = region.Spawns.FindIndex(s => oldSpawnName.Equals(s));
                    if( i != -1)
                    {
                        region.Spawns[i] = newSpawnName;
                    }
                }
            }
        }

        public void RenameExit(string oldExitName, string newExitName)
        {
            if (exits.Find(s => oldExitName.Equals(s.ExitName)) != null)
            {
                foreach (Exit exit in exits.FindAll(x => oldExitName.Equals(x.ExitName)))
                {
                    exit.ExitName = newExitName;
                }
                foreach (Entrance ent in entrances.FindAll(e => oldExitName.Equals(e.ExitName)))
                {
                    ent.ExitName = newExitName;
                }
                foreach (Entrance ent in entrances.FindAll(e => oldExitName.Equals(e.ReturnExitName)))
                {
                    ent.ReturnExitName = newExitName;
                }
                foreach (Region region in regions)
                {
                    int i = region.Exits.FindIndex(x => oldExitName.Equals(x));
                    if (i != -1)
                    {
                        region.Exits[i] = newExitName;
                    }
                }
            }
        }

        internal bool ConnectEntrance(string from, string to)
        {
            Entrance source = entrances.Find(e => from.Equals(e.EntranceName));
            Entrance dest = entrances.Find(e => to.Equals(e.EntranceName));
            if (source == null || dest == null)
            {
                return false;
            }
            Exit sourceExit = exits.Find(x => source.ExitName.Equals(x.ExitName));
            Exit destReturnExit = exits.Find(x => dest.ReturnExitName.Equals(x.ExitName));
            if ( sourceExit == null || destReturnExit == null )
            {
                return false;
            }

            sourceExit.SpawnName = dest.SpawnName;
            destReturnExit.SpawnName = source.ReturnSpawnName;
            return true;
        }

        internal ushort SpawnAddress( string SpawnName )
        {
            Spawn spawn = spawns.Find(s => SpawnName.Equals(s.SpawnName));
            if (spawn != null)
            {
                Region region = regions.Find(r => spawn.RegionName.Equals(r.RegionName));
                if (region != null)
                {
                    return (ushort)((region.ExternalSceneId << 9) + (spawn.SpawnIndex << 4));
                }
                else
                {
                    Debug.WriteLine($"{spawn.RegionName} not found");
                }
            }
            return 0xFFFF;
        }

        internal int SceneIndex( string RegionName)
        {
            Region region = regions.Find(r => RegionName.Equals(r.RegionName));
            return (region == null) ? -1 : region.SceneId;
        }

        internal void UpdateEntrances()
        {
            Exit t;
            foreach (Entrance e in entrances)
            {
                t = exits.Find(x => e.ExitName.Equals(x.ExitName));
                e.SpawnName = t.SpawnName;
                t = exits.Find(x => e.ReturnExitName.Equals(x.ExitName));
                e.ReturnSpawnName = t.SpawnName;
            }
        }

        internal void AddEntrance(string EntranceName, string SpawnRegion, string SpawnName, byte SpawnIndex, byte ExitIndex, string ReturnRegion, string ReturnSpawnName, byte ReturnSpawnIndex, byte ReturnExitIndex, string Type)
        {
            Region spawnRegion = regions.Find(r => SpawnRegion.Equals(r.RegionName));
            if (spawnRegion != null)
            {
                spawnRegion.Spawns.Add(SpawnName);
                spawnRegion.Exits.Add(ReturnSpawnName);
            }
            Region returnRegion = regions.Find(r => ReturnRegion.Equals(r.RegionName));
            if (returnRegion != null)
            {
                returnRegion.Spawns.Add(ReturnSpawnName);
                returnRegion.Exits.Add(SpawnName);
            }
            Exit x = new Exit(SpawnName, ReturnRegion, ReturnExitIndex, SpawnName);
            Spawn s = new Spawn(SpawnName, SpawnRegion, SpawnIndex);
            Entrance e = new Entrance(EntranceName, SpawnName, ReturnSpawnName, Type);
            Exit rx = new Exit(ReturnSpawnName, SpawnRegion, ExitIndex, ReturnSpawnName);
            Spawn rs = new Spawn(ReturnSpawnName, ReturnRegion, ReturnSpawnIndex);
            if (!"Overworld".Equals(Type)) { Type += " Exit"; }
            Entrance re = new Entrance(ReturnSpawnName, ReturnSpawnName, SpawnName, Type);
            spawns.Add(s);
            spawns.Add(rs);
            exits.Add(x);
            exits.Add(rx);
            entrances.Add(e);
            entrances.Add(re);
        }

        internal void AddRegion(string RegionName, ushort SceneId, ushort ExternalSceneId)
        {
            if (regions.Find(r => RegionName.Equals(r.RegionName)) == null)
            {
                Region r = new Region(RegionName, SceneId, ExternalSceneId);
                regions.Add(r);
            }
        }

        internal string ReverseEntrance(string EntranceName)
        {
            Entrance ent = entrances.Find(e => EntranceName.Equals(e.EntranceName));
            if( ent != null)
            {
                Entrance rev = entrances.Find(e => ent.ReturnExitName.Equals(e.ExitName));
                if( rev != null)
                {
                    return rev.EntranceName;
                }
            }
            return EntranceName;
        }

        internal bool EntranceRegionTypeCount(string entranceName, List<string> entranceTypes, int entCount)
        {
            string ent = entrances.Find(e => entranceName.Equals(e.EntranceName)).SpawnName;
            string reg = spawns.Find(s => ent.Equals(s.SpawnName)).RegionName;
            List<string> exitList = exits.FindAll(x => reg.Equals(x.RegionName)).Select(x => x.ExitName).ToList();
            int c = 0;
            exitList.ForEach(x => {
                Entrance en = entrances.Find(e => x.Equals(e.ExitName));
                if( en != null )
                {
                    foreach(string entranceType in entranceTypes)
                    {
                        if (entranceType.Equals(en.Type))
                        {
                            c++;
                            break;
                        }                        
                    }
                }
            });
            return c == entCount;
        }

        internal string EntranceType(string entranceName)
        {
            Entrance ent = entrances.Find(e=>entranceName.Equals(e.EntranceName));
            return (ent != null) ? ent.Type : "None";
        }

        internal bool EntranceHasProperty(string entranceName, string property)
        {
            Entrance ent = entrances.Find(e => entranceName.Equals(e.EntranceName));
            return (ent == null) ? false : ent.Properties.Contains(property);
        }
    }
}
