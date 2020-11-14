using MMR.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MMR.Randomizer.Utils
{
    public static class FakeNameUtils
    {
        /// <summary>
        /// Replacement full name mapping for fake item names.
        /// </summary>
        static readonly Dictionary<string, string> Full = CreateFullReplacements();

        /// <summary>
        /// Replacement word mapping for fake item names.
        /// </summary>
        static readonly Dictionary<string, string> Words = CreateWordReplacements();

        /// <summary>
        /// Get replacement full name mapping.
        /// </summary>
        /// <returns>Mapping.</returns>
        public static Dictionary<string, string> CreateFullReplacements()
        {
            var result = new Dictionary<string, string>();
            result.Add("Captain's Hat", "Skull Mask");
            result.Add("Don Gero's Mask", "Frog Choir Mask");
            result.Add("Double Defense", "Nayru's Love");
            result.Add("Fire Arrows", "Din's Fire");
            result.Add("Goron Mask", "Goron Tunic");
            result.Add("Great Fairy's Mask", "Gerudo Mask");
            result.Add("Heart Container", "Container of Heart");
            result.Add("Piece of Heart", "Heart Piece");
            result.Add("Song of Soaring", "Farore's Wind");
            result.Add("Zora Mask", "Zora Tunic");
            return result;
        }

        /// <summary>
        /// Get replacement word mapping.
        /// </summary>
        /// <returns>Mapping.</returns>
        public static Dictionary<string, string> CreateWordReplacements()
        {
            var result = new Dictionary<string, string>();
            result.Add("Attack", "Slash");
            result.Add("Awakening", "Ascending,Waking");
            result.Add("Bay", "Bae,Day");
            result.Add("Bean", "Plant,Seed");
            result.Add("Big", "Bigger,Large");
            result.Add("Biggest", "Best,Largest");
            result.Add("Blast", "Blasting,Bomb,Bomber");
            result.Add("Bomber", "Boomer");
            result.Add("Bomb", "Boom,Bomb-omb");
            result.Add("Boss", "Big,Master");
            result.Add("Bossa", "Star,Super,Tossa");
            result.Add("Bremen", "Brahmin,Eagle,Falco,Kass");
            result.Add("Bunny", "Funny,Hare,Rabbit");
            result.Add("Captain", "Keeta");
            result.Add("Circus", "Troupe");
            result.Add("Container", "Contaminator");
            result.Add("Couple's", "Divorce");
            result.Add("Deed", "Dead");
            result.Add("Deity", "Daemon,Diety,God");
            result.Add("Deku", "Scrub,Tree");
            result.Add("Don", "Lon");
            result.Add("Double", "Extra");
            result.Add("Dust", "Crust,Rust");
            result.Add("Elegy", "Melody");
            result.Add("Epona's", "Horse");
            result.Add("Emptiness", "Clones,Eligibility");
            result.Add("Empty", "Emptied,Full");
            result.Add("Fairy", "Pixie,Spirit");
            result.Add("Fierce", "Feared,Fearsome,Feisty");
            result.Add("Fire", "Lava,Flame");
            result.Add("Garo", "Gero,Ninja");
            result.Add("Gero", "Garo");
            result.Add("Giant's", "Colossus");
            result.Add("Gibdo", "Redead,Spooky");
            result.Add("Gilded", "Master");
            result.Add("Goht", "Goat");
            result.Add("Gold", "Golden,Shiny");
            result.Add("Goron", "Darmani,Darunia,Ganon");
            result.Add("Great", "Grate");
            result.Add("Gyorg", "Fish,Greg");
            result.Add("Hat", "Cap,Hood,Mask");
            result.Add("Healing", "Feeling,Sealing");
            result.Add("Heart", "Hate,Life");
            result.Add("Hero's", "Fairy,Hylian,Zero's");
            result.Add("Hood", "Hat,Mask");
            result.Add("Hookshot", "Longshot");
            result.Add("Ice", "Freeze,Icy");
            result.Add("Kafei", "Cafe,Kafie");
            result.Add("Kamaro", "Dancer,Karamo");
            result.Add("Keaton", "Keeton,Pikachu");
            result.Add("Keg", "Bomb,Explosive");
            result.Add("Large", "Big,Larger");
            result.Add("Largest", "Best,Biggest");
            result.Add("Letter", "Message,Parcel");
            result.Add("Light", "Lightning,Lemon");
            result.Add("Magic", "Magical");
            result.Add("Mama", "Mom,Mother");
            result.Add("Mask", "Hat,Hood");
            result.Add("Memories", "Dreams,Memoires");
            result.Add("Night", "Day");
            result.Add("Ocean", "Water,River");
            result.Add("Odolwa", "Odalwa");
            result.Add("Order", "Giants");
            result.Add("Pendant", "Jewel,Locket");
            result.Add("Pictobox", "Camerabox,Picto Box,Picturebox");
            result.Add("Piece", "Part,Peace");
            result.Add("Postman's", "Delivery,Mailman's");
            result.Add("Potion", "Elixir,Medicine,Tonic");
            result.Add("Powder", "Power,Super");
            result.Add("Power", "Energy");
            result.Add("Purple", "Violet");
            result.Add("Remains", "Fragments,Remnants");
            result.Add("Romani", "Roman,Moroni");
            result.Add("Room", "Door,Inn");
            result.Add("Scents", "Smells");
            result.Add("Shield", "Defense");
            result.Add("Silver", "Large");
            result.Add("Skulltula", "Spider");
            result.Add("Small", "Dungeon");
            result.Add("Snowhead", "Snowdead");
            result.Add("Soaring", "Flying,Warping");
            result.Add("Sonata", "Ballad,Santa");
            result.Add("Song", "Sound");
            result.Add("Spin", "Spain,Spinning");
            result.Add("Spirit", "Essence,Soul");
            result.Add("Stick", "Branch");
            result.Add("Stone", "Rock,Stoned");
            result.Add("Storms", "Rain,Thunder");
            result.Add("Stray", "Lost");
            result.Add("Swamp", "Forest,Woods");
            result.Add("Sword", "Blade");
            result.Add("Tear", "Jewel,Stone");
            result.Add("Temple", "Dungeon");
            result.Add("Title", "Tidal");
            result.Add("Town", "City,Village");
            result.Add("Truth", "Lying,Sight");
            result.Add("Twinmold", "Twinfold,Twinworm");
            result.Add("Wave", "Cave,Wade");
            result.Add("Woodfall", "Woodfail,Woodtall");
            result.Add("Zora", "Mikau,Ruto");
            result.Add("to", "for");
            return result;
        }

        /// <summary>
        /// Create a fake shop name for an item.
        /// </summary>
        /// <param name="name">Existing name</param>
        /// <param name="random">Random</param>
        /// <returns>Fake name.</returns>
        public static string CreateFakeName(string name, Random random)
        {
            var roll = random.Next(100);
            if (roll <= 10)
            {
                var result = ReplaceFull(name, random);
                if (result != null)
                {
                    return result;
                }
            }
            else if (roll <= 70)
            {
                var result = ReplaceWord(name, random);
                if (result != null)
                {
                    return result;
                }
            }

            return ReplaceVowels(name, random);
        }

        /// <summary>
        /// Attempt to create a fake name by replacing the existing name.
        /// </summary>
        /// <param name="name">Existing name</param>
        /// <param name="random">Random</param>
        /// <returns>Fake name if successful, or null if not successful.</returns>
        public static string ReplaceFull(string name, Random random)
        {
            string replacements;
            if (Full.TryGetValue(name, out replacements))
            {
                return replacements.Split(',').Random(random);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Attempt to create a fake name by replacing a word within the existing name.
        /// </summary>
        /// <param name="name">Existing name</param>
        /// <param name="random">Random</param>
        /// <returns>Fake name if successful, or null if not successful.</returns>
        public static string ReplaceWord(string name, Random random)
        {
            var subs = name.Split(' ');
            var index = random.Next(subs.Length);
            foreach (var kvp in Words)
            {
                var word = subs[index];
                if (word.StartsWith(kvp.Key))
                {
                    var allReplacements = kvp.Value.Split(',');
                    var replacement = allReplacements.Random(random);
                    word = word.Replace(kvp.Key, replacement);
                    subs[index] = word;
                    return string.Join(" ", subs);
                }
            }
            return null;
        }

        /// <summary>
        /// Create a fake shop name for an item by replacing several vowels.
        /// </summary>
        /// <param name="name">Existing name</param>
        /// <param name="random">Random</param>
        /// <returns>Fake name.</returns>
        /// <remarks>Basically a port of functionality found in OoTR for creating fake names.</remarks>
        public static string ReplaceVowels(string name, Random random, int minimum = 2)
        {
            var vowels = "aeiou".ToCharArray();
            var array = name.ToCharArray();
            var mapped = array.Select((chr, idx) => (chr, idx)).ToList();
            var indexes = (from x in mapped where vowels.Contains(x.chr) select x.idx).ToArray();
            var samples = indexes.Random(Math.Min(indexes.Length, minimum), random);
            foreach (var idx in samples)
            {
                // Replace existing vowel with random new vowel.
                var oldVowel = array[idx];
                var otherVowels = new string(vowels).Replace(oldVowel.ToString(), "").ToCharArray();
                var newVowel = otherVowels.Random(random);
                array[idx] = newVowel;
            }

            var newName = new string(array);
            var censor = new string[] { "cum", "cunt", "dike", "penis", "puss", "shit" };
            if (censor.Contains(newName))
            {
                return ReplaceVowels(name, random);
            }
            return newName;
        }
    }
}
