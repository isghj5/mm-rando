using MMR.Randomizer.Models.Rom;
using MMR.Randomizer.Utils;
using System;
using System.Collections.Generic;

namespace MMR.Randomizer.Asm
{
    /// <summary>
    /// Asm context.
    /// </summary>
    public class AsmContext
    {
        /// <summary>
        /// Patcher.
        /// </summary>
        public AsmPatcher Patcher { get; private set; }

        /// <summary>
        /// Symbols.
        /// </summary>
        public AsmSymbols Symbols { get; private set; }

        /// <summary>
        /// Extended <see cref="MessageTable"/>.
        /// </summary>
        public MessageTable ExtraMessages { get; private set; }

        /// <summary>
        /// Item mimic table used for ice traps.
        /// </summary>
        public MimicItemTable MimicTable { get; private set; }

        /// <summary>
        /// Virtual address for assembly payload file.
        /// </summary>
        public uint AsmAddress => (uint)(RomData.MMFileList[AsmIndex].Addr);

        /// <summary>
        /// File index for assembly payload file.
        /// </summary>
        public int AsmIndex { get; private set; } = -1;

        public AsmContext(AsmPatcher patcher, AsmSymbols symbols)
        {
            this.Patcher = patcher;
            this.Symbols = symbols;
        }

        /// <summary>
        /// Resolve a symbol address for the assembly payload file.
        /// </summary>
        /// <param name="sym">Symbol name</param>
        /// <returns></returns>
        public uint Resolve(string sym) => AsmAddress + Symbols.Offset(sym);

        /// <summary>
        /// Apply configuration which will be hardcoded into the patch file.
        /// </summary>
        /// <param name="options"></param>
        public void ApplyConfiguration(AsmOptionsGameplay options)
        {
            this.WriteClockTownStrayFairyIcon();
            this.WriteMiscConfig(options.MiscConfig);
            this.WriteMMRConfig(options.MMRConfig);
        }

        /// <summary>
        /// Apply configuration using the <see cref="AsmSymbols"/> data.
        /// </summary>
        /// <param name="options"></param>
        public void ApplyConfigurationPostPatch(AsmOptionsCosmetic options)
        {
            this.WriteDPadConfig(options.DPadConfig);
            this.WriteHudColorsConfig(options.HudColorsConfig);
            this.WriteWorldColorsConfig(options.WorldColorsConfig);
            this.WriteMusicConfig(options.MusicConfig);

            // Only write the MiscConfig hash (the rest should not be changeable post-patch)
            this.WriteMiscHash(options.Hash);
        }

        /// <summary>
        /// Try and apply configuration post-patch using the <see cref="Symbols"/> data.
        /// </summary>
        /// <param name="options"></param>
        public void TryApplyConfigurationPostPatch(AsmOptionsCosmetic options)
        {
            this.TryWriteDPadConfig(options.DPadConfig);
            this.TryWriteHudColorsConfig(options.HudColorsConfig);
            this.TryWriteWorldColorsConfig(options.WorldColorsConfig);
            this.TryWriteMusicConfig(options.MusicConfig);

            // Try and write the MiscConfig hash
            this.TryWriteMiscHash(options.Hash);
        }

        /// <summary>
        /// Apply and write patch data.
        /// </summary>
        /// <param name="options"></param>
        public void ApplyPatch(AsmOptionsGameplay options)
        {
            this.AsmIndex = this.Patcher.Apply(this.Symbols);
            this.ApplyConfiguration(options);
            this.PatchLoadAddress();
            this.ExtraMessages = this.CreateInitialExtMessageTable();
            this.MimicTable = this.CreateMimicItemTable();
        }

        /// <summary>
        /// Apply configuration after the patch file has been created (or applied) and the hash calculated.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="patch">Whether or not a patch file was applied</param>
        public void ApplyPostConfiguration(AsmOptionsCosmetic options, bool patch = false)
        {
            if (patch)
            {
                // If applying post-configuration after applying a patch file, it may be an older
                // patch file which does not support newer features (and thus would not have the
                // necessary symbols). Thus, "try" and apply it without throwing an exception if
                // symbols are not found.
                TryApplyConfigurationPostPatch(options);
            }
            else
            {
                // If applying post-configuration from the internal patch & symbols data, all
                // expected symbols should be present. Thus, an exception should be thrown if any
                // cannot be found.
                ApplyConfigurationPostPatch(options);
            }
        }

