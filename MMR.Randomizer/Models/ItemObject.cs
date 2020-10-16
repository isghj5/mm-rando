using System.Collections.Generic;
using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Utils;

namespace MMR.Randomizer.Models
{
    public class ItemObject
    {
        public int ID { get; set; }
        public Item Item => (Item)ID; // todo?
        public string Name { get; set; }
        public List<Item> DependsOnItems { get; set; } = new List<Item>();
        public List<List<Item>> Conditionals { get; set; } = new List<List<Item>>();
        public List<Item> CannotRequireItems { get; set; } = new List<Item>();
        public int TimeNeeded { get; set; }
        public int TimeAvailable { get; set; }
        public Item? NewLocation { get; set; }

        public bool IsTrick { get; set; }
        public string TrickTooltip { get; set; }

        public bool IsRandomized { get; set; }

        /// <summary>
        /// Name override used in spoiler log.
        /// </summary>
        public string NameOverride { get; set; }

        /// <summary>
        /// Item which is being mimiced, used by ice traps.
        /// </summary>
        public MimicItem Mimic { get; set; }

        /// <summary>
        /// Item to display in shops and prompts.
        /// </summary>
        public Item DisplayItem => this.Mimic?.Item ?? this.Item;

        /// <summary>
        /// Item name to display in shops and prompts.
        /// </summary>
        public string DisplayName => this.Mimic?.Name ?? this.Item.Name();

        #region MessageUtils Wrapper Methods

        public string GetArticle(string indefiniteArticle = null)
        {
            return MessageUtils.GetArticle(this.DisplayItem, indefiniteArticle, this.DisplayName);
        }

        public string GetPlural()
        {
            return MessageUtils.GetPlural(this.DisplayName);
        }

        public string GetPronoun()
        {
            return MessageUtils.GetPronoun(this.DisplayItem, this.DisplayName);
        }

        public string GetPronounOrAmount(string it = " It")
        {
            return MessageUtils.GetPronounOrAmount(this.DisplayItem, it, this.DisplayName);
        }

        public string GetVerb()
        {
            return MessageUtils.GetVerb(this.DisplayItem, this.DisplayName);
        }

        public string GetFor()
        {
            return MessageUtils.GetFor(this.DisplayItem);
        }

        public string GetAlternateName()
        {
            return MessageUtils.GetAlternateName(this.DisplayName);
        }

        #endregion
    }
}
