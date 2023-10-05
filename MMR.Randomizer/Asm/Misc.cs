using Be.IO;
using MMR.Common.Extensions;
using MMR.Randomizer.Models;
using MMR.Randomizer.Models.Settings;
using MMR.Randomizer.Utils;
using System.Collections.Generic;
using System.IO;

namespace MMR.Randomizer.Asm
{
    /// <summary>
    /// Crit wiggle state value.
    /// </summary>
    public enum CritWiggleState : byte
    {
        Default,
        AlwaysOn,
        AlwaysOff,
    }

    public enum QuestConsumeState : byte
    {
        Default,
        Always,
        Never,
    }

    public enum AutoInvertState : byte
    {
        Never,
        FirstCycle,
        Always,
    }

    public enum ChestGameMinimapState : byte
    {
        Off,
        Minimal,
        ConditionalSpoiler,
        Spoiler,
    }

    /// <summary>
    /// Speedups.
    /// </summary>
    public class MiscSpeedups
    {
        /// <summary>
        /// Whether or not to end Blast Mask Thief escape sequence early once the luggage is dropped.
        /// </summary>
        public bool BlastMaskThief { get; set; }

        /// <summary>
        /// Whether or not to end Boat Archery early if the player has enough points.
        /// </summary>
        public bool BoatArchery { get; set; }

        /// <summary>
        /// Whether or not to end Fisherman's Game early if the player has enough points.
        /// </summary>
        public bool FishermanGame { get; set; }

        /// <summary>
        /// Whether or not to change Sound Check to speed up actor cutscenes until song is fully composed.
        /// </summary>
        public bool SoundCheck { get; set; }

        /// <summary>
        /// Whether or not to change the hungry Goron to set a different value when rolling away and add more coordinates to his path.
        /// </summary>
        public bool DonGero { get; set; }

        /// <summary>
        /// Whether or not inputting Z/R should be handled during bank withdraw/deposit.
        /// </summary>
        public bool FastBankRupees { get; set; }

        /// <summary>
        /// Whether or not to grant both archery rewards with a sufficient score when relevant.
        /// </summary>
        public bool DoubleArcheryRewards { get; set; }

        /// <summary>
        /// Whether or not to grant multiple bank deposit rewards for each threshold passed.
        /// </summary>
        public bool BankMultiRewards { get; set; } = true;

        /// <summary>
        /// Whether or not chests should always use the short opening animation.
        /// </summary>
        public bool ShortChestOpening { get; set; }

        /// <summary>
        /// Whether or not the Treasure Chest Game draws a spoiler minimap.
        /// </summary>
        public ChestGameMinimapState ChestGameMinimap { get; set; }

        /// <summary>
        /// Whether or not the Giant's Cutscene Skip is enabled.
        /// </summary>
        public bool SkipGiantsCutscene { get; set; }

        /// <summary>
        /// Whether or not to show the Oath Hint cutscene when SkipGiantsCutscene is enabled.
        /// </summary>
        public bool OathHint { get; set; }

        /// <summary>
        /// Convert to a <see cref="uint"/> integer.
        /// </summary>
        /// <returns>Integer</returns>
        public uint ToInt()
        {
            uint flags = 0;
            flags |= (this.SoundCheck ? (uint)1 : 0) << 31;
            flags |= (this.BlastMaskThief ? (uint)1 : 0) << 30;
            flags |= (this.FishermanGame ? (uint)1 : 0) << 29;
            flags |= (this.BoatArchery ? (uint)1 : 0) << 28;
            flags |= (this.DonGero ? (uint)1 : 0) << 27;
            flags |= (this.FastBankRupees ? (uint)1 : 0) << 26;
            flags |= (this.DoubleArcheryRewards ? (uint)1 : 0) << 25;
            flags |= (this.BankMultiRewards ? (uint)1 : 0) << 24;
            flags |= (this.ShortChestOpening ? (uint)1 : 0) << 23;
            flags |= (((uint)this.ChestGameMinimap) & 3) << 21;
            flags |= (this.SkipGiantsCutscene ? (uint)1 : 0) << 20;
            flags |= (this.OathHint ? (uint)1 : 0) << 19;
            return flags;
        }
    }

