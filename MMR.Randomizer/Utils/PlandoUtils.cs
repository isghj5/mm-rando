using MMR.Randomizer.GameObjects;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq;
using MMR.Randomizer.Models.Rom;
using MMR.Randomizer.Constants;

namespace MMR.Randomizer.Utils
{

    public abstract class PlandoCombo
    {
        public string Name;
        public string Notes;
        public int    ItemDrawCount = -1; // plural item list: how many items are randomly selected instead of all
    }

    public class PlandoItemCombo : PlandoCombo
    {
        public List<Item>  ItemList;
        public List<Item>  CheckList;

        /*PlandoItemCombo(string name, string notes, List<Item> itemList, List<Item> checkList, int count = -1 )
        {
            this.Name = name;
            this.Notes = notes;
            this.ItemList = itemList;
            this.CheckList = checkList;
            this.ItemDrawCount = count;
        }*/
    }

    public class PlandoMusicCombo : PlandoCombo
    {
        public List<string> SongsList;
        public List<string> SlotsList;
    }

    // will allow you to select which hints you want
    // public class PlandoHintCombo

    // will allow you to select which entrances you want
    // public class PlandoEntranceCombo

    // will allow you to select which enemies you want
    // public class PlandoEnemyCombo

    class PlandoUtils
    {
        // read plando list(s) from file
        public static List<PlandoItemCombo> ReadAllItemPlandoFiles(string directory = ".")
        {
            // any file with FILENAME_Plando.json in the base directory is a plando file
            // resource folders getting nuked, cannot use, just assume base directory is best places for now
            List<PlandoItemCombo> PlandoList = new List<PlandoItemCombo>();
            foreach (String filePath in Directory.GetFiles(directory, "*Plando.json"))
            {
                String filename = Path.GetFileName(filePath);
                try
                {
                    string filetext = File.ReadAllText(filename);
                    // the string enum converter reads the item enumerators as strings rather than their int values, so we can read item/checks by enum
                    // eg, the json can have ItemList: ["MaskBunnyHood"] instead of ItemList: [ 22 ]
                    List<PlandoItemCombo> tmp_list = JsonConvert.DeserializeObject<List<PlandoItemCombo>>(filetext, new Newtonsoft.Json.Converters.StringEnumConverter());
                    PlandoList = PlandoList.Concat(tmp_list).ToList();
                }
                catch (Exception e)
                {
                    Debug.Print("Error: exception occurred reading plando file: " + e.ToString());
                    #if DEBUG
                      throw new Exception("plando file read error: " + e.ToString() + " file: " + Path.GetFileName(filePath));
                    #endif
                }
            }
            return PlandoList;
        }

        public static List<PlandoMusicCombo> ReadAllMusicPlandoFiles(string directory = "Resources/music")
        {
            List<PlandoMusicCombo> PlandoList = new List<PlandoMusicCombo>();
            foreach (String filePath in Directory.GetFiles(directory, "*MusicPlando.json"))
            {
                try
                {
                    string filetext = File.ReadAllText(filePath);
                    List<PlandoMusicCombo> tmp_list = JsonConvert.DeserializeObject<List<PlandoMusicCombo>>(filetext);
                    PlandoList = PlandoList.Concat(tmp_list).ToList();
                }
                catch (Exception e)
                {
                    Debug.Print("Error: exception occurred reading plando file: " + e.ToString());
                    #if DEBUG
                      throw new Exception("plando file read error: " + e.ToString() + " file: " + Path.GetFileName(filePath));
                    #endif
                }
            }
            return PlandoList;
        }

