using MMR.Randomizer.Extensions;
using MMR.Randomizer.Utils;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MMR.Randomizer.Asm
{
    /// <summary>
    /// Indexes for extended objects.
    /// </summary>
    public class ObjectIndexes
    {
        public short? DoubleDefense;
        public short? MagicPower;
        public short? Fairies;
        public short? Skulltula;
        public short? MusicNotes;
    }

    /// <summary>
    /// Loader for extended object data.
    /// </summary>
    public class ExtendedObjects
    {
        /// <summary>
        /// Offsets of objects relative to start of <see cref="ObjectBundle"/> data.
        /// </summary>
        public List<(uint, uint)> Offsets { get; } = new List<(uint, uint)>();

        /// <summary>
        /// Object bundle data.
        /// </summary>
        public ObjectBundle Bundle { get; } = new ObjectBundle();

        /// <summary>
        /// Next index to use when adding an extended object.
        /// </summary>
        public short LatestIndex { get; private set; } = 0x283;

        /// <summary>
        /// Object indexes.
        /// </summary>
        public ObjectIndexes Indexes { get; } = new ObjectIndexes();

        /// <summary>
        /// Create an <see cref="ExtendedObjects"/> with all relevant extended objects added.
        /// </summary>
        /// <param name="fairies">Whether or not to include Stray Fairy objects</param>
        /// <param name="skulltulas">Whether or not to include Skulltula Token objects</param>
        /// <returns>ExtendedObjects</returns>
        public static ExtendedObjects Create(bool fairies = false, bool skulltulas = false)
        {
            var result = new ExtendedObjects();
            result.AddExtendedObjects(fairies, skulltulas);
            return result;
        }

        /// <summary>
        /// Increment <see cref="LatestIndex"/> by an amount and return the previous value.
        /// </summary>
        /// <param name="amount">Amount to increment</param>
        /// <returns>Previous index value</returns>
        short AdvanceIndex(short amount = 1)
        {
            var index = this.LatestIndex;
            this.LatestIndex += amount;
            return index;
        }

        /// <summary>
        /// Get all offsets relative to a virtual ROM base address.
        /// </summary>
        /// <param name="baseAddress">Virtual ROM base address</param>
        /// <returns>Tuple with start and end addresses</returns>
        public (uint, uint)[] GetAddresses(uint baseAddress)
        {
            return this.Offsets.Select(pair => {
                return (baseAddress + pair.Item1, baseAddress + pair.Item2);
            }).ToArray();
        }

        /// <summary>
        /// Add all relevant extended objects.
        /// </summary>
        /// <param name="fairies">Whether or not to include Stray Fairy objects</param>
        /// <param name="skulltulas">Whether or not to include Skulltula Token objects</param>
        void AddExtendedObjects(bool fairies = false, bool skulltulas = false)
        {
            // Add Double Defense
            this.Offsets.Add(AddDoubleDefense());
            Indexes.DoubleDefense = AdvanceIndex();

            // Add Magic Power
            this.Offsets.Add(AddMagicPower());
            Indexes.MagicPower = AdvanceIndex();

            // Add Songs
            this.Offsets.Add(AddMusicNotes());
            this.Indexes.MusicNotes = AdvanceIndex();

            // Add Skulltula Tokens
            if (skulltulas)
            {
                AddAllSkulltulaTokens();
                this.Indexes.Skulltula = AdvanceIndex(2);
            }

            // Add Stray Fairies
            if (fairies)
            {
                AddAllStrayFairies();
                this.Indexes.Fairies = AdvanceIndex(5);
            }
        }

        /// <summary>
        /// Add Double Defense object.
        /// </summary>
        /// <returns>Offsets</returns>
        (uint, uint) AddDoubleDefense()
        {
            var data = CloneExistingData(726);

            // Exterior primary & env colors.
            WriteByte(data, 0x1294, 0xFF, 0xCF, 0x0F);
            WriteByte(data, 0x12B4, 0xFF, 0x46, 0x32);

            // Exterior combine mode.
            WriteUint(data, 0x12A8, 0xFC173C60, 0x150C937F);

            // Interior primary & env colors.
            WriteByte(data, 0x1474, 0xFF, 0xFF, 0xFF);
            WriteByte(data, 0x1494, 0xFF, 0xFF, 0xFF);

            return this.Bundle.Append(data);
        }

        /// <summary>
        /// Add Double Defense object.
        /// </summary>
        /// <returns>Offsets</returns>
        (uint, uint) AddMagicPower()
        {
            var data = CloneExistingData(736);

            // small magic jar
            WriteByte(data, 0x59C, 0xFF, 0xBD, 0x00); // ribbon 0xFF, 0xFF, 0xFF
            WriteByte(data, 0x654, 0x64, 0xFA, 0x64); // body 0x28, 0x64, 0x28
            WriteByte(data, 0x7BC, 0x19, 0x7D, 0x32); // cap 0x0A, 0x32, 0x14

            // large magic jar
            WriteByte(data, 0xEFC, 0xFF, 0xBD, 0x00); // ribbon 0xFF, 0xFF, 0x96
            WriteByte(data, 0xFB4, 0x64, 0xFA, 0x64); // body 0x28, 0x64, 0x28
            WriteByte(data, 0x119C, 0x19, 0x7D, 0x32); // cap 0x0A, 0x32, 0x14

            return this.Bundle.Append(data);
        }

        #region Stray Fairies

        /// <summary>
        /// Colors used for Stray Fairy object data.
        /// </summary>
        struct StrayFairyColors
        {
            public Color OuterPrimColor;
            public Color InnerPrimColor;
            public Color InnerEnvColor;
        }

        /// <summary>
        /// Add all Stray Fairy objects.
        /// </summary>
        void AddAllStrayFairies()
        {
            this.Offsets.Add(AddClockTownStrayFairy());
            this.Offsets.Add(AddWoodfallStrayFairy());
            this.Offsets.Add(AddSnowheadStrayFairy());
            this.Offsets.Add(AddGreatBayStrayFairy());
            this.Offsets.Add(AddStoneTowerStrayFairy());
        }

        /// <summary>
        /// Add a Stray Fairy object with specific colors.
        /// </summary>
        /// <param name="colors">Colors</param>
        /// <returns>Offsets</returns>
        (uint, uint) AddStrayFairy(StrayFairyColors colors)
        {
            var data = CloneExistingData(823);

            // Exterior primary color.
            WriteByte(data, 0xBEC, colors.OuterPrimColor.ToBytesRGB());

            // Interior combine mode.
            // Default is: 0xFC271C60 0x35FCF378
            // WriteUint(data, 0xEF8, colors.InnerCombine1, colors.InnerCombine2);

            // Interior primary & env colors.
            WriteByte(data, 0xF04, colors.InnerPrimColor.ToBytesRGB());
            WriteByte(data, 0xF0C, colors.InnerEnvColor.ToBytesRGB());

            return this.Bundle.Append(data);
        }

        /// <summary>
        /// Add an object for the Clock Town Stray Fairy.
        /// </summary>
        /// <returns>Offsets</returns>
        (uint, uint) AddClockTownStrayFairy()
        {
            var colors = new StrayFairyColors
            {
                OuterPrimColor = Color.FromArgb(0xFF, 0xA5, 0x00),
                InnerPrimColor = Color.FromArgb(0xFF, 0xFF, 0xDC),
                InnerEnvColor = Color.FromArgb(0xFF, 0x80, 0x00),
            };
            return AddStrayFairy(colors);
        }

        /// <summary>
        /// Add an object for Woodfall Stray Fairies.
        /// </summary>
        /// <returns>Offsets</returns>
        (uint, uint) AddWoodfallStrayFairy()
        {
            var colors = new StrayFairyColors
            {
                OuterPrimColor = Color.FromArgb(0xFF, 0x69, 0xB4),
                InnerPrimColor = Color.FromArgb(0xFF, 0xDC, 0xFF),
                InnerEnvColor = Color.FromArgb(0xFF, 0x00, 0x64),
            };
            return AddStrayFairy(colors);
        }

        /// <summary>
        /// Add an object for Snowhead Stray Fairies.
        /// </summary>
        /// <returns>Offsets</returns>
        (uint, uint) AddSnowheadStrayFairy()
        {
            var colors = new StrayFairyColors
            {
                OuterPrimColor = Color.FromArgb(0x00, 0xC0, 0x00),
                InnerPrimColor = Color.FromArgb(0xDC, 0xFF, 0xFF),
                InnerEnvColor = Color.FromArgb(0x00, 0xFF, 0x32),
            };
            return AddStrayFairy(colors);
        }

        /// <summary>
        /// Add an object for Great Bay Stray Fairies.
        /// </summary>
        /// <returns>Offsets</returns>
        (uint, uint) AddGreatBayStrayFairy()
        {
            var colors = new StrayFairyColors
            {
                OuterPrimColor = Color.FromArgb(0x18, 0x74, 0xCD),
                InnerPrimColor = Color.FromArgb(0xDC, 0xFF, 0xFF),
                InnerEnvColor = Color.FromArgb(0x00, 0x64, 0xFF),
            };
            return AddStrayFairy(colors);
        }

        /// <summary>
        /// Add an object for Stone Tower Stray Fairies.
        /// </summary>
        /// <returns>Offsets</returns>
        (uint, uint) AddStoneTowerStrayFairy()
        {
            var colors = new StrayFairyColors
            {
                OuterPrimColor = Color.FromArgb(0xFF, 0xFF, 0x00),
                InnerPrimColor = Color.FromArgb(0xFF, 0xFF, 0xDC),
                InnerEnvColor = Color.FromArgb(0xFF, 0xFF, 0x00),
            };
            return AddStrayFairy(colors);
        }

        #endregion

        #region Skulltula Tokens

        /// <summary>
        /// Add all Skulltula Token objects.
        /// </summary>
        void AddAllSkulltulaTokens()
        {
            this.Offsets.Add(AddSwampSkulltulaToken());
            this.Offsets.Add(AddOceanSkulltulaToken());
        }

        /// <summary>
        /// Add a Skulltula Token with a specific flame color.
        /// </summary>
        /// <param name="prim">Primitive color</param>
        /// <param name="env">Environment color</param>
        /// <returns>Offsets</returns>
        (uint, uint) AddSkulltulaToken(Color prim, Color env)
        {
            var data = CloneExistingData(808);

            WriteByte(data, 0x454, prim.ToBytesRGB());
            WriteByte(data, 0x45C, env.ToBytesRGB());

            return this.Bundle.Append(data);
        }

        /// <summary>
        /// Add an object for Ocean Spiderhouse Skulltula Tokens.
        /// </summary>
        /// <returns>Offsets</returns>
        (uint, uint) AddOceanSkulltulaToken()
        {
            var prim = Color.FromArgb(0xAA, 0xFF, 0xFF);
            var env = Color.FromArgb(0x00, 0x00, 0xFF);
            return AddSkulltulaToken(prim, env);
        }

        /// <summary>
        /// Add an object for Swamp Spiderhouse Skulltula Tokens.
        /// </summary>
        /// <returns>Offsets</returns>
        (uint, uint) AddSwampSkulltulaToken()
        {
            var prim = Color.FromArgb(0xAA, 0xFF, 0xFF);
            var env = Color.FromArgb(0x00, 0xFF, 0x00);
            return AddSkulltulaToken(prim, env);
        }

        #endregion

        #region Notes

        (uint, uint) AddMusicNotes()
        {
            var data = CloneExistingData(721);

            //WriteByte(data, 0xA84, ); // green // changing this didn't work for some reason
            //WriteByte(data, 0xA94, ); // unused red?
            WriteByte(data, 0xAA4, 0xFF, 0xFF, 0xFF); // white instead of blue
            WriteByte(data, 0xAB4, 0xFF, 0x32, 0x00); // red instead of orange
            //WriteByte(data, 0xAC4, ); // purple
            //WriteByte(data, 0xAD4, ); // unused yellow?
            
            return this.Bundle.Append(data);
        }

        #endregion

        #region Static Helper Functions

        /// <summary>
        /// Clone data from an existing <see cref="Models.Rom.MMFile"/>.
        /// </summary>
        /// <param name="fileIndex">Existing file index</param>
        /// <returns>Cloned data as bytes</returns>
        static byte[] CloneExistingData(int fileIndex)
        {
            RomUtils.CheckCompressed(fileIndex);
            var clone = RomData.MMFileList[fileIndex].Data.Clone();
            return (byte[])clone;
        }

        /// <summary>
        /// Write <see cref="byte"/> values to a buffer at a specific offset.
        /// </summary>
        /// <param name="data">Buffer</param>
        /// <param name="offset">Offset</param>
        /// <param name="values">Values to write</param>
        static void WriteByte(byte[] data, int offset, params byte[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                data[offset + i] = values[i];
            }
        }

        /// <summary>
        /// Write <see cref="uint"/> values to a buffer at a specific offset.
        /// </summary>
        /// <param name="data">Buffer</param>
        /// <param name="offset">Offset</param>
        /// <param name="values">Values to write</param>
        static void WriteUint(byte[] data, int offset, params uint[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                var cur = offset + (i * 4);
                var bytes = ConvertUtils.IntToBytes((int)values[i]);
                WriteByte(data, cur, bytes);
            }
        }

        #endregion
    }
}