    /// <summary>
    /// Miscellaneous flags.
    /// </summary>
    public class MiscFlags
    {
        /// <summary>
        /// Whether or not to enable cycling arrow types while using the bow.
        /// </summary>
        public bool ArrowCycling { get; set; } = true;

        /// <summary>
        /// Whether or not to show magic being consumed ahead of time when using elemental arrows.
        /// </summary>
        public bool ArrowMagic => ArrowCycling;

        /// <summary>
        /// Behaviour of crit wiggle.
        /// </summary>
        public CritWiggleState CritWiggle { get; set; }

        /// <summary>
        /// Whether or not to disable crit wiggle (alternative state is default behaviour).
        /// </summary>
        public bool CritWiggleDisable {
            get { return (this.CritWiggle == CritWiggleState.AlwaysOff) ? true : false; }
            set { this.CritWiggle = (value ? CritWiggleState.AlwaysOff : CritWiggleState.Default); }
        }

        /// <summary>
        /// Whether or not to use the closest cow to the player when giving an item.
        /// </summary>
        public bool CloseCows { get; set; } = true;

        /// <summary>
        /// Whether or not to draw hash icons on the file select screen.
        /// </summary>
        public bool DrawHash { get; set; } = true;

        /// <summary>
        /// Whether or not to activate beach cutscene earlier when pushing Mikau to shore.
        /// </summary>
        public bool EarlyMikau { get; set; }

        /// <summary>
        /// Whether or not to make Stray Fairy chests behave like normal chests.
        /// </summary>
        public bool FairyChests { get; set; }

        /// <summary>
        /// Whether or not to show health bars when targeting an enemy.
        /// </summary>
        public bool TargetHealth { get; set; }

        /// <summary>
        /// Whether or not to allow player to climb anywhere. (Combined with additional hacks)
        /// </summary>
        public bool ClimbAnything { get; set; }

        /// <summary>
        /// Whether or not to apply Elegy of Emptiness speedups.
        /// </summary>
        public bool ElegySpeedup { get; set; } = true;

        /// <summary>
        /// Whether or not to enable faster pushing and pulling speeds.
        /// </summary>
        public bool FastPush { get; set; } = true;

        /// <summary>
        /// Whether or not to enable freestanding models.
        /// </summary>
        //public bool FreestandingModels { get; set; } = true;

        /// <summary>
        /// Whether or not to enable continuous deku hopping.
        /// </summary>
        public bool ContinuousDekuHopping { get; set; } = false;

        public bool IronGoron { get; set; } = false;

        /// <summary>
        /// Whether or not to enable shop models.
        /// </summary>
        //public bool ShopModels { get; set; } = true;

        /// <summary>
        /// Whether or not to enable progressive upgrades.
        /// </summary>
        public bool ProgressiveUpgrades { get; set; } = true;

        /// <summary>
        /// Whether or not traps should behave slightly differently from other items in certain situations.
        /// </summary>
        public bool TrapQuirks { get; set; }

        /// <summary>
        /// Whether or not to allow using the ocarina underwater.
        /// </summary>
        public bool OcarinaUnderwater { get; set; } = true;

        /// <summary>
        /// Behaviour of how quest items should be consumed.
        /// </summary>
        public QuestConsumeState QuestConsume => this.QuestItemStorage ? QuestConsumeState.Always : QuestConsumeState.Default;

        /// <summary>
        /// Whether or not to enable Quest Item Storage.
        /// </summary>
        public bool QuestItemStorage { get; set; } = true;

        /// <summary>
        /// Whether or not to enable spawning scarecrow without Scarecrow's Song.
        /// </summary>
        public bool FreeScarecrow { get; set; }

        /// <summary>
        /// Whether or not to max out rupees when finding a wallet upgrade.
        /// </summary>
        public bool FillWallet { get; set; }

        /// <summary>
        /// Whether or not time should be auto-inverted at the start of a cycle.
        /// </summary>
        public AutoInvertState AutoInvert { get; set; }

        /// <summary>
        /// Whether or not hit tags and invisible rupees should sparkle.
        /// </summary>
        public bool HiddenRupeesSparkle { get; set; }

        /// <summary>
        /// Whether or not to fix some code to prevent crashes when using various glitches.
        /// </summary>
        public bool SaferGlitches { get; set; } = true;

