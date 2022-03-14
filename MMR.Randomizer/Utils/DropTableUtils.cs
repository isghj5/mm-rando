using MMR.Randomizer.Constants;
using MMR.Randomizer.Models;
using MMR.Randomizer.Models.Settings;
using System;

namespace MMR.Randomizer.Utils
{
    public enum Item00Type
    {
        /* 0x00 */ ITEM00_RUPEE_GREEN,
        /* 0x01 */ ITEM00_RUPEE_BLUE,
        /* 0x02 */ ITEM00_RUPEE_RED,
        /* 0x03 */ ITEM00_HEART,
        /* 0x04 */ ITEM00_BOMBS_A,
        /* 0x05 */ ITEM00_ARROWS_10,
        /* 0x06 */ ITEM00_HEART_PIECE,
        /* 0x07 */ ITEM00_HEART_CONTAINER,
        /* 0x08 */ ITEM00_ARROWS_30,
        /* 0x09 */ ITEM00_ARROWS_40,
        /* 0x0A */ ITEM00_ARROWS_50,
        /* 0x0B */ ITEM00_BOMBS_B,
        /* 0x0C */ ITEM00_NUTS_1,
        /* 0x0D */ ITEM00_STICK,
        /* 0x0E */ ITEM00_MAGIC_LARGE,
        /* 0x0F */ ITEM00_MAGIC_SMALL,
        /* 0x10 */ ITEM00_MASK,
        /* 0x11 */ ITEM00_SMALL_KEY,
        /* 0x12 */ ITEM00_FLEXIBLE,
        /* 0x13 */ ITEM00_RUPEE_HUGE,
        /* 0x14 */ ITEM00_RUPEE_PURPLE,
        /* 0x15 */ ITEM00_3_HEARTS,
        /* 0x16 */ ITEM00_SHIELD_HERO,
        /* 0x17 */ ITEM00_NUTS_10,
        /* 0x18 */ ITEM00_NOTHING,
        /* 0x19 */ ITEM00_BOMBS_0,
        /* 0x1A */ ITEM00_BIG_FAIRY,
        /* 0x1B */ ITEM00_MAP,
        /* 0x1C */ ITEM00_COMPASS,
        /* 0x1D */ ITEM00_MUSHROOM_CLOUD,

        /* 0xFF */ ITEM00_NO_DROP = -1
    } // Item00Type


    public enum DropTable
    {
        DROPTABLE_TERMINA_FIELD = 0,
        DROPTABLE_UNK1 = 0x1,
        DROPTABLE_SNOWBALLS = 0x2,
        DROPTABLE_UNUSED_3 = 0x3, // mxz thinks this is unused
        DROPTABLE_IKANA_ROCKS = 0x4,
        DROPTABLE_EMPTY_5 = 0x5,  // mxz thinks this is unused
        DROPTABLE_UNK6 = 0x6,
        DROPTABLE_HEARTS_ONLY = 0x7,
        DROPTABLE_UNK8 = 0x8,
        DROPTABLE_DEAD_CREATURES = 0x9,
        DROPTABLE_UNKA = 0xA,
        DROPTABLE_LENS_CAVE = 0xB,
        DROPTABLE_MAGIC_ONLY_LESS = 0xC,
        DROPTABLE_MAGIC_ONLY_FAMOS = 0xD,
        DROPTABLE_UNKE = 0xE,
        DROPTABLE_UNKF = 0xF,
        DROPTABLE_REAL_BOMBCHU = 0x10,
    } // DropTable

    /*****************************
    ***  Drop table Reference  ***  
    ******************************
     0  00 00 01 --  -- 10 10 --  -- 04 0F --  -- -- 03 12  termina field and grotto bushes
     1  00 -- -- --  -- 10 10 --  -- -- 0F --  03 03 03 12  couple of these bushes in swamp, but not the whole ring
     2  00 00 -- --  -- 10 10 --  -- 04 0F 0E  0E 03 03 12  ?
     3  00 -- 01 02  -- 10 10 --  -- 04 0F --  -- 03 03 12  according to mxzrules this is completley unused... can we reuse?
     4  10 10 10 10  -- -- -- --  -- -- -- --  -- -- -- --  arrows only
     5  -- -- -- --  -- -- -- --  -- -- -- --  -- -- -- --  according to mxzrules this is completley unused... can we reuse?
     6  -- -- -- --  03 03 03 03  03 03 03 03  03 03 03 12  hearts and flexible
     7  03 03 03 03  03 03 03 03  03 03 03 03  03 03 03 03  all recovery hearts
     8  00 00 00 01  01 -- -- --  -- -- -- --  -- -- -- --  pocket change only
     9  01 01 01 01  01 01 01 01  01 02 02 02  02 02 02 02  money only, but more money, swamp archery bushes?
     a  05 05 05 05  05 05 05 05  05 05 08 08  0F 0F 0E 0E  arrows magic
     b  -- -- -- --  -- -- -- 04  04 04 04 04  04 04 04 04  bombs, probably the bushes in lens grotto
     c  -- -- -- --  -- -- -- --  0F 0F 0F 0F  0F 0F 0E 0E  magic
     d  0F 0F 0F 0F  0F 0F 0F 0F  0F 0F 0E 0E  0E 0E 0E 0E  magic
     e  -- -- -- --  0C 0C -- 05  05 05 0D 0D  -- 03 03 12  sticks nuts and arrows, flexible
     f  00 01 01 02  -- 05 05 08  04 -- 0D 0F  0E 03 03 12  stick? nah
    10  00 03 03 0F  10 -- -- --  -- -- -- --  -- -- -- --  sml magic , recovery heart, arrows, green rupee
    ****************************** */

