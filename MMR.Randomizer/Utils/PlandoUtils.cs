using MMR.Randomizer.GameObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;
using MMR.Randomizer.Models.Rom;
using MMR.Randomizer.Constants;
using MMR.Common.Utils;
using MMR.Randomizer.Extensions;

namespace MMR.Randomizer.Utils
{

    public abstract class PlandoCombo
    {
        public string Name { get; set; }
        public string Notes { get; set; }
        public int ItemDrawCount { get; set; } = -1; // plural item list: how many items are randomly selected instead of all
        public bool SkipIfError { get; set; } = false;
    }

    [System.Diagnostics.DebuggerDisplay("[{Name}]")]
    public class PlandoItemCombo : PlandoCombo
    {
        public List<String> ItemList { get; set; } // must remain "ItemList" for backwards compatibility, even though this is now Item/region mix
        public List<Item> ItemListConverted { get; set; }
        public List<String> CheckList { get; set; } // must remain "ItemList" for backwards compatibility, even though this is now Item/region mix
        public List<Item> CheckListConverted { get; set; }
        public List<String> CheckListInverted { get; set; }
        public List<Item> CheckListInvertedConverted { get; set; }

        public bool SkipLogic { get; set; } = false;


        public static PlandoItemCombo Copy(PlandoItemCombo pic)
        {
            return new PlandoItemCombo
            {
                // to list makes a copy
                ItemListConverted = pic.ItemListConverted.ToList(),
                CheckListConverted = pic.CheckListConverted.ToList(),
                CheckList = (pic.CheckList == null) ? null : pic.CheckList.ToList(),
                CheckListInverted = (pic.CheckListInverted == null) ? null : pic.CheckListInverted.ToList(),
                CheckListInvertedConverted = (pic.CheckListInvertedConverted == null) ? null : pic.CheckListInvertedConverted.ToList(),
                SkipLogic = pic.SkipLogic,
                ItemDrawCount = pic.ItemDrawCount,
                Name = pic.Name,
                Notes = pic.Notes
            };
        }
    }

    [System.Diagnostics.DebuggerDisplay("[{Name}]")]
    public class PlandoMusicCombo : PlandoCombo
    {
        public List<string> SongsList { get; set; }
        public List<string> SlotsList { get; set; }
    }

    // will allow you to select which hints you want
    // public class PlandoHintCombo

    // will allow you to select which entrances you want
    // public class PlandoEntranceCombo

    // will allow you to select which enemies you want
    // public class PlandoEnemyCombo

    class PlandoUtils
    {
        /// read plando list(s) from file
        /// itemList is for region checking
        public static List<PlandoItemCombo> ReadAllItemPlandoFiles(List<Item> randomizerItemList, MMR.Randomizer.ItemList itemList)
        {
            // any file with FILEName_ItemPlando.json in the base directory is a plando file
            // resource folders getting nuked, cannot use, just assume base directory is best places for now
            var itemPlandoList = new List<PlandoItemCombo>();
            foreach (var filePath in Directory.GetFiles(Values.MainDirectory, "*ItemPlando.json"))
            {
                var fileName = Path.GetFileName(filePath);
                string filetext = File.ReadAllText(fileName);
                try
                {
                    // the string enum converter reads the item enumerators as strings rather than their int values, so we can read item/checks by enum
                    // eg, the json can have ItemList: ["MaskBunnyHood"] instead of ItemList: [ 22 ]
                    var workingList = JsonSerializer.Deserialize<List<PlandoItemCombo>>(filetext);
                    // for item in workingList, get object reference from ItemListConverted, beacuse we need to modify these later
                    foreach (PlandoItemCombo pic in workingList)
                    {

                        pic.ItemListConverted = ConvertStringsToItemsAndRegions(pic.ItemList, randomizerItemList, itemList);

                        for (int i = 0; i < pic.ItemListConverted.Count; i++)
                        {
                            Item? itemSearch = randomizerItemList.Find(u => u == pic.ItemListConverted[i]);
                            if (itemSearch != null)
                            {
                                pic.ItemListConverted[i] = (Item) itemSearch;
                            }
                        }

                        if (pic.CheckListInverted?.Count > 0) // inverted list
                        {
                            pic.CheckListInvertedConverted = ConvertStringsToItemsAndRegions(pic.CheckListInverted, randomizerItemList, itemList);

                            // start with a list of all checks, remove from inverted list
                            var invertedList = randomizerItemList.ToList();
                            for (int i = 0; i < pic.CheckListInvertedConverted.Count; i++)
                            {
                                Item? checkSearch = randomizerItemList.Find(u => u == pic.CheckListInvertedConverted[i]);
                                if (checkSearch != null)
                                {
                                    invertedList.Remove((Item) checkSearch);
                                }
                            }
                            pic.CheckListConverted = invertedList;
                        }
                        else // regular check list
                        {
                            pic.CheckListConverted = ConvertStringsToItemsAndRegions(pic.CheckList, randomizerItemList, itemList);

                            for (int i = 0; i < pic.CheckListConverted.Count; i++)
                            {
                                Item? checkSearch = randomizerItemList.Find(u => u == pic.CheckListConverted[i]);
                                if (checkSearch != null)
                                {
                                    pic.CheckListConverted[i] = (Item) checkSearch;
                                }
                            }
                        }
                    }
                    itemPlandoList = itemPlandoList.Concat(workingList).ToList();
                }
                catch (System.Text.Json.JsonException e)
                {
                    if (filetext[0] != '[')
                    {
                        throw new Exception("The following plando file failed to parse:\n"
                                      + Path.GetFileName(filePath) + "\n\n"
                                      + "There is no starting square bracket \"[\" to mark this file a list of events.\n"
                                      + "Even if there is only one event the file needs to have a list to parse. Example:\n"
                                      + "[\n"
                                      + "  {\n"
                                      + "    // your event data goes here \n"
                                      + "  },\n"
                                      + "]\n" );
                    }
                    else
                    {
                        Debug.Print("Error: exception occurred reading plando file: " + e.ToString());
                        throw new Exception("The following plando file failed to parse:\n"
                                          + Path.GetFileName(filePath) + "\n\n"
                                          + "That means it was not in acceptable json format.\n"
                                          + "Common reasons are missing punctuation or characters,\n"
                                          + "   like a missing comma separating items in a list\n"
                                          + "   or a missing comma separating parts of a single combo\n"
                                          + "   or a missing \" character at the start/end of an item\n"
                                          + "Sometimes the line number of the error is below the actual issue\n\n"
                                          + "The location of the parse error was reported at\n"
                                          + "line number: " + e.LineNumber + ", " + e.BytePositionInLine + " characters deep.");
                    }

                }
                catch (Exception e)
                {
                    Debug.Print("Error: exception occurred reading plando file: " + e.ToString());
                    throw new Exception("plando file read exception:\n" + e.ToString() + "\n file: " + Path.GetFileName(filePath));
                }
            }
            return itemPlandoList;
        }

