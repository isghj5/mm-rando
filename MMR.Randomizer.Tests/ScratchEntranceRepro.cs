using System;
using System.Collections.Generic;
using System.Linq;
using MMR.Randomizer.Extensions;
using MMR.Randomizer.Models;
using MMR.Randomizer.Models.Settings;
using NUnit.Framework;

namespace MMR.Randomizer.Tests
{
    public class ScratchEntranceRepro
    {
        private const int seed = 0;

        private void Run(LogicMode mode, EntranceMode em)
        {
            var settings = new GameplaySettings
            {
                LogicMode = mode,
                EntranceMode = em,
            };

            var randomizer = new Randomizer(settings, seed);
            var randomized = randomizer.Randomize(new NoProgressReporter());

            // --- replicate SpoilerUtils.CreateSpoilerLog entrance dict (lines 49-70) ---
            var entrances = new List<Item>();
            if (settings.EntranceMode.HasFlag(EntranceMode.DungeonEntrances))
            {
                entrances.AddRange(Enum.GetValues<Item>().Where(item => item.EntranceType() == EntranceType.Dungeon));
            }
            if (settings.EntranceMode.HasFlag(EntranceMode.BossRooms))
            {
                entrances.AddRange(Enum.GetValues<Item>().Where(item => item.EntranceType() == EntranceType.Boss));
            }
            if (settings.EntranceMode.HasFlag(EntranceMode.Grottos))
            {
                entrances.AddRange(Enum.GetValues<Item>().Where(item => item.EntranceType() == EntranceType.Grotto));
            }
            if (settings.EntranceMode.HasFlag(EntranceMode.SimpleInteriors))
            {
                entrances.AddRange(Enum.GetValues<Item>().Where(item => item.EntranceType() == EntranceType.Interior));
            }

            Console.WriteLine($"=== {mode} / {settings.EntranceMode} ===");
            Console.WriteLine($"entrances.Count = {entrances.Count}");
            var nullCount = entrances.Count(e => randomized.ItemList[e].NewLocation == null);
            Console.WriteLine($"null NewLocation count = {nullCount}");
            var distinct = entrances.Where(e => randomized.ItemList[e].NewLocation != null)
                                    .Select(e => randomized.ItemList[e].NewLocation.Value)
                                    .Distinct().Count();
            Console.WriteLine($"distinct NewLocation.Value = {distinct}");

            var dict = new Dictionary<Item, Item>();
            int added = 0, skippedDup = 0;
            foreach (var entrance in entrances.Where(e => randomized.ItemList[e].NewLocation != null))
            {
                var key = randomized.ItemList[entrance].NewLocation.Value;
                if (dict.ContainsKey(key))
                {
                    skippedDup++;
                }
                else
                {
                    dict.Add(key, entrance);
                    added++;
                }
            }
            Console.WriteLine($"dict added = {added}, dup keys = {skippedDup}, dict.Count = {dict.Count}");

            Console.WriteLine("---- per-entrance detail ----");
            foreach (var e in entrances)
            {
                var nl = randomized.ItemList[e].NewLocation;
                Console.WriteLine($"  {e,-40} NewLocation={(nl == null ? "<null>" : nl.Value.ToString())}");
            }
            Console.WriteLine();
        }

        [Test]
        public void Repro()
        {
            var all = EntranceMode.DungeonEntrances | EntranceMode.BossRooms | EntranceMode.Grottos | EntranceMode.SimpleInteriors;
            Run(LogicMode.NoLogic, all);
            Run(LogicMode.Casual, all);
        }
    }
}
