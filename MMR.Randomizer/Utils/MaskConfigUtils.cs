using MMR.Common.Extensions;
using MMR.Randomizer.Asm;
using MMR.Randomizer.Attributes;
using MMR.Randomizer.Constants;
using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Models;
using MMR.Randomizer.Models.Rom;
using MMR.Randomizer.Models.Settings;
using System.Collections.Generic;
using System.Linq;

namespace MMR.Randomizer.Utils
{
    public static class MaskConfigUtils
    {
        public static byte NpcKafeiDrawMask { get; set; } = 0x05;
        public static bool DonGeroGoronDrawMask { get; set; } = true;
        public static bool PostmanDrawHat { get; set; } = true;
        public static bool DrawMaskOfTruth { get; set; } = true;
        public static bool DrawGaroMask { get; set; } = true;
        public static bool DrawPendantOfMemories { get; set; } = true;

        public static void UpdateMaskConfig(ItemObject itemObject, GetItemEntry newItem, Item item, ushort getItemIndex)
        {
            // catch the Keaton Mask check and set a maskID byte
            if (getItemIndex == 0x80)
            {
                UpdateKeatonMaskConfig(itemObject, newItem, item);
            }
            // catch the Don Gero Mask check and set draw flag value
            if (getItemIndex == 0x88)
            {
                UpdateDonGeroMaskConfig(itemObject, newItem, item);
            }
            // catch the Postman's Hat check and set draw flag value
            if (getItemIndex == 0x84)
            {
                UpdatePostmanHatConfig(itemObject, newItem, item);
            }
            // catch the Mask of Truth check and set draw flag value
            if (getItemIndex == 0x8A)
            {
                UpdateMaskOfTruthConfig(itemObject, newItem, item);
            }
            // catch the Garo's Mask check and set draw flag value
            if (getItemIndex == 0x81)
            {
                UpdateGaroMaskConfig(itemObject, newItem, item);
            }
            if (getItemIndex == 0xAB)
            {
                UpdatePendantOfMemoriesConfig(itemObject, newItem, item);
            }
        }

        public static void UpdateKeatonMaskConfig(ItemObject itemObject, GetItemEntry newItem, Item item)
        {
            ushort itemGet;
            int kafeimaskID;

            if (newItem.ItemGained == 0xB0) //it's a trap, get what it's supposed to mimic
            {
                var itemGetMimic = itemObject.Mimic.Item;
                if (itemGetMimic.ItemCategory() == ItemCategory.Masks)
                {
                    var mimicvalue = itemGetMimic.GetAttribute<StartingItemAttribute>();
                    itemGet = mimicvalue.Value;
                }
                else
                {
                    itemGet = 0x00;
                }
            }
            else
            {
                if (item.ItemCategory() == ItemCategory.Masks)
                {
                    itemGet = newItem.ItemGained;
                }
                else
                {
                    itemGet = 0x00;
                }
            }


            if (itemGet >= 0x36 && itemGet <= 0x49) //non-transform mask itemGained ids map to playermaskID in same order, just offset
            {
                kafeimaskID = itemGet - 0x35;
            }
            else if (itemGet >= 0x32) //transform masks are in a different order
            {
                if (itemGet == 0x32) //deku
                {
                    kafeimaskID = 0x18;
                }
                else if (itemGet == 0x33) //goron
                {
                    kafeimaskID = 0x16;
                }
                else if (itemGet == 0x34) //zora
                {
                    kafeimaskID = 0x17;
                }
                else //fierce deity
                {
                    kafeimaskID = 0x15;
                }
            }
            else //it's not a mask, a value of 0 will tell the asm/c hooks to draw a getItem
            {
                kafeimaskID = 0x00;
            }

            NpcKafeiDrawMask = (byte)kafeimaskID;
        }

        public static void UpdateDonGeroMaskConfig(ItemObject itemObject, GetItemEntry newItem, Item item)
        {
            if (newItem.ItemGained == 0xB0)
            {
                string itemMimicName = itemObject.Mimic.Item.GetAttribute<ItemNameAttribute>()?.Name;
                if (itemMimicName == "Don Gero's Mask")
                {
                    DonGeroGoronDrawMask = true;
                }
                else
                {
                    DonGeroGoronDrawMask = false;
                }
            }
            else
            {
                string newItemName = item.GetAttribute<ItemNameAttribute>()?.Name;
                if (newItemName == "Don Gero's Mask")
                {
                    DonGeroGoronDrawMask = true;
                }
                else
                {
                    DonGeroGoronDrawMask = false;
                }
            }
        }

        public static void UpdatePostmanHatConfig(ItemObject itemObject, GetItemEntry newItem, Item item)
        {
            if (newItem.ItemGained == 0xB0)
            {
                string itemMimicName = itemObject.Mimic.Item.GetAttribute<ItemNameAttribute>()?.Name;
                if (itemMimicName == "Postman's Hat")
                {
                    PostmanDrawHat = true;
                }
                else
                {
                    PostmanDrawHat = false;
                }
            }
            else
            {
                string newItemName = item.GetAttribute<ItemNameAttribute>()?.Name;
                if (newItemName == "Postman's Hat")
                {
                    PostmanDrawHat = true;
                }
                else
                {
                    PostmanDrawHat = false;
                }
            }
        }

        public static void UpdateMaskOfTruthConfig(ItemObject itemObject, GetItemEntry newItem, Item item)
        {
            if (newItem.ItemGained == 0xB0)
            {
                string itemMimicName = itemObject.Mimic.Item.GetAttribute<ItemNameAttribute>()?.Name;
                if (itemMimicName == "Mask of Truth")
                {
                    DrawMaskOfTruth = true;
                }
                else
                {
                    DrawMaskOfTruth = false;
                }
            }
            else
            {
                string newItemName = item.GetAttribute<ItemNameAttribute>()?.Name;
                if (newItemName == "Mask of Truth")
                {
                    DrawMaskOfTruth = true;
                }
                else
                {
                    DrawMaskOfTruth = false;
                }
            }
        }

        public static void UpdateGaroMaskConfig(ItemObject itemObject, GetItemEntry newItem, Item item)
        {
            if (newItem.ItemGained == 0xB0)
            {
                string itemMimicName = itemObject.Mimic.Item.GetAttribute<ItemNameAttribute>()?.Name;
                if (itemMimicName == "Garo's Mask")
                {
                    DrawGaroMask = true;
                }
                else
                {
                    DrawGaroMask = false;
                }
            }
            else
            {
                string newItemName = item.GetAttribute<ItemNameAttribute>()?.Name;
                if (newItemName == "Garo's Mask")
                {
                    DrawGaroMask = true;
                }
                else
                {
                    DrawGaroMask = false;
                }
            }
        }

        public static void UpdatePendantOfMemoriesConfig(ItemObject itemObject, GetItemEntry newItem, Item item)
        {
            if (newItem.ItemGained == 0xB0)
            {
                string itemMimicName = itemObject.Mimic.Item.GetAttribute<ItemNameAttribute>()?.Name;
                if (itemMimicName == "Pendant of Memories")
                {
                    DrawPendantOfMemories = true;
                }
                else
                {
                    DrawPendantOfMemories = false;
                }
            }
            else
            {
                string newItemName = item.GetAttribute<ItemNameAttribute>()?.Name;
                if (newItemName == "Pendant of Memories")
                {
                    DrawPendantOfMemories = true;
                }
                else
                {
                    DrawPendantOfMemories = false;
                }
            }
        }
    }
}