        /// <summary>
        /// Load from internal resource files.
        /// </summary>
        /// <returns></returns>
        public static AsmContext LoadInternal()
        {
            var patcher = AsmPatcher.Load();
            var symbols = AsmSymbols.Load();
            return new AsmContext(patcher, symbols);
        }

        /// <summary>
        /// Create initial mimic item table for ice traps.
        /// </summary>
        /// <returns></returns>
        public MimicItemTable CreateMimicItemTable()
        {
            var addr = Resolve("ITEM_OVERRIDE_COUNT");
            var count = ReadWriteUtils.ReadU32((int)addr);
            return new MimicItemTable((int)count);
        }

        /// <summary>
        /// Create initial extended <see cref="MessageTable"/> for extra messages.
        /// </summary>
        /// <returns></returns>
        public MessageTable CreateInitialExtMessageTable()
        {
            var addr = Resolve("EXT_MSG_TABLE_COUNT");
            var count = ReadWriteUtils.ReadU32((int)addr);
            return new MessageTable(count);
        }

        /// <summary>
        /// Patch the virtual address used to load the assembly payload file at boot.
        /// </summary>
        public void PatchLoadAddress() => ReadWriteUtils.WriteCodeSignedHiLo(0x801748B4, AsmAddress);

        /// <summary>
        /// Read the hash icon table from the assembly payload file.
        /// </summary>
        /// <returns>Table bytes.</returns>
        /// <exception cref="Exception"></exception>
        public byte[] ReadHashIconsTable()
        {
            var addr = Resolve("HASH_ICONS");
            var count = ReadWriteUtils.ReadU16((int)(addr + 4));
            if (count != 0x40)
                throw new Exception("Bad symbol count for hash icons");
            var bytes = ReadWriteUtils.ReadBytes((int)(addr + 6), count);
            return bytes;
        }

        /// <summary>
        /// Write an <see cref="AsmConfig"/> structure to ROM.
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="config"></param>
        void WriteAsmConfig(string symbol, AsmConfig config)
        {
            var addr = Resolve(symbol);
            var version = ReadWriteUtils.ReadU32((int)(addr + 4));
            var bytes = config.ToBytes(version);
            ReadWriteUtils.WriteToROM((int)(addr + 4), bytes);
        }

        /// <summary>
        /// Write bytes for Clock Town stray fairy icon to ROM.
        /// </summary>
        public void WriteClockTownStrayFairyIcon()
        {
            var addr = Resolve("TOWN_FAIRY_BYTES");
            var icon = ImageUtils.GetClockTownStrayFairyIcon();
            ReadWriteUtils.WriteToROM((int)addr, icon);
        }

        /// <summary>
        /// Write a <see cref="DPadConfig"/> to the ROM.
        /// </summary>
        /// <param name="config"></param>
        public void WriteDPadConfig(DPadConfig config)
        {
            WriteAsmConfig("DPAD_CONFIG", config);
        }