    class DropTableUtils
    {
        public const int CodeFID = 31;
        public const int DropTableVROMAddress = 0xC444B4;
        public const int DropTableAddress = 0x1084B4;

        public static int SlotAddr(DropTable tableIndex, int slotIndex)
        {
            return DropTableAddress + ((int)tableIndex * 0x10) + slotIndex;
        }

        public static void ChangeDrop(Item00Type dropType, int replacementSlot = DropTableAddress, byte amount = 1)
        {
            RomData.MMFileList[CodeFID].Data[replacementSlot] = (byte) dropType;
            // how many items are dropped is the table that follows, aligns exactly with 0x110
            RomData.MMFileList[CodeFID].Data[replacementSlot + 0x110] = amount;
        }

        public static void WriteNutsAndSticksDrops(RandomizedResult randomizedSettings)
        {
            /// adds deku sticks and deku nuts as additional drops to the drop tables, very useful in enemizer
            /// when an actor drops an item, they roll a 16 side die, sometimes hardcode overrides in special cases (fairy)
            ///   all of the slots replaced here with sticks and nuts were empty in vanilla
            /// image guide from mzxrules of the drop tables in vanilla
            /// https://pbs.twimg.com/media/Dct7fa6X4AEeYpv?format=jpg&name=large 

            if (randomizedSettings.Settings.NutandStickDrops == NutAndStickDrops.Default)
            {
                return;
            }

            int bushCount = (int)randomizedSettings.Settings.NutandStickDrops;
            byte nutCount = randomizedSettings.Settings.NutandStickDrops == NutAndStickDrops.Light ? (byte)0x1 : (byte)bushCount;
            byte stickCount = (byte)Math.Max((int)randomizedSettings.Settings.NutandStickDrops - 2, 1);

            // termina field bushes 1/16 drop table entry
            ChangeDrop(Item00Type.ITEM00_NUTS_1, SlotAddr(DropTable.DROPTABLE_TERMINA_FIELD, 0x3), nutCount);
            ChangeDrop(Item00Type.ITEM00_STICK, SlotAddr(DropTable.DROPTABLE_TERMINA_FIELD, 0xB), stickCount);

            if (bushCount >= 2) // medium and higher
            {
                // another slot in the TF grass drop table
                ChangeDrop(Item00Type.ITEM00_NUTS_1, SlotAddr(DropTable.DROPTABLE_TERMINA_FIELD, 0x4), nutCount);
                ChangeDrop(Item00Type.ITEM00_STICK, SlotAddr(DropTable.DROPTABLE_TERMINA_FIELD, 0xC), stickCount);
            }
            if (bushCount >= 3) // extra and higher
            {
                // another slot in the TF grass drop table
                ChangeDrop(Item00Type.ITEM00_NUTS_1, SlotAddr(DropTable.DROPTABLE_TERMINA_FIELD, 0x8), nutCount);
                ChangeDrop(Item00Type.ITEM00_STICK, SlotAddr(DropTable.DROPTABLE_TERMINA_FIELD, 0xD), stickCount);

                // if extra and higher, add some to non termina field droplists
                ChangeDrop(Item00Type.ITEM00_NUTS_1, SlotAddr(DropTable.DROPTABLE_UNK1, 0x1), nutCount); // stalchild and south swamp
                ChangeDrop(Item00Type.ITEM00_STICK, SlotAddr(DropTable.DROPTABLE_UNK1, 0x2), stickCount);

                ChangeDrop(Item00Type.ITEM00_NUTS_1, SlotAddr(DropTable.DROPTABLE_LENS_CAVE, 0x0), nutCount); 
                ChangeDrop(Item00Type.ITEM00_STICK, SlotAddr(DropTable.DROPTABLE_LENS_CAVE, 0x1), stickCount);
            }
            if (bushCount >= 4) // mayhem
            {
                // nuts and sticks in weird drop tables too for mayhem
                ChangeDrop(Item00Type.ITEM00_NUTS_1, SlotAddr(DropTable.DROPTABLE_IKANA_ROCKS, 0x4), nutCount);
                ChangeDrop(Item00Type.ITEM00_STICK, SlotAddr(DropTable.DROPTABLE_IKANA_ROCKS, 0x5), stickCount);

                ChangeDrop(Item00Type.ITEM00_NUTS_1, SlotAddr(DropTable.DROPTABLE_SNOWBALLS, 0x2), nutCount);
                ChangeDrop(Item00Type.ITEM00_STICK, SlotAddr(DropTable.DROPTABLE_SNOWBALLS, 0x3), stickCount);

                ChangeDrop(Item00Type.ITEM00_NUTS_1, SlotAddr(DropTable.DROPTABLE_REAL_BOMBCHU, 0x6), nutCount);
                ChangeDrop(Item00Type.ITEM00_STICK, SlotAddr(DropTable.DROPTABLE_REAL_BOMBCHU, 0x7), stickCount);
            }
        }

