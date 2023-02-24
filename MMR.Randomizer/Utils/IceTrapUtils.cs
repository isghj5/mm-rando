using MMR.Common.Extensions;
using MMR.Randomizer.Attributes;
using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Models;
using MMR.Randomizer.Models.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MMR.Randomizer.Utils
{
    public static class IceTrapUtils
    {
        /// <summary>
        /// Get all junk items which are replaceable by ice traps.
        /// </summary>
        /// <param name="itemList">Source items</param>
        /// <param name="onslaught">Whether or not non-randomized junk items should be selected as well</param>
        /// <returns>Replaceable junk items.</returns>
        public static ItemObject[] GetJunkItems(ItemList itemList, bool onslaught = false)
        {
            // Use JunkItem list, but exclude Heart Pieces and Heart Containers.
            return itemList.FindAll(x => {
                return (x.IsRandomized || onslaught)
                && x.NewLocation != null
                && ItemUtils.IsJunk(x.Item)
                && !x.Item.IsFake()
                && !ItemUtils.IsStartingLocation(x.NewLocation.Value);
            }).ToArray();
        }

        /// <summary>
        /// Select random junk items to replace given <see cref="IceTraps"/> settings.
        /// </summary>
        /// <param name="itemList">Source items</param>
        /// <param name="iceTraps">Settings</param>
        /// <param name="random">Random</param>
        /// <returns>Junk items to replace.</returns>
        public static ItemObject[] SelectJunkItems(ItemList itemList, IceTraps iceTraps, Random random)
        {
            var onslaught = iceTraps == IceTraps.Onslaught;
            var allJunk = GetJunkItems(itemList, onslaught);
            if (iceTraps == IceTraps.None)
            {
                return new ItemObject[0];
            }
            else if (iceTraps == IceTraps.Normal)
            {
                var amount = allJunk.Length / 15;
                return allJunk.Random(amount, random);
            }
            else if (iceTraps == IceTraps.Extra)
            {
                var amount = allJunk.Length / 7;
                return allJunk.Random(amount, random);
            }
            else
            {
                return allJunk;
            }
        }

        /// <summary>
        /// Get chest type override for ice trap based on appearance setting.
        /// </summary>
        /// <param name="appearance">Ice trap appearance setting</param>
        /// <param name="random">Random</param>
        /// <returns>Chest type.</returns>
        public static ChestTypeAttribute.ChestType GetIceTrapChestTypeOverride(IceTrapAppearance appearance, Random random)
        {
            if (appearance == IceTrapAppearance.MajorItems)
            {
                return ChestTypeAttribute.ChestType.LargeGold;
            }
            else if (appearance == IceTrapAppearance.JunkItems)
            {
                return ChestTypeAttribute.ChestType.SmallWooden;
            }
            else
            {
                var choices = new ChestTypeAttribute.ChestType[]
                {
                    ChestTypeAttribute.ChestType.LargeGold,
                    ChestTypeAttribute.ChestType.SmallGold,
                    ChestTypeAttribute.ChestType.SmallWooden,
                };
                return choices[random.Next(choices.Length)];
            }
        }

        /// <summary>
        /// Build set of mimic items from the randomization pool which ice traps may use.
        /// </summary>
        /// <param name="itemList">Randomized items</param>
        /// <param name="appearance">Ice trap appearance setting</param>
        /// <param name="isHighlyRestricted">A function that indicates whether or not items may be used as mimic</param>
        /// <returns>Mimic item set.</returns>
        public static HashSet<MimicItem> BuildIceTrapMimicSet(ItemList itemList, IceTrapAppearance appearance, Func<Item, bool> isHighlyRestricted)
        {
            var mimics = new HashSet<MimicItem>();
            bool allowNonRandomized = false;
            do
            {
                foreach (var itemObj in itemList.Where(io => allowNonRandomized || io.IsRandomized))
                {
                    var index = itemObj.Item.GetItemIndex();
                    if (index.HasValue && !isHighlyRestricted(itemObj.Item))
                    {
                        var chestType = itemObj.Item.ChestType();
                        if ((appearance == IceTrapAppearance.MajorItems && chestType == ChestTypeAttribute.ChestType.LargeGold) ||
                            (appearance == IceTrapAppearance.MajorItems && chestType == ChestTypeAttribute.ChestType.SmallGold) ||
                            (appearance == IceTrapAppearance.JunkItems && chestType == ChestTypeAttribute.ChestType.SmallWooden) ||
                            (appearance == IceTrapAppearance.Anything))
                        {
                            // Add mimic item to set.
                            var mimicItem = new MimicItem(itemObj.Item);
                            mimics.Add(mimicItem);
                        }
                    }
                }
                if (allowNonRandomized && mimics.Count == 0)
                {
                    throw new Exception("Failed to build a Mimic set.");
                }
                allowNonRandomized = true;
            } while (mimics.Count == 0);
            return mimics;
        }

        /// <summary>
        /// How many ice traps are set to be bomb traps instead.
        /// </summary>
        public static int GetBombTrapAmount(BombTraps bombtraps, int trapitemsamount)
        {
            if (bombtraps == BombTraps.None)
            {
                return 0;
            }
            else if (bombtraps == BombTraps.Few)
            {
                return trapitemsamount / 5;
            }
            else if (bombtraps == BombTraps.Half)
            {
                return trapitemsamount / 2;
            }
            else if (bombtraps == BombTraps.Most)
            {
                return (int)(trapitemsamount * .75);
            }
            else
            {
                return trapitemsamount;
            }
        }
    }
}
