using MMR.Common.Extensions;
using MMR.Randomizer.Attributes;
using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Models;
using MMR.Randomizer.Models.Rom;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace MMR.Randomizer.Utils
{
    public static class MessageUtils
    {
        static ReadOnlyCollection<byte> MessageHeader
            = new ReadOnlyCollection<byte>(new byte[] {
                2, 0, 0xFE, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF
        });

        public static List<MessageEntry> MakeGossipQuotes(RandomizedResult randomizedResult)
        {
            if (randomizedResult.Settings.GossipHintStyle == GossipHintStyle.Default)
                return new List<MessageEntry>();

            var random = new Random(randomizedResult.Seed);

            var randomizedItems = new List<ItemObject>();
            var hintableItems = new List<ItemObject>();
            var itemsInRegions = new Dictionary<Region, List<ItemObject>>();
            foreach (var item in randomizedResult.ItemList)
            {
                if (item.NewLocation == null)
                {
                    continue;
                }

                if (item.Item.IsEntrance())
                {
                    continue;
                }

                if (randomizedResult.Settings.ClearHints)
                {
                    // skip free items
                    if (ItemUtils.IsStartingLocation(item.NewLocation.Value))
                    {
                        continue;
                    }
                }

                if (!item.IsRandomized)
                {
                    continue;
                }

                randomizedItems.Add(item);

                var itemName = item.Item.Name();
                if (randomizedResult.Settings.GossipHintStyle != GossipHintStyle.Competitive 
                    && (itemName.Contains("Heart") || itemName.Contains("Rupee"))
                    && (randomizedResult.Settings.ClearHints || random.Next(8) != 0))
                {
                    continue;
                }

                if (randomizedResult.Settings.GossipHintStyle == GossipHintStyle.Competitive)
                {
                    var preventRegions = new List<Region> { Region.TheMoon, Region.BottleCatch, Region.Misc };
                    var itemRegion = item.NewLocation.Value.Region();
                    if (itemRegion.HasValue
                        && !preventRegions.Contains(itemRegion.Value)
                        && !randomizedResult.Settings.CustomJunkLocations.Contains(item.NewLocation.Value))
                    {
                        if (!itemsInRegions.ContainsKey(itemRegion.Value))
                        {
                            itemsInRegions[itemRegion.Value] = new List<ItemObject>();
                        }
                        itemsInRegions[itemRegion.Value].Add(item);
                    }

                    var competitiveHintInfo = item.NewLocation.Value.GetAttribute<GossipCompetitiveHintAttribute>();
                    if (competitiveHintInfo == null)
                    {
                        continue;
                    }

                    if (randomizedResult.Settings.CustomJunkLocations.Contains(item.NewLocation.Value))
                    {
                        randomizedItems.Remove(item);
                        continue;
                    }

                    if (competitiveHintInfo.Condition != null && competitiveHintInfo.Condition(randomizedResult.Settings))
                    {
                        randomizedItems.Remove(item);
                        continue;
                    }
                }

                hintableItems.Add(item);
            }

            var unusedItems = hintableItems.ToList();
            var itemsToCombineWith = new List<ItemObject>();
            var competitiveHints = new List<(string message, List<GossipQuote> allowedGossipQuotes)>();

            if (randomizedResult.Settings.GossipHintStyle == GossipHintStyle.Competitive)
            {
                var gossipStoneRequirements = LogicUtils.GetGossipStoneRequirements(randomizedResult.ItemList, randomizedResult.Logic, randomizedResult.Settings);

                var totalUniqueGossipHints = Enum.GetValues(typeof(GossipQuote)).Cast<GossipQuote>().Count(gq => !gq.IsMoonGossipStone()) / 2;

                var numberOfRequiredHints = randomizedResult.Settings.AddSongs ? 4 : 3;
                var numberOfNonRequiredHints = 3;
                var maxNumberOfSongOnlyHints = 3;
                var maxNumberOfClockTownHints = 2;

                var numberOfLocationHints = totalUniqueGossipHints - numberOfRequiredHints - numberOfNonRequiredHints;
                unusedItems = hintableItems.GroupBy(io => io.NewLocation.Value.GetAttribute<GossipCompetitiveHintAttribute>().Priority)
                                        .OrderByDescending(g => g.Key)
                                        .Select(g => g.OrderBy(_ => random.Next()).AsEnumerable())
                                        .Aggregate((g1, g2) => g1.Concat(g2))
                                        .Take(numberOfLocationHints)
                                        .ToList();
                var combinedItems = unusedItems
                    .SelectMany(io => io.NewLocation.Value.GetAttributes<GossipCombineAttribute>().Select(gca => gca.OtherItem))
                    .Where(item => randomizedItems.Any(io => io.NewLocation == item))
                    .Select(item => randomizedItems.Single(io => io.NewLocation == item))
                    .Where(io => !unusedItems.Contains(io))
                    ;
                itemsToCombineWith.AddRange(combinedItems);

                unusedItems.AddRange(unusedItems);

                foreach (var unusedItem in unusedItems)
                {
                    (var messageText, var combined) = BuildItemHint(unusedItem, randomizedResult.Settings.GossipHintStyle, false, randomizedResult.Settings.ClearHints, false, itemsToCombineWith, hintableItems, random);
                    //(var messageText2, var combined2) = BuildItemHint(unusedItem, randomizedResult.Settings.GossipHintStyle, false, randomizedResult.Settings.ClearHints, false, itemsToCombineWith, hintableItems, random);

                    var allowedGossipQuotes = combined
                        .Select(io => gossipStoneRequirements.Where(kvp => !kvp.Value.Contains(io.Item)).Select(kvp => kvp.Key))
                        .Aggregate((list1, list2) => list1.Intersect(list2))
                        .ToList();
                    competitiveHints.Add((messageText, allowedGossipQuotes));
                    //competitiveHints.Add((messageText2, allowedGossipQuotes));
                }

                var importantRegionCounts = new Dictionary<Region, List<ItemObject>>();
                var nonImportantRegionCounts = new Dictionary<Region, List<ItemObject>>();
                var songOnlyRegionCounts = new Dictionary<Region, List<ItemObject>>();
                var clockTownRegionCounts = new Dictionary<Region, List<ItemObject>>();
                foreach (var kvp in itemsInRegions)
                {
                    var requiredItems = kvp.Value.Where(io => ItemUtils.IsRequired(io.Item, randomizedResult) && !unusedItems.Contains(io) && !itemsToCombineWith.Contains(io)).ToList();
                    var importantItems = kvp.Value.Where(io => ItemUtils.IsImportant(io.Item, randomizedResult)).ToList();

                    if (requiredItems.Count == 0 && importantItems.Count > 0)
                    {
                        continue;
                    }

                    Dictionary<Region, List<ItemObject>> dict;
                    if (requiredItems.Count == 0)
                    {
                        dict = nonImportantRegionCounts;
                    }
                    else if (Gossip.ClockTownRegions.Contains(kvp.Key))
                    {
                        dict = clockTownRegionCounts;
                    }
                    else if (!randomizedResult.Settings.AddSongs && kvp.Value.Count(io => ItemUtils.IsRequired(io.Item, randomizedResult) && !ItemUtils.IsSong(io.Item) && !unusedItems.Contains(io)) == 0)
                    {
                        dict = songOnlyRegionCounts;
                    }
                    else
                    {
                        dict = importantRegionCounts;
                    }
                    
                    dict[kvp.Key] = requiredItems;
                }

                var chosenSongOnlyRegions = 0;
                var chosenClockTownRegions = 0;
                for (var i = 0; i < numberOfRequiredHints; i++)
                {
                    var regionCounts = importantRegionCounts.AsEnumerable();
                    if (chosenClockTownRegions < maxNumberOfClockTownHints)
                    {
                        regionCounts = regionCounts.Concat(clockTownRegionCounts);
                    }
                    if (chosenSongOnlyRegions < maxNumberOfSongOnlyHints)
                    {
                        regionCounts = regionCounts.Concat(songOnlyRegionCounts);
                    }
                    if (!regionCounts.Any())
                    {
                        regionCounts = regionCounts.Concat(clockTownRegionCounts);
                    //}
                    //if (!regionCounts.Any())
                    //{
                        regionCounts = regionCounts.Concat(songOnlyRegionCounts);
                    }
                    if (regionCounts.Any())
                    {
                        var chosen = regionCounts.ToList().Random(random);
                        var allowedGossipQuotes = chosen.Value
                            .Select(io => gossipStoneRequirements.Where(kvp => !kvp.Value.Contains(io.Item)).Select(kvp => kvp.Key))
                            .Aggregate((list1, list2) => list1.Intersect(list2))
                            .ToList();
                        competitiveHints.Add((BuildRegionHint(chosen, random), allowedGossipQuotes));
                        competitiveHints.Add((BuildRegionHint(chosen, random), allowedGossipQuotes));
                        if (songOnlyRegionCounts.Remove(chosen.Key))
                        {
                            chosenSongOnlyRegions++;
                        }
                        else if (clockTownRegionCounts.Remove(chosen.Key))
                        {
                            chosenClockTownRegions++;
                        }
                        else
                        {
                            importantRegionCounts.Remove(chosen.Key);
                        }
                    }
                }

                for (var i = 0; i < numberOfNonRequiredHints; i++)
                {
                    if (nonImportantRegionCounts.Any())
                    {
                        var chosen = nonImportantRegionCounts.ToList().Random(random);
                        competitiveHints.Add((BuildRegionHint(chosen, random), new List<GossipQuote>()));
                        competitiveHints.Add((BuildRegionHint(chosen, random), new List<GossipQuote>()));
                        nonImportantRegionCounts.Remove(chosen.Key);
                    }
                }
            }

            List<MessageEntry> finalHints = new List<MessageEntry>();

            while (competitiveHints.Any(ch => ch.allowedGossipQuotes.Count > 0))
            {
                var competitiveHint = competitiveHints
                    .Where(ch => ch.allowedGossipQuotes.Count > 0)
                    .OrderBy(ch => ch.allowedGossipQuotes.Count)
                    .ThenBy(ch => random.Next())
                    .First();

                var gossipQuote = competitiveHint.allowedGossipQuotes.Random(random);
                finalHints.Add(new MessageEntry()
                {
                    Id = (ushort)gossipQuote,
                    Message = competitiveHint.message,
                    Header = MessageHeader.ToArray()
                });
                competitiveHints.Remove(competitiveHint);
                foreach (var ch in competitiveHints)
                {
                    ch.allowedGossipQuotes.Remove(gossipQuote);
                }
            }

            foreach (var gossipQuote in Enum.GetValues(typeof(GossipQuote)).Cast<GossipQuote>().OrderBy(gq => random.Next()))
            {
                if (finalHints.Any(me => me.Id == (ushort)gossipQuote))
                {
                    continue;
                }

                string messageText = null;
                var isMoonGossipStone = gossipQuote.IsMoonGossipStone();
                if (!isMoonGossipStone && competitiveHints.Any())
                {
                    var competitiveHint = competitiveHints.Random(random);
                    messageText = competitiveHint.message;
                    competitiveHints.Remove(competitiveHint);
                }

                if (messageText == null)
                {
                    var restrictionAttributes = gossipQuote.GetAttributes<GossipRestrictAttribute>().ToList();
                    ItemObject item = null;
                    var forceClear = false;
                    while (item == null)
                    {
                        if (restrictionAttributes.Any() && (isMoonGossipStone || randomizedResult.Settings.GossipHintStyle == GossipHintStyle.Relevant))
                        {
                            var chosen = restrictionAttributes.Random(random);
                            var candidateItem = chosen.Type == GossipRestrictAttribute.RestrictionType.Item
                                ? randomizedResult.ItemList.Single(io => io.Item == chosen.Item)
                                : randomizedResult.ItemList.Single(io => io.NewLocation == chosen.Item);
                            if (isMoonGossipStone || unusedItems.Contains(candidateItem))
                            {
                                item = candidateItem;
                                forceClear = chosen.ForceClear;
                            }
                            else
                            {
                                restrictionAttributes.Remove(chosen);
                            }
                        }
                        else if (unusedItems.Any())
                        {
                            if (randomizedResult.Settings.GossipHintStyle == GossipHintStyle.Competitive)
                            {
                                item = unusedItems.FirstOrDefault(io => unusedItems.Count(x => x.Item == io.Item) == 1);
                                if (item == null)
                                {
                                    item = unusedItems.Random(random);
                                }
                            }
                            else
                            {
                                item = unusedItems.Random(random);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (!isMoonGossipStone)
                    {
                        unusedItems.Remove(item);
                    }

                    if (item != null)
                    {
                        (var hint, var combined) = BuildItemHint(item, randomizedResult.Settings.GossipHintStyle, forceClear, randomizedResult.Settings.ClearHints, isMoonGossipStone, itemsToCombineWith, hintableItems, random);
                        messageText = hint;
                    }
                }

                if (messageText == null)
                {
                    messageText = Gossip.JunkMessages.Random(random);
                }

                finalHints.Add(new MessageEntry()
                {
                    Id = (ushort)gossipQuote,
                    Message = messageText,
                    Header = MessageHeader.ToArray()
                });
            }

            // forced hints 
            //soundEffectId = 0x690A; // grandma laugh
            //var MikauBaybee = 0x6175;
            var granny = 0x690C;
            //string sfx = $"{(char)((soundEffectId >> 8) & 0xFF)}{(char)(soundEffectId & 0xFF)}";

            var soundEffectMikau = $"{(char)((granny >> 8) & 0xFF)}{(char)(granny & 0xFF)}";
            //var secret_ending_hint = $"\x1E{soundEffectMikau}{start} {string.Join(" and ", locationMessages.Select(locationName => $"\x01{locationName}\x00"))} {mid} {string.Join(hasOrder ? " then " : " and ", itemMessages.Select(itemName => $"\x06{itemName}\x00"))}...\xBF".Wrap(35, "\x11");
            // start and mid are just "It seems" and "conceals" these are just the binders
            // color change: $"\x01{locationName}\x00" dark red  and $"\x06{itemName}\x00" pink) TextCommands has the rest '\x03'
            // word wrap does not happen auto, use \x11 when we want to wrap
            //(var secret_ending_hint, var combined) = BuildItemHint(item, randomizedResult.Settings.GossipHintStyle, forceClear, randomizedResult.Settings.ClearHints, isMoonGossipStone, itemsToCombineWith, hintableItems, random);

            var secret_ending_hint = $"\x1E{soundEffectMikau} It is said that \x03the power of music\x00\x11 can\x06 fly you to the moon\x00 ...\xBF";
            finalHints.Find(u => u.Id == (ushort)GossipQuote.SwampSpiderHouse).Message = secret_ending_hint;

            var eggshint = $"\x1E{soundEffectMikau}It is said that \x01the children of zora\x00\x11teach \x07the power to heal\x00 ...\xBF";
            finalHints.Find(u => u.Id == (ushort)GossipQuote.OceanZoraGame).Message = eggshint;

            eggshint = $"\x1E{soundEffectMikau}It seems that \x01pinnacle rock\x00\x11 contains \x07 nothing but ice traps\x00 ...\xBF";
            finalHints.Find(u => u.Id == (ushort)GossipQuote.RanchCuccoShack).Message = eggshint;

            var newhint = $"\x1E{soundEffectMikau}It seems that the\x01 frozen mountains\x00\x11 conceal a\x06 heroic weapon\x00 ...\xBF";
            finalHints.Find(u => u.Id == (ushort)GossipQuote.CanyonDock).Message = newhint;

            newhint = $"\x1E{soundEffectMikau}The reward for winning\x06 Goron Race\x00\x11 is a\x06 dangerous mask\x00 ...\xBF";
            finalHints.Find(u => u.Id == (ushort)GossipQuote.RanchCuccoShack).Message = newhint;

            newhint = $"\x1E{soundEffectMikau}Something \x04Mysterious\x00 is happening\x11 at the\x06 Pirates Fortress\x00 ...\xBF";
            finalHints.Find(u => u.Id == (ushort)GossipQuote.CanyonRavine).Message = newhint;

            newhint = $"\x1E{soundEffectMikau}Something \x04Mysterious\x00 is happening\x11 at the\x06 Romani Ranch\x00 ...\xBF";
            finalHints.Find(u => u.Id == (ushort)GossipQuote.CanyonSpiritHouse).Message = newhint;

            newhint = $"\x1E{soundEffectMikau}Something \x01very powerful\x00 is waiting\x11 at the\x06 stockpot inn\x00 ...\xBF";
            finalHints.Find(u => u.Id == (ushort)GossipQuote.MilkRoad).Message = newhint;

            newhint = $"\x1E{soundEffectMikau}Masks of\x04 fallen heroes\x00 are being held\x11 by\x06 restless wandering souls\x00 ...\xBF";
            finalHints.Find(u => u.Id == (ushort)GossipQuote.TerminaGossipLarge).Message = newhint;

            /*
            newhint = $"\x1E{soundEffectMikau}It seems\x01 MMARO\x00 is never\x11 getting an\x11is \x06\x00 ...\xBF";
            finalHints.Find(u => u.Id == (ushort)GossipQuote.TerminaGossipLarge).Message = newhint;

            newhint = $"\x1E{soundEffectMikau}Something is \x04wrong\x00 in Termina\x11I can feel it ...\xBF";
            finalHints.Find(u => u.Id == (ushort)GossipQuote.TerminaGossipPipes).Message = newhint;
            */

            int nothing_at_all = 0;


            return finalHints;
        }

        private static (string, List<ItemObject>) BuildItemHint(ItemObject item, GossipHintStyle gossipHintStyle, bool forceClear, bool clearHints, bool isMoonGossipStone, List<ItemObject> itemsToCombineWith, List<ItemObject> hintableItems, Random random)
        {
            ushort soundEffectId = 0x690C; // grandma curious
            var itemNames = new List<string>();
            var locationNames = new List<string>();
            bool hasOrder = item.NewLocation.Value.HasAttribute<GossipCombineOrderAttribute>();
            var combined = new List<ItemObject>();
            if (forceClear || clearHints)
            {
                itemNames.Add(item.Item.Name());
                locationNames.Add(item.NewLocation.Value.Location());
                if (!isMoonGossipStone)
                {
                    var gossipCombineAttributes = item.NewLocation.Value.GetAttributes<GossipCombineAttribute>();
                    combined = itemsToCombineWith.Where(io => gossipCombineAttributes.Any(gca => gca.OtherItem == io.NewLocation)).ToList();
                    if (combined.Any())
                    {
                        combined.Add(item);
                        combined = combined.OrderBy(io => io.NewLocation.Value.GetAttribute<GossipCombineOrderAttribute>()?.Order ?? random.Next()).ToList();
                        locationNames.Clear();
                        itemNames.Clear();
                        var combinedName = gossipCombineAttributes.First().CombinedName;
                        if (!string.IsNullOrWhiteSpace(combinedName))
                        {
                            locationNames.Add(combinedName);
                        }
                        else
                        {
                            locationNames.AddRange(combined.Select(io => io.NewLocation.Value.Location()));
                        }
                        itemNames.AddRange(combined.Select(io => io.Item.Name()));
                    }
                    else
                    {
                        combined.Add(item);
                    }
                }
            }
            else
            {
                if (item.Mimic != null)
                {
                    // If item has a mimic and not using clear hints, always use a fake hint.
                    soundEffectId = 0x690A; // grandma laugh
                    itemNames.Add(item.Mimic.Item.ItemHints().Random(random));
                    locationNames.Add(item.NewLocation.Value.LocationHints().Random(random));
                }
                else if (isMoonGossipStone || gossipHintStyle == GossipHintStyle.Competitive || random.Next(100) >= 5) // 5% chance of fake/junk hint if it's not a moon gossip stone or competitive style
                {
                    itemNames.Add(item.Item.ItemHints().Random(random));
                    locationNames.Add(item.NewLocation.Value.LocationHints().Random(random));
                }
                else
                {
                    if (random.Next(2) == 0) // 50% chance for fake hint. otherwise default to junk hint.
                    {
                        soundEffectId = 0x690A; // grandma laugh
                        itemNames.Add(item.Item.ItemHints().Random(random));
                        locationNames.Add(hintableItems.Random(random).Item.LocationHints().Random(random));
                    }
                }
            }
            if (itemNames.Any() && locationNames.Any())
            {
                return (BuildGossipQuote(soundEffectId, locationNames, itemNames, hasOrder, random), combined);
            }
            return (null, null);
        }

        private static string BuildRegionHint(KeyValuePair<Region, List<ItemObject>> regionInfo, Random random)
        {
            var region = regionInfo.Key;
            var numberOfRequiredItems = regionInfo.Value.Count;

            ushort soundEffectId = 0x690C; // grandma curious
            string start = Gossip.MessageStartSentences.Random(random);

            string sfx = $"{(char)((soundEffectId >> 8) & 0xFF)}{(char)(soundEffectId & 0xFF)}";
            var locationMessage = region.Name();
            var mid = "is";
            var itemMessage = numberOfRequiredItems > 0
                ? "on the Way of the Hero"
                : "a foolish choice";
            char color;
            if (numberOfRequiredItems > 0)
            {
                color = TextCommands.ColorYellow;
            }
            else
            {
                color = TextCommands.ColorSilver;
            }

            return $"\x1E{sfx}{start} {color}{locationMessage}{TextCommands.ColorWhite} {mid} {itemMessage}...\xBF".Wrap(35, "\x11");

            //var mid = "has";
            //return $"\x1E{sfx}{start} {TextCommands.ColorRed}{locationMessage}{TextCommands.ColorWhite} {mid} {color}{NumberToWords(numberOfImportantItems)} important item{(numberOfImportantItems == 1 ? "" : "s")}{TextCommands.ColorWhite}...\xBF".Wrap(35, "\x11");
        }

        private static string BuildGossipQuote(ushort soundEffectId, IEnumerable<string> locationMessages, IEnumerable<string> itemMessages, bool hasOrder, Random random)
        {


            int startIndex = random.Next(Gossip.MessageStartSentences.Count);
            int midIndex = random.Next(Gossip.MessageMidSentences.Count);
            string start = Gossip.MessageStartSentences[startIndex];
            string mid = Gossip.MessageMidSentences[midIndex];

            string sfx = $"{(char)((soundEffectId >> 8) & 0xFF)}{(char)(soundEffectId & 0xFF)}";

            return $"\x1E{sfx}{start} {string.Join(" and ", locationMessages.Select(locationName => $"\x01{locationName}\x00"))} {mid} {string.Join(hasOrder ? " then " : " and ", itemMessages.Select(itemName => $"\x06{itemName}\x00"))}...\xBF".Wrap(35, "\x11");
        }

        public static string BuildShopDescriptionMessage(string title, int cost, string description)
        {
            return $"\x01{title}: {cost} Rupees\x11\x00{description.Wrap(35, "\x11")}\x1A\xBF";
        }

        public static string BuildShopPurchaseMessage(string title, int cost, Item item)
        {
            return $"{title}: {cost} Rupees\x11 \x11\x02\xC2I'll buy {GetPronoun(item)}\x11No thanks\xBF";
        }

        public static string GetArticle(Item item, string indefiniteArticle = null, string name = null)
        {
            var shopTexts = item.ShopTexts();
            return shopTexts.IsMultiple
                ? ""
                : shopTexts.IsDefinite
                    ? "the "
                    : indefiniteArticle ?? (Regex.IsMatch(name ?? item.Name(), "^[aeiou]", RegexOptions.IgnoreCase)
                        ? "an "
                        : "a ");
        }

        public static string GetPronoun(Item item, string name = null)
        {
            var shopTexts = item.ShopTexts();
            var itemAmount = Regex.Replace(name ?? item.Name(), "[^0-9]", "");
            return shopTexts.IsMultiple && !string.IsNullOrWhiteSpace(itemAmount)
                ? "them"
                : "it";
        }

        public static string GetPronounOrAmount(Item item, string it = " It", string name = null)
        {
            var shopTexts = item.ShopTexts();
            var itemAmount = Regex.Replace(name ?? item.Name(), "[^0-9]", "");
            return shopTexts.IsMultiple
                ? string.IsNullOrWhiteSpace(itemAmount)
                    ? it
                    : " " + itemAmount
                : shopTexts.IsDefinite
                    ? it
                    : " One";
        }

        public static string GetVerb(Item item, string name = null)
        {
            var shopTexts = item.ShopTexts();
            var itemAmount = Regex.Replace(name ?? item.Name(), "[^0-9]", "");
            return shopTexts.IsMultiple && !string.IsNullOrWhiteSpace(itemAmount)
                ? "are"
                : "is";
        }

        public static string GetFor(Item item)
        {
            var shopTexts = item.ShopTexts();
            return shopTexts.IsDefinite
                ? "is"
                : "for";
        }

        public static string GetAlternateName(string name)
        {
            return Regex.Replace(name, "[0-9]+ ", "");
        }

        public static string GetAlternateName(Item item)
        {
            return GetAlternateName(item.Name());
        }

        static string GetRawPlural(string name)
        {
            var useEs = "ch,i,ns,o,sh,ss,x".Split(',').Any(x => name.EndsWith(x));
            if (useEs)
            {
                // Use "es" ending instead of "s".
                return $"{name}es";
            }
            else if ("by,ry".Split(',').Any(x => name.EndsWith(x)))
            {
                // Replace "y" => "ies" in certain situations.
                var withoutY = name.Substring(0, name.Length - 1);
                return $"{withoutY}ies";
            }
            else if (name.EndsWith("s"))
            {
                // Assume name is already plural.
                return name;
            }
            else
            {
                return $"{name}s";
            }
        }

        public static string GetPlural(string name)
        {
            var alt = GetAlternateName(name);
            var altSplit = alt.Split(' ');

            // Check if the plural is identical to the singular.
            var samePlural = "Bombchu".Split(',').Any(x => alt.Equals(x));
            if (samePlural)
            {
                return alt;
            }

            // Check if there is a starting noun which should be pluralized instead.
            var startingNoun = "Bottle,Elegy,Lens,Letter,Map,Mask,Oath,Pendant,Piece,Sonata,Song".Split(',').Any(x => altSplit[0].Equals(x));
            if (startingNoun)
            {
                altSplit[0] = GetRawPlural(altSplit[0]);
                return string.Join(" ", altSplit);
            }

            return GetRawPlural(alt);
        }

        private static string[] numberWordUnitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        private static string[] numberWordTensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                if (number < 20)
                    words += numberWordUnitsMap[number];
                else
                {
                    words += numberWordTensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + numberWordUnitsMap[number % 10];
                }
            }

            return words;
        }
    }
}