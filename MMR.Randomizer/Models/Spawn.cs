namespace MMR.Randomizer.Models
{

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
}
