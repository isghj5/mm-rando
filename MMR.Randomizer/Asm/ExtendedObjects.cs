using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Models;
using MMR.Randomizer.Models.Rom;
using MMR.Randomizer.Utils;
using System;
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
        public ushort? RoyalWallet;
        public ushort? DoubleDefense;
        public ushort? MagicPower;
        public ushort? Fairies;
        public ushort? Skulltula;
        public ushort? MusicNotes;
        public ushort? Rupees;
        public ushort? Milk;
        public ushort? BossKeys;
        public ushort? SmallKeys;
        public ushort? Compasses;
        public ushort? DungeonMaps;
        public ushort? NotebookPage;
        public ushort? Frogs;
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
        public ushort LatestIndex { get; private set; } = 0x283;

        /// <summary>
        /// Object indexes.
        /// </summary>
        public ObjectIndexes Indexes { get; } = new ObjectIndexes();

        public List<MiscSmithyModel> SmithyModels { get; } = new List<MiscSmithyModel>();

        /// <summary>
        /// Attempt to resolve the extended object Id for a <see cref="GetItemEntry"/>.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns>Object Id if resolved.</returns>
        public ItemGraphic? ResolveGraphics(GetItemEntry entry)
        {
            // Notebook Pages
            if (entry.ItemGained == 0xB2 && entry.Object == 0x253 && Indexes.NotebookPage.HasValue)
            {
                return new ItemGraphic(Indexes.NotebookPage.Value, 0xC);
            }

            // Royal Wallet.
            if (entry.ItemGained == 0xA4 && entry.Object == 0xA8 && Indexes.RoyalWallet.HasValue)
            {
                return new ItemGraphic(Indexes.RoyalWallet.Value, 0x22);
            }

            // Milk Refill
            if (entry.ItemGained == 0xA0 && entry.Object == 0xB6 && Indexes.Milk.HasValue)
            {
                return new ItemGraphic(Indexes.Milk.Value, 0x31);
            }

            // Chateau Refill
            if (entry.ItemGained == 0x9F && entry.Object == 0x227 && Indexes.Milk.HasValue)
            {
                return new ItemGraphic(Indexes.Milk.Value, 0x32);
            }

            // Boss Keys
            if (entry.ItemGained == 0x74 && entry.Object == 0x92 && Indexes.BossKeys.HasValue)
            {
                var index = entry.Type >> 4;
                return new ItemGraphic((ushort)(Indexes.BossKeys.Value + index), entry.Index);
            }

            // Small Keys
            if (entry.ItemGained == 0x78 && entry.Object == 0x86 && Indexes.SmallKeys.HasValue)
            {
                var index = entry.Type >> 4;
                return new ItemGraphic((ushort)(Indexes.SmallKeys.Value + index), entry.Index);
            }

            // Compasses
            if (entry.ItemGained == 0x75 && entry.Object == 0x91 && Indexes.Compasses.HasValue)
            {
                var index = entry.Type >> 4;
                return new ItemGraphic((ushort)(Indexes.Compasses.Value + index), entry.Index);
            }

            // Dungeon Maps
            if (entry.ItemGained == 0x76 && entry.Object == 0xA0 && Indexes.DungeonMaps.HasValue)
            {
                var index = entry.Type >> 4;
                return new ItemGraphic((ushort)(Indexes.DungeonMaps.Value + index), entry.Index);
            }

            // set DrawItem function for Spin Attack
            if (entry.ItemGained == 0xA6 && entry.Object == 0x148)
            {
                return new ItemGraphic(0x148, 0x4B);
            }
            // Update gi-table for Skulltula Tokens.
            if (entry.ItemGained == 0x6E && entry.Object == 0x125 && Indexes.Skulltula != null)
            {
                var index = entry.Message == 0x51 ? 1 : 0;
                return new ItemGraphic((ushort)(Indexes.Skulltula.Value + index), entry.Index);
            }

            // Update gi-table for Stray Fairies.
            if (entry.ItemGained == 0xA8 && entry.Object == 0x13A && Indexes.Fairies != null)
            {
                var index = entry.Type >> 4;
                return new ItemGraphic((ushort)(Indexes.Fairies.Value + index), entry.Index);
            }

            // Update gi-table for Double Defense.
            if (entry.ItemGained == 0xA7 && entry.Object == 0x96 && Indexes.DoubleDefense != null)
            {
                return new ItemGraphic(Indexes.DoubleDefense.Value, entry.Index);
            }

            // Update gi-table for Notes.
            if (((entry.ItemGained >= 0x66 && entry.ItemGained <= 0x6C) || entry.ItemGained == 0x62 || entry.ItemGained == 0x73) && entry.Object == 0x8F && Indexes.MusicNotes != null)
            {
                return new ItemGraphic(Indexes.MusicNotes.Value, entry.Index);
            }

            // Update gi-table for Magic Power
            if (entry.ItemGained == 0xA5 && entry.Object == 0xA4 && Indexes.MagicPower != null)
            {
                return new ItemGraphic(Indexes.MagicPower.Value, entry.Index);
            }

            // Update gi-table for Extra Rupees
            if ((entry.ItemGained == 0xB1 || entry.ItemGained == 0xB5) && entry.Object == 0x13F && Indexes.Rupees != null)
            {
                return new ItemGraphic(Indexes.Rupees.Value, entry.Index);
            }

            // Frogs
            if (entry.ItemGained == 0xB4 && entry.Object == 0x266 && Indexes.Frogs.HasValue)
            {
                var index = (byte) ((entry.Type >> 4) switch
                {
                    1 => 0x47,
                    2 => 0x4D,
                    3 => 0x61,
                    4 => 0x62,
                    _ => throw new NotImplementedException()
                });
                return new ItemGraphic(Indexes.Frogs.Value, index);
            }

            return null;

            // TODO: Move behavior for resolving others into here.
        }

        /// <summary>
        /// Create an <see cref="ExtendedObjects"/> with all relevant extended objects added.
        /// </summary>
        /// <param name="fairies">Whether or not to include Stray Fairy objects</param>
        /// <param name="skulltulas">Whether or not to include Skulltula Token objects</param>
        /// <returns>ExtendedObjects</returns>
        public static ExtendedObjects Create(Item smithy1Item, Item smithy2Item, bool fairies = false, bool skulltulas = false, bool progressiveUpgrades = false)
        {
            var result = new ExtendedObjects();
            result.AddExtendedObjects(smithy1Item, smithy2Item, fairies, skulltulas, progressiveUpgrades);
            return result;
        }

        /// <summary>
        /// Increment <see cref="LatestIndex"/> by an amount and return the previous value.
        /// </summary>
        /// <param name="amount">Amount to increment</param>
        /// <returns>Previous index value</returns>
        ushort AdvanceIndex(ushort amount = 1)
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
        void AddExtendedObjects(Item smithy1Item, Item smithy2Item, bool fairies = false, bool skulltulas = false, bool progressiveUpgrades = false)
        {
            // Add Notebook Page
            this.Offsets.Add(AddNotebookPage());
            Indexes.NotebookPage = AdvanceIndex();

            // Add Royal Wallet.
            this.Offsets.Add(AddRoyalWallet());
            Indexes.RoyalWallet = AdvanceIndex();

            // Add Double Defense
            this.Offsets.Add(AddDoubleDefense());
            Indexes.DoubleDefense = AdvanceIndex();

            // Add Magic Power
            this.Offsets.Add(AddMagicPower());
            Indexes.MagicPower = AdvanceIndex();

            // Add Songs
            this.Offsets.Add(AddMusicNotes());
            this.Indexes.MusicNotes = AdvanceIndex();

            // Add Rupees
            this.Offsets.Add(AddRupees());
            this.Indexes.Rupees = AdvanceIndex();

            // Add Milk
            this.Offsets.Add(AddMilk());
            this.Indexes.Milk = AdvanceIndex();

            // Add Boss Keys
            AddBossKeys();
            this.Indexes.BossKeys = AdvanceIndex(4);

            // Add Small Keys
            AddSmallKeys();
            this.Indexes.SmallKeys = AdvanceIndex(4);

            // Add Compasses
            AddCompasses();
            this.Indexes.Compasses = AdvanceIndex(4);

            // Add Dungeon Maps
            AddDungeonMaps();
            this.Indexes.DungeonMaps = AdvanceIndex(4);

            // Add Frogs
            AddFrogs();
            this.Indexes.Frogs = AdvanceIndex();

            // Include Spin Attack Energy model into Kokiri Sword
            ObjUtils.InsertObj(Resources.models.gi_spinattack, 0x148);

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

            AddSmithyItems(smithy1Item, smithy2Item, progressiveUpgrades);
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

        #region Rupees

        (uint, uint) AddRupees()
        {
            var data = CloneExistingData(825);

            //WriteByte(data, 0x4AC + 0x20 * 0, 0xFF, 0xFF, 0xFF); // Green Primary
            //WriteByte(data, 0x4B4 + 0x20 * 0, 0xFF, 0xFF, 0xFF); // Green Env
            //WriteByte(data, 0x4AC + 0xC0 + 0x20 * 0, 0xFF, 0xFF, 0xFF); // Green Primary
            //WriteByte(data, 0x4B4 + 0xC0 + 0x20 * 0, 0xFF, 0xFF, 0xFF); // Green Env

            //WriteByte(data, 0x4AC + 0x20 * 1, 0xFF, 0xFF, 0xFF); // Blue Primary
            //WriteByte(data, 0x4B4 + 0x20 * 1, 0xFF, 0xFF, 0xFF); // Blue Env
            //WriteByte(data, 0x4AC + 0xC0 + 0x20 * 1, 0xFF, 0xFF, 0xFF); // Blue Primary
            //WriteByte(data, 0x4B4 + 0xC0 + 0x20 * 1, 0xFF, 0xFF, 0xFF); // Blue Env

            WriteByte(data, 0x4AC + 0x20 * 2, 0xFF, 0x84, 0x55); // Red Primary
            WriteByte(data, 0x4B4 + 0x20 * 2, 0x78, 0x00, 0x21); // Red Env
            WriteByte(data, 0x4AC + 0xC0 + 0x20 * 2, 0xFF, 0xE4, 0xC6); // Red Primary
            WriteByte(data, 0x4B4 + 0xC0 + 0x20 * 2, 0xCC, 0x00, 0x32); // Red Env

            //WriteByte(data, 0x4AC + 0x20 * 3, 0xFF, 0xFF, 0xFF); // Purple Primary
            //WriteByte(data, 0x4B4 + 0x20 * 3, 0xFF, 0xFF, 0xFF); // Purple Env
            //WriteByte(data, 0x4AC + 0xC0 + 0x20 * 3, 0xFF, 0xFF, 0xFF); // Purple Primary
            //WriteByte(data, 0x4B4 + 0xC0 + 0x20 * 3, 0xFF, 0xFF, 0xFF); // Purple Env

            WriteByte(data, 0x4AC + 0x20 * 4, 0x0, 0x0, 0x0); // Silver Primary
            WriteByte(data, 0x4B4 + 0x20 * 4, 0x0, 0x0, 0x0); // Silver Env
            WriteByte(data, 0x4AC + 0xC0 + 0x20 * 4, 0x0, 0x0, 0x0); // Silver Primary
            WriteByte(data, 0x4B4 + 0xC0 + 0x20 * 4, 0xFF, 0xFF, 0xFF); // Silver Env

            //WriteByte(data, 0x4AC + 0x20 * 5, 0xFF, 0xFF, 0xFF); // Gold Primary
            //WriteByte(data, 0x4B4 + 0x20 * 5, 0xFF, 0xFF, 0xFF); // Gold Env
            //WriteByte(data, 0x4AC + 0xC0 + 0x20 * 5, 0xFF, 0xFF, 0xFF); // Gold Primary
            //WriteByte(data, 0x4B4 + 0xC0 + 0x20 * 5, 0xFF, 0xFF, 0xFF); // Gold Env

            return this.Bundle.Append(data);
        }

        #endregion

        #region Milk

        (uint, uint) AddMilk()
        {
            var data = CloneExistingData(752);

            // Jar
            WriteByte(data, 0x1270 + 0xC, 0xFF, 0xFF, 0xFF); // Green Primary
            WriteByte(data, 0x1270 + 0x14, 0x64, 0x64, 0x64); // Green Env

            // Liquid
            WriteByte(data, 0x12D0 + 0xC, 0xFF, 0xFF, 0xFF); // Green Primary 2
            WriteByte(data, 0x12D0 + 0x14, 0xFF, 0xFF, 0xFF); // Green Env 2

            // Pattern
            WriteByte(data, 0x1330 + 0xC, 0, 0x20, 0xFF); // Green Primary 3
            WriteByte(data, 0x1330 + 0x14, 0, 0x20, 0xFF); // Green Env 3

            // Jar
            WriteByte(data, 0x1270 + 0x20 * 1 + 0xC, 0xFF, 0xFF, 0xFF); // Red Primary
            WriteByte(data, 0x1270 + 0x20 * 1 + 0x14, 0x64, 0x64, 0x64); // Red Env

            // Liquid
            WriteByte(data, 0x12D0 + 0x20 * 1 + 0xC, 0xFF, 0xFF, 0xFF); // Red Primary 2
            WriteByte(data, 0x12D0 + 0x20 * 1 + 0x14, 0xFF, 0xFF, 0xFF); // Red Env 2

            // Pattern
            WriteByte(data, 0x1330 + 0x58 * 1 + 0xC, 0x6E, 0x46, 0x00); // Red Primary 3
            WriteByte(data, 0x1330 + 0x58 * 1 + 0x14, 0x69, 0x0, 0x50); // Red Env 3

            // ENDDL before drawing spoon. Affects green, red and blue variants.
            WriteByte(data, 0x1698, 0xDF);

            return this.Bundle.Append(data);
        }

        #endregion

        #region Boss Keys

        (uint, uint) AddBossKey(Color env, Color prim)
        {
            var data = CloneExistingData(724);

            WriteByte(data, 0xF24, prim.ToBytesRGB());
            WriteByte(data, 0xF2C, env.ToBytesRGB());

            return this.Bundle.Append(data);
        }

        void AddBossKeys()
        {
            this.Offsets.Add(AddBossKey(Color.FromArgb(0xFF, 0x00, 0x64), Color.FromArgb(0xFF, 0xAA, 0xFF))); // Woodfall
            this.Offsets.Add(AddBossKey(Color.FromArgb(0x00, 0xFF, 0x32), Color.FromArgb(0xAA, 0xFF, 0xAA))); // Snowhead
            this.Offsets.Add(AddBossKey(Color.FromArgb(0x00, 0x64, 0xFF), Color.FromArgb(0xAA, 0xFF, 0xFF))); // Great Bay
            this.Offsets.Add(AddBossKey(Color.FromArgb(0xFF, 0xFF, 0x00), Color.FromArgb(0xFF, 0xFF, 0xAA))); // Stone Tower
        }

        #endregion

        #region Small Keys

        (uint, uint) AddSmallKey(Color prim, Color env)
        {
            var data = CloneExistingData(716);

            WriteByte(data, 0x81C, prim.ToBytesRGB());
            WriteByte(data, 0x824, env.ToBytesRGB());

            return this.Bundle.Append(data);
        }

        void AddSmallKeys()
        {
            this.Offsets.Add(AddSmallKey(Color.FromArgb(0xFF, 0xAA, 0xFF), Color.FromArgb(0x73, 0x00, 0x73))); // Woodfall
            this.Offsets.Add(AddSmallKey(Color.FromArgb(0xAA, 0xFF, 0xAA), Color.FromArgb(0x00, 0x73, 0x00))); // Snowhead
            this.Offsets.Add(AddSmallKey(Color.FromArgb(0xAA, 0xFF, 0xFF), Color.FromArgb(0x00, 0x73, 0x73))); // Great Bay
            this.Offsets.Add(AddSmallKey(Color.FromArgb(0xFF, 0xFF, 0x00), Color.FromArgb(0x73, 0x73, 0x00))); // Stone Tower
        }

        #endregion

        #region Compasses

        (uint, uint) AddCompass(Color prim, Color env)
        {
            var data = CloneExistingData(723);

            WriteByte(data, 0x97C, prim.ToBytesRGB());
            WriteByte(data, 0x984, env.ToBytesRGB());

            return this.Bundle.Append(data);
        }

        void AddCompasses()
        {
            this.Offsets.Add(AddCompass(Color.FromArgb(0x96, 0x32, 0x96), Color.FromArgb(0x28, 0x14, 0x28))); // Woodfall
            this.Offsets.Add(AddCompass(Color.FromArgb(0x32, 0x96, 0x32), Color.FromArgb(0x14, 0x28, 0x14))); // Snowhead
            this.Offsets.Add(AddCompass(Color.FromArgb(0x32, 0x96, 0x96), Color.FromArgb(0x14, 0x28, 0x28))); // Great Bay
            this.Offsets.Add(AddCompass(Color.FromArgb(0x96, 0x96, 0x00), Color.FromArgb(0x28, 0x28, 0x00))); // Stone Tower
        }

        #endregion

        #region Dungeon Maps

        (uint, uint) AddDungeonMap(Color prim, Color env)
        {
            var data = CloneExistingData(733);

            WriteByte(data, 0x3DC, prim.ToBytesRGB());
            WriteByte(data, 0x3E4, env.ToBytesRGB());

            return this.Bundle.Append(data);
        }

        void AddDungeonMaps()
        {
            this.Offsets.Add(AddDungeonMap(Color.FromArgb(0x96, 0x32, 0x96), Color.FromArgb(0x28, 0x14, 0x28))); // Woodfall
            this.Offsets.Add(AddDungeonMap(Color.FromArgb(0x32, 0x96, 0x32), Color.FromArgb(0x14, 0x28, 0x14))); // Snowhead
            this.Offsets.Add(AddDungeonMap(Color.FromArgb(0x32, 0x96, 0x96), Color.FromArgb(0x14, 0x28, 0x28))); // Great Bay
            this.Offsets.Add(AddDungeonMap(Color.FromArgb(0x96, 0x96, 0x00), Color.FromArgb(0x28, 0x28, 0x00))); // Stone Tower
        }

        #endregion

        #region Royal Wallet

        (uint, uint) AddRoyalWallet()
        {
            var data = CloneExistingData(739);

            WriteByte(data, 0x177C, 0xFF, 0xFF, 0xFF); // Wallet exterior prim.
            WriteByte(data, 0x1784, 0xD0, 0xB0, 0xFF); // Wallet exterior env.
            WriteByte(data, 0x17FC, 0xA0, 0x40, 0xFF); // Rupee exterior prim.
            WriteByte(data, 0x1804, 0x50, 0x00, 0xC0); // Rupee exterior env.
            WriteByte(data, 0x181C, 0x80, 0x00, 0xA0); // Rope color prim.
            WriteByte(data, 0x1824, 0x20, 0x20, 0x20); // Rope color env.
            WriteByte(data, 0x183C, 0xA0, 0x40, 0xFF); // Rupee interior prim.
            WriteByte(data, 0x1844, 0xFF, 0xC0, 0xFF); // Rupee interior env.

            return this.Bundle.Append(data);
        }

        #endregion

        #region Smithy Items

        private Dictionary<byte, int[]> _displayListsToIgnore = new Dictionary<byte, int[]>
        {
            { 10, new int[] { 2 } }, // ignore compass glass
            { 66, new int[] { 3, 4 } }, // ignore gold rupee glow
            { 78, new int[] { 3 } }, // ignore stray fairy model's sprite
            { 79, new int[] { 3, 4 } }, // ignore green rupee glow
            { 80, new int[] { 3, 4 } }, // ignore blue rupee glow
            { 81, new int[] { 3, 4 } }, // ignore red rupee glow
            { 83, new int[] { 3, 4 } }, // ignore purple rupee glow
            { 84, new int[] { 3, 4 } }, // ignore silver rupee glow
            { 89, new int[] { 2 } }, // moons tear glow
            { 98, new int[] { 3 } }, // seahorse glow
        };

        private Dictionary<byte, int[]> _verticesToIgnore = new Dictionary<byte, int[]>
        {
            { 74, new int[] { 2 } }, // skip skulltula token flame
        };

        ((uint, uint), MiscSmithyModel) AddSmithyItem(Item item)
        {
            var index = item.GetItemIndex();
            if (index.HasValue)
            {
                return AddSmithyItem(index.Value);
            }
            var getItem = item.ExclusiveItemEntry();
            return AddSmithyItem(new ItemGraphic(getItem.Object, getItem.Index));
        }

        ((uint, uint), MiscSmithyModel) AddSmithyItem(ushort giIndex)
        {
            var giEntry = RomData.GetItemList[giIndex];
            var objectId = giEntry.Object;
            var graphicId = giEntry.Index;

            var graphics = ResolveGraphics(giEntry);
            if (graphics.HasValue)
            {
                objectId = graphics.Value.ObjectId;
                graphicId = graphics.Value.GraphicId;
            }

            return AddSmithyItem(new ItemGraphic(objectId, graphicId));
        }

        ((uint, uint), MiscSmithyModel) AddSmithyItem(ItemGraphic itemGraphic)
        {
            var objectId = itemGraphic.ObjectId;
            var graphicId = itemGraphic.GraphicId;

            var objectToLoad = objectId;

            if (objectToLoad == 0)
            {
                // Assume Boss Remains
                objectToLoad = 0x1CC;
            }

            graphicId = (byte)((graphicId >= 0x80 ? 0x100 - graphicId : graphicId) - 1);
            var codeFile = ReadWriteUtils.GetFile(FileIndex.code);
            byte[] objectData;
            if (objectToLoad < 0x283)
            {
                var objectFileTableOffset = 0x11CC80;
                var objectAddress = ReadWriteUtils.Arr_ReadS32(codeFile.Data, objectFileTableOffset + objectToLoad * 8);
                var objectFileNumber = RomUtils.AddrToFile(objectAddress);
                objectData = GetExistingData(objectFileNumber);
            }
            else
            {
                objectData = this.Bundle.Get(objectToLoad - 0x283);
            }
            var drawItemTableOffset = 0x1156B0;
            var displayListIndex = 1;
            int displayListOffset = ReadWriteUtils.Arr_ReadS32(codeFile.Data, drawItemTableOffset + graphicId * 0x24 + displayListIndex * 4) & 0xFFFFFF;
            var vertices = new List<byte[]>();
            var displayLists = new List<byte[]>();
            var displayListsToIgnore = _displayListsToIgnore.GetValueOrDefault(graphicId);
            int[] verticesToIgnore = null;
            if (objectId != 0x148) // spin attack reuses spider token drawTable entry
            {
                verticesToIgnore = _verticesToIgnore.GetValueOrDefault(graphicId);
            }
            var vertexCommandCount = 0;
            var ignoringTriangles = false;
            while (displayListOffset != 0)
            {
                if (displayListsToIgnore?.Contains(displayListIndex) != true)
                {
                    byte displayListType;
                    var displayListEntryOffset = 0;
                    var displayListStack = new Stack<(int listOffset, int entryOffset)>();
                    do
                    {
                        var displayList = new byte[8];
                        ReadWriteUtils.Arr_Insert(objectData, displayListOffset + displayListEntryOffset, 8, displayList, 0);
                        displayListType = displayList[0];
                        switch (displayListType)
                        {
                            case 0xDE: // gsSPDisplayList
                                if (displayList[4] == 6)
                                {
                                    var addToStack = displayList[1] == 0;
                                    var newOffset = ReadWriteUtils.Arr_ReadS32(displayList, 4) & 0xFFFFFF;
                                    if (addToStack)
                                    {
                                        displayListStack.Push((displayListOffset, displayListEntryOffset));
                                    }
                                    displayListOffset = newOffset;
                                    displayListEntryOffset = 0;
                                    continue;
                                }
                                break;
                            case 0xDF: // gsSPEndDisplayList
                                if (displayListStack.Count > 0)
                                {
                                    var stackItem = displayListStack.Pop();
                                    displayListOffset = stackItem.listOffset;
                                    displayListEntryOffset = stackItem.entryOffset + 8;
                                    displayListType = 0;
                                    continue;
                                }
                                break;
                            case 1: // gsSPVertex
                                if (verticesToIgnore?.Contains(vertexCommandCount) == true)
                                {
                                    ignoringTriangles = true;
                                    vertexCommandCount++;
                                    break;
                                }
                                else
                                {
                                    vertexCommandCount++;
                                    ignoringTriangles = false;
                                }
                                var numVertices = (ReadWriteUtils.Arr_ReadU16(displayList, 1) & 0x0FF0) >> 4;
                                var verticesOffset = ReadWriteUtils.Arr_ReadS32(displayList, 4) & 0xFFFFFF;
                                var newVerticesOffset = vertices.Count * 0x10;
                                for (var i = 0; i < numVertices; i++)
                                {
                                    var vertex = new byte[0x10];
                                    ReadWriteUtils.Arr_Insert(objectData, verticesOffset + i * 0x10, 0x10, vertex, 0);
                                    vertices.Add(vertex);
                                }
                                ReadWriteUtils.Arr_WriteU32(displayList, 4, (uint)(newVerticesOffset | 0x06000000));
                                displayLists.Add(displayList);
                                break;
                            case 6: // gsSP2Triangles
                            case 5: // gsSP1Triangle
                                if (!ignoringTriangles)
                                {
                                    displayLists.Add(displayList);
                                }
                                break;
                        }
                        displayListEntryOffset += 8;
                    } while (displayListType != 0xDF); // gsSPEndDisplayList
                }

                if (displayListIndex == 8)
                {
                    break;
                }

                displayListIndex++;
                displayListOffset = ReadWriteUtils.Arr_ReadS32(codeFile.Data, drawItemTableOffset + graphicId * 0x24 + displayListIndex * 4) & 0xFFFFFF;
            }

            displayLists.Add(new byte[] { 0xDF, 0, 0, 0, 0, 0, 0, 0 });

            var result = new List<byte>();

            var smithy = GetExistingData(958);

            // copied vertices
            result.AddRange(vertices.SelectMany(x => x));

            // texture from smithy
            var textureOffset = (ushort)result.Count;
            result.AddRange(smithy.Skip(0xF6B0).Take(0x100));

            var displayListEntry = (ushort)result.Count;

            

            // smithy setup
            var smithySetup = smithy.Skip(0xE8F0).Take(0xC0).ToArray();
            ReadWriteUtils.Arr_WriteU16(smithySetup, 0x46, textureOffset);
            ReadWriteUtils.Arr_WriteU16(smithySetup, 0x7E, textureOffset);
            result.AddRange(smithySetup);

            result.AddRange(displayLists.SelectMany(x => x));

            if (result.Count % 0x10 != 0)
            {
                result.AddRange(Enumerable.Repeat<byte>(0, 8));
            }

            var smithyModel = new MiscSmithyModel(objectId, (byte)(graphicId + 1), (ushort)AdvanceIndex(), displayListEntry);

            return (this.Bundle.Append(result.ToArray()), smithyModel);
        }

        private List<List<Item>> _progressiveItemsList = new List<List<Item>>
        {
            new List<Item> { Item.StartingSword, Item.UpgradeRazorSword, Item.UpgradeGildedSword },
            new List<Item> { Item.FairyMagic, Item.FairyDoubleMagic },
            new List<Item> { Item.UpgradeAdultWallet, Item.UpgradeGiantWallet, Item.UpgradeRoyalWallet },
            new List<Item> { Item.ItemBombBag, Item.UpgradeBigBombBag, Item.UpgradeBiggestBombBag },
            new List<Item> { Item.ItemBow, Item.UpgradeBigQuiver, Item.UpgradeBiggestQuiver },
        };

        private Dictionary<Item, ushort> _additionalItemsDict = new Dictionary<Item, ushort>
        {
            { Item.ItemBottleMadameAroma, 0x91 },
            { Item.ItemBottleAliens, 0x92 },
            { Item.ItemBottleWitch, 0x5B },
            { Item.ItemBottleGoronRace, 0x93 },
        };

        void AddSmithyItemWithAdditionals(Item smithyItem, bool progressiveUpgrades)
        {
            var smithy = AddSmithyItem(smithyItem);
            this.Offsets.Add(smithy.Item1);
            SmithyModels.Add(smithy.Item2);

            if (progressiveUpgrades)
            {
                var progressiveItems = _progressiveItemsList.FirstOrDefault(l => l.Contains(smithyItem));
                if (progressiveItems != null)
                {
                    foreach (var item in progressiveItems.Where(item => item != smithyItem))
                    {
                        var itemResult = AddSmithyItem(item);
                        this.Offsets.Add(itemResult.Item1);
                        SmithyModels.Add(itemResult.Item2);
                    }
                }
            }

            if (_additionalItemsDict.ContainsKey(smithyItem))
            {
                var itemResult = AddSmithyItem(_additionalItemsDict[smithyItem]);
                this.Offsets.Add(itemResult.Item1);
                SmithyModels.Add(itemResult.Item2);
            }
        }

        void AddSmithyItems(Item smithy1Item, Item smithy2Item, bool progressiveUpgrades)
        {
            var heart = AddSmithyItem(Item.RecoveryHeart);
            this.Offsets.Add(heart.Item1);
            SmithyModels.Add(heart.Item2);

            AddSmithyItemWithAdditionals(smithy1Item, progressiveUpgrades);

            AddSmithyItemWithAdditionals(smithy2Item, progressiveUpgrades);
        }

        #endregion

        #region Notebook Page

        (uint, uint) AddNotebookPage()
        {
            // Clone bombers notebook model
            var data = CloneExistingData(1066);

            // Remove cover label
            WriteUint(data, 0xB00,
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0
            );

            return this.Bundle.Append(data);
        }

        #endregion

        #region Frogs

        (uint, uint) AddFrog()
        {
            return this.Bundle.Append(Resources.models.gi_frog);
        }

        void AddFrogs()
        {
            this.Offsets.Add(AddFrog());

            // Change unused getItem draw entries for use with frog model

            // Removed as yellow frog is not randomized.
            // ID 0x38, replaces null entry, yellow frog
            // ReadWriteUtils.WriteU32ToROM(0xB3C000 + 0x115E70, 0x06000000);

            //ID 0x47, replaces null entry, cyan frog
            ReadWriteUtils.WriteU32ToROM(0xB3C000 + 0x116088, 0x800EF054);
            ReadWriteUtils.WriteU32ToROM(0xB3C000 + 0x11608C, 0x06000010);

            // ID 0x4D, replaces null entry, pink frog
            ReadWriteUtils.WriteU32ToROM(0xB3C000 + 0x116160, 0x800EF054);
            ReadWriteUtils.WriteU32ToROM(0xB3C000 + 0x116164, 0x06000020);

            // ID 0x61, replaces unused bottled seahorse model, blue frog
            ReadWriteUtils.WriteU32ToROM(0xB3C000 + 0x116430, 0x800EF054);
            ReadWriteUtils.WriteU32ToROM(0xB3C000 + 0x116434, 0x06000030);
            ReadWriteUtils.WriteU32ToROM(0xB3C000 + 0x116438, 0);

            // ID 0x62, replaces unused bottled loach model, white frog
            ReadWriteUtils.WriteU32ToROM(0xB3C000 + 0x116454, 0x800EF054);
            ReadWriteUtils.WriteU32ToROM(0xB3C000 + 0x116458, 0x06000040);
            ReadWriteUtils.WriteU32ToROM(0xB3C000 + 0x11645C, 0);
        }

        #endregion

        #region Static Helper Functions

        static byte[] GetExistingData(int fileIndex)
        {
            RomUtils.CheckCompressed(fileIndex);
            return RomData.MMFileList[fileIndex].Data;
        }

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