        /// <summary>
        /// Whether or not Bombchu should be able to be dropped.
        /// </summary>
        public bool BombchuDrops { get; set; }

        /// <summary>
        /// Whether or not instant transformation should be enabled.
        /// </summary>
        public bool InstantTransform { get; set; }

        /// <summary>
        /// Whether or not bomb arrows should be enabled.
        /// </summary>
        public bool BombArrows { get; set; }

        /// <summary>
        /// Whether or to enable logic needed for Giant Mask Anywhere to work.
        /// </summary>
        public bool GiantMaskAnywhere { get; set; }

        public bool FewerHealthDrops { get; set; }

        public MiscFlags()
        {
        }

        public MiscFlags(uint flags)
        {
            Load(flags);
        }

        /// <summary>
        /// Load from a <see cref="uint"/> integer.
        /// </summary>
        /// <param name="flags">Flags integer</param>
        void Load(uint flags)
        {
            this.CritWiggle = (CritWiggleState)(flags >> 30);
            this.DrawHash = ((flags >> 29) & 1) == 1;
            this.FastPush = ((flags >> 28) & 1) == 1;
            this.OcarinaUnderwater = ((flags >> 27) & 1) == 1;
            this.QuestItemStorage = ((flags >> 26) & 1) == 1;
            this.CloseCows = ((flags >> 25) & 1) == 1;
            this.ArrowCycling = ((flags >> 22) & 1) == 1;
            this.ContinuousDekuHopping = ((flags >> 19) & 1) == 1;
            this.ProgressiveUpgrades = ((flags >> 18) & 1) == 1;
            this.TrapQuirks = ((flags >> 17) & 1) == 1;
            this.EarlyMikau = ((flags >> 16) & 1) == 1;
            this.FairyChests = ((flags >> 15) & 1) == 1;
            this.TargetHealth = ((flags >> 14) & 1) == 1;
            this.ClimbAnything = ((flags >> 13) & 1) == 1;
            this.FreeScarecrow = ((flags >> 12) & 1) == 1;
            this.FillWallet = ((flags >> 11) & 1) == 1;
            this.AutoInvert = (AutoInvertState)((flags >> 9) & 3);
            this.HiddenRupeesSparkle = ((flags >> 8) & 1) == 1;
            this.SaferGlitches = ((flags >> 7) & 1) == 1;
            this.BombchuDrops = ((flags >> 6) & 1) == 1;
            this.InstantTransform = ((flags >> 5) & 1) == 1;
            this.BombArrows = ((flags >> 4) & 1) == 1;
            this.GiantMaskAnywhere = ((flags >> 3) & 1) == 1;
            this.FewerHealthDrops = ((flags >> 2) & 1) == 1;
            this.IronGoron = ((flags >> 1) & 1) == 1;
        }

        /// <summary>
        /// Convert to a <see cref="uint"/> integer.
        /// </summary>
        /// <returns>Integer</returns>
        public uint ToInt()
        {
            uint flags = 0;
            flags |= (((uint)this.CritWiggle) & 3) << 30;
            flags |= (this.DrawHash ? (uint)1 : 0) << 29;
            flags |= (this.FastPush ? (uint)1 : 0) << 28;
            flags |= (this.OcarinaUnderwater ? (uint)1 : 0) << 27;
            flags |= (this.QuestItemStorage ? (uint)1 : 0) << 26;
            flags |= (this.CloseCows ? (uint)1 : 0) << 25;
            flags |= (((uint)this.QuestConsume) & 3) << 23;
            flags |= (this.ArrowCycling ? (uint)1 : 0) << 22;
            flags |= (this.ArrowMagic ? (uint)1 : 0) << 21;
            flags |= (this.ElegySpeedup ? (uint)1 : 0) << 20;
            flags |= (this.ContinuousDekuHopping ? (uint)1 : 0) << 19;
            flags |= (this.ProgressiveUpgrades ? (uint)1 : 0) << 18;
            flags |= (this.TrapQuirks ? (uint)1 : 0) << 17;
            flags |= (this.EarlyMikau ? (uint)1 : 0) << 16;
            flags |= (this.FairyChests ? (uint)1 : 0) << 15;
            flags |= (this.TargetHealth ? (uint)1 : 0) << 14;
            flags |= (this.ClimbAnything ? (uint)1 : 0) << 13;
            flags |= (this.FreeScarecrow ? (uint)1 : 0) << 12;
            flags |= (this.FillWallet ? (uint)1 : 0) << 11;
            flags |= (((uint)this.AutoInvert) & 3) << 9;
            flags |= (this.HiddenRupeesSparkle ? (uint)1 : 0) << 8;
            flags |= (this.SaferGlitches ? (uint)1 : 0) << 7;
            flags |= (this.BombchuDrops ? (uint)1 : 0) << 6;
            flags |= (this.InstantTransform ? (uint)1 : 0) << 5;
            flags |= (this.BombArrows ? (uint)1 : 0) << 4;
            flags |= (this.GiantMaskAnywhere ? (uint)1 : 0) << 3;
            flags |= (this.FewerHealthDrops ? (uint)1 : 0) << 2;
            flags |= (this.IronGoron ? (uint)1 : 0) << 1;
            return flags;
        }
    }