        public static List<Item> ConvertStringsToItemsAndRegions(List<string> strings, List<Item> randomizerItemList, MMR.Randomizer.ItemList itemList)
        {
            /// we now have regions and items in the same item and check pools
            ///  so this gets repeated for both lists for item plando

            if (strings == null) // with checks, we can have an inverted list where the other is null
            {
                return null;
            }

            var returnItems = new List<Item>();
            for (int i = 0; i < strings.Count; i++)
            {
                // test if item is a region or an item
                var stringValue = strings[i];
                if (Enum.IsDefined(typeof(Region), stringValue))
                {
                    Enum.TryParse(stringValue, out Region regionEnum);
                    List<Item> allItemsInRegion = randomizerItemList.FindAll(item => item.Region(itemList) == regionEnum);
                    returnItems.AddRange(allItemsInRegion);

                }
                else // regular item
                {
                    Enum.TryParse(stringValue, out Item itemEnum);
                    returnItems.Add(itemEnum);
                }
            }

            return returnItems;
        }

        public static List<PlandoMusicCombo> ReadAllMusicPlandoFiles(string directory = "Resources/music")
        {
            var musicPlandoList = new List<PlandoMusicCombo>();
            foreach (String filePath in Directory.GetFiles(directory, "*MusicPlando.json"))
            {
                try
                {
                    var fileText = File.ReadAllText(filePath);
                    var workingList = JsonSerializer.Deserialize<List<PlandoMusicCombo>>(fileText);

                    if (workingList == null)
                        throw new Exception($"MusicPlando: Plando file [{filePath}] failed to parse"); // not sure this one isnt an exception
                    foreach (var plandoEvent in workingList)
                    {
                        if (plandoEvent.SongsList == null)
                        {
                            throw new Exception($"MusicPlando: Plando file\n [{filePath}]\n" +
                                $" has a broken SongsList for event:\n [{plandoEvent.Name}]");
                        }
                        if (plandoEvent.SlotsList == null)
                        {
                            throw new Exception($"MusicPlando: Plando file\n [{filePath}]\n" +
                                $" has a broken SlotsList for event:\n [{plandoEvent.Name}]");
                        }

                    }

                    musicPlandoList = musicPlandoList.Concat(workingList).ToList();
                }
                catch (Exception ex)
                {
                    Debug.Print("Error: exception occurred reading plando file: " + ex.ToString());
#if DEBUG
                      throw new Exception($"plando file read error: " + ex.ToString() + " file: " + Path.GetFileName(filePath));
#endif
                }
            }
            return musicPlandoList;
        }

