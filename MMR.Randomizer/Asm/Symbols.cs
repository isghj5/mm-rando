using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Models.Rom;
using MMR.Randomizer.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace MMR.Randomizer.Asm
{
    /// <summary>
    /// Symbols used in the ASM patch.
    /// </summary>
    public class Symbols
    {
        private Dictionary<string, uint> _symbols = new Dictionary<string, uint>();

        /// <summary>
        /// Virtual address of the <see cref="MMFile"/> containing serialized <see cref="Symbols"/> data.
        /// </summary>
        public static readonly uint MMFILE_START = 0x03F00000;

        /// <summary>
        /// Address of payload end.
        /// </summary>
        public uint PayloadEnd => this["PAYLOAD_END"];

        /// <summary>
        /// Address of payload start.
        /// </summary>
        public uint PayloadStart => this["PAYLOAD_START"];

        /// <summary>
        /// Address of payload start.
        /// </summary>
        public uint KaleidoPayloadStart => this["KALEIDOPAYLOAD_START"];

        /// <summary>
        /// Address of payload start.
        /// </summary>
        public uint KaleidoPayloadEnd => this["KALEIDOPAYLOAD_END"];

        /// <summary>
        /// Get the value of a symbol by name.
        /// </summary>
        /// <param name="name">Symbol name</param>
        /// <returns>Symbol value</returns>
        public uint this[string name] {
            get {
                return this._symbols[name];
            }
        }

        private Symbols()
            : this(new Dictionary<string, uint>())
        {
        }

        private Symbols(Dictionary<string, uint> symbols)
        {
            this._symbols = symbols;
        }

        /// <summary>
        /// Check if a certain symbol exists.
        /// </summary>
        /// <param name="name">Symbol name</param>
        /// <returns>True if exists, false if not</returns>
        public bool Has(string name)
        {
            return this._symbols.ContainsKey(name);
        }

        /// <summary>
        /// Create a special <see cref="MMFile"/> with serialized <see cref="Symbols"/> data.
        /// </summary>
        /// <returns>MMFile</returns>
        public MMFile CreateMMFile()
        {
            var start = MMFILE_START;
            var data = this.ToBytes();

            var file = new MMFile
            {
                Addr = (int)start,
                End = (int)start + data.Length,
                IsCompressed = false,
                IsStatic = true,
                Data = data,
            };

            return file;
        }

        /// <summary>
        /// Create initial mimic item table for ice traps.
        /// </summary>
        /// <returns>Mimic table.</returns>
        public MimicItemTable CreateMimicItemTable()
        {
            var addr = this["ITEM_OVERRIDE_COUNT"];
            var count = ReadWriteUtils.ReadU32((int)addr);
            return new MimicItemTable((int)count);
        }

        /// <summary>
        /// Write <see cref="MimicItemTable"/> table to ROM.
        /// </summary>
        /// <param name="table">Table</param>
        public void WriteMimicItemTable(MimicItemTable table)
        {
            var addr = this["ITEM_OVERRIDE_ENTRIES"];
            ReadWriteUtils.WriteToROM((int)addr, table.Build());
        }

        /// <summary>
        /// Create initial extended <see cref="MessageTable"/> for extra messages.
        /// </summary>
        /// <returns>Extended MessageTable</returns>
        public MessageTable CreateInitialExtMessageTable()
        {
            var addr = this["EXT_MSG_TABLE_COUNT"];
            var count = ReadWriteUtils.ReadU32((int)addr);
            return new MessageTable(count);
        }

        /// <summary>
        /// Write extended <see cref="MessageTable"/>.
        /// </summary>
        /// <param name="table">Extended MessageTable</param>
        public void WriteExtMessageTable(MessageTable table)
        {
            // Write extended message table entries, and append new file for extended message table data.
            var addr = this["EXT_MSG_TABLE"];
            var index = MessageTable.WriteExtended(table, addr);

            // Write index of message table data.
            var fileIndexAddr = this["EXT_MSG_DATA_FILE"];
            ReadWriteUtils.WriteU32ToROM((int)fileIndexAddr, (uint)index);
        }

        /// <summary>
        /// Write an <see cref="AsmConfig"/> structure to ROM.
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="config">Config</param>
        void WriteAsmConfig(string symbol, AsmConfig config)
        {
            var addr = this[symbol];
            var version = ReadWriteUtils.ReadU32((int)(addr + 4));
            var bytes = config.ToBytes(version);
            ReadWriteUtils.WriteToROM((int)(addr + 4), bytes);
        }

        /// <summary>
        /// Write bytes for Clock Town stray fairy icon to ROM.
        /// </summary>
        public void WriteClockTownStrayFairyIcon()
        {
            var addr = this["TOWN_FAIRY_BYTES"];
            var icon = ImageUtils.GetClockTownStrayFairyIcon();
            ReadWriteUtils.WriteToROM((int)addr, icon);
        }

        /// <summary>
        /// Write the D-Pad configuration structure to ROM.
        /// </summary>
        /// <param name="config">D-Pad config</param>
        public void WriteDPadConfig(DPadConfig config)
        {
            // If there's a DPAD_STATE symbol, use the legacy function instead.
            if (this.Has("DPAD_STATE"))
            {
                WriteDPadConfigLegacy(config);
                return;
            }

            WriteAsmConfig("DPAD_CONFIG", config);
        }

        /// <summary>
        /// Write a <see cref="DPadConfig"/> to the ROM.
        /// </summary>
        /// <remarks>Assumes <see cref="Patcher"/> file has been inserted.</remarks>
        /// <param name="config">D-Pad config</param>
        void WriteDPadConfigLegacy(DPadConfig config)
        {
            // Write DPad config bytes.
            var addr = this["DPAD_CONFIG"];
            ReadWriteUtils.WriteToROM((int)addr, config.Pad.Bytes);

            // Write DPad state byte.
            addr = this["DPAD_STATE"];
            ReadWriteUtils.WriteToROM((int)addr, (byte)config.State);
        }

        /// <summary>
        /// Write extended object virtual ROM addresses to the ROM.
        /// </summary>
        /// <param name="addresses">Tuples containing the virtual ROM start and end addresses</param>
        public void WriteExtendedObjects((uint start, uint end)[] addresses)
        {
            var addr = (int)this["EXT_OBJECTS"];
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
        /// <param name="config">HUD colors config</param>
        public void WriteHudColorsConfig(HudColorsConfig config)
        {
            WriteAsmConfig("HUD_COLOR_CONFIG", config);
        }

        /// <summary>
        /// Write a <see cref="MiscConfig"/> to the ROM.
        /// </summary>
        /// <param name="config">Misc config</param>
        public void WriteMiscConfig(MiscConfig config)
        {
            WriteAsmConfig("MISC_CONFIG", config);
        }

        /// <summary>
        /// Write a <see cref="MMRConfig"/> to the ROM.
        /// </summary>
        /// <param name="config">MMR config</param>
        public void WriteMMRConfig(MMRConfig config)
        {
            WriteAsmConfig("MMR_CONFIG", config);
        }

        /// <summary>
        /// Write the <see cref="MiscConfig"/> hash bytes without overwriting other parts of the structure.
        /// </summary>
        /// <param name="hash">Hash bytes</param>
        public void WriteMiscHash(byte[] hash)
        {
            var bytes = ReadWriteUtils.CopyBytes(hash, 0x10);
            var addr = this["MISC_CONFIG"];
            ReadWriteUtils.WriteToROM((int)(addr + 8), bytes);
        }

        /// <summary>
        /// Write a <see cref="WorldColorsConfig"/> to the ROM.
        /// </summary>
        /// <param name="config">World Colors config.</param>
        public void WriteWorldColorsConfig(WorldColorsConfig config)
        {
            config.PatchObjects();
            WriteAsmConfig("WORLD_COLOR_CONFIG", config);
        }

        public void WriteMusicConfig(MusicConfig config)
        {
            WriteAsmConfig("MUSIC_CONFIG", config);
        }

        /// <summary>
        /// Try and write a <see cref="DPadConfig"/> to the ROM.
        /// </summary>
        /// <param name="config">D-Pad config</param>
        /// <returns>True if successful, false if the <see cref="DPadConfig"/> symbol was not found.</returns>
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
        /// Try and write a <see cref="HudColorsConfig"/> to the ROM.
        /// </summary>
        /// <param name="config">HUD colors config</param>
        /// <returns>True if successful, false if the <see cref="HudColorsConfig"/> symbol was not found.</returns>
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
        /// Try and write the <see cref="MiscConfig"/> hash bytes.
        /// </summary>
        /// <param name="hash">Hash bytes</param>
        /// <returns>True if successful, false if the <see cref="MiscConfig"/> symbol was not found.</returns>
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
        /// Try and write a <see cref="WorldColorsConfig"/> to the ROM.
        /// </summary>
        /// <param name="config">World Colors config.</param>
        /// <returns>True if successful, false if the <see cref="WorldColorsConfig"/> symbol was not found.</returns>
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
        /// Load <see cref="Symbols"/> from serialized data.
        /// </summary>
        /// <param name="bytes">Bytes</param>
        /// <returns>Symbols</returns>
        public static Symbols FromBytes(byte[] bytes)
        {
            var symbols = new Symbols();

            using (var memStream = new MemoryStream(bytes))
            using (var reader = new BinaryReader(memStream))
            {
                reader.ReadUInt32(); // Unused for now
                var count = reader.ReadUInt32();
                for (uint i = 0; i < count; i++)
                {
                    var namelen = reader.ReadUInt16();
                    var namebytes = reader.ReadBytes(namelen);
                    var name = Encoding.ASCII.GetString(namebytes);
                    var value = reader.ReadUInt32();
                    symbols._symbols.Add(name, value);
                }
            }

            return symbols;
        }

        /// <summary>
        /// Load <see cref="Symbols"/> from a JSON <see cref="string"/>.
        /// </summary>
        /// <param name="json">JSON string</param>
        /// <returns>Symbols</returns>
        public static Symbols FromJSON(string json)
        {
            var result = JsonSerializer.Deserialize<Dictionary<string, string>>(json)
                .Select(x => new KeyValuePair<string, uint>(x.Key, Convert.ToUInt32(x.Value, 16)))
                .ToDictionary(x => x.Key, x => x.Value);
            return new Symbols(result);
        }

        /// <summary>
        /// Load <see cref="Symbols"/> from serialized data in a <see cref="MMFile"/>.
        /// </summary>
        /// <param name="file">MMFile</param>
        /// <returns>Symbols</returns>
        public static Symbols FromMMFile(MMFile file)
        {
            return FromBytes(file.Data);
        }

        /// <summary>
        /// Load <see cref="Symbols"/> from a special <see cref="MMFile"/> already in the ROM.
        /// </summary>
        /// <returns>Symbols</returns>
        public static Symbols FromROM()
        {
            int index = RomUtils.AddrToFile((int)MMFILE_START);

            // Might be unable to find MMFile with Symbols data for older patch files
            if (index < 0)
                return null;

            var file = RomData.MMFileList[index];
            return Symbols.FromMMFile(file);
        }

        /// <summary>
        /// Load <see cref="Symbols"/> from the default resource file.
        /// </summary>
        /// <returns>Symbols</returns>
        public static Symbols Load()
        {
            return FromJSON(Resources.asm.symbols);
        }

        /// <summary>
        /// Serialize into bytes.
        /// </summary>
        /// <returns>Bytes</returns>
        public byte[] ToBytes()
        {
            using (var memStream = new MemoryStream())
            using (var writer = new BinaryWriter(memStream))
            {
                writer.Write((uint)0); // Potentially use this as a "version" field later
                writer.Write((uint)_symbols.Count);
                foreach (var symbol in _symbols)
                {
                    // Symbols should always be ASCII encode-able
                    var name = Encoding.ASCII.GetBytes(symbol.Key);
                    writer.Write((ushort)name.Length);
                    writer.Write(name);
                    writer.Write(symbol.Value);
                }

                while (memStream.Position % 0x10 != 0)
                {
                    writer.Write((byte)0);
                }

                return memStream.ToArray();
            }
        }

        /// <summary>
        /// Apply configuration which will be hardcoded into the patch file.
        /// </summary>
        /// <param name="options">Options</param>
        public void ApplyConfiguration(AsmOptionsGameplay options)
        {
            this.WriteClockTownStrayFairyIcon();
            this.WriteMiscConfig(options.MiscConfig);
            this.WriteMMRConfig(options.MMRConfig);
        }

        /// <summary>
        /// Apply configuration using the <see cref="Symbols"/> data.
        /// </summary>
        /// <param name="options">Options</param>
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
        /// <param name="options">Options</param>
        public void TryApplyConfigurationPostPatch(AsmOptionsCosmetic options)
        {
            this.TryWriteDPadConfig(options.DPadConfig);
            this.TryWriteHudColorsConfig(options.HudColorsConfig);
            this.TryWriteWorldColorsConfig(options.WorldColorsConfig);
            this.TryWriteMusicConfig(options.MusicConfig);

            // Try and write the MiscConfig hash
            this.TryWriteMiscHash(options.Hash);
        }

        public byte[] ReadHashIconsTable()
        {
            var addr = this["HASH_ICONS"];
            var count = ReadWriteUtils.ReadU16((int)(addr + 4));
            if (count != 0x40)
                throw new Exception("Bad symbol count for hash icons");
            var bytes = ReadWriteUtils.ReadBytes((int)(addr + 6), count);
            return bytes;
        }
    }
}