    /// <summary>
    /// Internal flags.
    /// </summary>
    public class InternalFlags
    {
        /// <summary>
        /// Whether or not Vanilla Layout is being used, which determines if certain mod files are included.
        /// </summary>
        public bool VanillaLayout { get; set; }

        /// <summary>
        /// Convert to a <see cref="uint"/> integer.
        /// </summary>
        /// <returns>Integer</returns>
        public uint ToInt()
        {
            uint flags = 0;
            flags |= (this.VanillaLayout ? (uint)1 : 0) << 31;
            return flags;
        }
    }

    public class MiscShorts
    {
        public ushort CollectableTableFileIndex { get; set; }

        public ushort BankWithdrawFee { get; set; } = 4;

        /// <summary>
        /// Convert to a <see cref="uint"/> integer.
        /// </summary>
        /// <returns>Integer</returns>
        public uint ToInt()
        {
            uint flags = 0;
            flags |= (uint)CollectableTableFileIndex << 16;
            flags |= BankWithdrawFee;
            return flags;
        }
    }

    public class MiscSmithyModel
    {
        public const int Size = 0xC;

        private readonly short oldObjectId;
        private readonly byte oldGraphicId;
        private readonly short newObjectId;
        private readonly uint displayListOffset;

        public MiscSmithyModel(short oldObjectId, byte oldGraphicId, short newObjectId, uint displayListOffset)
        {
            this.oldObjectId = oldObjectId;
            this.oldGraphicId = oldGraphicId;
            this.newObjectId = newObjectId;
            this.displayListOffset = displayListOffset;
        }

        public byte[] ToByteArray()
        {
            byte[] bytes = new byte[Size];
            ReadWriteUtils.Arr_WriteU16(bytes, 0, (ushort)oldObjectId);
            bytes[2] = oldGraphicId;
            ReadWriteUtils.Arr_WriteU16(bytes, 4, (ushort)newObjectId);
            ReadWriteUtils.Arr_WriteU32(bytes, 8, displayListOffset);
            return bytes;
        }
    }

    public class MiscSmithy
    {
        public List<MiscSmithyModel> Models { get; set; }

        /// <summary>
        /// Convert to a <see cref="uint"/> integer.
        /// </summary>
        /// <returns>Integer</returns>
        public byte[] ToByteArray()
        {
            var result = new byte[MiscSmithyModel.Size * 10];

            for (var i = 0; i < Models.Count; i++)
            {
                var model = Models[i];
                ReadWriteUtils.Arr_Insert(model.ToByteArray(), 0, MiscSmithyModel.Size, result, i * MiscSmithyModel.Size);
            }

            return result;
        }
    }

    public class MiscBytes
    {
        public byte NpcKafeiReplaceMask { get; set; }

        public byte RequiredBossRemains { get; set; } = 4;

        /// <summary>
        /// Convert to a <see cref="uint"/> integer.
        /// </summary>
        /// <returns>Integer</returns>

        public uint ToInt()
        {
            uint flags = 0;
            flags |= (uint)NpcKafeiReplaceMask << 24;
            flags |= (uint)RequiredBossRemains << 16;
            return flags;
        }


    }

    public class MiscDrawFlags
    {
        /// <summary>
        /// Whether or not to enable freestanding models.
        /// </summary>
        public bool FreestandingModels { get; set; } = true;

