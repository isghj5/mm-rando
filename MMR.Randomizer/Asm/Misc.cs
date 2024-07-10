using Be.IO;
using MMR.Common.Extensions;
using MMR.Common.Utils;
using MMR.Randomizer.Models;
using MMR.Randomizer.Models.Settings;
using MMR.Randomizer.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        public bool EasyFrameByFrame { get; set; }

        public bool FairyMaskShimmer { get; set; }

        public bool SkulltulaTokenSounds { get; set; }

        public bool TakeDamageOnEpona { get; set; }

        public bool TakeDamageWhileShielding { get; set; }

        public bool TakeDamageFromVoid { get; set; }
        public bool OceanTokensRandomized { get; set; }
        public bool MoonCrashFileErase { get; set; }

        public MiscFlags()
        {
        }

        public MiscFlags(byte[] flags)
        {
            Load(flags);
        }

        /// <summary>
        /// Load from a <see cref="uint"/> integer.
        /// </summary>
        /// <param name="flags">Flags integer</param>
        void Load(byte[] flags)
        {
            var bitUnpacker = new BitUnpacker(flags);
            CritWiggle = (CritWiggleState)bitUnpacker.ReadS32(2);
            DrawHash = bitUnpacker.ReadBool();
            FastPush = bitUnpacker.ReadBool();
            OcarinaUnderwater = bitUnpacker.ReadBool();
            QuestItemStorage = bitUnpacker.ReadBool();
            CloseCows = bitUnpacker.ReadBool();
            bitUnpacker.ReadS32(2); // QuestConsume
            ArrowCycling = bitUnpacker.ReadBool();
            bitUnpacker.ReadBool(); // ArrowMagic
            ElegySpeedup = bitUnpacker.ReadBool();
            ContinuousDekuHopping = bitUnpacker.ReadBool();
            ProgressiveUpgrades = bitUnpacker.ReadBool();
            TrapQuirks = bitUnpacker.ReadBool();
            EarlyMikau = bitUnpacker.ReadBool();
            FairyChests = bitUnpacker.ReadBool();
            TargetHealth = bitUnpacker.ReadBool();
            ClimbAnything = bitUnpacker.ReadBool();
            FreeScarecrow = bitUnpacker.ReadBool();
            FillWallet = bitUnpacker.ReadBool();
            AutoInvert = (AutoInvertState)bitUnpacker.ReadS32(2);
            HiddenRupeesSparkle = bitUnpacker.ReadBool();
            SaferGlitches = bitUnpacker.ReadBool();
            BombchuDrops = bitUnpacker.ReadBool();
            InstantTransform = bitUnpacker.ReadBool();
            BombArrows = bitUnpacker.ReadBool();
            GiantMaskAnywhere = bitUnpacker.ReadBool();
            FewerHealthDrops = bitUnpacker.ReadBool();
            IronGoron = bitUnpacker.ReadBool();
            EasyFrameByFrame = bitUnpacker.ReadBool();
            FairyMaskShimmer = bitUnpacker.ReadBool();
            SkulltulaTokenSounds = bitUnpacker.ReadBool();
            TakeDamageOnEpona = bitUnpacker.ReadBool();
            TakeDamageWhileShielding = bitUnpacker.ReadBool();
            TakeDamageFromVoid = bitUnpacker.ReadBool();
            OceanTokensRandomized = bitUnpacker.ReadBool();
            MoonCrashFileErase = bitUnpacker.ReadBool();
        }

        /// <summary>
        /// Convert to a <see cref="uint"/> integer.
        /// </summary>
        /// <returns>Integer</returns>
        public byte[] ToByteArray()
        {
            var bitPacker = new BitPacker();
            bitPacker.Write((int)CritWiggle, 2);
            bitPacker.Write(DrawHash);
            bitPacker.Write(FastPush);
            bitPacker.Write(OcarinaUnderwater);
            bitPacker.Write(QuestItemStorage);
            bitPacker.Write(CloseCows);
            bitPacker.Write((int)QuestConsume, 2);
            bitPacker.Write(ArrowCycling);
            bitPacker.Write(ArrowMagic);
            bitPacker.Write(ElegySpeedup);
            bitPacker.Write(ContinuousDekuHopping);
            bitPacker.Write(ProgressiveUpgrades);
            bitPacker.Write(TrapQuirks);
            bitPacker.Write(EarlyMikau);
            bitPacker.Write(FairyChests);
            bitPacker.Write(TargetHealth);
            bitPacker.Write(ClimbAnything);
            bitPacker.Write(FreeScarecrow);
            bitPacker.Write(FillWallet);
            bitPacker.Write((int)AutoInvert, 2);
            bitPacker.Write(HiddenRupeesSparkle);
            bitPacker.Write(SaferGlitches);
            bitPacker.Write(BombchuDrops);
            bitPacker.Write(InstantTransform);
            bitPacker.Write(BombArrows);
            bitPacker.Write(GiantMaskAnywhere);
            bitPacker.Write(FewerHealthDrops);
            bitPacker.Write(IronGoron);
            bitPacker.Write(EasyFrameByFrame);
            bitPacker.Write(FairyMaskShimmer);
            bitPacker.Write(SkulltulaTokenSounds);
            bitPacker.Write(TakeDamageOnEpona);
            bitPacker.Write(TakeDamageWhileShielding);
            bitPacker.Write(TakeDamageFromVoid);
            bitPacker.Write(OceanTokensRandomized);
            bitPacker.Write(MoonCrashFileErase);
            return bitPacker.ToByteArray(4);
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
        public bool VictoryDirectToCredits { get; set; }
        public bool VictoryCantFightMajora { get; set; }
        public bool VictoryFairies { get; set; }
        public bool VictorySkullTokens { get; set; }
        public bool VictoryNonTransformationMasks { get; set; }
        public bool VictoryTransformationMasks { get; set; }
        public bool VictoryNotebook { get;  set; }
        public bool VictoryHearts { get;  set; }
        public byte VictoryBossRemainsCount { get; set; }

        /// <summary>
        /// Convert to a <see cref="uint"/> integer.
        /// </summary>
        /// <returns>Integer</returns>
        public byte[] ToByteArray()
        {
            var bitPacker = new BitPacker();
            bitPacker.Write(VanillaLayout);
            bitPacker.Write(VictoryDirectToCredits);
            bitPacker.Write(VictoryCantFightMajora);
            bitPacker.Write(VictoryFairies);
            bitPacker.Write(VictorySkullTokens);
            bitPacker.Write(VictoryNonTransformationMasks);
            bitPacker.Write(VictoryTransformationMasks);
            bitPacker.Write(VictoryNotebook);
            bitPacker.Write(VictoryHearts);
            bitPacker.Write(VictoryBossRemainsCount, 3);
            return bitPacker.ToByteArray(4);
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

        private readonly ushort oldObjectId;
        private readonly byte oldGraphicId;
        private readonly ushort newObjectId;
        private readonly uint displayListOffset;

        public MiscSmithyModel(ushort oldObjectId, byte oldGraphicId, ushort newObjectId, uint displayListOffset)
        {
            this.oldObjectId = oldObjectId;
            this.oldGraphicId = oldGraphicId;
            this.newObjectId = newObjectId;
            this.displayListOffset = displayListOffset;
        }

        public byte[] ToByteArray()
        {
            byte[] bytes = new byte[Size];
            ReadWriteUtils.Arr_WriteU16(bytes, 0, oldObjectId);
            bytes[2] = oldGraphicId;
            ReadWriteUtils.Arr_WriteU16(bytes, 4, newObjectId);
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

            if (Models != null)
            {
                for (var i = 0; i < Models.Count; i++)
                {
                    var model = Models[i];
                    ReadWriteUtils.Arr_Insert(model.ToByteArray(), 0, MiscSmithyModel.Size, result, i * MiscSmithyModel.Size);
                }
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
        public byte[] Flags;
        public byte[] InternalFlags;
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
                writer.Write(this.Flags);

                // Version 1
                if (this.Version >= 1)
                {
                    writer.Write(this.InternalFlags);
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
            this.Speedups.OathHint = settings.OathHint;

            // If using Adult Link model, allow Mikau cutscene to activate early.
            this.Flags.EarlyMikau = settings.Character == Character.AdultLink;

            this.Flags.FairyChests = settings.StrayFairyMode.HasFlag(StrayFairyMode.ChestsOnly);

            this.Flags.OceanTokensRandomized = settings.FairyAndSkullHint && settings.CustomItemList.Any(ItemUtils.OceanSkulltulaTokens().Contains);

            this.DrawFlags.DrawDonGeroMask = MaskConfigUtils.DonGeroGoronDrawMask;
            this.DrawFlags.DrawPostmanHat = MaskConfigUtils.PostmanDrawHat;
            this.DrawFlags.DrawMaskOfTruth = MaskConfigUtils.DrawMaskOfTruth;
            this.DrawFlags.DrawGaroMask = MaskConfigUtils.DrawGaroMask;
            this.DrawFlags.DrawPendantOfMemories = MaskConfigUtils.DrawPendantOfMemories;

            // Update internal flags.
            this.InternalFlags.VanillaLayout = settings.LogicMode == LogicMode.Vanilla;
            this.InternalFlags.VictoryDirectToCredits = settings.VictoryMode.HasFlag(VictoryMode.DirectToCredits);
            this.InternalFlags.VictoryCantFightMajora = settings.VictoryMode.HasFlag(VictoryMode.CantFightMajora);
            this.InternalFlags.VictoryFairies = settings.VictoryMode.HasFlag(VictoryMode.Fairies);
            this.InternalFlags.VictorySkullTokens = settings.VictoryMode.HasFlag(VictoryMode.SkullTokens);
            this.InternalFlags.VictoryNonTransformationMasks = settings.VictoryMode.HasFlag(VictoryMode.NonTransformationMasks);
            this.InternalFlags.VictoryTransformationMasks = settings.VictoryMode.HasFlag(VictoryMode.TransformationMasks);
            this.InternalFlags.VictoryNotebook = settings.VictoryMode.HasFlag(VictoryMode.Notebook);
            this.InternalFlags.VictoryHearts = settings.VictoryMode.HasFlag(VictoryMode.Hearts);
            byte bossRemainsAmount = 0;
            if (settings.VictoryMode.HasFlag(VictoryMode.FourBossRemains))
            {
                bossRemainsAmount = 4;
            }
            else if (settings.VictoryMode.HasFlag(VictoryMode.ThreeBossRemains))
            {
                bossRemainsAmount = 3;
            }
            else if (settings.VictoryMode.HasFlag(VictoryMode.TwoBossRemains))
            {
                bossRemainsAmount = 2;
            }
            else if (settings.VictoryMode.HasFlag(VictoryMode.OneBossRemains))
            {
                bossRemainsAmount = 1;
            }
            this.InternalFlags.VictoryBossRemainsCount = bossRemainsAmount;
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
                Flags = this.Flags.ToByteArray(),
                InternalFlags = this.InternalFlags.ToByteArray(),
                Speedups = this.Speedups.ToInt(),
                Shorts = this.Shorts.ToInt(),
                MMRBytes = this.MMRBytes.ToInt(),
                DrawFlags = this.DrawFlags.ToInt(),
                Smithy = this.Smithy.ToByteArray(),
            };
        }
    }
}