        public static void WriteBombchuDrops(RandomizedResult randomizedSettings)
        {
            // todo: add a settings param to control it

            /// adds a 1/16 chance to find 10 bombchus in
            ///   1) termina field grass
            ///   2) lens cave
            ///   and a few other drops

            const Item00Type ITEM00_BOMBCHU = Item00Type.ITEM00_SMALL_KEY; // used to be small key

            EnableBombchuDrops();

            ChangeDrop(ITEM00_BOMBCHU, SlotAddr(DropTable.DROPTABLE_TERMINA_FIELD, 0xD));

            ChangeDrop(ITEM00_BOMBCHU, SlotAddr(DropTable.DROPTABLE_LENS_CAVE, 0x2));
            ChangeDrop(ITEM00_BOMBCHU, SlotAddr(DropTable.DROPTABLE_LENS_CAVE, 0x3));
            ChangeDrop(ITEM00_BOMBCHU, SlotAddr(DropTable.DROPTABLE_LENS_CAVE, 0x4));

            ChangeDrop(ITEM00_BOMBCHU, SlotAddr(DropTable.DROPTABLE_MAGIC_ONLY_FAMOS, 0x0));
            ChangeDrop(ITEM00_BOMBCHU, SlotAddr(DropTable.DROPTABLE_MAGIC_ONLY_FAMOS, 0x1));
            ChangeDrop(ITEM00_BOMBCHU, SlotAddr(DropTable.DROPTABLE_MAGIC_ONLY_FAMOS, 0x2));

            ChangeDrop(ITEM00_BOMBCHU, SlotAddr(DropTable.DROPTABLE_UNK1, 0x5)); // southern swamp grass

            ChangeDrop(ITEM00_BOMBCHU, SlotAddr(DropTable.DROPTABLE_IKANA_ROCKS, 0x8)); // those rocks could have one sure

            ChangeDrop(ITEM00_BOMBCHU, SlotAddr(DropTable.DROPTABLE_UNUSED_3, 0x8)); // unused, but actor rando can put them down

            ChangeDrop(ITEM00_BOMBCHU, SlotAddr(DropTable.DROPTABLE_DEAD_CREATURES, 0x8)); // powerful creatures, sure
        }

        private static void EnableBombchuDrops()
        {
            /// adds [Bombchu x 10] as a possible drop from Item00
            ///  we have to change the drop system to allow this, as vanilla does not have a bombchu drop
            ///  we overwrite the [Small Key] drop which was unused to get this working

            var codeFileData = RomData.MMFileList[CodeFID].Data;

            // EnItem00_Init: we need to change the 0x11 param from small key to bombchu before it gets passed to Item_Give
            codeFileData[0x837] = 0x07;

            // EnItem00_Update: change the getItemId for smallKey type from GI_KEY_SMALL to GI_BOMBCHUS_10 
            codeFileData[0x14AF] = 0x1A; // 3C -> 1A

            // changing the sprite texture pointer in a list of all sprites for Item00, changing key -> bombchu
            ReadWriteUtils.Arr_WriteU16(codeFileData, 0x1084B2, 0xD6F0); // 0xC444B2 F7C0 -> D6F0

            // think this is where I removed a case from a switch case for small item key from EnItem00_Update
            // its waaay down at the end of the file because large switch cases use jump tables which are rodata, they get placed at the end
            codeFileData[0x136507] = 0x6C; // 0xC72507 40 -> 6C
        }
    }
}
