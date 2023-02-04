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

        public static List<MessageEntry> MakeGossipQuotes(
            IEnumerable<GossipQuote> gossipQuotes, GossipHintStyle hintStyle, RandomizedResult randomizedResult,
            int numberOfRequiredHints, int numberOfNonRequiredHints, int maxNumberOfClockTownHints,
            List<Region> hintedRegions, List<ItemObject> hintedItems)
        {
            if (hintStyle == GossipHintStyle.Default)
                return new List<MessageEntry>();

            var random = new Random(randomizedResult.Seed);

            var randomizedItems = new List<ItemObject>();
            var hintableItems = new List<ItemObject>();
            var itemsInRegions = new Dictionary<Region, List<(ItemObject io, Item locationForImportance)>>();
            foreach (var io in randomizedResult.ItemList)
            {
                if ((!io.IsRandomized || !io.NewLocation.Value.Region().HasValue) && (!io.Item.MainLocation().HasValue || !randomizedResult.ItemList[io.Item.MainLocation().Value].IsRandomized))
                {
                    continue;
                }

                // TODO make this less hard-coded
                if (io.NewLocation == Item.UpgradeRoyalWallet)
                {
                    continue;
                }

                var item = io.Item.MainLocation().HasValue ? randomizedResult.ItemList.Find(x => x.NewLocation == io.Item.MainLocation().Value) : io;

                if (!io.Item.MainLocation().HasValue)
                {
                    // skip free items
                    if (ItemUtils.IsStartingLocation(io.NewLocation.Value))
                    {
                        continue;
                    }
                }

                if (ItemUtils.IsRegionRestricted(randomizedResult.Settings, item.Item))
                {
                    continue;
                }

                randomizedItems.Add(item);

                if (hintStyle == GossipHintStyle.Competitive)
                {
                    var preventRegions = new List<Region> { Region.TheMoon, Region.BottleCatch, Region.Misc };
                    var locationForImportance = io.Item.MainLocation().HasValue ? io.Item : io.NewLocation.Value;
                    var itemRegion = locationForImportance.Region();
                    if (itemRegion.HasValue
                        && !preventRegions.Contains(itemRegion.Value)
                        && !randomizedResult.Settings.CustomJunkLocations.Contains(item.NewLocation.Value))
                    {
                        if (!itemsInRegions.ContainsKey(itemRegion.Value))
                        {
                            itemsInRegions[itemRegion.Value] = new List<(ItemObject, Item)>();
                        }
                        itemsInRegions[itemRegion.Value].Add((item, locationForImportance));
                    }

                    if (hintedItems.Contains(item))
                    {
                        continue;
                    }

                    if (randomizedResult.Settings.OverrideHintPriorities != null)
                    {
                        if (!randomizedResult.Settings.OverrideHintPriorities.Any(items => items.Contains(item.NewLocation.Value)))
                        {
                            continue;
                        }
                    }
                    else
                    {
                        var competitiveHintInfo = item.NewLocation.Value.GetAttribute<GossipCompetitiveHintAttribute>();
                        if (competitiveHintInfo == null)
                        {
                            continue;
                        }

                        if (competitiveHintInfo.Condition != null && !competitiveHintInfo.Condition(randomizedResult.Settings))
                        {
                            randomizedItems.Remove(item);
                            continue;
                        }
                    }

                    if (randomizedResult.Settings.CustomJunkLocations.Contains(io.NewLocation.Value))
                    {
                        randomizedItems.Remove(item);
                        continue;
                    }
                }

                hintableItems.Add(item);
            }

            var unusedItems = hintableItems.ToList();
            var itemsToCombineWith = new List<ItemObject>();
            var competitiveHints = new List<(string message, string clearMessage, List<GossipQuote> allowedGossipQuotes)>();

            if (hintStyle == GossipHintStyle.Competitive)
            {
                var gossipStoneRequirements = LogicUtils.GetGossipStoneRequirements(gossipQuotes, randomizedResult.ItemList, randomizedResult.Logic, randomizedResult.Settings, randomizedResult.CheckedImportanceLocations);

                var totalUniqueGossipHints = gossipQuotes.Count() / 2;

                var numberOfLocationHints = totalUniqueGossipHints - numberOfRequiredHints - numberOfNonRequiredHints;

                Func<ItemObject, int> getPriority = randomizedResult.Settings.OverrideHintPriorities != null
                    ? (io) => randomizedResult.Settings.OverrideHintPriorities.FindIndex(locations => locations.Contains(io.NewLocation.Value))
                    : (io) => -io.NewLocation.Value.GetAttribute<GossipCompetitiveHintAttribute>().Priority;

                unusedItems = hintableItems.GroupBy(io => io.NewLocation.Value.GetAttribute<GossipCombineAttribute>()?.CombinedName ?? io.NewLocation.Value.ToString())
                                        .Select(g => g.OrderBy(getPriority).First())
                                        .GroupBy(getPriority)
                                        .OrderBy(g => g.Key)
                                        .Select(g => g.OrderBy(_ => random.Next()).AsEnumerable())
                                        .Aggregate(Enumerable.Empty<ItemObject>(), (g1, g2) => g1.Concat(g2))
                                        .Take(numberOfLocationHints)
                                        .ToList();
                var combinedItems = unusedItems
                    .SelectMany(io => io.NewLocation.Value.GetAttributes<GossipCombineAttribute>().SelectMany(gca => gca.OtherItems))
                    .Where(item => randomizedItems.Any(io => io.NewLocation == item))
                    .Select(item => randomizedItems.Single(io => io.NewLocation == item))
                    .Where(io => !unusedItems.Contains(io) && hintableItems.Contains(io))
                    ;
                itemsToCombineWith.AddRange(combinedItems);

                unusedItems.AddRange(unusedItems);

                Func<ItemObject, bool> shouldIndicatePriority = randomizedResult.Settings.OverrideHintPriorities != null && randomizedResult.Settings.OverrideImportanceIndicatorTiers != null
                    ? io => randomizedResult.Settings.OverrideImportanceIndicatorTiers.Contains(getPriority(io))
                    : io => true;

                foreach (var unusedItem in unusedItems)
                {
                    (var messageText, var clearMessageText, var combined) = BuildItemHint(
                        unusedItem,
                        randomizedResult,
                        hintStyle,
                        itemsToCombineWith,
                        hintableItems,
                        shouldIndicatePriority,
                        random
                        );

                    var allowedGossipQuotes = combined
                        .Select(io => gossipStoneRequirements.Where(kvp => kvp.Value?.Contains(io.NewLocation.Value) == false).Select(kvp => kvp.Key))
                        .Aggregate((list1, list2) => list1.Intersect(list2))
                        .ToList();
                    competitiveHints.Add((messageText, clearMessageText, allowedGossipQuotes));

                    hintedItems.AddRange(combined);
                }

                var importantRegionCounts = new Dictionary<Region, List<(ItemObject io, Item locationForImportance)>>();
                var nonImportantRegionCounts = new Dictionary<Region, List<(ItemObject, Item)>>();
                var songOnlyRegionCounts = new Dictionary<Region, List<(ItemObject, Item)>>();
                var clockTownRegionCounts = new Dictionary<Region, List<(ItemObject, Item)>>();
                foreach (var kvp in itemsInRegions)
                {
                    var requiredItems = kvp.Value.Where(io => ItemUtils.IsRequired(io.io.Item, io.locationForImportance, randomizedResult) && !unusedItems.Contains(io.io) && !itemsToCombineWith.Contains(io.io)).ToList();
                    var importantItems = kvp.Value.Where(io => ItemUtils.IsImportant(io.io.Item, io.locationForImportance, randomizedResult)).ToList();

                    Dictionary<Region, List<(ItemObject, Item)>> dict;
                    if (requiredItems.Count == 0 && importantItems.Count > 0)
                    {
                        if (!randomizedResult.Settings.AddSongs && importantItems.All(io => ItemUtils.IsSong(io.io.Item)))
                        {
                            dict = songOnlyRegionCounts;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else if (requiredItems.Count == 0)
                    {
                        dict = nonImportantRegionCounts;
                    }
                    else if (Gossip.ClockTownRegions.Contains(kvp.Key))
                    {
                        dict = clockTownRegionCounts;
                    }
                    else
                    {
                        dict = importantRegionCounts;
                    }
                    
                    dict[kvp.Key] = requiredItems;

                    if (!randomizedResult.Settings.AddSongs && requiredItems.Count > 0 && requiredItems.All(io => ItemUtils.IsSong(io.io.Item)) && importantItems.All(io => ItemUtils.IsSong(io.io.Item)))
                    {
                        songOnlyRegionCounts[kvp.Key] = requiredItems;
                    }
                }

                for (var i = 0; i < numberOfNonRequiredHints; i++)
                {
                    var regionCounts = nonImportantRegionCounts.AsEnumerable();
                    regionCounts = regionCounts.Concat(songOnlyRegionCounts);
                    regionCounts = regionCounts.Where(kvp => !hintedRegions.Contains(kvp.Key));
                    if (regionCounts.Any())
                    {
                        var chosen = regionCounts.ToList().Random(random, kvp => itemsInRegions[kvp.Key].Count);
                        RegionHintType regionHintType;
                        if (songOnlyRegionCounts.Remove(chosen.Key))
                        {
                            regionHintType = RegionHintType.OnlyImportantSong;
                        }
                        else
                        {
                            nonImportantRegionCounts.Remove(chosen.Key);
                            regionHintType = RegionHintType.NoneRequired;
                        }

                        competitiveHints.Add((BuildRegionHint(chosen, regionHintType, random), null, new List<GossipQuote>()));
                        competitiveHints.Add((BuildRegionHint(chosen, regionHintType, random), null, new List<GossipQuote>()));

                        hintedRegions.Add(chosen.Key);
                    }
                }

                var chosenClockTownRegions = 0;
                for (var i = 0; i < numberOfRequiredHints; i++)
                {
                    var regionCounts = importantRegionCounts.AsEnumerable();
                    if (chosenClockTownRegions < maxNumberOfClockTownHints)
                    {
                        regionCounts = regionCounts.Concat(clockTownRegionCounts);
                    }
                    if (!regionCounts.Any())
                    {
                        regionCounts = regionCounts.Concat(clockTownRegionCounts);
                    }
                    regionCounts = regionCounts.Where(kvp => !hintedRegions.Contains(kvp.Key));
                    if (regionCounts.Any())
                    {
                        var chosen = regionCounts.ToList().Random(random);
                        var allowedGossipQuotes = chosen.Value
                            .Select(io => gossipStoneRequirements.Where(kvp => kvp.Value?.Contains(io.locationForImportance) == false).Select(kvp => kvp.Key))
                            .Aggregate((list1, list2) => list1.Intersect(list2))
                            .ToList();
                        competitiveHints.Add((BuildRegionHint(chosen, RegionHintType.SomeRequired, random), null, allowedGossipQuotes));
                        competitiveHints.Add((BuildRegionHint(chosen, RegionHintType.SomeRequired, random), null, allowedGossipQuotes));
                        if (clockTownRegionCounts.Remove(chosen.Key))
                        {
                            chosenClockTownRegions++;
                        }
                        else
                        {
                            importantRegionCounts.Remove(chosen.Key);
                        }

                        hintedRegions.Add(chosen.Key);
                    }
                }

                unusedItems.Clear();
            }

            List<MessageEntry> finalHints = new List<MessageEntry>();

            void addHint(GossipQuote gossipQuote, string message)
            {
                var header = MessageHeader.ToArray();

                if (gossipQuote.IsGaroHint())
                {
                    header[0] = 0;
                    header[1] = 1;
                    message = message.Replace("\xBF", "\x19\xBF");
                }

                finalHints.Add(new MessageEntry()
                {
                    Id = (ushort)gossipQuote,
                    Message = message,
                    Header = header
                });
            }

            while (competitiveHints.Any(ch => ch.allowedGossipQuotes.Count > 0))
            {
                var competitiveHint = competitiveHints
                    .Where(ch => ch.allowedGossipQuotes.Count > 0)
                    .OrderBy(ch => ch.allowedGossipQuotes.Count)
                    .ThenBy(ch => random.Next())
                    .First();

                var gossipQuote = competitiveHint.allowedGossipQuotes.Random(random);
                var clearHintsEnabled = gossipQuote.IsGaroHint() ? randomizedResult.Settings.ClearGaroHints : randomizedResult.Settings.ClearHints;
                addHint(gossipQuote, clearHintsEnabled && competitiveHint.clearMessage != null ? competitiveHint.clearMessage : competitiveHint.message);
                competitiveHints.Remove(competitiveHint);
                foreach (var ch in competitiveHints)
                {
                    ch.allowedGossipQuotes.Remove(gossipQuote);
                }
            }

            foreach (var gossipQuote in gossipQuotes.OrderBy(gq => random.Next()))
            {
                if (finalHints.Any(me => me.Id == (ushort)gossipQuote))
                {
                    continue;
                }

                string messageText = null;
                var isMoonGossipStone = gossipQuote.IsMoonGossipStone();
                var clearHintsEnabled = gossipQuote.IsGaroHint() ? randomizedResult.Settings.ClearGaroHints : randomizedResult.Settings.ClearHints;
                if (competitiveHints.Any())
                {
                    var competitiveHint = competitiveHints.Random(random);
                    messageText = clearHintsEnabled && competitiveHint.clearMessage != null ? competitiveHint.clearMessage : competitiveHint.message;
                    competitiveHints.Remove(competitiveHint);
                }

                if (messageText == null)
                {
                    var restrictionAttributes = gossipQuote.GetAttributes<GossipRestrictAttribute>().ToList();
                    ItemObject item = null;
                    var forceClear = false;
                    while (item == null)
                    {
                        if (restrictionAttributes.Any() && hintStyle == GossipHintStyle.Relevant)
                        {
                            var chosen = restrictionAttributes.Random(random);
                            var candidateItem = chosen.Type == GossipRestrictAttribute.RestrictionType.Item
                                ? randomizedResult.ItemList.Single(io => io.ID == (int)chosen.Item)
                                : randomizedResult.ItemList.Single(io => io.NewLocation == chosen.Item);
                            if (unusedItems.Contains(candidateItem))
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
                            if (hintStyle == GossipHintStyle.Competitive)
                            {
                                item = unusedItems.FirstOrDefault(io => unusedItems.Count(x => x.ID == io.ID) == 1);
                                if (item == null)
                                {
                                    item = unusedItems.Random(random);
                                }
                            }
                            else
                            {
                                item = unusedItems.Random(random);
                                if (ItemUtils.IsJunk(item.Item) && (clearHintsEnabled || random.Next(8) != 0))
                                {
                                    item = null;
                                }
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
                        (var hint, var clearHint, var combined) = BuildItemHint(
                            item,
                            randomizedResult,
                            hintStyle,
                            itemsToCombineWith,
                            hintableItems,
                            null,
                            random
                            );
                        messageText = hint;
                        if (clearHint != null && clearHintsEnabled)
                        {
                            messageText = clearHint;
                        }
                    }
                }

                if (messageText == null)
                {
                    messageText = Gossip.JunkMessages.Random(random);
                }

                addHint(gossipQuote, messageText);
            }

            return finalHints;
        }

        private static (string message, string clearMessage, List<ItemObject> combined) BuildItemHint(ItemObject item, RandomizedResult randomizedResult,
            GossipHintStyle hintStyle, List<ItemObject> itemsToCombineWith, List<ItemObject> hintableItems, Func<ItemObject, bool> shouldIndicateImportance, Random random)
        {
            ushort soundEffectId = 0x690C; // grandma curious
            var itemNames = new List<string>();
            var locationNames = new List<string>();
            bool hasOrder = item.NewLocation.Value.HasAttribute<GossipCombineOrderAttribute>();
            var combined = new List<ItemObject>();
            combined.Add(item);

            var article = randomizedResult.Settings.ProgressiveUpgrades && item.Item.HasAttribute<ProgressiveAttribute>() ? "a " : GetArticle(item.Item);
            var color = TextCommands.ColorPink;
            var importance = "";
            if (randomizedResult.Settings.HintsIndicateImportance && shouldIndicateImportance?.Invoke(item) == true)
            {
                var locationForImportance = item.Item.MainLocation().HasValue ? item.Item : item.NewLocation.Value;
                var isRequired = ItemUtils.IsRequired(item.Item, locationForImportance, randomizedResult, true);
                if (!ItemUtils.IsLogicallyJunk(item.Item))
                {
                    importance = isRequired ? " (required)" : " (not required)";
                }
                color = isRequired ? TextCommands.ColorYellow : TextCommands.ColorSilver;
            }
            itemNames.Add(article + color + item.Item.ProgressiveUpgradeName(randomizedResult.Settings.ProgressiveUpgrades) + TextCommands.ColorWhite + importance);
            locationNames.Add(item.NewLocation.Value.Location());
            if (hintStyle != GossipHintStyle.Relevant)
            {
                var gossipCombineAttribute = item.NewLocation.Value.GetAttribute<GossipCombineAttribute>();
                combined = itemsToCombineWith.Where(io => gossipCombineAttribute?.OtherItems.Contains(io.NewLocation.Value) == true).ToList();
                if (combined.Any())
                {
                    combined.Add(item);
                    combined = combined.OrderBy(io => io.NewLocation.Value.GetAttribute<GossipCombineOrderAttribute>()?.Order ?? random.Next()).ToList();
                    locationNames.Clear();
                    itemNames.Clear();
                    var combinedName = gossipCombineAttribute.CombinedName;
                    if (!string.IsNullOrWhiteSpace(combinedName))
                    {
                        locationNames.Add(combinedName);
                    }
                    else
                    {
                        locationNames.AddRange(combined.Select(io => io.NewLocation.Value.Location()));
                    }
                    itemNames.AddRange(combined.Select(io =>
                    {
                        article = randomizedResult.Settings.ProgressiveUpgrades && io.Item.HasAttribute<ProgressiveAttribute>() ? "a " : GetArticle(io.Item);
                        color = TextCommands.ColorPink;
                        importance = "";
                        if (randomizedResult.Settings.HintsIndicateImportance && shouldIndicateImportance?.Invoke(io) == true)
                        {
                            var locationForImportance = io.Item.MainLocation().HasValue ? io.Item : io.NewLocation.Value;
                            var isRequired = ItemUtils.IsRequired(io.Item, locationForImportance, randomizedResult, true);
                            if (!ItemUtils.IsLogicallyJunk(io.Item))
                            {
                                importance = isRequired ? " (required)" : " (not required)";
                            }
                            color = isRequired ? TextCommands.ColorYellow : TextCommands.ColorSilver;
                        }
                        return article + color + io.Item.ProgressiveUpgradeName(randomizedResult.Settings.ProgressiveUpgrades) + TextCommands.ColorWhite + importance;
                    }));
                }
                else
                {
                    combined.Add(item);
                }
            }
            string clearMessage = null;
            if (itemNames.Any() && locationNames.Any())
            {
                clearMessage = BuildGossipQuote(soundEffectId, locationNames, itemNames, hasOrder, random);
            }

            itemNames.Clear();
            locationNames.Clear();
            if (item.Mimic != null)
            {
                // If item has a mimic and not using clear hints, always use a fake hint.
                soundEffectId = 0x690A; // grandma laugh
                itemNames.Add(item.Mimic.Item.ItemHints().Random(random));
                locationNames.Add(item.NewLocation.Value.LocationHints().Random(random));
            }
            else if (hintStyle != GossipHintStyle.Random || random.Next(100) >= 5) // 5% chance of fake/junk hint if it's not a moon gossip stone or competitive style
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
                    locationNames.Add(hintableItems.Random(random).NewLocation.Value.LocationHints().Random(random));
                }
            }
            if (itemNames.Any())
            {
                itemNames[0] = $"{TextCommands.ColorPink}{itemNames[0]}{TextCommands.ColorWhite}";
            }

            string message = null;
            if (itemNames.Any() && locationNames.Any())
            {
                message = BuildGossipQuote(soundEffectId, locationNames, itemNames, hasOrder, random);
                //return (BuildGossipQuote(soundEffectId, locationNames, itemNames, hasOrder, random), combined);
            }

            if (message != null || clearMessage != null)
            {
                return (message, clearMessage, combined);
            }

            return (null, null, null);
        }

        private enum RegionHintType
        {
            NoneRequired,
            SomeRequired,
            OnlyImportantSong,
        }

        private static string BuildRegionHint(KeyValuePair<Region, List<(ItemObject, Item)>> regionInfo, RegionHintType regionHintType, Random random)
        {
            var region = regionInfo.Key;
            //var numberOfRequiredItems = regionInfo.Value.Count;

            ushort soundEffectId = 0x690C; // grandma curious
            string start = Gossip.MessageStartSentences.Random(random);

            string sfx = $"{(char)((soundEffectId >> 8) & 0xFF)}{(char)(soundEffectId & 0xFF)}";
            var locationMessage = region.Name();
            var mid = "is";
            var (itemMessage, color) = regionHintType switch
            {
                RegionHintType.NoneRequired => ("a foolish choice", TextCommands.ColorSilver),
                RegionHintType.SomeRequired => ("on the Way of the Hero", TextCommands.ColorYellow),
                RegionHintType.OnlyImportantSong => ("foolish except for its song", TextCommands.ColorOrange),
                _ => throw new ArgumentException("Invalid argument.", nameof(regionHintType))
            };

            return $"\x1E{sfx}{start} {color}{locationMessage}{TextCommands.ColorWhite} {mid} {itemMessage}...\xBF".Wrap(35, "\x11", "\x10");

            //var mid = "has";
            //return $"\x1E{sfx}{start} {TextCommands.ColorRed}{locationMessage}{TextCommands.ColorWhite} {mid} {color}{NumberToWords(numberOfImportantItems)} important item{(numberOfImportantItems == 1 ? "" : "s")}{TextCommands.ColorWhite}...\xBF".Wrap(35, "\x11");
        }

        private static string BuildGossipQuote(ushort soundEffectId, IEnumerable<string> locationMessages, IEnumerable<string> itemMessages, bool hasOrder, Random random)
        {
            string start = Gossip.MessageStartSentences.Random(random);
            string mid = Gossip.MessageMidSentences.Random(random);

            string sfx = $"{(char)((soundEffectId >> 8) & 0xFF)}{(char)(soundEffectId & 0xFF)}";

            return $"\x1E{sfx}{start} {string.Join(" and ", locationMessages.Select(locationName => $"\x01{locationName}\x00"))} {mid} {string.Join(hasOrder ? " then " : " and ", itemMessages)}...\xBF".Wrap(35, "\x11", "\x10");
        }

        public static string GetArticle(Item item, string indefiniteArticle = null)
        {
            var shopTexts = item.ShopTexts();
            return shopTexts?.IsMultiple == true
                ? ""
                : shopTexts?.IsDefinite == true
                    ? "the "
                    : indefiniteArticle ?? (Regex.IsMatch(item.Name(), "^[aeiou]", RegexOptions.IgnoreCase)
                        ? "an "
                        : "a ");
        }

        public static string GetPronoun(Item item)
        {
            var shopTexts = item.ShopTexts();
            var itemAmount = Regex.Replace(item.Name(), "[^0-9]", "");
            return shopTexts.IsMultiple && !string.IsNullOrWhiteSpace(itemAmount)
                ? "them"
                : "it";
        }

        public static string GetPronounOrAmount(Item item)
        {
            var shopTexts = item.ShopTexts();
            var itemAmount = Regex.Replace(item.Name(), "[^0-9]", "");
            return shopTexts.IsMultiple
                ? string.IsNullOrWhiteSpace(itemAmount)
                    ? " it"
                    : " " + itemAmount
                : shopTexts.IsDefinite
                    ? " it"
                    : " one";
        }

        public static string GetVerb(Item item)
        {
            var shopTexts = item.ShopTexts();
            var itemAmount = Regex.Replace(item.Name(), "[^0-9]", "");
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
