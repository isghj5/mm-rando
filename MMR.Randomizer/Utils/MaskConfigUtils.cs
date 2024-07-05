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

        public static void UpdateMaskConfig(Item location, Item displayItem)
        {
            switch (location)
            {
                case Item.MaskKeaton:
                    UpdateKeatonMaskConfig(displayItem);
                    break;
                case Item.MaskDonGero:
                    UpdateDonGeroMaskConfig(displayItem);
                    break;
                case Item.MaskPostmanHat:
                    UpdatePostmanHatConfig(displayItem);
                    break;
                case Item.MaskTruth:
                    UpdateMaskOfTruthConfig(displayItem);
                    break;
                case Item.MaskGaro:
                    UpdateGaroMaskConfig(displayItem);
                    break;
                case Item.TradeItemPendant:
                    UpdatePendantOfMemoriesConfig(displayItem);
                    break;
            }
        }

        public static void UpdateKeatonMaskConfig(Item displayItem)
        {
            byte itemGet = displayItem.ItemCategory() == ItemCategory.Masks
                ? displayItem.GetAttribute<StartingItemAttribute>().Value
                : (byte)0x00;

            byte kafeimaskID;

            if (itemGet >= 0x36 && itemGet <= 0x49) //non-transform mask itemGained ids map to playermaskID in same order, just offset
            {
                kafeimaskID = (byte)(itemGet - 0x35);
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

            NpcKafeiDrawMask = kafeimaskID;
        }

        public static void UpdateDonGeroMaskConfig(Item displayItem)
        {
            DonGeroGoronDrawMask = displayItem == Item.MaskDonGero;
        }

        public static void UpdatePostmanHatConfig(Item displayItem)
        {
            PostmanDrawHat = displayItem == Item.MaskPostmanHat;
        }

        public static void UpdateMaskOfTruthConfig(Item displayItem)
        {
            DrawMaskOfTruth = displayItem == Item.MaskTruth;
        }

        public static void UpdateGaroMaskConfig(Item displayItem)
        {
            DrawGaroMask = displayItem == Item.MaskGaro;
        }

        public static void UpdatePendantOfMemoriesConfig(Item displayItem)
        {
            DrawPendantOfMemories = displayItem == Item.TradeItemPendant;
        }
    }
}