        public static List<(SequenceInfo, SequenceInfo)> GetRandomizedSongPlacements(Random random, System.Text.StringBuilder song_log)
        {
            void DebugOut(String s)
            {
                Debug.WriteLine(s);
                song_log.AppendLine(s);
            }

            List<(SequenceInfo, SequenceInfo)> ReturnSongTupleList = new List<(SequenceInfo, SequenceInfo)>();
            List<PlandoMusicCombo> AllPlandoMusicCombos = ReadAllMusicPlandoFiles(Values.MusicDirectory);

            foreach(PlandoMusicCombo music_combo in AllPlandoMusicCombos)
            {
                // shuffle songs and slots based on our random seed
                music_combo.SongsList = music_combo.SongsList.OrderBy(x => random.Next()).ToList();
                music_combo.SlotsList = music_combo.SlotsList.OrderBy(x => random.Next()).ToList();

                // clean combo of already placed items and checks
                List<string> PreviouslyUsedSongs = ReturnSongTupleList.Select(u => u.Item1.Name).ToList();
                foreach (string i in music_combo.SongsList.ToList())
                {
                    if (PreviouslyUsedSongs.Contains(i))
                    {
                        DebugOut("Song already placed, removed from combo: " + i);
                        music_combo.SongsList.Remove(i);
                    }
                    else if (! RomData.SequenceList.Any(u => u.Name == i))
                    {
                        DebugOut("Song does not exist in sequence pool, did you misspell the song name? " + i);
                        music_combo.SongsList.Remove(i);
                    }
                }

                List<string> PreviouslyUsedSlots = ReturnSongTupleList.Select(u => u.Item2.Name).ToList();
                foreach (string i in music_combo.SlotsList.ToList())
                {
                    if (PreviouslyUsedSlots.Contains(i))
                    {
                        DebugOut("Slot already used, removed from combo: " + i);
                        music_combo.SlotsList.Remove(i);
                    }
                    else if (!RomData.TargetSequences.Any(u => u.Name == i))
                    {
                        DebugOut("Slot does not exist in slot pool, did you misspell the slot name or forget to add it to seqs.txt? " + i);
                        music_combo.SlotsList.Remove(i);
                    }
                }

                if (music_combo.SongsList.Count == 0)
                {
                    DebugOut("Plando Music Combo is starved, all songs have already been placed: " + music_combo.Name);
                    continue;
                }
                if (music_combo.SlotsList.Count == 0)
                {
                    DebugOut("Plando Music Combo is starved, all slots are already filled: " + music_combo.Name);
                    continue;
                }

                if (music_combo.ItemDrawCount <= -1 || music_combo.ItemDrawCount > music_combo.SongsList.Count)
                    music_combo.ItemDrawCount = music_combo.SongsList.Count;

                for (int i = 0; i < music_combo.ItemDrawCount && i < music_combo.SlotsList.Count; i++)
                {
                    SequenceInfo song = RomData.SequenceList.Find(u => u.Name == music_combo.SongsList[i]);
                    SequenceInfo slot = RomData.TargetSequences.Find(u => u.Name == music_combo.SlotsList[i]);

                    ReturnSongTupleList.Add((song, slot));
                    DebugOut("* Song placed: " + song.Name + " placed in slot " + slot.Name);
                }
            }

            return ReturnSongTupleList;
        }

        /// with seed, get list of item->check tuples for randomizer.cs::RandomizeItems()
        public static List<(Item, Item)> GetRandomizedItemPlacements(Random random, List<Item> itemPool)
        {

            List<(Item, Item)> ReturnItemTupleList = new List<(Item item, Item check)>();
            List<PlandoItemCombo> AllPlandoItemCombos = ReadAllItemPlandoFiles();

            foreach(PlandoItemCombo item_combo in AllPlandoItemCombos)
            {
                // shuffle item and check order
                item_combo.ItemList  = item_combo.ItemList.OrderBy(x => random.Next()).ToList();
                item_combo.CheckList = item_combo.CheckList.OrderBy(x => random.Next()).ToList();

                // clean combo of already placed items and checks
                List<Item> PreviouslyUsedItems = ReturnItemTupleList.Select(u => u.Item1).ToList();
                foreach (Item i in item_combo.ItemList.ToList()) 
                {
                    if (PreviouslyUsedItems.Contains(i))
                    {
                        Debug.WriteLine("Item already placed, removed from combo: " + i);
                        item_combo.ItemList.Remove(i);
                    }
                    else if ( !itemPool.Contains(i))
                    {
                        Debug.WriteLine("Item does not exist in randomized item pool, did you forget to randomize it? " + i);
                        item_combo.ItemList.Remove(i);
                    }
                }

                List<Item> PreviouslyUsedChecks = ReturnItemTupleList.Select(u => u.Item2).ToList();
                foreach (Item i in item_combo.CheckList.ToList())
                {
                    if (PreviouslyUsedChecks.Contains(i))
                    {
                        Debug.WriteLine("Check already used, removed from combo: " + i);
                        item_combo.CheckList.Remove(i);
                    }
                    else if ( !itemPool.Contains(i))
                    {
                        Debug.WriteLine("Check does not exist in randomized item pool, did you forget to randomize it? " + i);
                        item_combo.CheckList.Remove(i);
                    }
                }

                if (item_combo.ItemList.Count == 0)
                {
                    Debug.WriteLine("Plando Item Combo is starved, all items have already been placed: " + item_combo.Name);
                    continue;
                }
                if (item_combo.CheckList.Count == 0)
                {
                    Debug.WriteLine("Plando Item Combo is starved, all checks are already filled: " + item_combo.Name);
                    continue;
                }

                if (item_combo.ItemDrawCount <= -1 || item_combo.ItemDrawCount > item_combo.ItemList.Count)
                    item_combo.ItemDrawCount = item_combo.ItemList.Count;

                for (int i = 0; i < item_combo.ItemDrawCount && i < item_combo.CheckList.Count; i++)
                {
                    ReturnItemTupleList.Add((item_combo.ItemList[i], item_combo.CheckList[i]));
                    Debug.WriteLine("* Item placed: " + item_combo.ItemList[i] + " placed in check " + item_combo.CheckList[i]);
                }
            }

            return ReturnItemTupleList;
        }

    }
}
