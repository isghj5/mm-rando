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
    public static class TrapUtils
    {
        /// <summary>
        /// Get all junk items which are replaceable by traps.
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
        /// Select random junk items to replace given <see cref="TrapAmount"/> settings.
        /// </summary>
        /// <param name="itemList">Source items</param>
        /// <param name="trapAmount">Settings</param>
        /// <param name="random">Random</param>
        /// <returns>Junk items to replace.</returns>
        public static ItemObject[] SelectJunkItems(ItemList itemList, TrapAmount trapAmount, Random random)
        {
            var onslaught = trapAmount == TrapAmount.Onslaught;
            var allJunk = GetJunkItems(itemList, onslaught);
            if (trapAmount == TrapAmount.None)
            {
                return new ItemObject[0];
            }
            else if (trapAmount == TrapAmount.Normal)
            {
                var amount = allJunk.Length / 15;
                return allJunk.Random(amount, random);
            }
            else if (trapAmount == TrapAmount.Extra)
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
        /// Get chest type override for trap based on appearance setting.
        /// </summary>
        /// <param name="appearance">Trap appearance setting</param>
        /// <param name="random">Random</param>
        /// <returns>Chest type.</returns>
        public static ChestTypeAttribute.ChestType GetTrapChestTypeOverride(TrapAppearance appearance, Random random)
        {
            if (appearance == TrapAppearance.MajorItems)
            {
                return ChestTypeAttribute.ChestType.LargeGold;
            }
            else if (appearance == TrapAppearance.JunkItems)
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
        /// Build set of mimic items from the randomization pool which traps may use.
        /// </summary>
        /// <param name="itemList">Randomized items</param>
        /// <param name="appearance">Trap appearance setting</param>
        /// <param name="isHighlyRestricted">A function that indicates whether or not items may be used as mimic</param>
        /// <returns>Mimic item set.</returns>
        public static HashSet<MimicItem> BuildTrapMimicSet(ItemList itemList, TrapAppearance appearance, Func<Item, bool> isHighlyRestricted)
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
                        if ((appearance == TrapAppearance.MajorItems && chestType == ChestTypeAttribute.ChestType.LargeGold) ||
                            (appearance == TrapAppearance.MajorItems && chestType == ChestTypeAttribute.ChestType.SmallGold) ||
                            (appearance == TrapAppearance.JunkItems && chestType == ChestTypeAttribute.ChestType.SmallWooden) ||
                            (appearance == TrapAppearance.Anything))
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
    }
}