        public static List<(SequenceInfo, SequenceInfo)> GetRandomizedSongPlacements(Random random, System.Text.StringBuilder songLog)
        {
            void DebugOut(string s)
            {
                Debug.WriteLine(s);
                songLog.AppendLine(s);
            }

            var returnSongTupleList = new List<(SequenceInfo, SequenceInfo)>();
            var allPlandoMusicCombos = ReadAllMusicPlandoFiles(Values.MusicDirectory);

            foreach (PlandoMusicCombo musicCombo in allPlandoMusicCombos)
            {
                // shuffle songs and slots based on our random seed
                musicCombo.SongsList = musicCombo.SongsList.OrderBy(x => random.Next()).ToList();
                musicCombo.SlotsList = musicCombo.SlotsList.OrderBy(x => random.Next()).ToList();

                // clean combo of already placed items and checks
                var previouslyUsedSongs = returnSongTupleList.Select(u => u.Item1.Name).ToList();
                foreach (string i in musicCombo.SongsList.ToList())
                {
                    if (previouslyUsedSongs.Contains(i))
                    {
                        DebugOut("Song already placed, removed from combo: " + i);
                        musicCombo.SongsList.Remove(i);
                    }
                    else if (! RomData.SequenceList.Any(u => u.Name == i))
                    {
                        throw new Exception("Music Plando Error: " +
                            "Song does not exist in sequence pool, did you misspell the song Name? \n" + i);
                    }
                }

                var previouslyUsedSlots = returnSongTupleList.Select(u => u.Item2.Name).ToList();
                foreach (string i in musicCombo.SlotsList.ToList())
                {
                    if (previouslyUsedSlots.Contains(i))
                    {
                        DebugOut("Slot already used, removed from combo: " + i);
                        musicCombo.SlotsList.Remove(i);
                    }
                    else if (!RomData.TargetSequences.Any(u => u.Name == i))
                    {
                        throw new Exception("Music Plando Error: " +
                            "Slot does not exist in slot pool, did you misspell the slot Name or forget to add it to seqs.txt? \n" + i);
                    }
                }

                if (musicCombo.SongsList.Count == 0)
                {
                    throw new Exception("Music Plando Error: " +
                        "Plando Music Combo is starved, all songs have already been placed: \n" + musicCombo.Name);
                }
                if (musicCombo.SlotsList.Count == 0)
                {
                    throw new Exception("Music Plando Error: " +
                        "Plando Music Combo is starved, all slots are already filled: \n" + musicCombo.Name);
                }

                if (musicCombo.ItemDrawCount <= -1 || musicCombo.ItemDrawCount > musicCombo.SongsList.Count)
                    musicCombo.ItemDrawCount = musicCombo.SongsList.Count;

                for (int i = 0; i < musicCombo.ItemDrawCount && i < musicCombo.SlotsList.Count; i++)
                {
                    SequenceInfo song = RomData.SequenceList.Find(u => u.Name == musicCombo.SongsList[i]);
                    SequenceInfo slot = RomData.TargetSequences.Find(u => u.Name == musicCombo.SlotsList[i]);

                    returnSongTupleList.Add((song, slot));
                    DebugOut("* Song placed: " + song.Name + " placed in slot " + slot.Name);
                }
            }

            return returnSongTupleList;
        }

        // remove items and checks already taken
        public static PlandoItemCombo CleanItemCombo(PlandoItemCombo itemCombo, Random random, List<Item> randomizerItemPool, ItemList randomizerItemList)
        {
            PlandoItemCombo returnCombo = new PlandoItemCombo
            {
                ItemListConverted = itemCombo.ItemListConverted.OrderBy(x => random.Next()).ToList(),
                CheckListConverted = itemCombo.CheckListConverted.OrderBy(x => random.Next()).ToList(),
                SkipLogic = itemCombo.SkipLogic,
                ItemDrawCount = itemCombo.ItemDrawCount,
                Name = itemCombo.Name,
                Notes = itemCombo.Notes
            };

            // clean combo of already placed items and checks
            foreach (Item item in returnCombo.ItemListConverted.ToList()) 
            {
                var itemVar = randomizerItemList[item];
                if (itemVar.NewLocation.HasValue)
                {
                    Debug.WriteLine("Item has already been placed. " + item);
                    returnCombo.ItemListConverted.Remove(item);
                }
            }

            foreach (Item check in returnCombo.CheckListConverted.ToList())
            {
                if ( ! randomizerItemPool.Contains(check))
                {
                    Debug.WriteLine("Check does not exist in randomized item pool, either already taken or not randomized: " + check);
                    returnCombo.CheckListConverted.Remove(check);
                }
            }

            if (returnCombo.ItemListConverted.Count == 0)
            {
                Debug.WriteLine("Plando Item Combo is starved, all items have already been placed: " + returnCombo.Name);
                return null;
            }
            if (returnCombo.CheckListConverted.Count == 0)
            {
                Debug.WriteLine("Plando Item Combo is starved, all checks are already filled: " + returnCombo.Name);
                return null;
            }

            if (returnCombo.ItemDrawCount <= -1)
                returnCombo.ItemDrawCount = returnCombo.ItemListConverted.Count;

            return returnCombo;
        }

    }
}