        /// <summary>
        /// A flag for a getItem draw hook, for whether to draw the hungry goron's Don Gero Mask, or a getItem.
        /// </summary>
        public bool DrawDonGeroMask { get; set; }

        /// <summary>
        /// A flag for a getItem draw hook, for whether to draw the postman's in-model hat, or a getItem.
        /// </summary>
        ///
        public bool DrawPostmanHat { get; set; }

        /// <summary>
        /// A flag for a getItem draw hook, for whether to draw the cursed skulltula man's mask, or a getItem.
        /// </summary>
        ///
        public bool DrawMaskOfTruth { get; set; }

        /// <summary>
        /// A flag for a getItem draw hook, for whether to draw the Gorman Brothers' Garo's Mask, or a getItem.
        /// </summary>
        ///
        public bool DrawGaroMask { get; set; }

        /// <summary>
        /// A flag for a getItem draw hook, for whether to draw Kafei's in-model Pendant of Memories, or a getItem.
        /// </summary>
        /// 
        public bool DrawPendantOfMemories { get; set; }

        /// <summary>
        /// Whether or not to enable shop models.
        /// </summary>
        public bool ShopModels { get; set; } = true;


        /// <summary>
        /// Convert to a <see cref="uint"/> integer.
        /// </summary>
        /// <returns>Integer</returns>

        public uint ToInt()
        {
            uint flags = 0;
            flags |= (this.FreestandingModels ? (uint)1 : 0) << 31;
            flags |= (this.DrawDonGeroMask ? (uint)1 : 0) << 30;
            flags |= (this.DrawPostmanHat ? (uint)1 : 0) << 29;
            flags |= (this.DrawMaskOfTruth ? (uint)1 : 0) << 28;
            flags |= (this.DrawGaroMask ? (uint)1 : 0) << 27;
            flags |= (this.DrawPendantOfMemories ? (uint)1 : 0) << 26;
            flags |= (this.ShopModels ? (uint)1 : 0) << 25;
            return flags;
        }
    }

    /// <summary>
    /// Miscellaneous configuration structure.
    /// </summary>
    public struct MiscConfigStruct : IAsmConfigStruct
    {
        public uint Version;
        public byte[] Hash;
        public uint Flags;
        public uint InternalFlags;
        public uint Speedups;
        public uint Shorts;
        public uint MMRBytes;
        public uint DrawFlags;
        public byte[] Smithy;

        /// <summary>
        /// Convert to bytes.
        /// </summary>
        /// <returns>Bytes</returns>
        public byte[] ToBytes()
        {
            using (var memStream = new MemoryStream())
            using (var writer = new BeBinaryWriter(memStream))
            {
                writer.WriteUInt32(this.Version);

                // Version 0
                writer.WriteBytes(this.Hash);
                writer.WriteUInt32(this.Flags);

                // Version 1
                if (this.Version >= 1)
                {
                    writer.WriteUInt32(this.InternalFlags);
                }

                // Version 3
                if (this.Version >= 3)
                {
                    writer.WriteUInt32(this.Speedups);
                    writer.WriteUInt32(this.Shorts);
                    writer.WriteUInt32(this.MMRBytes);
                    writer.WriteUInt32(this.DrawFlags);
                    writer.Write(this.Smithy);
                }

                return memStream.ToArray();
            }
        }
    }

    /// <summary>
    /// Miscellaneous configuration.
    /// </summary>
    public class MiscConfig : AsmConfig
    {
        /// <summary>
        /// Hash bytes.
        /// </summary>
        public byte[] Hash { get; set; }

        /// <summary>
        /// Flags.
        /// </summary>
        public MiscFlags Flags { get; set; }

        /// <summary>
        /// Internal flags.
        /// </summary>
        public InternalFlags InternalFlags { get; set; }

        /// <summary>
        /// Speedups.
        /// </summary>
        public MiscSpeedups Speedups { get; set; }

        public MiscShorts Shorts { get; set; }

        public MiscBytes MMRBytes { get; set; }

        public MiscDrawFlags DrawFlags { get; set; }

        public MiscSmithy Smithy { get; set; }

        public MiscConfig()
            : this(new byte[0], new MiscFlags(), new InternalFlags(), new MiscSpeedups(), new MiscShorts(), new MiscBytes(), new MiscDrawFlags(), new MiscSmithy())
        {
        }