        /// <summary>
        /// Try and write a <see cref="DPadConfig"/> to the ROM.
        /// </summary>
        /// <param name="config"></param>
        /// <returns><see langword="true"/> if successful, <see langword="false"/> if the <see cref="DPadConfig"/> symbol was not found.</returns>
        public bool TryWriteDPadConfig(DPadConfig config)
        {
            try
            {
                WriteDPadConfig(config);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Write the extended <see cref="MessageTable"/> to ROM.
        /// </summary>
        public void WriteExtMessageTable()
        {
            WriteExtMessageTable(ExtraMessages);
        }

        /// <summary>
        /// Write the extended <see cref="MessageTable"/> to ROM.
        /// </summary>
        /// <param name="table"></param>
        public void WriteExtMessageTable(MessageTable table)
        {
            // Write extended message table entries, and append new file for extended message table data.
            var addr = Resolve("EXT_MSG_TABLE");
            var index = MessageTable.WriteExtended(table, addr);

            // Write index of message table data.
            var fileIndexAddr = AsmAddress + Symbols.Offset("EXT_MSG_DATA_FILE");
            ReadWriteUtils.WriteU32ToROM((int)fileIndexAddr, (uint)index);
        }

        /// <summary>
        /// Write extended object virtual ROM addresses to the ROM.
        /// </summary>
        /// <param name="addresses">Tuples containing the virtual ROM start and end addresses</param>
        public void WriteExtendedObjects((uint start, uint end)[] addresses)
        {
            var addr = (int)Resolve("EXT_OBJECTS");
            for (int i = 0; i < addresses.Length; i++)
            {
                var offset = addr + (i + 1) * 8;
                var pair = addresses[i];

                // Write start and end VROM addresses for object.
                ReadWriteUtils.WriteToROM(offset + 0, ConvertUtils.IntToBytes((int)pair.start));
                ReadWriteUtils.WriteToROM(offset + 4, ConvertUtils.IntToBytes((int)pair.end));
            }
        }

        /// <summary>
        /// Write a <see cref="HudColorsConfig"/> to the ROM.
        /// </summary>
        /// <param name="config"></param>
        public void WriteHudColorsConfig(HudColorsConfig config)
        {
            WriteAsmConfig("HUD_COLOR_CONFIG", config);
        }

        /// <summary>
        /// Try and write a <see cref="HudColorsConfig"/> to the ROM.
        /// </summary>
        /// <param name="config"></param>
        /// <returns><see langword="true"/> if successful, <see langword="false"/> if the <see cref="HudColorsConfig"/> symbol was not found.</returns>
        public bool TryWriteHudColorsConfig(HudColorsConfig config)
        {
            try
            {
                WriteHudColorsConfig(config);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Write the <see cref="MimicItemTable"/> table to ROM.
        /// </summary>
        public void WriteMimicItemTable()
        {
            WriteMimicItemTable(MimicTable);
        }

        /// <summary>
        /// Write the <see cref="MimicItemTable"/> table to ROM.
        /// </summary>
        /// <param name="table"></param>
        public void WriteMimicItemTable(MimicItemTable table)
        {
            var addr = Resolve("ITEM_OVERRIDE_ENTRIES");
            ReadWriteUtils.WriteToROM((int)addr, table.Build());
        }

        /// <summary>
        /// Write a <see cref="MiscConfig"/> to the ROM.
        /// </summary>
        /// <param name="config"></param>
        public void WriteMiscConfig(MiscConfig config)
        {
            WriteAsmConfig("MISC_CONFIG", config);
        }

        /// <summary>
        /// Write the <see cref="MiscConfig"/> hash bytes without overwriting other parts of the structure.
        /// </summary>
        /// <param name="hash">Hash bytes</param>
        public void WriteMiscHash(byte[] hash)
        {
            var bytes = ReadWriteUtils.CopyBytes(hash, 0x10);
            var addr = Resolve("MISC_CONFIG");
            ReadWriteUtils.WriteToROM((int)(addr + 8), bytes);
        }

        /// <summary>
        /// Try and write the <see cref="MiscConfig"/> hash bytes.
        /// </summary>
        /// <param name="hash">Hash bytes</param>
        /// <returns><see langword="true"/> if successful, <see langword="false"/> if the <see cref="MiscConfig"/> symbol was not found.</returns>
        public bool TryWriteMiscHash(byte[] hash)
        {
            try
            {
                WriteMiscHash(hash);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Write a <see cref="MusicConfig"/> to the ROM.
        /// </summary>
        /// <param name="config"></param>
        public void WriteMusicConfig(MusicConfig config)
        {
            WriteAsmConfig("MUSIC_CONFIG", config);
        }

        /// <summary>
        /// Try and write a <see cref="MusicConfig"/> to the ROM.
        /// </summary>
        /// <param name="config"></param>
        /// <returns><see langword="true"/> if successful, <see langword="false"/> if the <see cref="MusicConfig"/> symbol was not found.</returns>
        public bool TryWriteMusicConfig(MusicConfig config)
        {
            try
            {
                WriteMusicConfig(config);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Write a <see cref="MMRConfig"/> to the ROM.
        /// </summary>
        /// <param name="config"></param>
        public void WriteMMRConfig(MMRConfig config)
        {
            WriteAsmConfig("MMR_CONFIG", config);
        }

        /// <summary>
        /// Write a <see cref="WorldColorsConfig"/> to the ROM.
        /// </summary>
        /// <param name="config"></param>
        public void WriteWorldColorsConfig(WorldColorsConfig config)
        {
            config.PatchObjects();
            WriteAsmConfig("WORLD_COLOR_CONFIG", config);
        }

        /// <summary>
        /// Try and write a <see cref="WorldColorsConfig"/> to the ROM.
        /// </summary>
        /// <param name="config"></param>
        /// <returns><see langword="true"/> if successful, <see langword="false"/> if the <see cref="WorldColorsConfig"/> symbol was not found.</returns>
        public bool TryWriteWorldColorsConfig(WorldColorsConfig config)
        {
            try
            {
                WriteWorldColorsConfig(config);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }
    }
}
