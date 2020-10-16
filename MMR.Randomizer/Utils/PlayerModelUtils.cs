using MMR.Randomizer.Constants;
using System.IO;

namespace MMR.Randomizer.Utils
{
    public static class PlayerModelUtils
    {
        /// <summary>
        /// Apply all patches for Adult Link mod provided by SkilarsArt.
        /// </summary>
        public static void ApplyAdultLinkPatches()
        {
            // Overwrite segmented address: 0x0601E244
            ReadWriteUtils.WriteU32ToROM(0xC56350, 0x060122C4); // Physical: 0xBA5E70

            // ???
            ReadWriteUtils.WriteU16ToROM(0xC5637E, 0x02BC);
            ReadWriteUtils.WriteU16ToROM(0xC56380, 0x0226);
            ReadWriteUtils.WriteU16ToROM(0xC56382, 0x010E);
            ReadWriteUtils.WriteU16ToROM(0xC56384, 0x02BC);
            ReadWriteUtils.WriteU16ToROM(0xC56386, 0x012C);
            ReadWriteUtils.WriteU16ToROM(0xC5638C, 0x0258);
            ReadWriteUtils.WriteU16ToROM(0xC56392, 0x024E);
            ReadWriteUtils.WriteU16ToROM(0xC56398, 0x00C8);
            ReadWriteUtils.WriteU16ToROM(0xC5639A, 0x0082);

            // Write chunk of segmented addresses for player model.
            var segaddrs = File.ReadAllBytes(Path.Combine(Values.ObjsDirectory, "adult-link-code-segaddrs.bin"));
            ReadWriteUtils.WriteToROM(0xC5653C, segaddrs);

            // Update sword hitbox?
            {
                // Kokiri Sword:        3000.0 => 4000.0
                ReadWriteUtils.WriteU32ToROM(0xC572BC, 0x457A0000);
                // Razor Sword:         3000.0 => 4000.0
                ReadWriteUtils.WriteU32ToROM(0xC572C0, 0x457A0000);
                // Gilded Sword:        4000.0 => 5500.0
                ReadWriteUtils.WriteU32ToROM(0xC572C4, 0x45ABE000);
                // Great Fairy's Sword: 5500.0 => 5500.0
                ReadWriteUtils.WriteU32ToROM(0xC572C8, 0x45ABE000);
            }

            // Overwrite segmented address: 0x06017818 => 0x0601BE60
            ReadWriteUtils.WriteU32ToROM(0xC572D4, 0x0601BE60);

            // Overwrite function pointer: 0x800B7078 => 0x800B7058
            // Last function in function pointer array (length 5) at RDRAM: 0x801DCA58
            // Old function: F0 = F2 + 44.0
            // New function: F0 = F2 + 68.0
            ReadWriteUtils.WriteU32ToROM(0xC72FA8, 0x800B7058); // Physical: 0xBC2AC8

            // Patch player_actor.
            {
                // Replace floats? Physical: 0xC25D38
                ReadWriteUtils.WriteU32ToROM(0xCD6218, 0x42600000); // 40.0   => 56.0
                ReadWriteUtils.WriteU32ToROM(0xCD621C, 0x42B40000); // 60.0   => 90.0
                ReadWriteUtils.WriteU32ToROM(0xCD6220, 0x3F800000); // 0.647  => 1.0
                ReadWriteUtils.WriteU32ToROM(0xCD6224, 0x42DE0000); // 71.0   => 111.0
                ReadWriteUtils.WriteU32ToROM(0xCD6228, 0x428C0000); // 50.0   => 70.0
                ReadWriteUtils.WriteU32ToROM(0xCD622C, 0x429ECCCD); // 49.0   => 79.4
                ReadWriteUtils.WriteU32ToROM(0xCD6230, 0x426C0000); // 39.0   => 59.0
                ReadWriteUtils.WriteU32ToROM(0xCD6234, 0x42240000); // 27.0   => 41.0
                ReadWriteUtils.WriteU32ToROM(0xCD6238, 0x41980000); // 19.0
                ReadWriteUtils.WriteU32ToROM(0xCD623C, 0x42100000); // 22.0   => 36.0
                ReadWriteUtils.WriteU32ToROM(0xCD6240, 0x42480000); // 32.4   => 50.0
                ReadWriteUtils.WriteU32ToROM(0xCD6244, 0x42600000); // 32.0   => 56.0
                ReadWriteUtils.WriteU32ToROM(0xCD6248, 0x42880000); // 48.0   => 68.0
                ReadWriteUtils.WriteU32ToROM(0xCD624C, 0x428C0000); // 45.294 => 70.0
                ReadWriteUtils.WriteU32ToROM(0xCD6250, 0x41900000); // 14.0   => 18.0
                ReadWriteUtils.WriteU32ToROM(0xCD6254, 0x41B80000); // 12.0   => 23.0
                ReadWriteUtils.WriteU32ToROM(0xCD6258, 0x428C0000); // 55.0   => 70.0

                // Replace unknown data chunk immediately following floats. Physical: 0xC25D7C
                // This affects Link's voice (child voice to adult voice).
                var data1 = File.ReadAllBytes(Path.Combine(Values.ObjsDirectory, "adult-link-player-actor-data-1.bin"));
                ReadWriteUtils.WriteToROM(0xCD625C, data1);

                // Replace segmented addresses following previous chunk. Physical: 0xC25DD8
                var data2 = File.ReadAllBytes(Path.Combine(Values.ObjsDirectory, "adult-link-player-actor-data-2.bin"));
                ReadWriteUtils.WriteToROM(0xCD62B8, data2);
            }

            // Patch Arms_Hook (Hookshot) actor.
            {
                // Update segmented address in code: 0x0602D960 => 0x06029D60
                // Old: addiu t0, 0x0602
                //      addiu t0, t0, 0xD960
                // New: addiu t0, 0x0602
                //      addiu t0, t0, 0x9D60
                ReadWriteUtils.WriteU32ToROM(0xD3BC50, 0x25089D60); // Physical: 0xC8B770
            }

            // Patch En_Zog (Mikau) actor.
            {
                // Update Mikau mass value to 0 during actor init, so Mikau can be "pushed" via collision.
                // Old: addiu t6, r0, 0x00FF
                // New: addiu t6, r0, 0x0000
                ReadWriteUtils.WriteU32ToROM(0xFF87D0, 0x240E0000); // Physical: 0xF482F0

                // Update instruction to use new distance to allow pushing Mikau from: 40.0 => 96.0
                // Old: lui at, 0x4220
                // New: lui at, 0x42C0
                ReadWriteUtils.WriteU32ToROM(0xFFA188, 0x3C0142C0); // Physical: 0xF49CA8
            }

            // Patch vertex data and DLists in gameplay_keep.
            {
                // Write vertex buffer (1).
                var vtx1 = File.ReadAllBytes(Path.Combine(Values.ObjsDirectory, "adult-link-gameplay-keep-vtx-1.bin"));
                ReadWriteUtils.WriteToROM(0x10B0510, vtx1); // Physical: 0xFFFFB0

                // Write vertex buffer (2).
                var vtx2 = File.ReadAllBytes(Path.Combine(Values.ObjsDirectory, "adult-link-gameplay-keep-vtx-2.bin"));
                ReadWriteUtils.WriteToROM(0x10B0A90, vtx2); // Physical: 0x1000530

                // Write vertex buffer (3).
                var vtx3 = File.ReadAllBytes(Path.Combine(Values.ObjsDirectory, "adult-link-gameplay-keep-vtx-3.bin"));
                ReadWriteUtils.WriteToROM(0x10B1810, vtx3); // Physical: 0x10012B0

                // Replace DList instruction: G_RDPPIPESYNC => G_DL
                ReadWriteUtils.WriteU64ToROM(0x10B18F0, 0xDE0000000405A2E0); // gsSPDisplayList(0x0405A2E0);

                // Write vertex buffer (4).
                var vtx4 = File.ReadAllBytes(Path.Combine(Values.ObjsDirectory, "adult-link-gameplay-keep-vtx-4.bin"));
                ReadWriteUtils.WriteToROM(0x10E52A0, vtx4); // Physical: 0x1034D40

                // Replace vertex data with small DList (offset 0x5A2E0).
                ReadWriteUtils.WriteU64ToROM(0x10E52E0, 0xE700000000000000); // gsDPPipeSync();
                ReadWriteUtils.WriteU64ToROM(0x10E52E8, 0xDA3800010405A2A0); // gsSPMatrix(G_MTX_NOPUSH, 0x0405A2A0);
                ReadWriteUtils.WriteU64ToROM(0x10E52F0, 0xDF00000000000000); // gsSPEndDisplayList();
            }

            // Patch DLists for masks (field models).
            {
                // Update Keaton Mask to call gameplay_keep DList: gsDPPipeSync() => gsSPDisplayList(0x0405A2E0)
                ReadWriteUtils.WriteU64ToROM(0x11B14A8, 0xDE0000000405A2E0); // Physical: 0x10FB618

                // Update Bunny Hood to call gameplay_keep DLists.
                ReadWriteUtils.WriteU64ToROM(0x11B2620, 0xDE0000000405A2E0); // Physical: 0x10FC260
                ReadWriteUtils.WriteU64ToROM(0x11B2780, 0xDE0000000405A2E8); // Physical: 0x10FC3C0
                ReadWriteUtils.WriteU64ToROM(0x11B27B0, 0xDE0000000405A2E0); // Physical: 0x10FC3F0
                ReadWriteUtils.WriteU64ToROM(0x11B2890, 0xDE0000000405A2E8); // Physical: 0x10FC4D0
                ReadWriteUtils.WriteU64ToROM(0x11B28C0, 0xDE0000000405A2E0); // Physical: 0x10FC500

                // Update Mask of Scents to call gameplay_keep DList: gsDPPipeSync() => gsSPDisplayList(0x0405A2E0)
                ReadWriteUtils.WriteU64ToROM(0x11D6718, 0xDE0000000405A2E0); // Physical: 0x11175B8
            }

            // Replace En_Horse actor (Epona).
            var horse = File.ReadAllBytes(Path.Combine(Values.ObjsDirectory, "adult-en-horse.bin"));
            ReadWriteUtils.WriteToROM(0xCF5950, horse);

            // Insert player model object.
            var model = File.ReadAllBytes(Path.Combine(Values.ObjsDirectory, "adult-obj-link.bin"));
            ObjUtils.InsertObj(model, 0x11);

            // Insert adult Epona model object.
            var epona = File.ReadAllBytes(Path.Combine(Values.ObjsDirectory, "adult-obj-epona.bin"));
            ObjUtils.InsertObj(epona, 0x7D);
        }
    }
}