        public MiscConfig(byte[] hash, MiscFlags flags, InternalFlags internalFlags, MiscSpeedups speedups, MiscShorts shorts, MiscBytes mmrbytes, MiscDrawFlags drawFlags, MiscSmithy smithy)
        {
            this.Hash = hash;
            this.Flags = flags;
            this.InternalFlags = internalFlags;
            this.Speedups = speedups;
            this.Shorts = shorts;
            this.MMRBytes = mmrbytes;
            this.DrawFlags = drawFlags;
            this.Smithy = smithy;
        }

        /// <summary>
        /// Finalize configuration using <see cref="GameplaySettings"/>.
        /// </summary>
        /// <param name="settings">Settings</param>
        public void FinalizeSettings(GameplaySettings settings)
        {
            // Update speedup flags which correspond with ShortenCutscenes.
            this.Speedups.BlastMaskThief = settings.ShortenCutsceneSettings.General.HasFlag(ShortenCutsceneGeneral.BlastMaskThief);
            this.Speedups.BoatArchery = settings.ShortenCutsceneSettings.General.HasFlag(ShortenCutsceneGeneral.BoatArchery);
            this.Speedups.FishermanGame = settings.ShortenCutsceneSettings.General.HasFlag(ShortenCutsceneGeneral.FishermanGame);
            this.Speedups.SoundCheck = settings.ShortenCutsceneSettings.General.HasFlag(ShortenCutsceneGeneral.MilkBarPerformance);
            this.Speedups.DonGero = settings.ShortenCutsceneSettings.General.HasFlag(ShortenCutsceneGeneral.HungryGoron);
            this.Speedups.FastBankRupees = settings.ShortenCutsceneSettings.General.HasFlag(ShortenCutsceneGeneral.FasterBankText);
            this.Speedups.ShortChestOpening = settings.ShortenCutsceneSettings.General.HasFlag(ShortenCutsceneGeneral.ShortChestOpening);
            this.Speedups.SkipGiantsCutscene = settings.ShortenCutsceneSettings.General.HasFlag(ShortenCutsceneGeneral.EverythingElse);
            this.Speedups.OathHint = settings.UpdateNPCText;

            // If using Adult Link model, allow Mikau cutscene to activate early.
            this.Flags.EarlyMikau = settings.Character == Character.AdultLink;

            this.Flags.FairyChests = settings.StrayFairyMode.HasFlag(StrayFairyMode.ChestsOnly);

            this.DrawFlags.DrawDonGeroMask = MaskConfigUtils.DonGeroGoronDrawMask;
            this.DrawFlags.DrawPostmanHat = MaskConfigUtils.PostmanDrawHat;
            this.DrawFlags.DrawMaskOfTruth = MaskConfigUtils.DrawMaskOfTruth;
            this.DrawFlags.DrawGaroMask = MaskConfigUtils.DrawGaroMask;
            this.DrawFlags.DrawPendantOfMemories = MaskConfigUtils.DrawPendantOfMemories;

            // Update internal flags.
            this.InternalFlags.VanillaLayout = settings.LogicMode == LogicMode.Vanilla;
            this.Shorts.CollectableTableFileIndex = ItemSwapUtils.COLLECTABLE_TABLE_FILE_INDEX;
            this.MMRBytes.NpcKafeiReplaceMask = MaskConfigUtils.NpcKafeiDrawMask;
        }

        /// <summary>
        /// Get a <see cref="MiscConfigStruct"/> representation.
        /// </summary>
        /// <param name="version">Structure version</param>
        /// <returns>Configuration structure</returns>
        public override IAsmConfigStruct ToStruct(uint version)
        {
            var hash = ReadWriteUtils.CopyBytes(this.Hash, 0x10);

            return new MiscConfigStruct
            {
                Version = version,
                Hash = hash,
                Flags = this.Flags.ToInt(),
                InternalFlags = this.InternalFlags.ToInt(),
                Speedups = this.Speedups.ToInt(),
                Shorts = this.Shorts.ToInt(),
                MMRBytes = this.MMRBytes.ToInt(),
                DrawFlags = this.DrawFlags.ToInt(),
                Smithy = this.Smithy.ToByteArray(),
            };
        }
    }
}
