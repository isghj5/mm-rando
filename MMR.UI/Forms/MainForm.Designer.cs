using MMR.Randomizer.Models.Settings;
using System;
using System.Drawing;
using System.Linq;

namespace MMR.UI.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bopen = new System.Windows.Forms.Button();
            this.openROM = new System.Windows.Forms.OpenFileDialog();
            this.openLogic = new System.Windows.Forms.OpenFileDialog();
            this.loadSettings = new System.Windows.Forms.OpenFileDialog();
            this.saveSettings = new System.Windows.Forms.SaveFileDialog();
            this.tROMName = new System.Windows.Forms.TextBox();
            this.tSettings = new System.Windows.Forms.TabControl();
            this.tabMain = new System.Windows.Forms.TabPage();
            this.tOtherCustomizations = new System.Windows.Forms.TabControl();
            this.tOtherCustomization = new System.Windows.Forms.TabPage();
            this.cRequiredBossRemains = new System.Windows.Forms.ComboBox();
            this.lRequiredRemains = new System.Windows.Forms.Label();
            this.cShuffleBosses = new System.Windows.Forms.CheckBox();
            this.cStartingItems = new System.Windows.Forms.ComboBox();
            this.lStartingItems = new System.Windows.Forms.Label();
            this.cProgressiveUpgrades = new System.Windows.Forms.CheckBox();
            this.cDEnt = new System.Windows.Forms.CheckBox();
            this.cMixSongs = new System.Windows.Forms.CheckBox();
            this.cEnemy = new System.Windows.Forms.CheckBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.lNumTricksEnabled = new System.Windows.Forms.Label();
            this.lItemPlacement = new System.Windows.Forms.Label();
            this.cItemPlacement = new System.Windows.Forms.ComboBox();
            this.bToggleTricks = new System.Windows.Forms.Button();
            this.cMode = new System.Windows.Forms.ComboBox();
            this.bLoadLogic = new System.Windows.Forms.Button();
            this.lMode = new System.Windows.Forms.Label();
            this.tbUserLogic = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.tJunkLocationsList = new System.Windows.Forms.TextBox();
            this.bJunkLocationsEditor = new System.Windows.Forms.Button();
            this.lJunkLocationsAmount = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tStartingItemList = new System.Windows.Forms.TextBox();
            this.lCustomStartingItemAmount = new System.Windows.Forms.Label();
            this.bStartingItemEditor = new System.Windows.Forms.Button();
            this.tabItemPool = new System.Windows.Forms.TabPage();
            this.cItemPoolAdvanced = new System.Windows.Forms.CheckBox();
            this.pClassicItemPool = new System.Windows.Forms.Panel();
            this.lItemPoolText = new System.Windows.Forms.Label();
            this.bItemPoolEdit = new System.Windows.Forms.Button();
            this.tItemPool = new System.Windows.Forms.TextBox();
            this.tableItemPool = new System.Windows.Forms.TableLayoutPanel();
            this.pLocationCategories = new System.Windows.Forms.Panel();
            this.tabGimmicks = new System.Windows.Forms.TabPage();
            this.gGimmicksChallenges = new System.Windows.Forms.GroupBox();
            this.cTakeDamageFromDexihands = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cTakeDamageFromGibdosFaster = new System.Windows.Forms.CheckBox();
            this.cTakeDamageGettingCaught = new System.Windows.Forms.CheckBox();
            this.cTakeDamageFromGorons = new System.Windows.Forms.CheckBox();
            this.cTakeDamageFromDog = new System.Windows.Forms.CheckBox();
            this.cTakeDamageFromVoid = new System.Windows.Forms.CheckBox();
            this.cTakeDamageWhileShielding = new System.Windows.Forms.CheckBox();
            this.cTakeDamageOnEpona = new System.Windows.Forms.CheckBox();
            this.cFewerHealthDrops = new System.Windows.Forms.CheckBox();
            this.cDType = new System.Windows.Forms.ComboBox();
            this.lDType = new System.Windows.Forms.Label();
            this.cDeathMoonCrash = new System.Windows.Forms.CheckBox();
            this.cByoAmmo = new System.Windows.Forms.CheckBox();
            this.cDMult = new System.Windows.Forms.ComboBox();
            this.lDMult = new System.Windows.Forms.Label();
            this.gGimmicksMovement = new System.Windows.Forms.GroupBox();
            this.cIronGoron = new System.Windows.Forms.CheckBox();
            this.cClimbMostSurfaces = new System.Windows.Forms.CheckBox();
            this.cHookshotAnySurface = new System.Windows.Forms.CheckBox();
            this.cFloors = new System.Windows.Forms.ComboBox();
            this.lFloors = new System.Windows.Forms.Label();
            this.lGravity = new System.Windows.Forms.Label();
            this.cGravity = new System.Windows.Forms.ComboBox();
            this.cContinuousDekuHopping = new System.Windows.Forms.CheckBox();
            this.gTraps = new System.Windows.Forms.GroupBox();
            this.lTrapWeightings = new System.Windows.Forms.Label();
            this.lTrapsAppearance = new System.Windows.Forms.Label();
            this.lTrapAmount = new System.Windows.Forms.Label();
            this.cTrapAmount = new System.Windows.Forms.ComboBox();
            this.cTrapsAppearance = new System.Windows.Forms.ComboBox();
            this.cIceTrapQuirks = new System.Windows.Forms.CheckBox();
            this.gGimmicksClock = new System.Windows.Forms.GroupBox();
            this.cAutoInvert = new System.Windows.Forms.ComboBox();
            this.lAutoInvert = new System.Windows.Forms.Label();
            this.cClockSpeed = new System.Windows.Forms.ComboBox();
            this.lClockSpeed = new System.Windows.Forms.Label();
            this.cHideClock = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gGimmicksOther = new System.Windows.Forms.GroupBox();
            this.cVanillaMoonTrials = new System.Windows.Forms.CheckBox();
            this.cBombArrows = new System.Windows.Forms.CheckBox();
            this.cGiantMaskAnywhere = new System.Windows.Forms.CheckBox();
            this.cInstantTransformations = new System.Windows.Forms.CheckBox();
            this.cFreeScarecrow = new System.Windows.Forms.CheckBox();
            this.cFDAnywhere = new System.Windows.Forms.CheckBox();
            this.cBlastCooldown = new System.Windows.Forms.ComboBox();
            this.cUnderwaterOcarina = new System.Windows.Forms.CheckBox();
            this.cSunsSong = new System.Windows.Forms.CheckBox();
            this.lBlastMask = new System.Windows.Forms.Label();
            this.lNutAndStickDrops = new System.Windows.Forms.Label();
            this.cNutAndStickDrops = new System.Windows.Forms.ComboBox();
            this.tabComfort = new System.Windows.Forms.TabPage();
            this.gHintsGeneral = new System.Windows.Forms.GroupBox();
            this.cFairyAndSkullHints = new System.Windows.Forms.CheckBox();
            this.cRemainsHint = new System.Windows.Forms.CheckBox();
            this.cOathHint = new System.Windows.Forms.CheckBox();
            this.bCustomizeHintPriorities = new System.Windows.Forms.Button();
            this.cHintImportance = new System.Windows.Forms.CheckBox();
            this.cMixGaroWithGossip = new System.Windows.Forms.CheckBox();
            this.gGaroHints = new System.Windows.Forms.GroupBox();
            this.cFreeGaroHints = new System.Windows.Forms.CheckBox();
            this.cCustomGaroWoth = new System.Windows.Forms.CheckBox();
            this.nMaxGaroCT = new System.Windows.Forms.NumericUpDown();
            this.lGaroHints = new System.Windows.Forms.Label();
            this.nMaxGaroFoolish = new System.Windows.Forms.NumericUpDown();
            this.cGaroHint = new System.Windows.Forms.ComboBox();
            this.nMaxGaroWotH = new System.Windows.Forms.NumericUpDown();
            this.cClearGaroHints = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.gSpeedUps = new System.Windows.Forms.GroupBox();
            this.cSpeedupBabyCucco = new System.Windows.Forms.CheckBox();
            this.cDoubleArcheryRewards = new System.Windows.Forms.CheckBox();
            this.cFasterBank = new System.Windows.Forms.CheckBox();
            this.cSkipBeaver = new System.Windows.Forms.CheckBox();
            this.cFasterLabFish = new System.Windows.Forms.CheckBox();
            this.cGoodDogRaceRNG = new System.Windows.Forms.CheckBox();
            this.cGoodDampeRNG = new System.Windows.Forms.CheckBox();
            this.gHints = new System.Windows.Forms.GroupBox();
            this.cCustomGossipWoth = new System.Windows.Forms.CheckBox();
            this.nMaxGossipCT = new System.Windows.Forms.NumericUpDown();
            this.nMaxGossipFoolish = new System.Windows.Forms.NumericUpDown();
            this.nMaxGossipWotH = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lGossipWothConfig = new System.Windows.Forms.Label();
            this.lGossip = new System.Windows.Forms.Label();
            this.cGossipHints = new System.Windows.Forms.ComboBox();
            this.cFreeHints = new System.Windows.Forms.CheckBox();
            this.cClearHints = new System.Windows.Forms.CheckBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.lLink = new System.Windows.Forms.Label();
            this.cLink = new System.Windows.Forms.ComboBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.cEasyFrameByFrame = new System.Windows.Forms.CheckBox();
            this.cImprovedCamera = new System.Windows.Forms.CheckBox();
            this.cGossipsTolerant = new System.Windows.Forms.CheckBox();
            this.cQuestItemKeep = new System.Windows.Forms.CheckBox();
            this.cUpdateNpcText = new System.Windows.Forms.CheckBox();
            this.cAddBombchuDrops = new System.Windows.Forms.CheckBox();
            this.lChestGame = new System.Windows.Forms.Label();
            this.cChestGameMinimap = new System.Windows.Forms.ComboBox();
            this.cSaferGlitches = new System.Windows.Forms.CheckBox();
            this.cSkulltulaTokenSounds = new System.Windows.Forms.CheckBox();
            this.cFairyMaskShimmer = new System.Windows.Forms.CheckBox();
            this.cInvisSparkle = new System.Windows.Forms.CheckBox();
            this.cFillWallet = new System.Windows.Forms.CheckBox();
            this.cTargetHealth = new System.Windows.Forms.CheckBox();
            this.cLenientGoronSpikes = new System.Windows.Forms.CheckBox();
            this.cImprovedPictobox = new System.Windows.Forms.CheckBox();
            this.cElegySpeedups = new System.Windows.Forms.CheckBox();
            this.cCloseCows = new System.Windows.Forms.CheckBox();
            this.cArrowCycling = new System.Windows.Forms.CheckBox();
            this.cFreestanding = new System.Windows.Forms.CheckBox();
            this.cFastPush = new System.Windows.Forms.CheckBox();
            this.cQText = new System.Windows.Forms.CheckBox();
            this.cShopAppearance = new System.Windows.Forms.CheckBox();
            this.cEponaSword = new System.Windows.Forms.CheckBox();
            this.cUpdateChests = new System.Windows.Forms.CheckBox();
            this.cDisableCritWiggle = new System.Windows.Forms.CheckBox();
            this.cQuestItemStorage = new System.Windows.Forms.CheckBox();
            this.cNoDowngrades = new System.Windows.Forms.CheckBox();
            this.tabShortenCutscenes = new System.Windows.Forms.TabPage();
            this.tShortenCutscenes = new System.Windows.Forms.TabControl();
            this.tabCosmetics = new System.Windows.Forms.TabPage();
            this.gCosmeticOther = new System.Windows.Forms.GroupBox();
            this.cRainbowTunic = new System.Windows.Forms.CheckBox();
            this.cBombTrapTunicColors = new System.Windows.Forms.CheckBox();
            this.cInstantPictobox = new System.Windows.Forms.CheckBox();
            this.cTatl = new System.Windows.Forms.ComboBox();
            this.lTatl = new System.Windows.Forms.Label();
            this.cTargettingStyle = new System.Windows.Forms.CheckBox();
            this.gCosmeticMusicSound = new System.Windows.Forms.GroupBox();
            this.cDisableFanfares = new System.Windows.Forms.CheckBox();
            this.cMusicTrackNames = new System.Windows.Forms.CheckBox();
            this.cRemoveMinorMusic = new System.Windows.Forms.CheckBox();
            this.lLuckRoll = new System.Windows.Forms.Label();
            this.tLuckRollPercentage = new System.Windows.Forms.NumericUpDown();
            this.lMusic = new System.Windows.Forms.Label();
            this.cMusic = new System.Windows.Forms.ComboBox();
            this.cSFX = new System.Windows.Forms.CheckBox();
            this.cCombatMusicDisable = new System.Windows.Forms.CheckBox();
            this.cEnableNightMusic = new System.Windows.Forms.CheckBox();
            this.cLowHealthSFXComboBox = new System.Windows.Forms.ComboBox();
            this.lLowHealthSFXComboBox = new System.Windows.Forms.Label();
            this.cHUDGroupBox = new System.Windows.Forms.GroupBox();
            this.cHueShiftMiscUI = new System.Windows.Forms.CheckBox();
            this.cHUDTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.cHUDHeartsComboBox = new System.Windows.Forms.ComboBox();
            this.cHeartsLabel = new System.Windows.Forms.Label();
            this.cMagicLabel = new System.Windows.Forms.Label();
            this.cHUDMagicComboBox = new System.Windows.Forms.ComboBox();
            this.btn_hud = new System.Windows.Forms.Button();
            this.tFormCosmetics = new System.Windows.Forms.TabControl();
            this.cDrawHash = new System.Windows.Forms.CheckBox();
            this.gGameOutput = new System.Windows.Forms.GroupBox();
            this.cHTMLLog = new System.Windows.Forms.CheckBox();
            this.cPatch = new System.Windows.Forms.CheckBox();
            this.cSpoiler = new System.Windows.Forms.CheckBox();
            this.cN64 = new System.Windows.Forms.CheckBox();
            this.cVC = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bApplyPatch = new System.Windows.Forms.Button();
            this.saveROM = new System.Windows.Forms.SaveFileDialog();
            this.cEnergy = new System.Windows.Forms.ColorDialog();
            this.cTunic = new System.Windows.Forms.ColorDialog();
            this.bRandomise = new System.Windows.Forms.Button();
            this.bReroll = new System.Windows.Forms.Button();
            this.saveWad = new System.Windows.Forms.SaveFileDialog();
            this.mMenu = new System.Windows.Forms.MenuStrip();
            this.mFile = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mCustomise = new System.Windows.Forms.ToolStripMenuItem();
            this.mDPadConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mLogicEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mManual = new System.Windows.Forms.ToolStripMenuItem();
            this.mSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.openBROM = new System.Windows.Forms.OpenFileDialog();
            this.pProgress = new System.Windows.Forms.ProgressBar();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.lStatus = new System.Windows.Forms.Label();
            this.tSeed = new System.Windows.Forms.TextBox();
            this.lSeed = new System.Windows.Forms.Label();
            this.cDummy = new System.Windows.Forms.CheckBox();
            this.openPatch = new System.Windows.Forms.OpenFileDialog();
            this.ttOutput = new System.Windows.Forms.TabControl();
            this.tpOutputSettings = new System.Windows.Forms.TabPage();
            this.tpPatchSettings = new System.Windows.Forms.TabPage();
            this.tPatch = new System.Windows.Forms.TextBox();
            this.bLoadPatch = new System.Windows.Forms.Button();
            this.bSkip = new System.Windows.Forms.Button();
            this.cImportanceCount = new System.Windows.Forms.CheckBox();
            this.cImportanceCountGaro = new System.Windows.Forms.CheckBox();
            this.tSettings.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tOtherCustomizations.SuspendLayout();
            this.tOtherCustomization.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabItemPool.SuspendLayout();
            this.tabGimmicks.SuspendLayout();
            this.gGimmicksChallenges.SuspendLayout();
            this.gGimmicksMovement.SuspendLayout();
            this.gTraps.SuspendLayout();
            this.gGimmicksClock.SuspendLayout();
            this.gGimmicksOther.SuspendLayout();
            this.tabComfort.SuspendLayout();
            this.gHintsGeneral.SuspendLayout();
            this.gGaroHints.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nMaxGaroCT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMaxGaroFoolish)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMaxGaroWotH)).BeginInit();
            this.gSpeedUps.SuspendLayout();
            this.gHints.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nMaxGossipCT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMaxGossipFoolish)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMaxGossipWotH)).BeginInit();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tabShortenCutscenes.SuspendLayout();
            this.tabCosmetics.SuspendLayout();
            this.gCosmeticOther.SuspendLayout();
            this.gCosmeticMusicSound.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tLuckRollPercentage)).BeginInit();
            this.cHUDGroupBox.SuspendLayout();
            this.cHUDTableLayoutPanel.SuspendLayout();
            this.gGameOutput.SuspendLayout();
            this.mMenu.SuspendLayout();
            this.ttOutput.SuspendLayout();
            this.tpOutputSettings.SuspendLayout();
            this.tpPatchSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // bopen
            // 
            this.bopen.Location = new System.Drawing.Point(14, 434);
            this.bopen.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bopen.Name = "bopen";
            this.bopen.Size = new System.Drawing.Size(112, 31);
            this.bopen.TabIndex = 0;
            this.bopen.Text = "Open ROM";
            this.bopen.UseVisualStyleBackColor = true;
            this.bopen.Click += new System.EventHandler(this.bopen_Click);
            // 
            // openROM
            // 
            this.openROM.Filter = "ROM files|*.z64";
            // 
            // openLogic
            // 
            this.openLogic.Filter = "Logic File|*.txt";
            // 
            // tROMName
            // 
            this.tROMName.Location = new System.Drawing.Point(138, 438);
            this.tROMName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tROMName.Name = "tROMName";
            this.tROMName.ReadOnly = true;
            this.tROMName.Size = new System.Drawing.Size(746, 23);
            this.tROMName.TabIndex = 1;
            // 
            // tSettings
            // 
            this.tSettings.Controls.Add(this.tabMain);
            this.tSettings.Controls.Add(this.tabItemPool);
            this.tSettings.Controls.Add(this.tabGimmicks);
            this.tSettings.Controls.Add(this.tabComfort);
            this.tSettings.Controls.Add(this.tabShortenCutscenes);
            this.tSettings.Controls.Add(this.tabCosmetics);
            this.tSettings.Location = new System.Drawing.Point(4, 28);
            this.tSettings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tSettings.Name = "tSettings";
            this.tSettings.SelectedIndex = 0;
            this.tSettings.Size = new System.Drawing.Size(892, 389);
            this.tSettings.TabIndex = 10;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tOtherCustomizations);
            this.tabMain.Controls.Add(this.groupBox9);
            this.tabMain.Controls.Add(this.groupBox6);
            this.tabMain.Controls.Add(this.groupBox4);
            this.tabMain.Location = new System.Drawing.Point(4, 24);
            this.tabMain.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabMain.Size = new System.Drawing.Size(884, 361);
            this.tabMain.TabIndex = 0;
            this.tabMain.Text = "Main Settings";
            this.tabMain.UseVisualStyleBackColor = true;
            // 
            // tOtherCustomizations
            // 
            this.tOtherCustomizations.Controls.Add(this.tOtherCustomization);
            this.tOtherCustomizations.Location = new System.Drawing.Point(7, 195);
            this.tOtherCustomizations.Name = "tOtherCustomizations";
            this.tOtherCustomizations.SelectedIndex = 0;
            this.tOtherCustomizations.Size = new System.Drawing.Size(868, 160);
            this.tOtherCustomizations.TabIndex = 22;
            // 
            // tOtherCustomization
            // 
            this.tOtherCustomization.Controls.Add(this.cRequiredBossRemains);
            this.tOtherCustomization.Controls.Add(this.lRequiredRemains);
            this.tOtherCustomization.Controls.Add(this.cShuffleBosses);
            this.tOtherCustomization.Controls.Add(this.cStartingItems);
            this.tOtherCustomization.Controls.Add(this.lStartingItems);
            this.tOtherCustomization.Controls.Add(this.cProgressiveUpgrades);
            this.tOtherCustomization.Controls.Add(this.cDEnt);
            this.tOtherCustomization.Controls.Add(this.cMixSongs);
            this.tOtherCustomization.Controls.Add(this.cEnemy);
            this.tOtherCustomization.Location = new System.Drawing.Point(4, 24);
            this.tOtherCustomization.Name = "tOtherCustomization";
            this.tOtherCustomization.Padding = new System.Windows.Forms.Padding(3);
            this.tOtherCustomization.Size = new System.Drawing.Size(860, 132);
            this.tOtherCustomization.TabIndex = 0;
            this.tOtherCustomization.Text = "Other";
            this.tOtherCustomization.UseVisualStyleBackColor = true;
            // 
            // cRequiredBossRemains
            // 
            this.cRequiredBossRemains.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cRequiredBossRemains.FormattingEnabled = true;
            this.cRequiredBossRemains.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4 (Default)"});
            this.cRequiredBossRemains.Location = new System.Drawing.Point(193, 76);
            this.cRequiredBossRemains.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cRequiredBossRemains.Name = "cRequiredBossRemains";
            this.cRequiredBossRemains.Size = new System.Drawing.Size(160, 23);
            this.cRequiredBossRemains.TabIndex = 30;
            // 
            // lRequiredRemains
            // 
            this.lRequiredRemains.AutoSize = true;
            this.lRequiredRemains.Location = new System.Drawing.Point(193, 61);
            this.lRequiredRemains.Name = "lRequiredRemains";
            this.lRequiredRemains.Size = new System.Drawing.Size(134, 15);
            this.lRequiredRemains.TabIndex = 29;
            this.lRequiredRemains.Text = "Boss Remains For Moon";
            // 
            // cShuffleBosses
            // 
            this.cShuffleBosses.AutoSize = true;
            this.cShuffleBosses.BackColor = System.Drawing.Color.Transparent;
            this.cShuffleBosses.ForeColor = System.Drawing.Color.Black;
            this.cShuffleBosses.Location = new System.Drawing.Point(193, 10);
            this.cShuffleBosses.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cShuffleBosses.Name = "cShuffleBosses";
            this.cShuffleBosses.Size = new System.Drawing.Size(127, 19);
            this.cShuffleBosses.TabIndex = 28;
            this.cShuffleBosses.Text = "Shuffle boss rooms";
            this.cShuffleBosses.UseVisualStyleBackColor = false;
            // 
            // cStartingItems
            // 
            this.cStartingItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cStartingItems.FormattingEnabled = true;
            this.cStartingItems.Items.AddRange(new object[] {
            "None",
            "Random",
            "Allow Temporary Items"});
            this.cStartingItems.Location = new System.Drawing.Point(6, 76);
            this.cStartingItems.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cStartingItems.Name = "cStartingItems";
            this.cStartingItems.Size = new System.Drawing.Size(160, 23);
            this.cStartingItems.TabIndex = 27;
            // 
            // lStartingItems
            // 
            this.lStartingItems.AutoSize = true;
            this.lStartingItems.Location = new System.Drawing.Point(6, 61);
            this.lStartingItems.Name = "lStartingItems";
            this.lStartingItems.Size = new System.Drawing.Size(80, 15);
            this.lStartingItems.TabIndex = 22;
            this.lStartingItems.Text = "Starting Items";
            // 
            // cProgressiveUpgrades
            // 
            this.cProgressiveUpgrades.AutoSize = true;
            this.cProgressiveUpgrades.BackColor = System.Drawing.Color.Transparent;
            this.cProgressiveUpgrades.ForeColor = System.Drawing.Color.Black;
            this.cProgressiveUpgrades.Location = new System.Drawing.Point(380, 10);
            this.cProgressiveUpgrades.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cProgressiveUpgrades.Name = "cProgressiveUpgrades";
            this.cProgressiveUpgrades.Size = new System.Drawing.Size(139, 19);
            this.cProgressiveUpgrades.TabIndex = 21;
            this.cProgressiveUpgrades.Text = "Progressive Upgrades";
            this.cProgressiveUpgrades.UseVisualStyleBackColor = false;
            // 
            // cDEnt
            // 
            this.cDEnt.AutoSize = true;
            this.cDEnt.BackColor = System.Drawing.Color.Transparent;
            this.cDEnt.ForeColor = System.Drawing.Color.Black;
            this.cDEnt.Location = new System.Drawing.Point(6, 10);
            this.cDEnt.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cDEnt.Name = "cDEnt";
            this.cDEnt.Size = new System.Drawing.Size(168, 19);
            this.cDEnt.TabIndex = 7;
            this.cDEnt.Text = "Shuffle dungeon entrances";
            this.cDEnt.UseVisualStyleBackColor = false;
            // 
            // cMixSongs
            // 
            this.cMixSongs.AutoSize = true;
            this.cMixSongs.BackColor = System.Drawing.Color.Transparent;
            this.cMixSongs.ForeColor = System.Drawing.Color.Black;
            this.cMixSongs.Location = new System.Drawing.Point(6, 36);
            this.cMixSongs.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cMixSongs.Name = "cMixSongs";
            this.cMixSongs.Size = new System.Drawing.Size(138, 19);
            this.cMixSongs.TabIndex = 3;
            this.cMixSongs.Text = "Mix songs with items";
            this.cMixSongs.UseVisualStyleBackColor = false;
            // 
            // cEnemy
            // 
            this.cEnemy.AutoSize = true;
            this.cEnemy.BackColor = System.Drawing.Color.Transparent;
            this.cEnemy.ForeColor = System.Drawing.Color.Black;
            this.cEnemy.Location = new System.Drawing.Point(193, 36);
            this.cEnemy.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cEnemy.Name = "cEnemy";
            this.cEnemy.Size = new System.Drawing.Size(149, 19);
            this.cEnemy.TabIndex = 9;
            this.cEnemy.Text = "Enemies/Actors (BETA!)";
            this.cEnemy.UseVisualStyleBackColor = false;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.lNumTricksEnabled);
            this.groupBox9.Controls.Add(this.lItemPlacement);
            this.groupBox9.Controls.Add(this.cItemPlacement);
            this.groupBox9.Controls.Add(this.bToggleTricks);
            this.groupBox9.Controls.Add(this.cMode);
            this.groupBox9.Controls.Add(this.bLoadLogic);
            this.groupBox9.Controls.Add(this.lMode);
            this.groupBox9.Controls.Add(this.tbUserLogic);
            this.groupBox9.Location = new System.Drawing.Point(7, 7);
            this.groupBox9.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox9.Size = new System.Drawing.Size(431, 172);
            this.groupBox9.TabIndex = 29;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Generation Settings";
            // 
            // lNumTricksEnabled
            // 
            this.lNumTricksEnabled.Location = new System.Drawing.Point(96, 22);
            this.lNumTricksEnabled.Name = "lNumTricksEnabled";
            this.lNumTricksEnabled.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lNumTricksEnabled.Size = new System.Drawing.Size(271, 18);
            this.lNumTricksEnabled.TabIndex = 21;
            this.lNumTricksEnabled.Text = "0 tricks enabled";
            // 
            // lItemPlacement
            // 
            this.lItemPlacement.AutoSize = true;
            this.lItemPlacement.BackColor = System.Drawing.Color.Transparent;
            this.lItemPlacement.ForeColor = System.Drawing.Color.Black;
            this.lItemPlacement.Location = new System.Drawing.Point(13, 111);
            this.lItemPlacement.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lItemPlacement.Name = "lItemPlacement";
            this.lItemPlacement.Size = new System.Drawing.Size(126, 15);
            this.lItemPlacement.TabIndex = 21;
            this.lItemPlacement.Text = "Item Placement Order:";
            // 
            // cItemPlacement
            // 
            this.cItemPlacement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cItemPlacement.FormattingEnabled = true;
            this.cItemPlacement.Items.AddRange(new object[] {
            "Random",
            "Bespoke",
            "Classic"});
            this.cItemPlacement.Location = new System.Drawing.Point(142, 104);
            this.cItemPlacement.Name = "cItemPlacement";
            this.cItemPlacement.Size = new System.Drawing.Size(121, 23);
            this.cItemPlacement.TabIndex = 20;
            // 
            // bToggleTricks
            // 
            this.bToggleTricks.Location = new System.Drawing.Point(328, 39);
            this.bToggleTricks.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bToggleTricks.Name = "bToggleTricks";
            this.bToggleTricks.Size = new System.Drawing.Size(96, 25);
            this.bToggleTricks.TabIndex = 19;
            this.bToggleTricks.Text = "Edit Tricks";
            this.bToggleTricks.UseVisualStyleBackColor = true;
            this.bToggleTricks.Click += new System.EventHandler(this.bToggleTricks_Click);
            // 
            // cMode
            // 
            this.cMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cMode.FormattingEnabled = true;
            this.cMode.Items.AddRange(new object[] {
            "Casual",
            "Glitched",
            "Vanilla Layout",
            "User Logic",
            "No Logic"});
            this.cMode.Location = new System.Drawing.Point(96, 40);
            this.cMode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cMode.Name = "cMode";
            this.cMode.Size = new System.Drawing.Size(224, 23);
            this.cMode.TabIndex = 1;
            this.cMode.SelectedIndexChanged += new System.EventHandler(this.cMode_SelectedIndexChanged);
            // 
            // bLoadLogic
            // 
            this.bLoadLogic.Location = new System.Drawing.Point(13, 71);
            this.bLoadLogic.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bLoadLogic.Name = "bLoadLogic";
            this.bLoadLogic.Size = new System.Drawing.Size(79, 28);
            this.bLoadLogic.TabIndex = 17;
            this.bLoadLogic.Text = "Open Logic";
            this.bLoadLogic.UseVisualStyleBackColor = true;
            this.bLoadLogic.Click += new System.EventHandler(this.bLoadLogic_Click);
            // 
            // lMode
            // 
            this.lMode.AutoSize = true;
            this.lMode.BackColor = System.Drawing.Color.Transparent;
            this.lMode.ForeColor = System.Drawing.Color.Black;
            this.lMode.Location = new System.Drawing.Point(10, 43);
            this.lMode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lMode.Name = "lMode";
            this.lMode.Size = new System.Drawing.Size(75, 15);
            this.lMode.TabIndex = 0;
            this.lMode.Text = "Mode/Logic:";
            // 
            // tbUserLogic
            // 
            this.tbUserLogic.Location = new System.Drawing.Point(96, 74);
            this.tbUserLogic.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbUserLogic.Name = "tbUserLogic";
            this.tbUserLogic.ReadOnly = true;
            this.tbUserLogic.Size = new System.Drawing.Size(328, 23);
            this.tbUserLogic.TabIndex = 18;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.tJunkLocationsList);
            this.groupBox6.Controls.Add(this.bJunkLocationsEditor);
            this.groupBox6.Controls.Add(this.lJunkLocationsAmount);
            this.groupBox6.Location = new System.Drawing.Point(446, 96);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox6.Size = new System.Drawing.Size(429, 83);
            this.groupBox6.TabIndex = 28;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Enforce Junk Locations";
            // 
            // tJunkLocationsList
            // 
            this.tJunkLocationsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tJunkLocationsList.Location = new System.Drawing.Point(13, 51);
            this.tJunkLocationsList.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tJunkLocationsList.Name = "tJunkLocationsList";
            this.tJunkLocationsList.Size = new System.Drawing.Size(361, 23);
            this.tJunkLocationsList.TabIndex = 26;
            this.tJunkLocationsList.Text = "--";
            this.tJunkLocationsList.TextChanged += new System.EventHandler(this.tJunkLocationsList_TextChanged);
            // 
            // bJunkLocationsEditor
            // 
            this.bJunkLocationsEditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bJunkLocationsEditor.Location = new System.Drawing.Point(371, 50);
            this.bJunkLocationsEditor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bJunkLocationsEditor.Name = "bJunkLocationsEditor";
            this.bJunkLocationsEditor.Size = new System.Drawing.Size(46, 25);
            this.bJunkLocationsEditor.TabIndex = 26;
            this.bJunkLocationsEditor.Text = "Edit";
            this.bJunkLocationsEditor.UseVisualStyleBackColor = true;
            this.bJunkLocationsEditor.Click += new System.EventHandler(this.bJunkLocationsEditor_Click);
            // 
            // lJunkLocationsAmount
            // 
            this.lJunkLocationsAmount.AutoSize = true;
            this.lJunkLocationsAmount.Location = new System.Drawing.Point(10, 28);
            this.lJunkLocationsAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lJunkLocationsAmount.Name = "lJunkLocationsAmount";
            this.lJunkLocationsAmount.Size = new System.Drawing.Size(121, 15);
            this.lJunkLocationsAmount.TabIndex = 27;
            this.lJunkLocationsAmount.Text = "0/0 locations selected";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tStartingItemList);
            this.groupBox4.Controls.Add(this.lCustomStartingItemAmount);
            this.groupBox4.Controls.Add(this.bStartingItemEditor);
            this.groupBox4.Location = new System.Drawing.Point(446, 7);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox4.Size = new System.Drawing.Size(429, 83);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Extra Starting Items";
            // 
            // tStartingItemList
            // 
            this.tStartingItemList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tStartingItemList.Location = new System.Drawing.Point(13, 52);
            this.tStartingItemList.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tStartingItemList.Name = "tStartingItemList";
            this.tStartingItemList.Size = new System.Drawing.Size(361, 23);
            this.tStartingItemList.TabIndex = 26;
            this.tStartingItemList.Text = "--";
            this.tStartingItemList.TextChanged += new System.EventHandler(this.tStartingItemList_TextChanged);
            // 
            // lCustomStartingItemAmount
            // 
            this.lCustomStartingItemAmount.AutoSize = true;
            this.lCustomStartingItemAmount.Location = new System.Drawing.Point(10, 29);
            this.lCustomStartingItemAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lCustomStartingItemAmount.Name = "lCustomStartingItemAmount";
            this.lCustomStartingItemAmount.Size = new System.Drawing.Size(102, 15);
            this.lCustomStartingItemAmount.TabIndex = 27;
            this.lCustomStartingItemAmount.Text = "0/0 items selected";
            // 
            // bStartingItemEditor
            // 
            this.bStartingItemEditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bStartingItemEditor.Location = new System.Drawing.Point(372, 51);
            this.bStartingItemEditor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bStartingItemEditor.Name = "bStartingItemEditor";
            this.bStartingItemEditor.Size = new System.Drawing.Size(46, 25);
            this.bStartingItemEditor.TabIndex = 26;
            this.bStartingItemEditor.Text = "Edit";
            this.bStartingItemEditor.UseVisualStyleBackColor = true;
            this.bStartingItemEditor.Click += new System.EventHandler(this.bStartingItemEditor_Click);
            // 
            // tabItemPool
            // 
            this.tabItemPool.Controls.Add(this.cItemPoolAdvanced);
            this.tabItemPool.Controls.Add(this.pClassicItemPool);
            this.tabItemPool.Controls.Add(this.lItemPoolText);
            this.tabItemPool.Controls.Add(this.bItemPoolEdit);
            this.tabItemPool.Controls.Add(this.tItemPool);
            this.tabItemPool.Controls.Add(this.tableItemPool);
            this.tabItemPool.Controls.Add(this.pLocationCategories);
            this.tabItemPool.Location = new System.Drawing.Point(4, 24);
            this.tabItemPool.Name = "tabItemPool";
            this.tabItemPool.Size = new System.Drawing.Size(884, 361);
            this.tabItemPool.TabIndex = 6;
            this.tabItemPool.Text = "Item Randomization";
            this.tabItemPool.UseVisualStyleBackColor = true;
            // 
            // cItemPoolAdvanced
            // 
            this.cItemPoolAdvanced.AutoSize = true;
            this.cItemPoolAdvanced.Location = new System.Drawing.Point(7, 52);
            this.cItemPoolAdvanced.Name = "cItemPoolAdvanced";
            this.cItemPoolAdvanced.Size = new System.Drawing.Size(107, 19);
            this.cItemPoolAdvanced.TabIndex = 27;
            this.cItemPoolAdvanced.Text = "Advanced View";
            this.cItemPoolAdvanced.UseVisualStyleBackColor = true;
            this.cItemPoolAdvanced.CheckedChanged += new System.EventHandler(this.cItemPoolAdvanced_CheckedChanged);
            // 
            // pClassicItemPool
            // 
            this.pClassicItemPool.Location = new System.Drawing.Point(7, 77);
            this.pClassicItemPool.Name = "pClassicItemPool";
            this.pClassicItemPool.Size = new System.Drawing.Size(869, 281);
            this.pClassicItemPool.TabIndex = 26;
            // 
            // lItemPoolText
            // 
            this.lItemPoolText.AutoSize = true;
            this.lItemPoolText.Location = new System.Drawing.Point(7, 34);
            this.lItemPoolText.Name = "lItemPoolText";
            this.lItemPoolText.Size = new System.Drawing.Size(122, 15);
            this.lItemPoolText.TabIndex = 25;
            this.lItemPoolText.Text = "0/0 items randomized";
            // 
            // bItemPoolEdit
            // 
            this.bItemPoolEdit.Location = new System.Drawing.Point(801, 3);
            this.bItemPoolEdit.Name = "bItemPoolEdit";
            this.bItemPoolEdit.Size = new System.Drawing.Size(75, 25);
            this.bItemPoolEdit.TabIndex = 24;
            this.bItemPoolEdit.Text = "Edit";
            this.bItemPoolEdit.UseVisualStyleBackColor = true;
            this.bItemPoolEdit.Click += new System.EventHandler(this.bItemPoolEdit_Click);
            // 
            // tItemPool
            // 
            this.tItemPool.Location = new System.Drawing.Point(7, 4);
            this.tItemPool.Name = "tItemPool";
            this.tItemPool.Size = new System.Drawing.Size(788, 23);
            this.tItemPool.TabIndex = 23;
            this.tItemPool.TextChanged += new System.EventHandler(this.tItemPool_TextChanged);
            // 
            // tableItemPool
            // 
            this.tableItemPool.AutoScroll = true;
            this.tableItemPool.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableItemPool.ColumnCount = 1;
            this.tableItemPool.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableItemPool.Location = new System.Drawing.Point(3, 160);
            this.tableItemPool.Name = "tableItemPool";
            this.tableItemPool.RowCount = 1;
            this.tableItemPool.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableItemPool.Size = new System.Drawing.Size(878, 198);
            this.tableItemPool.TabIndex = 22;
            this.tableItemPool.Visible = false;
            // 
            // pLocationCategories
            // 
            this.pLocationCategories.Location = new System.Drawing.Point(7, 30);
            this.pLocationCategories.Name = "pLocationCategories";
            this.pLocationCategories.Size = new System.Drawing.Size(874, 129);
            this.pLocationCategories.TabIndex = 28;
            this.pLocationCategories.Visible = false;
            // 
            // tabGimmicks
            // 
            this.tabGimmicks.Controls.Add(this.gGimmicksChallenges);
            this.tabGimmicks.Controls.Add(this.gGimmicksMovement);
            this.tabGimmicks.Controls.Add(this.gTraps);
            this.tabGimmicks.Controls.Add(this.gGimmicksClock);
            this.tabGimmicks.Controls.Add(this.label4);
            this.tabGimmicks.Controls.Add(this.gGimmicksOther);
            this.tabGimmicks.Location = new System.Drawing.Point(4, 24);
            this.tabGimmicks.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabGimmicks.Name = "tabGimmicks";
            this.tabGimmicks.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabGimmicks.Size = new System.Drawing.Size(884, 361);
            this.tabGimmicks.TabIndex = 3;
            this.tabGimmicks.Text = "Gimmicks";
            this.tabGimmicks.UseVisualStyleBackColor = true;
            // 
            // gGimmicksChallenges
            // 
            this.gGimmicksChallenges.Controls.Add(this.cTakeDamageFromDexihands);
            this.gGimmicksChallenges.Controls.Add(this.label2);
            this.gGimmicksChallenges.Controls.Add(this.cTakeDamageFromGibdosFaster);
            this.gGimmicksChallenges.Controls.Add(this.cTakeDamageGettingCaught);
            this.gGimmicksChallenges.Controls.Add(this.cTakeDamageFromGorons);
            this.gGimmicksChallenges.Controls.Add(this.cTakeDamageFromDog);
            this.gGimmicksChallenges.Controls.Add(this.cTakeDamageFromVoid);
            this.gGimmicksChallenges.Controls.Add(this.cTakeDamageWhileShielding);
            this.gGimmicksChallenges.Controls.Add(this.cTakeDamageOnEpona);
            this.gGimmicksChallenges.Controls.Add(this.cFewerHealthDrops);
            this.gGimmicksChallenges.Controls.Add(this.cDType);
            this.gGimmicksChallenges.Controls.Add(this.lDType);
            this.gGimmicksChallenges.Controls.Add(this.cDeathMoonCrash);
            this.gGimmicksChallenges.Controls.Add(this.cByoAmmo);
            this.gGimmicksChallenges.Controls.Add(this.cDMult);
            this.gGimmicksChallenges.Controls.Add(this.lDMult);
            this.gGimmicksChallenges.Location = new System.Drawing.Point(388, 92);
            this.gGimmicksChallenges.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gGimmicksChallenges.Name = "gGimmicksChallenges";
            this.gGimmicksChallenges.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gGimmicksChallenges.Size = new System.Drawing.Size(290, 259);
            this.gGimmicksChallenges.TabIndex = 33;
            this.gGimmicksChallenges.TabStop = false;
            this.gGimmicksChallenges.Text = "Challenges";
            // 
            // cTakeDamageFromDexihands
            // 
            this.cTakeDamageFromDexihands.AutoSize = true;
            this.cTakeDamageFromDexihands.BackColor = System.Drawing.Color.Transparent;
            this.cTakeDamageFromDexihands.ForeColor = System.Drawing.Color.Black;
            this.cTakeDamageFromDexihands.Location = new System.Drawing.Point(153, 213);
            this.cTakeDamageFromDexihands.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cTakeDamageFromDexihands.Name = "cTakeDamageFromDexihands";
            this.cTakeDamageFromDexihands.Size = new System.Drawing.Size(112, 19);
            this.cTakeDamageFromDexihands.TabIndex = 35;
            this.cTakeDamageFromDexihands.Text = "From Dexihands";
            this.cTakeDamageFromDexihands.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 120);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 15);
            this.label2.TabIndex = 34;
            this.label2.Text = "More Damage Sources";
            // 
            // cTakeDamageFromGibdosFaster
            // 
            this.cTakeDamageFromGibdosFaster.AutoSize = true;
            this.cTakeDamageFromGibdosFaster.BackColor = System.Drawing.Color.Transparent;
            this.cTakeDamageFromGibdosFaster.ForeColor = System.Drawing.Color.Black;
            this.cTakeDamageFromGibdosFaster.Location = new System.Drawing.Point(154, 138);
            this.cTakeDamageFromGibdosFaster.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cTakeDamageFromGibdosFaster.Name = "cTakeDamageFromGibdosFaster";
            this.cTakeDamageFromGibdosFaster.Size = new System.Drawing.Size(128, 19);
            this.cTakeDamageFromGibdosFaster.TabIndex = 33;
            this.cTakeDamageFromGibdosFaster.Text = "From Gibdos Faster";
            this.cTakeDamageFromGibdosFaster.UseVisualStyleBackColor = false;
            // 
            // cTakeDamageGettingCaught
            // 
            this.cTakeDamageGettingCaught.AutoSize = true;
            this.cTakeDamageGettingCaught.BackColor = System.Drawing.Color.Transparent;
            this.cTakeDamageGettingCaught.ForeColor = System.Drawing.Color.Black;
            this.cTakeDamageGettingCaught.Location = new System.Drawing.Point(153, 163);
            this.cTakeDamageGettingCaught.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cTakeDamageGettingCaught.Name = "cTakeDamageGettingCaught";
            this.cTakeDamageGettingCaught.Size = new System.Drawing.Size(107, 19);
            this.cTakeDamageGettingCaught.TabIndex = 32;
            this.cTakeDamageGettingCaught.Text = "Getting Caught";
            this.cTakeDamageGettingCaught.UseVisualStyleBackColor = false;
            // 
            // cTakeDamageFromGorons
            // 
            this.cTakeDamageFromGorons.AutoSize = true;
            this.cTakeDamageFromGorons.BackColor = System.Drawing.Color.Transparent;
            this.cTakeDamageFromGorons.ForeColor = System.Drawing.Color.Black;
            this.cTakeDamageFromGorons.Location = new System.Drawing.Point(7, 213);
            this.cTakeDamageFromGorons.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cTakeDamageFromGorons.Name = "cTakeDamageFromGorons";
            this.cTakeDamageFromGorons.Size = new System.Drawing.Size(95, 19);
            this.cTakeDamageFromGorons.TabIndex = 31;
            this.cTakeDamageFromGorons.Text = "From Gorons";
            this.cTakeDamageFromGorons.UseVisualStyleBackColor = false;
            // 
            // cTakeDamageFromDog
            // 
            this.cTakeDamageFromDog.AutoSize = true;
            this.cTakeDamageFromDog.BackColor = System.Drawing.Color.Transparent;
            this.cTakeDamageFromDog.ForeColor = System.Drawing.Color.Black;
            this.cTakeDamageFromDog.Location = new System.Drawing.Point(153, 188);
            this.cTakeDamageFromDog.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cTakeDamageFromDog.Name = "cTakeDamageFromDog";
            this.cTakeDamageFromDog.Size = new System.Drawing.Size(79, 19);
            this.cTakeDamageFromDog.TabIndex = 30;
            this.cTakeDamageFromDog.Text = "From Dog";
            this.cTakeDamageFromDog.UseVisualStyleBackColor = false;
            // 
            // cTakeDamageFromVoid
            // 
            this.cTakeDamageFromVoid.AutoSize = true;
            this.cTakeDamageFromVoid.BackColor = System.Drawing.Color.Transparent;
            this.cTakeDamageFromVoid.ForeColor = System.Drawing.Color.Black;
            this.cTakeDamageFromVoid.Location = new System.Drawing.Point(8, 188);
            this.cTakeDamageFromVoid.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cTakeDamageFromVoid.Name = "cTakeDamageFromVoid";
            this.cTakeDamageFromVoid.Size = new System.Drawing.Size(80, 19);
            this.cTakeDamageFromVoid.TabIndex = 29;
            this.cTakeDamageFromVoid.Text = "From Void";
            this.cTakeDamageFromVoid.UseVisualStyleBackColor = false;
            // 
            // cTakeDamageWhileShielding
            // 
            this.cTakeDamageWhileShielding.AutoSize = true;
            this.cTakeDamageWhileShielding.BackColor = System.Drawing.Color.Transparent;
            this.cTakeDamageWhileShielding.ForeColor = System.Drawing.Color.Black;
            this.cTakeDamageWhileShielding.Location = new System.Drawing.Point(8, 163);
            this.cTakeDamageWhileShielding.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cTakeDamageWhileShielding.Name = "cTakeDamageWhileShielding";
            this.cTakeDamageWhileShielding.Size = new System.Drawing.Size(108, 19);
            this.cTakeDamageWhileShielding.TabIndex = 28;
            this.cTakeDamageWhileShielding.Text = "While Shielding";
            this.cTakeDamageWhileShielding.UseVisualStyleBackColor = false;
            // 
            // cTakeDamageOnEpona
            // 
            this.cTakeDamageOnEpona.AutoSize = true;
            this.cTakeDamageOnEpona.BackColor = System.Drawing.Color.Transparent;
            this.cTakeDamageOnEpona.ForeColor = System.Drawing.Color.Black;
            this.cTakeDamageOnEpona.Location = new System.Drawing.Point(8, 138);
            this.cTakeDamageOnEpona.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cTakeDamageOnEpona.Name = "cTakeDamageOnEpona";
            this.cTakeDamageOnEpona.Size = new System.Drawing.Size(78, 19);
            this.cTakeDamageOnEpona.TabIndex = 27;
            this.cTakeDamageOnEpona.Text = "On Epona";
            this.cTakeDamageOnEpona.UseVisualStyleBackColor = false;
            // 
            // cFewerHealthDrops
            // 
            this.cFewerHealthDrops.AutoSize = true;
            this.cFewerHealthDrops.BackColor = System.Drawing.Color.Transparent;
            this.cFewerHealthDrops.ForeColor = System.Drawing.Color.Black;
            this.cFewerHealthDrops.Location = new System.Drawing.Point(7, 90);
            this.cFewerHealthDrops.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cFewerHealthDrops.Name = "cFewerHealthDrops";
            this.cFewerHealthDrops.Size = new System.Drawing.Size(129, 19);
            this.cFewerHealthDrops.TabIndex = 26;
            this.cFewerHealthDrops.Text = "Fewer Health Drops";
            this.cFewerHealthDrops.UseVisualStyleBackColor = false;
            // 
            // cDType
            // 
            this.cDType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cDType.FormattingEnabled = true;
            this.cDType.Items.AddRange(new object[] {
            "Default",
            "Fire",
            "Ice",
            "Shock",
            "Knockdown",
            "Random"});
            this.cDType.Location = new System.Drawing.Point(154, 38);
            this.cDType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cDType.Name = "cDType";
            this.cDType.Size = new System.Drawing.Size(128, 23);
            this.cDType.TabIndex = 0;
            // 
            // lDType
            // 
            this.lDType.AutoSize = true;
            this.lDType.Location = new System.Drawing.Point(154, 22);
            this.lDType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lDType.Name = "lDType";
            this.lDType.Size = new System.Drawing.Size(89, 15);
            this.lDType.TabIndex = 1;
            this.lDType.Text = "Damage effects";
            // 
            // cDeathMoonCrash
            // 
            this.cDeathMoonCrash.AutoSize = true;
            this.cDeathMoonCrash.BackColor = System.Drawing.Color.Transparent;
            this.cDeathMoonCrash.ForeColor = System.Drawing.Color.Black;
            this.cDeathMoonCrash.Location = new System.Drawing.Point(7, 67);
            this.cDeathMoonCrash.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cDeathMoonCrash.Name = "cDeathMoonCrash";
            this.cDeathMoonCrash.Size = new System.Drawing.Size(136, 19);
            this.cDeathMoonCrash.TabIndex = 25;
            this.cDeathMoonCrash.Text = "Death is Moon Crash";
            this.cDeathMoonCrash.UseVisualStyleBackColor = false;
            // 
            // cByoAmmo
            // 
            this.cByoAmmo.AutoSize = true;
            this.cByoAmmo.BackColor = System.Drawing.Color.Transparent;
            this.cByoAmmo.ForeColor = System.Drawing.Color.Black;
            this.cByoAmmo.Location = new System.Drawing.Point(154, 67);
            this.cByoAmmo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cByoAmmo.Name = "cByoAmmo";
            this.cByoAmmo.Size = new System.Drawing.Size(89, 19);
            this.cByoAmmo.TabIndex = 24;
            this.cByoAmmo.Text = "BYO Ammo";
            this.cByoAmmo.UseVisualStyleBackColor = false;
            // 
            // cDMult
            // 
            this.cDMult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cDMult.FormattingEnabled = true;
            this.cDMult.Items.AddRange(new object[] {
            "Default",
            "2x",
            "4x",
            "1-hit KO",
            "Doom"});
            this.cDMult.Location = new System.Drawing.Point(7, 38);
            this.cDMult.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cDMult.Name = "cDMult";
            this.cDMult.Size = new System.Drawing.Size(129, 23);
            this.cDMult.TabIndex = 0;
            // 
            // lDMult
            // 
            this.lDMult.AutoSize = true;
            this.lDMult.Location = new System.Drawing.Point(7, 22);
            this.lDMult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lDMult.Name = "lDMult";
            this.lDMult.Size = new System.Drawing.Size(85, 15);
            this.lDMult.TabIndex = 1;
            this.lDMult.Text = "Damage mode";
            // 
            // gGimmicksMovement
            // 
            this.gGimmicksMovement.Controls.Add(this.cIronGoron);
            this.gGimmicksMovement.Controls.Add(this.cClimbMostSurfaces);
            this.gGimmicksMovement.Controls.Add(this.cHookshotAnySurface);
            this.gGimmicksMovement.Controls.Add(this.cFloors);
            this.gGimmicksMovement.Controls.Add(this.lFloors);
            this.gGimmicksMovement.Controls.Add(this.lGravity);
            this.gGimmicksMovement.Controls.Add(this.cGravity);
            this.gGimmicksMovement.Controls.Add(this.cContinuousDekuHopping);
            this.gGimmicksMovement.Location = new System.Drawing.Point(7, 7);
            this.gGimmicksMovement.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gGimmicksMovement.Name = "gGimmicksMovement";
            this.gGimmicksMovement.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gGimmicksMovement.Size = new System.Drawing.Size(190, 211);
            this.gGimmicksMovement.TabIndex = 32;
            this.gGimmicksMovement.TabStop = false;
            this.gGimmicksMovement.Text = "Movement";
            // 
            // cIronGoron
            // 
            this.cIronGoron.AutoSize = true;
            this.cIronGoron.BackColor = System.Drawing.Color.Transparent;
            this.cIronGoron.ForeColor = System.Drawing.Color.Black;
            this.cIronGoron.Location = new System.Drawing.Point(7, 185);
            this.cIronGoron.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cIronGoron.Name = "cIronGoron";
            this.cIronGoron.Size = new System.Drawing.Size(83, 19);
            this.cIronGoron.TabIndex = 29;
            this.cIronGoron.Text = "Iron Goron";
            this.cIronGoron.UseVisualStyleBackColor = false;
            // 
            // cClimbMostSurfaces
            // 
            this.cClimbMostSurfaces.AutoSize = true;
            this.cClimbMostSurfaces.BackColor = System.Drawing.Color.Transparent;
            this.cClimbMostSurfaces.ForeColor = System.Drawing.Color.Black;
            this.cClimbMostSurfaces.Location = new System.Drawing.Point(7, 160);
            this.cClimbMostSurfaces.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cClimbMostSurfaces.Name = "cClimbMostSurfaces";
            this.cClimbMostSurfaces.Size = new System.Drawing.Size(135, 19);
            this.cClimbMostSurfaces.TabIndex = 28;
            this.cClimbMostSurfaces.Text = "Climb Most Surfaces";
            this.cClimbMostSurfaces.UseVisualStyleBackColor = false;
            // 
            // cHookshotAnySurface
            // 
            this.cHookshotAnySurface.AutoSize = true;
            this.cHookshotAnySurface.BackColor = System.Drawing.Color.Transparent;
            this.cHookshotAnySurface.ForeColor = System.Drawing.Color.Black;
            this.cHookshotAnySurface.Location = new System.Drawing.Point(7, 135);
            this.cHookshotAnySurface.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cHookshotAnySurface.Name = "cHookshotAnySurface";
            this.cHookshotAnySurface.Size = new System.Drawing.Size(144, 19);
            this.cHookshotAnySurface.TabIndex = 27;
            this.cHookshotAnySurface.Text = "Hookshot Any Surface";
            this.cHookshotAnySurface.UseVisualStyleBackColor = false;
            // 
            // cFloors
            // 
            this.cFloors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cFloors.FormattingEnabled = true;
            this.cFloors.Items.AddRange(new object[] {
            "Default",
            "Sand",
            "Ice",
            "Snow",
            "Random"});
            this.cFloors.Location = new System.Drawing.Point(7, 78);
            this.cFloors.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cFloors.Name = "cFloors";
            this.cFloors.Size = new System.Drawing.Size(176, 23);
            this.cFloors.TabIndex = 0;
            // 
            // lFloors
            // 
            this.lFloors.AutoSize = true;
            this.lFloors.Location = new System.Drawing.Point(7, 62);
            this.lFloors.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lFloors.Name = "lFloors";
            this.lFloors.Size = new System.Drawing.Size(65, 15);
            this.lFloors.TabIndex = 1;
            this.lFloors.Text = "Floor types";
            // 
            // lGravity
            // 
            this.lGravity.AutoSize = true;
            this.lGravity.Location = new System.Drawing.Point(7, 18);
            this.lGravity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lGravity.Name = "lGravity";
            this.lGravity.Size = new System.Drawing.Size(87, 15);
            this.lGravity.TabIndex = 1;
            this.lGravity.Text = "Gravity / Speed";
            // 
            // cGravity
            // 
            this.cGravity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cGravity.FormattingEnabled = true;
            this.cGravity.Items.AddRange(new object[] {
            "Default",
            "High speed (many softlocks)",
            "Super low gravity",
            "Low gravity",
            "High gravity"});
            this.cGravity.Location = new System.Drawing.Point(7, 35);
            this.cGravity.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cGravity.Name = "cGravity";
            this.cGravity.Size = new System.Drawing.Size(176, 23);
            this.cGravity.TabIndex = 0;
            // 
            // cContinuousDekuHopping
            // 
            this.cContinuousDekuHopping.AutoSize = true;
            this.cContinuousDekuHopping.BackColor = System.Drawing.Color.Transparent;
            this.cContinuousDekuHopping.ForeColor = System.Drawing.Color.Black;
            this.cContinuousDekuHopping.Location = new System.Drawing.Point(7, 110);
            this.cContinuousDekuHopping.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cContinuousDekuHopping.Name = "cContinuousDekuHopping";
            this.cContinuousDekuHopping.Size = new System.Drawing.Size(168, 19);
            this.cContinuousDekuHopping.TabIndex = 26;
            this.cContinuousDekuHopping.Text = "Continuous Deku Hopping";
            this.cContinuousDekuHopping.UseVisualStyleBackColor = false;
            // 
            // gTraps
            // 
            this.gTraps.Controls.Add(this.lTrapWeightings);
            this.gTraps.Controls.Add(this.lTrapsAppearance);
            this.gTraps.Controls.Add(this.lTrapAmount);
            this.gTraps.Controls.Add(this.cTrapAmount);
            this.gTraps.Controls.Add(this.cTrapsAppearance);
            this.gTraps.Controls.Add(this.cIceTrapQuirks);
            this.gTraps.Location = new System.Drawing.Point(205, 92);
            this.gTraps.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gTraps.Name = "gTraps";
            this.gTraps.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gTraps.Size = new System.Drawing.Size(175, 259);
            this.gTraps.TabIndex = 31;
            this.gTraps.TabStop = false;
            this.gTraps.Text = "Traps";
            // 
            // lTrapWeightings
            // 
            this.lTrapWeightings.AutoSize = true;
            this.lTrapWeightings.Location = new System.Drawing.Point(7, 129);
            this.lTrapWeightings.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lTrapWeightings.Name = "lTrapWeightings";
            this.lTrapWeightings.Size = new System.Drawing.Size(92, 15);
            this.lTrapWeightings.TabIndex = 32;
            this.lTrapWeightings.Text = "Trap Weightings";
            // 
            // lTrapsAppearance
            // 
            this.lTrapsAppearance.AutoSize = true;
            this.lTrapsAppearance.Location = new System.Drawing.Point(7, 62);
            this.lTrapsAppearance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lTrapsAppearance.Name = "lTrapsAppearance";
            this.lTrapsAppearance.Size = new System.Drawing.Size(70, 15);
            this.lTrapsAppearance.TabIndex = 30;
            this.lTrapsAppearance.Text = "Appearance";
            // 
            // lTrapAmount
            // 
            this.lTrapAmount.AutoSize = true;
            this.lTrapAmount.Location = new System.Drawing.Point(7, 18);
            this.lTrapAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lTrapAmount.Name = "lTrapAmount";
            this.lTrapAmount.Size = new System.Drawing.Size(76, 15);
            this.lTrapAmount.TabIndex = 28;
            this.lTrapAmount.Text = "Trap Amount";
            // 
            // cTrapAmount
            // 
            this.cTrapAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cTrapAmount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cTrapAmount.FormattingEnabled = true;
            this.cTrapAmount.Items.AddRange(new object[] {
            "None",
            "Normal",
            "Extra",
            "Mayhem",
            "Onslaught"});
            this.cTrapAmount.Location = new System.Drawing.Point(7, 35);
            this.cTrapAmount.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cTrapAmount.Name = "cTrapAmount";
            this.cTrapAmount.Size = new System.Drawing.Size(160, 23);
            this.cTrapAmount.TabIndex = 26;
            // 
            // cTrapsAppearance
            // 
            this.cTrapsAppearance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cTrapsAppearance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cTrapsAppearance.FormattingEnabled = true;
            this.cTrapsAppearance.Items.AddRange(new object[] {
            "Major Items",
            "Junk Items",
            "Anything"});
            this.cTrapsAppearance.Location = new System.Drawing.Point(7, 78);
            this.cTrapsAppearance.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cTrapsAppearance.Name = "cTrapsAppearance";
            this.cTrapsAppearance.Size = new System.Drawing.Size(160, 23);
            this.cTrapsAppearance.TabIndex = 27;
            // 
            // cIceTrapQuirks
            // 
            this.cIceTrapQuirks.AutoSize = true;
            this.cIceTrapQuirks.BackColor = System.Drawing.Color.Transparent;
            this.cIceTrapQuirks.ForeColor = System.Drawing.Color.Black;
            this.cIceTrapQuirks.Location = new System.Drawing.Point(7, 107);
            this.cIceTrapQuirks.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cIceTrapQuirks.Name = "cIceTrapQuirks";
            this.cIceTrapQuirks.Size = new System.Drawing.Size(98, 19);
            this.cIceTrapQuirks.TabIndex = 29;
            this.cIceTrapQuirks.Text = "Enable Quirks";
            this.cIceTrapQuirks.UseVisualStyleBackColor = false;
            // 
            // gGimmicksClock
            // 
            this.gGimmicksClock.Controls.Add(this.cAutoInvert);
            this.gGimmicksClock.Controls.Add(this.lAutoInvert);
            this.gGimmicksClock.Controls.Add(this.cClockSpeed);
            this.gGimmicksClock.Controls.Add(this.lClockSpeed);
            this.gGimmicksClock.Controls.Add(this.cHideClock);
            this.gGimmicksClock.Location = new System.Drawing.Point(7, 221);
            this.gGimmicksClock.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gGimmicksClock.Name = "gGimmicksClock";
            this.gGimmicksClock.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gGimmicksClock.Size = new System.Drawing.Size(190, 130);
            this.gGimmicksClock.TabIndex = 30;
            this.gGimmicksClock.TabStop = false;
            this.gGimmicksClock.Text = "Clock";
            // 
            // cAutoInvert
            // 
            this.cAutoInvert.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cAutoInvert.FormattingEnabled = true;
            this.cAutoInvert.Items.AddRange(new object[] {
            "Never",
            "First Cycle",
            "Always"});
            this.cAutoInvert.Location = new System.Drawing.Point(7, 77);
            this.cAutoInvert.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cAutoInvert.Name = "cAutoInvert";
            this.cAutoInvert.Size = new System.Drawing.Size(176, 23);
            this.cAutoInvert.TabIndex = 18;
            // 
            // lAutoInvert
            // 
            this.lAutoInvert.AutoSize = true;
            this.lAutoInvert.Location = new System.Drawing.Point(4, 61);
            this.lAutoInvert.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lAutoInvert.Name = "lAutoInvert";
            this.lAutoInvert.Size = new System.Drawing.Size(68, 15);
            this.lAutoInvert.TabIndex = 19;
            this.lAutoInvert.Text = "Auto-invert";
            // 
            // cClockSpeed
            // 
            this.cClockSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cClockSpeed.FormattingEnabled = true;
            this.cClockSpeed.Items.AddRange(new object[] {
            "Default",
            "1/3x",
            "2/3x",
            "2x",
            "3x",
            "6x"});
            this.cClockSpeed.Location = new System.Drawing.Point(7, 35);
            this.cClockSpeed.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cClockSpeed.Name = "cClockSpeed";
            this.cClockSpeed.Size = new System.Drawing.Size(176, 23);
            this.cClockSpeed.TabIndex = 15;
            // 
            // lClockSpeed
            // 
            this.lClockSpeed.AutoSize = true;
            this.lClockSpeed.Location = new System.Drawing.Point(4, 19);
            this.lClockSpeed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lClockSpeed.Name = "lClockSpeed";
            this.lClockSpeed.Size = new System.Drawing.Size(39, 15);
            this.lClockSpeed.TabIndex = 16;
            this.lClockSpeed.Text = "Speed";
            // 
            // cHideClock
            // 
            this.cHideClock.AutoSize = true;
            this.cHideClock.BackColor = System.Drawing.Color.Transparent;
            this.cHideClock.ForeColor = System.Drawing.Color.Black;
            this.cHideClock.Location = new System.Drawing.Point(6, 106);
            this.cHideClock.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cHideClock.Name = "cHideClock";
            this.cHideClock.Size = new System.Drawing.Size(65, 19);
            this.cHideClock.TabIndex = 17;
            this.cHideClock.Text = "Hide UI";
            this.cHideClock.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(301, 12);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(266, 75);
            this.label4.TabIndex = 14;
            this.label4.Text = "WARNING!\r\nMost of these settings are not considered in logic\r\nand some can cause " +
    "the seed to be unbeatable.\r\nUse at your own risk!\r\nItems marked with * are consi" +
    "dered in logic.";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gGimmicksOther
            // 
            this.gGimmicksOther.Controls.Add(this.cVanillaMoonTrials);
            this.gGimmicksOther.Controls.Add(this.cBombArrows);
            this.gGimmicksOther.Controls.Add(this.cGiantMaskAnywhere);
            this.gGimmicksOther.Controls.Add(this.cInstantTransformations);
            this.gGimmicksOther.Controls.Add(this.cFreeScarecrow);
            this.gGimmicksOther.Controls.Add(this.cFDAnywhere);
            this.gGimmicksOther.Controls.Add(this.cBlastCooldown);
            this.gGimmicksOther.Controls.Add(this.cUnderwaterOcarina);
            this.gGimmicksOther.Controls.Add(this.cSunsSong);
            this.gGimmicksOther.Controls.Add(this.lBlastMask);
            this.gGimmicksOther.Controls.Add(this.lNutAndStickDrops);
            this.gGimmicksOther.Controls.Add(this.cNutAndStickDrops);
            this.gGimmicksOther.Location = new System.Drawing.Point(686, 7);
            this.gGimmicksOther.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gGimmicksOther.Name = "gGimmicksOther";
            this.gGimmicksOther.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gGimmicksOther.Size = new System.Drawing.Size(190, 344);
            this.gGimmicksOther.TabIndex = 34;
            this.gGimmicksOther.TabStop = false;
            this.gGimmicksOther.Text = "Other";
            // 
            // cVanillaMoonTrials
            // 
            this.cVanillaMoonTrials.AutoSize = true;
            this.cVanillaMoonTrials.BackColor = System.Drawing.Color.Transparent;
            this.cVanillaMoonTrials.ForeColor = System.Drawing.Color.Black;
            this.cVanillaMoonTrials.Location = new System.Drawing.Point(7, 285);
            this.cVanillaMoonTrials.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cVanillaMoonTrials.Name = "cVanillaMoonTrials";
            this.cVanillaMoonTrials.Size = new System.Drawing.Size(158, 19);
            this.cVanillaMoonTrials.TabIndex = 30;
            this.cVanillaMoonTrials.Text = "Vanilla Moon Trial Access";
            this.cVanillaMoonTrials.UseVisualStyleBackColor = false;
            // 
            // cBombArrows
            // 
            this.cBombArrows.AutoSize = true;
            this.cBombArrows.BackColor = System.Drawing.Color.Transparent;
            this.cBombArrows.ForeColor = System.Drawing.Color.Black;
            this.cBombArrows.Location = new System.Drawing.Point(7, 260);
            this.cBombArrows.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cBombArrows.Name = "cBombArrows";
            this.cBombArrows.Size = new System.Drawing.Size(98, 19);
            this.cBombArrows.TabIndex = 29;
            this.cBombArrows.Text = "Bomb Arrows";
            this.cBombArrows.UseVisualStyleBackColor = false;
            // 
            // cGiantMaskAnywhere
            // 
            this.cGiantMaskAnywhere.AutoSize = true;
            this.cGiantMaskAnywhere.BackColor = System.Drawing.Color.Transparent;
            this.cGiantMaskAnywhere.ForeColor = System.Drawing.Color.Black;
            this.cGiantMaskAnywhere.Location = new System.Drawing.Point(7, 210);
            this.cGiantMaskAnywhere.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cGiantMaskAnywhere.Name = "cGiantMaskAnywhere";
            this.cGiantMaskAnywhere.Size = new System.Drawing.Size(147, 19);
            this.cGiantMaskAnywhere.TabIndex = 28;
            this.cGiantMaskAnywhere.Text = "Giant\'s Mask anywhere";
            this.cGiantMaskAnywhere.UseVisualStyleBackColor = false;
            // 
            // cInstantTransformations
            // 
            this.cInstantTransformations.AutoSize = true;
            this.cInstantTransformations.BackColor = System.Drawing.Color.Transparent;
            this.cInstantTransformations.ForeColor = System.Drawing.Color.Black;
            this.cInstantTransformations.Location = new System.Drawing.Point(7, 235);
            this.cInstantTransformations.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cInstantTransformations.Name = "cInstantTransformations";
            this.cInstantTransformations.Size = new System.Drawing.Size(150, 19);
            this.cInstantTransformations.TabIndex = 28;
            this.cInstantTransformations.Text = "Instant Transformations";
            this.cInstantTransformations.UseVisualStyleBackColor = false;
            // 
            // cFreeScarecrow
            // 
            this.cFreeScarecrow.AutoSize = true;
            this.cFreeScarecrow.BackColor = System.Drawing.Color.Transparent;
            this.cFreeScarecrow.ForeColor = System.Drawing.Color.Black;
            this.cFreeScarecrow.Location = new System.Drawing.Point(7, 160);
            this.cFreeScarecrow.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cFreeScarecrow.Name = "cFreeScarecrow";
            this.cFreeScarecrow.Size = new System.Drawing.Size(143, 19);
            this.cFreeScarecrow.TabIndex = 27;
            this.cFreeScarecrow.Text = "Free Scarecrow\'s Song";
            this.cFreeScarecrow.UseVisualStyleBackColor = false;
            // 
            // cFDAnywhere
            // 
            this.cFDAnywhere.AutoSize = true;
            this.cFDAnywhere.BackColor = System.Drawing.Color.Transparent;
            this.cFDAnywhere.ForeColor = System.Drawing.Color.Black;
            this.cFDAnywhere.Location = new System.Drawing.Point(7, 185);
            this.cFDAnywhere.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cFDAnywhere.Name = "cFDAnywhere";
            this.cFDAnywhere.Size = new System.Drawing.Size(180, 19);
            this.cFDAnywhere.TabIndex = 23;
            this.cFDAnywhere.Text = "Fierce Deity\'s Mask anywhere";
            this.cFDAnywhere.UseVisualStyleBackColor = false;
            // 
            // cBlastCooldown
            // 
            this.cBlastCooldown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBlastCooldown.FormattingEnabled = true;
            this.cBlastCooldown.Items.AddRange(new object[] {
            "Default",
            "Instant",
            "Very short",
            "Short",
            "Long",
            "Very Long"});
            this.cBlastCooldown.Location = new System.Drawing.Point(7, 35);
            this.cBlastCooldown.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cBlastCooldown.Name = "cBlastCooldown";
            this.cBlastCooldown.Size = new System.Drawing.Size(123, 23);
            this.cBlastCooldown.TabIndex = 20;
            // 
            // cUnderwaterOcarina
            // 
            this.cUnderwaterOcarina.AutoSize = true;
            this.cUnderwaterOcarina.BackColor = System.Drawing.Color.Transparent;
            this.cUnderwaterOcarina.Location = new System.Drawing.Point(7, 107);
            this.cUnderwaterOcarina.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cUnderwaterOcarina.Name = "cUnderwaterOcarina";
            this.cUnderwaterOcarina.Size = new System.Drawing.Size(131, 19);
            this.cUnderwaterOcarina.TabIndex = 22;
            this.cUnderwaterOcarina.Text = "Underwater Ocarina";
            this.cUnderwaterOcarina.UseVisualStyleBackColor = false;
            // 
            // cSunsSong
            // 
            this.cSunsSong.AutoSize = true;
            this.cSunsSong.BackColor = System.Drawing.Color.Transparent;
            this.cSunsSong.ForeColor = System.Drawing.Color.Black;
            this.cSunsSong.Location = new System.Drawing.Point(7, 133);
            this.cSunsSong.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cSunsSong.Name = "cSunsSong";
            this.cSunsSong.Size = new System.Drawing.Size(122, 19);
            this.cSunsSong.TabIndex = 21;
            this.cSunsSong.Text = "Enable Sun\'s Song";
            this.cSunsSong.UseVisualStyleBackColor = false;
            // 
            // lBlastMask
            // 
            this.lBlastMask.AutoSize = true;
            this.lBlastMask.Location = new System.Drawing.Point(8, 18);
            this.lBlastMask.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lBlastMask.Name = "lBlastMask";
            this.lBlastMask.Size = new System.Drawing.Size(121, 15);
            this.lBlastMask.TabIndex = 19;
            this.lBlastMask.Text = "Blast Mask Cooldown";
            // 
            // lNutAndStickDrops
            // 
            this.lNutAndStickDrops.AutoSize = true;
            this.lNutAndStickDrops.Location = new System.Drawing.Point(8, 62);
            this.lNutAndStickDrops.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lNutAndStickDrops.Name = "lNutAndStickDrops";
            this.lNutAndStickDrops.Size = new System.Drawing.Size(112, 15);
            this.lNutAndStickDrops.TabIndex = 25;
            this.lNutAndStickDrops.Text = "Nut and Stick Drops";
            // 
            // cNutAndStickDrops
            // 
            this.cNutAndStickDrops.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cNutAndStickDrops.FormattingEnabled = true;
            this.cNutAndStickDrops.Items.AddRange(new object[] {
            "Default",
            "Light",
            "Medium",
            "Extra",
            "Mayhem"});
            this.cNutAndStickDrops.Location = new System.Drawing.Point(7, 78);
            this.cNutAndStickDrops.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cNutAndStickDrops.Name = "cNutAndStickDrops";
            this.cNutAndStickDrops.Size = new System.Drawing.Size(123, 23);
            this.cNutAndStickDrops.TabIndex = 26;
            // 
            // tabComfort
            // 
            this.tabComfort.Controls.Add(this.gHintsGeneral);
            this.tabComfort.Controls.Add(this.gGaroHints);
            this.tabComfort.Controls.Add(this.gSpeedUps);
            this.tabComfort.Controls.Add(this.gHints);
            this.tabComfort.Controls.Add(this.groupBox8);
            this.tabComfort.Controls.Add(this.groupBox7);
            this.tabComfort.Location = new System.Drawing.Point(4, 24);
            this.tabComfort.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabComfort.Name = "tabComfort";
            this.tabComfort.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabComfort.Size = new System.Drawing.Size(884, 361);
            this.tabComfort.TabIndex = 1;
            this.tabComfort.Text = "Comfort";
            this.tabComfort.UseVisualStyleBackColor = true;
            // 
            // gHintsGeneral
            // 
            this.gHintsGeneral.Controls.Add(this.cFairyAndSkullHints);
            this.gHintsGeneral.Controls.Add(this.cRemainsHint);
            this.gHintsGeneral.Controls.Add(this.cOathHint);
            this.gHintsGeneral.Controls.Add(this.bCustomizeHintPriorities);
            this.gHintsGeneral.Controls.Add(this.cHintImportance);
            this.gHintsGeneral.Controls.Add(this.cMixGaroWithGossip);
            this.gHintsGeneral.Location = new System.Drawing.Point(7, 250);
            this.gHintsGeneral.Name = "gHintsGeneral";
            this.gHintsGeneral.Size = new System.Drawing.Size(341, 96);
            this.gHintsGeneral.TabIndex = 39;
            this.gHintsGeneral.TabStop = false;
            this.gHintsGeneral.Text = "Hints";
            // 
            // cFairyAndSkullHints
            // 
            this.cFairyAndSkullHints.AutoSize = true;
            this.cFairyAndSkullHints.Location = new System.Drawing.Point(200, 70);
            this.cFairyAndSkullHints.Name = "cFairyAndSkullHints";
            this.cFairyAndSkullHints.Size = new System.Drawing.Size(115, 19);
            this.cFairyAndSkullHints.TabIndex = 19;
            this.cFairyAndSkullHints.Text = "Fairies and Skulls";
            this.cFairyAndSkullHints.UseVisualStyleBackColor = true;
            // 
            // cRemainsHint
            // 
            this.cRemainsHint.AutoSize = true;
            this.cRemainsHint.Location = new System.Drawing.Point(200, 20);
            this.cRemainsHint.Name = "cRemainsHint";
            this.cRemainsHint.Size = new System.Drawing.Size(98, 19);
            this.cRemainsHint.TabIndex = 18;
            this.cRemainsHint.Text = "Boss Remains";
            this.cRemainsHint.UseVisualStyleBackColor = true;
            // 
            // cOathHint
            // 
            this.cOathHint.AutoSize = true;
            this.cOathHint.Location = new System.Drawing.Point(200, 45);
            this.cOathHint.Name = "cOathHint";
            this.cOathHint.Size = new System.Drawing.Size(52, 19);
            this.cOathHint.TabIndex = 17;
            this.cOathHint.Text = "Oath";
            this.cOathHint.UseVisualStyleBackColor = true;
            // 
            // bCustomizeHintPriorities
            // 
            this.bCustomizeHintPriorities.Location = new System.Drawing.Point(10, 67);
            this.bCustomizeHintPriorities.Name = "bCustomizeHintPriorities";
            this.bCustomizeHintPriorities.Size = new System.Drawing.Size(184, 23);
            this.bCustomizeHintPriorities.TabIndex = 16;
            this.bCustomizeHintPriorities.Text = "Customize Hint Priorities";
            this.bCustomizeHintPriorities.UseVisualStyleBackColor = true;
            this.bCustomizeHintPriorities.Click += new System.EventHandler(this.bCustomizeHintPriorities_Click);
            // 
            // cHintImportance
            // 
            this.cHintImportance.AutoSize = true;
            this.cHintImportance.Location = new System.Drawing.Point(11, 20);
            this.cHintImportance.Name = "cHintImportance";
            this.cHintImportance.Size = new System.Drawing.Size(132, 19);
            this.cHintImportance.TabIndex = 0;
            this.cHintImportance.Text = "Indicate Importance";
            this.cHintImportance.UseVisualStyleBackColor = true;
            // 
            // cMixGaroWithGossip
            // 
            this.cMixGaroWithGossip.AutoSize = true;
            this.cMixGaroWithGossip.BackColor = System.Drawing.Color.Transparent;
            this.cMixGaroWithGossip.ForeColor = System.Drawing.Color.Black;
            this.cMixGaroWithGossip.Location = new System.Drawing.Point(11, 45);
            this.cMixGaroWithGossip.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cMixGaroWithGossip.Name = "cMixGaroWithGossip";
            this.cMixGaroWithGossip.Size = new System.Drawing.Size(166, 19);
            this.cMixGaroWithGossip.TabIndex = 15;
            this.cMixGaroWithGossip.Text = "Mix Gossip and Garo Hints";
            this.cMixGaroWithGossip.UseVisualStyleBackColor = false;
            // 
            // gGaroHints
            // 
            this.gGaroHints.Controls.Add(this.cImportanceCountGaro);
            this.gGaroHints.Controls.Add(this.cFreeGaroHints);
            this.gGaroHints.Controls.Add(this.cCustomGaroWoth);
            this.gGaroHints.Controls.Add(this.nMaxGaroCT);
            this.gGaroHints.Controls.Add(this.lGaroHints);
            this.gGaroHints.Controls.Add(this.nMaxGaroFoolish);
            this.gGaroHints.Controls.Add(this.cGaroHint);
            this.gGaroHints.Controls.Add(this.nMaxGaroWotH);
            this.gGaroHints.Controls.Add(this.cClearGaroHints);
            this.gGaroHints.Controls.Add(this.label5);
            this.gGaroHints.Controls.Add(this.label10);
            this.gGaroHints.Controls.Add(this.label9);
            this.gGaroHints.Location = new System.Drawing.Point(260, 122);
            this.gGaroHints.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gGaroHints.Name = "gGaroHints";
            this.gGaroHints.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gGaroHints.Size = new System.Drawing.Size(246, 120);
            this.gGaroHints.TabIndex = 38;
            this.gGaroHints.TabStop = false;
            this.gGaroHints.Text = "Garo Hints";
            // 
            // cFreeGaroHints
            // 
            this.cFreeGaroHints.AutoSize = true;
            this.cFreeGaroHints.BackColor = System.Drawing.Color.Transparent;
            this.cFreeGaroHints.ForeColor = System.Drawing.Color.Black;
            this.cFreeGaroHints.Location = new System.Drawing.Point(117, 13);
            this.cFreeGaroHints.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cFreeGaroHints.Name = "cFreeGaroHints";
            this.cFreeGaroHints.Size = new System.Drawing.Size(77, 19);
            this.cFreeGaroHints.TabIndex = 37;
            this.cFreeGaroHints.Text = "Free hints";
            this.cFreeGaroHints.UseVisualStyleBackColor = false;
            // 
            // cCustomGaroWoth
            // 
            this.cCustomGaroWoth.AutoSize = true;
            this.cCustomGaroWoth.Checked = true;
            this.cCustomGaroWoth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cCustomGaroWoth.Location = new System.Drawing.Point(15, 94);
            this.cCustomGaroWoth.Name = "cCustomGaroWoth";
            this.cCustomGaroWoth.Size = new System.Drawing.Size(15, 14);
            this.cCustomGaroWoth.TabIndex = 36;
            this.cCustomGaroWoth.UseVisualStyleBackColor = true;
            this.cCustomGaroWoth.CheckedChanged += new System.EventHandler(this.cCustomGaroWoth_CheckedChanged);
            // 
            // nMaxGaroCT
            // 
            this.nMaxGaroCT.Location = new System.Drawing.Point(160, 91);
            this.nMaxGaroCT.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.nMaxGaroCT.Name = "nMaxGaroCT";
            this.nMaxGaroCT.Size = new System.Drawing.Size(31, 23);
            this.nMaxGaroCT.TabIndex = 35;
            // 
            // lGaroHints
            // 
            this.lGaroHints.AutoSize = true;
            this.lGaroHints.BackColor = System.Drawing.Color.Transparent;
            this.lGaroHints.ForeColor = System.Drawing.Color.Black;
            this.lGaroHints.Location = new System.Drawing.Point(13, 24);
            this.lGaroHints.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lGaroHints.Name = "lGaroHints";
            this.lGaroHints.Size = new System.Drawing.Size(95, 15);
            this.lGaroHints.TabIndex = 20;
            this.lGaroHints.Text = "Hint Distribution";
            // 
            // nMaxGaroFoolish
            // 
            this.nMaxGaroFoolish.Location = new System.Drawing.Point(108, 91);
            this.nMaxGaroFoolish.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.nMaxGaroFoolish.Name = "nMaxGaroFoolish";
            this.nMaxGaroFoolish.Size = new System.Drawing.Size(31, 23);
            this.nMaxGaroFoolish.TabIndex = 34;
            this.nMaxGaroFoolish.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // cGaroHint
            // 
            this.cGaroHint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cGaroHint.FormattingEnabled = true;
            this.cGaroHint.Items.AddRange(new object[] {
            "Default",
            "Random",
            "Relevant",
            "Competitive"});
            this.cGaroHint.Location = new System.Drawing.Point(15, 39);
            this.cGaroHint.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cGaroHint.Name = "cGaroHint";
            this.cGaroHint.Size = new System.Drawing.Size(92, 23);
            this.cGaroHint.TabIndex = 19;
            // 
            // nMaxGaroWotH
            // 
            this.nMaxGaroWotH.Location = new System.Drawing.Point(58, 91);
            this.nMaxGaroWotH.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.nMaxGaroWotH.Name = "nMaxGaroWotH";
            this.nMaxGaroWotH.Size = new System.Drawing.Size(31, 23);
            this.nMaxGaroWotH.TabIndex = 33;
            this.nMaxGaroWotH.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // cClearGaroHints
            // 
            this.cClearGaroHints.AutoSize = true;
            this.cClearGaroHints.BackColor = System.Drawing.Color.Transparent;
            this.cClearGaroHints.ForeColor = System.Drawing.Color.Black;
            this.cClearGaroHints.Location = new System.Drawing.Point(117, 34);
            this.cClearGaroHints.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cClearGaroHints.Name = "cClearGaroHints";
            this.cClearGaroHints.Size = new System.Drawing.Size(82, 19);
            this.cClearGaroHints.TabIndex = 16;
            this.cClearGaroHints.Text = "Clear hints";
            this.cClearGaroHints.UseVisualStyleBackColor = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(145, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(12, 15);
            this.label5.TabIndex = 32;
            this.label5.Text = "/";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(58, 76);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(141, 15);
            this.label10.TabIndex = 30;
            this.label10.Text = "WotH  / Foolish / Max CT";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(95, 93);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(12, 15);
            this.label9.TabIndex = 31;
            this.label9.Text = "/";
            // 
            // gSpeedUps
            // 
            this.gSpeedUps.Controls.Add(this.cSpeedupBabyCucco);
            this.gSpeedUps.Controls.Add(this.cDoubleArcheryRewards);
            this.gSpeedUps.Controls.Add(this.cFasterBank);
            this.gSpeedUps.Controls.Add(this.cSkipBeaver);
            this.gSpeedUps.Controls.Add(this.cFasterLabFish);
            this.gSpeedUps.Controls.Add(this.cGoodDogRaceRNG);
            this.gSpeedUps.Controls.Add(this.cGoodDampeRNG);
            this.gSpeedUps.Location = new System.Drawing.Point(7, 7);
            this.gSpeedUps.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gSpeedUps.Name = "gSpeedUps";
            this.gSpeedUps.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gSpeedUps.Size = new System.Drawing.Size(499, 109);
            this.gSpeedUps.TabIndex = 37;
            this.gSpeedUps.TabStop = false;
            this.gSpeedUps.Text = "Speed Ups";
            // 
            // cSpeedupBabyCucco
            // 
            this.cSpeedupBabyCucco.AutoSize = true;
            this.cSpeedupBabyCucco.Location = new System.Drawing.Point(292, 25);
            this.cSpeedupBabyCucco.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cSpeedupBabyCucco.Name = "cSpeedupBabyCucco";
            this.cSpeedupBabyCucco.Size = new System.Drawing.Size(121, 19);
            this.cSpeedupBabyCucco.TabIndex = 6;
            this.cSpeedupBabyCucco.Text = "Baby Cuccos Map";
            this.cSpeedupBabyCucco.UseVisualStyleBackColor = true;
            // 
            // cDoubleArcheryRewards
            // 
            this.cDoubleArcheryRewards.AutoSize = true;
            this.cDoubleArcheryRewards.Location = new System.Drawing.Point(140, 77);
            this.cDoubleArcheryRewards.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cDoubleArcheryRewards.Name = "cDoubleArcheryRewards";
            this.cDoubleArcheryRewards.Size = new System.Drawing.Size(155, 19);
            this.cDoubleArcheryRewards.TabIndex = 5;
            this.cDoubleArcheryRewards.Text = "Double Archery Rewards";
            this.cDoubleArcheryRewards.UseVisualStyleBackColor = true;
            // 
            // cFasterBank
            // 
            this.cFasterBank.AutoSize = true;
            this.cFasterBank.Location = new System.Drawing.Point(10, 77);
            this.cFasterBank.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cFasterBank.Name = "cFasterBank";
            this.cFasterBank.Size = new System.Drawing.Size(86, 19);
            this.cFasterBank.TabIndex = 4;
            this.cFasterBank.Text = "Faster Bank";
            this.cFasterBank.UseVisualStyleBackColor = true;
            // 
            // cSkipBeaver
            // 
            this.cSkipBeaver.AutoSize = true;
            this.cSkipBeaver.Location = new System.Drawing.Point(140, 25);
            this.cSkipBeaver.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cSkipBeaver.Name = "cSkipBeaver";
            this.cSkipBeaver.Size = new System.Drawing.Size(133, 19);
            this.cSkipBeaver.TabIndex = 0;
            this.cSkipBeaver.Text = "Skip Younger Beaver";
            this.cSkipBeaver.UseVisualStyleBackColor = true;
            // 
            // cFasterLabFish
            // 
            this.cFasterLabFish.AutoSize = true;
            this.cFasterLabFish.Location = new System.Drawing.Point(10, 51);
            this.cFasterLabFish.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cFasterLabFish.Name = "cFasterLabFish";
            this.cFasterLabFish.Size = new System.Drawing.Size(103, 19);
            this.cFasterLabFish.TabIndex = 2;
            this.cFasterLabFish.Text = "Faster Lab Fish";
            this.cFasterLabFish.UseVisualStyleBackColor = true;
            // 
            // cGoodDogRaceRNG
            // 
            this.cGoodDogRaceRNG.AutoSize = true;
            this.cGoodDogRaceRNG.Location = new System.Drawing.Point(140, 51);
            this.cGoodDogRaceRNG.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cGoodDogRaceRNG.Name = "cGoodDogRaceRNG";
            this.cGoodDogRaceRNG.Size = new System.Drawing.Size(135, 19);
            this.cGoodDogRaceRNG.TabIndex = 3;
            this.cGoodDogRaceRNG.Text = "Good Dog Race RNG";
            this.cGoodDogRaceRNG.UseVisualStyleBackColor = true;
            // 
            // cGoodDampeRNG
            // 
            this.cGoodDampeRNG.AutoSize = true;
            this.cGoodDampeRNG.Location = new System.Drawing.Point(10, 25);
            this.cGoodDampeRNG.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cGoodDampeRNG.Name = "cGoodDampeRNG";
            this.cGoodDampeRNG.Size = new System.Drawing.Size(123, 19);
            this.cGoodDampeRNG.TabIndex = 1;
            this.cGoodDampeRNG.Text = "Good Dampe RNG";
            this.cGoodDampeRNG.UseVisualStyleBackColor = true;
            // 
            // gHints
            // 
            this.gHints.Controls.Add(this.cImportanceCount);
            this.gHints.Controls.Add(this.cCustomGossipWoth);
            this.gHints.Controls.Add(this.nMaxGossipCT);
            this.gHints.Controls.Add(this.nMaxGossipFoolish);
            this.gHints.Controls.Add(this.nMaxGossipWotH);
            this.gHints.Controls.Add(this.label8);
            this.gHints.Controls.Add(this.label7);
            this.gHints.Controls.Add(this.lGossipWothConfig);
            this.gHints.Controls.Add(this.lGossip);
            this.gHints.Controls.Add(this.cGossipHints);
            this.gHints.Controls.Add(this.cFreeHints);
            this.gHints.Controls.Add(this.cClearHints);
            this.gHints.Location = new System.Drawing.Point(7, 122);
            this.gHints.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gHints.Name = "gHints";
            this.gHints.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gHints.Size = new System.Drawing.Size(245, 120);
            this.gHints.TabIndex = 36;
            this.gHints.TabStop = false;
            this.gHints.Text = "Gossip Stone Hints";
            // 
            // cCustomGossipWoth
            // 
            this.cCustomGossipWoth.AutoSize = true;
            this.cCustomGossipWoth.Checked = true;
            this.cCustomGossipWoth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cCustomGossipWoth.Location = new System.Drawing.Point(15, 94);
            this.cCustomGossipWoth.Name = "cCustomGossipWoth";
            this.cCustomGossipWoth.Size = new System.Drawing.Size(15, 14);
            this.cCustomGossipWoth.TabIndex = 30;
            this.cCustomGossipWoth.UseVisualStyleBackColor = true;
            this.cCustomGossipWoth.CheckedChanged += new System.EventHandler(this.cCustomGossipWoth_CheckedChanged);
            // 
            // nMaxGossipCT
            // 
            this.nMaxGossipCT.Location = new System.Drawing.Point(153, 91);
            this.nMaxGossipCT.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.nMaxGossipCT.Name = "nMaxGossipCT";
            this.nMaxGossipCT.Size = new System.Drawing.Size(31, 23);
            this.nMaxGossipCT.TabIndex = 29;
            this.nMaxGossipCT.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // nMaxGossipFoolish
            // 
            this.nMaxGossipFoolish.Enabled = false;
            this.nMaxGossipFoolish.Location = new System.Drawing.Point(101, 91);
            this.nMaxGossipFoolish.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.nMaxGossipFoolish.Name = "nMaxGossipFoolish";
            this.nMaxGossipFoolish.Size = new System.Drawing.Size(31, 23);
            this.nMaxGossipFoolish.TabIndex = 28;
            this.nMaxGossipFoolish.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // nMaxGossipWotH
            // 
            this.nMaxGossipWotH.Enabled = false;
            this.nMaxGossipWotH.Location = new System.Drawing.Point(51, 91);
            this.nMaxGossipWotH.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.nMaxGossipWotH.Name = "nMaxGossipWotH";
            this.nMaxGossipWotH.Size = new System.Drawing.Size(31, 23);
            this.nMaxGossipWotH.TabIndex = 27;
            this.nMaxGossipWotH.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(138, 93);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(12, 15);
            this.label8.TabIndex = 25;
            this.label8.Text = "/";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(88, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(12, 15);
            this.label7.TabIndex = 23;
            this.label7.Text = "/";
            // 
            // lGossipWothConfig
            // 
            this.lGossipWothConfig.AutoSize = true;
            this.lGossipWothConfig.Location = new System.Drawing.Point(51, 76);
            this.lGossipWothConfig.Name = "lGossipWothConfig";
            this.lGossipWothConfig.Size = new System.Drawing.Size(141, 15);
            this.lGossipWothConfig.TabIndex = 21;
            this.lGossipWothConfig.Text = "WotH  / Foolish / Max CT";
            // 
            // lGossip
            // 
            this.lGossip.AutoSize = true;
            this.lGossip.BackColor = System.Drawing.Color.Transparent;
            this.lGossip.ForeColor = System.Drawing.Color.Black;
            this.lGossip.Location = new System.Drawing.Point(12, 24);
            this.lGossip.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lGossip.Name = "lGossip";
            this.lGossip.Size = new System.Drawing.Size(95, 15);
            this.lGossip.TabIndex = 20;
            this.lGossip.Text = "Hint Distribution";
            // 
            // cGossipHints
            // 
            this.cGossipHints.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cGossipHints.FormattingEnabled = true;
            this.cGossipHints.Items.AddRange(new object[] {
            "Default",
            "Random",
            "Relevant",
            "Competitive"});
            this.cGossipHints.Location = new System.Drawing.Point(15, 39);
            this.cGossipHints.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cGossipHints.Name = "cGossipHints";
            this.cGossipHints.Size = new System.Drawing.Size(92, 23);
            this.cGossipHints.TabIndex = 19;
            // 
            // cFreeHints
            // 
            this.cFreeHints.AutoSize = true;
            this.cFreeHints.BackColor = System.Drawing.Color.Transparent;
            this.cFreeHints.ForeColor = System.Drawing.Color.Black;
            this.cFreeHints.Location = new System.Drawing.Point(117, 13);
            this.cFreeHints.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cFreeHints.Name = "cFreeHints";
            this.cFreeHints.Size = new System.Drawing.Size(77, 19);
            this.cFreeHints.TabIndex = 15;
            this.cFreeHints.Text = "Free hints";
            this.cFreeHints.UseVisualStyleBackColor = false;
            // 
            // cClearHints
            // 
            this.cClearHints.AutoSize = true;
            this.cClearHints.BackColor = System.Drawing.Color.Transparent;
            this.cClearHints.ForeColor = System.Drawing.Color.Black;
            this.cClearHints.Location = new System.Drawing.Point(117, 34);
            this.cClearHints.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cClearHints.Name = "cClearHints";
            this.cClearHints.Size = new System.Drawing.Size(82, 19);
            this.cClearHints.TabIndex = 16;
            this.cClearHints.Text = "Clear hints";
            this.cClearHints.UseVisualStyleBackColor = false;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.lLink);
            this.groupBox8.Controls.Add(this.cLink);
            this.groupBox8.Location = new System.Drawing.Point(355, 250);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox8.Size = new System.Drawing.Size(151, 96);
            this.groupBox8.TabIndex = 35;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Cosmetic Customization";
            // 
            // lLink
            // 
            this.lLink.AutoSize = true;
            this.lLink.BackColor = System.Drawing.Color.Transparent;
            this.lLink.ForeColor = System.Drawing.Color.Black;
            this.lLink.Location = new System.Drawing.Point(4, 23);
            this.lLink.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lLink.Name = "lLink";
            this.lLink.Size = new System.Drawing.Size(76, 15);
            this.lLink.TabIndex = 9;
            this.lLink.Text = "Player model";
            // 
            // cLink
            // 
            this.cLink.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cLink.FormattingEnabled = true;
            this.cLink.Items.AddRange(new object[] {
            "Link (MM)",
            "Link (OoT)",
            "Adult Link",
            "Kafei"});
            this.cLink.Location = new System.Drawing.Point(7, 38);
            this.cLink.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cLink.Name = "cLink";
            this.cLink.Size = new System.Drawing.Size(129, 23);
            this.cLink.TabIndex = 10;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.cEasyFrameByFrame);
            this.groupBox7.Controls.Add(this.cImprovedCamera);
            this.groupBox7.Controls.Add(this.cGossipsTolerant);
            this.groupBox7.Controls.Add(this.cQuestItemKeep);
            this.groupBox7.Controls.Add(this.cUpdateNpcText);
            this.groupBox7.Controls.Add(this.cAddBombchuDrops);
            this.groupBox7.Controls.Add(this.lChestGame);
            this.groupBox7.Controls.Add(this.cChestGameMinimap);
            this.groupBox7.Controls.Add(this.cSaferGlitches);
            this.groupBox7.Controls.Add(this.cSkulltulaTokenSounds);
            this.groupBox7.Controls.Add(this.cFairyMaskShimmer);
            this.groupBox7.Controls.Add(this.cInvisSparkle);
            this.groupBox7.Controls.Add(this.cFillWallet);
            this.groupBox7.Controls.Add(this.cTargetHealth);
            this.groupBox7.Controls.Add(this.cLenientGoronSpikes);
            this.groupBox7.Controls.Add(this.cImprovedPictobox);
            this.groupBox7.Controls.Add(this.cElegySpeedups);
            this.groupBox7.Controls.Add(this.cCloseCows);
            this.groupBox7.Controls.Add(this.cArrowCycling);
            this.groupBox7.Controls.Add(this.cFreestanding);
            this.groupBox7.Controls.Add(this.cFastPush);
            this.groupBox7.Controls.Add(this.cQText);
            this.groupBox7.Controls.Add(this.cShopAppearance);
            this.groupBox7.Controls.Add(this.cEponaSword);
            this.groupBox7.Controls.Add(this.cUpdateChests);
            this.groupBox7.Controls.Add(this.cDisableCritWiggle);
            this.groupBox7.Controls.Add(this.cQuestItemStorage);
            this.groupBox7.Controls.Add(this.cNoDowngrades);
            this.groupBox7.Location = new System.Drawing.Point(513, 7);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox7.Size = new System.Drawing.Size(360, 339);
            this.groupBox7.TabIndex = 34;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Comfort Options";
            // 
            // cEasyFrameByFrame
            // 
            this.cEasyFrameByFrame.AutoSize = true;
            this.cEasyFrameByFrame.Location = new System.Drawing.Point(10, 267);
            this.cEasyFrameByFrame.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cEasyFrameByFrame.Name = "cEasyFrameByFrame";
            this.cEasyFrameByFrame.Size = new System.Drawing.Size(137, 19);
            this.cEasyFrameByFrame.TabIndex = 51;
            this.cEasyFrameByFrame.Text = "Easy Frame By Frame";
            this.cEasyFrameByFrame.UseVisualStyleBackColor = true;
            // 
            // cImprovedCamera
            // 
            this.cImprovedCamera.AutoSize = true;
            this.cImprovedCamera.Location = new System.Drawing.Point(10, 245);
            this.cImprovedCamera.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cImprovedCamera.Name = "cImprovedCamera";
            this.cImprovedCamera.Size = new System.Drawing.Size(121, 19);
            this.cImprovedCamera.TabIndex = 50;
            this.cImprovedCamera.Text = "Improved Camera";
            this.cImprovedCamera.UseVisualStyleBackColor = true;
            // 
            // cGossipsTolerant
            // 
            this.cGossipsTolerant.AutoSize = true;
            this.cGossipsTolerant.Location = new System.Drawing.Point(172, 267);
            this.cGossipsTolerant.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cGossipsTolerant.Name = "cGossipsTolerant";
            this.cGossipsTolerant.Size = new System.Drawing.Size(140, 19);
            this.cGossipsTolerant.TabIndex = 49;
            this.cGossipsTolerant.Text = "Tolerant Gossip Angle";
            this.cGossipsTolerant.UseVisualStyleBackColor = true;
            // 
            // cQuestItemKeep
            // 
            this.cQuestItemKeep.AutoSize = true;
            this.cQuestItemKeep.BackColor = System.Drawing.Color.Transparent;
            this.cQuestItemKeep.Location = new System.Drawing.Point(172, 179);
            this.cQuestItemKeep.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cQuestItemKeep.Name = "cQuestItemKeep";
            this.cQuestItemKeep.Size = new System.Drawing.Size(162, 19);
            this.cQuestItemKeep.TabIndex = 48;
            this.cQuestItemKeep.Text = "Quest items through time";
            this.cQuestItemKeep.UseVisualStyleBackColor = false;
            // 
            // cUpdateNpcText
            // 
            this.cUpdateNpcText.AutoSize = true;
            this.cUpdateNpcText.Location = new System.Drawing.Point(172, 91);
            this.cUpdateNpcText.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cUpdateNpcText.Name = "cUpdateNpcText";
            this.cUpdateNpcText.Size = new System.Drawing.Size(115, 19);
            this.cUpdateNpcText.TabIndex = 47;
            this.cUpdateNpcText.Text = "Update NPC Text";
            this.cUpdateNpcText.UseVisualStyleBackColor = true;
            // 
            // cAddBombchuDrops
            // 
            this.cAddBombchuDrops.AutoSize = true;
            this.cAddBombchuDrops.Location = new System.Drawing.Point(172, 245);
            this.cAddBombchuDrops.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cAddBombchuDrops.Name = "cAddBombchuDrops";
            this.cAddBombchuDrops.Size = new System.Drawing.Size(137, 19);
            this.cAddBombchuDrops.TabIndex = 46;
            this.cAddBombchuDrops.Text = "Add Bombchu Drops";
            this.cAddBombchuDrops.UseVisualStyleBackColor = true;
            // 
            // lChestGame
            // 
            this.lChestGame.AutoSize = true;
            this.lChestGame.BackColor = System.Drawing.Color.Transparent;
            this.lChestGame.ForeColor = System.Drawing.Color.Black;
            this.lChestGame.Location = new System.Drawing.Point(8, 295);
            this.lChestGame.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lChestGame.Name = "lChestGame";
            this.lChestGame.Size = new System.Drawing.Size(156, 15);
            this.lChestGame.TabIndex = 45;
            this.lChestGame.Text = "Treasure Chest Game Spoiler";
            // 
            // cChestGameMinimap
            // 
            this.cChestGameMinimap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cChestGameMinimap.FormattingEnabled = true;
            this.cChestGameMinimap.Items.AddRange(new object[] {
            "Off",
            "Minimal",
            "Conditional Spoiler",
            "Spoiler"});
            this.cChestGameMinimap.Location = new System.Drawing.Point(10, 310);
            this.cChestGameMinimap.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cChestGameMinimap.Name = "cChestGameMinimap";
            this.cChestGameMinimap.Size = new System.Drawing.Size(129, 23);
            this.cChestGameMinimap.TabIndex = 44;
            // 
            // cSaferGlitches
            // 
            this.cSaferGlitches.AutoSize = true;
            this.cSaferGlitches.Location = new System.Drawing.Point(10, 223);
            this.cSaferGlitches.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cSaferGlitches.Name = "cSaferGlitches";
            this.cSaferGlitches.Size = new System.Drawing.Size(97, 19);
            this.cSaferGlitches.TabIndex = 43;
            this.cSaferGlitches.Text = "Safer Glitches";
            this.cSaferGlitches.UseVisualStyleBackColor = true;
            // 
            // cSkulltulaTokenSounds
            // 
            this.cSkulltulaTokenSounds.AutoSize = true;
            this.cSkulltulaTokenSounds.Location = new System.Drawing.Point(172, 311);
            this.cSkulltulaTokenSounds.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cSkulltulaTokenSounds.Name = "cSkulltulaTokenSounds";
            this.cSkulltulaTokenSounds.Size = new System.Drawing.Size(147, 19);
            this.cSkulltulaTokenSounds.TabIndex = 44;
            this.cSkulltulaTokenSounds.Text = "Detect Skulltula Tokens";
            this.cSkulltulaTokenSounds.UseVisualStyleBackColor = true;
            // 
            // cFairyMaskShimmer
            // 
            this.cFairyMaskShimmer.AutoSize = true;
            this.cFairyMaskShimmer.Location = new System.Drawing.Point(172, 289);
            this.cFairyMaskShimmer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cFairyMaskShimmer.Name = "cFairyMaskShimmer";
            this.cFairyMaskShimmer.Size = new System.Drawing.Size(125, 19);
            this.cFairyMaskShimmer.TabIndex = 43;
            this.cFairyMaskShimmer.Text = "Detect Stray Fairies";
            this.cFairyMaskShimmer.UseVisualStyleBackColor = true;
            // 
            // cInvisSparkle
            // 
            this.cInvisSparkle.AutoSize = true;
            this.cInvisSparkle.Location = new System.Drawing.Point(10, 201);
            this.cInvisSparkle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cInvisSparkle.Name = "cInvisSparkle";
            this.cInvisSparkle.Size = new System.Drawing.Size(147, 19);
            this.cInvisSparkle.TabIndex = 42;
            this.cInvisSparkle.Text = "Hidden Rupees Sparkle";
            this.cInvisSparkle.UseVisualStyleBackColor = true;
            // 
            // cFillWallet
            // 
            this.cFillWallet.AutoSize = true;
            this.cFillWallet.Location = new System.Drawing.Point(10, 179);
            this.cFillWallet.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cFillWallet.Name = "cFillWallet";
            this.cFillWallet.Size = new System.Drawing.Size(139, 19);
            this.cFillWallet.TabIndex = 41;
            this.cFillWallet.Text = "Fill wallet on upgrade";
            this.cFillWallet.UseVisualStyleBackColor = true;
            // 
            // cTargetHealth
            // 
            this.cTargetHealth.AutoSize = true;
            this.cTargetHealth.Location = new System.Drawing.Point(10, 157);
            this.cTargetHealth.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cTargetHealth.Name = "cTargetHealth";
            this.cTargetHealth.Size = new System.Drawing.Size(116, 19);
            this.cTargetHealth.TabIndex = 40;
            this.cTargetHealth.Text = "Target Health Bar";
            this.cTargetHealth.UseVisualStyleBackColor = true;
            // 
            // cLenientGoronSpikes
            // 
            this.cLenientGoronSpikes.AutoSize = true;
            this.cLenientGoronSpikes.Location = new System.Drawing.Point(10, 135);
            this.cLenientGoronSpikes.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cLenientGoronSpikes.Name = "cLenientGoronSpikes";
            this.cLenientGoronSpikes.Size = new System.Drawing.Size(137, 19);
            this.cLenientGoronSpikes.TabIndex = 39;
            this.cLenientGoronSpikes.Text = "Lenient Goron Spikes";
            this.cLenientGoronSpikes.UseVisualStyleBackColor = true;
            // 
            // cImprovedPictobox
            // 
            this.cImprovedPictobox.AutoSize = true;
            this.cImprovedPictobox.Location = new System.Drawing.Point(10, 113);
            this.cImprovedPictobox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cImprovedPictobox.Name = "cImprovedPictobox";
            this.cImprovedPictobox.Size = new System.Drawing.Size(127, 19);
            this.cImprovedPictobox.TabIndex = 38;
            this.cImprovedPictobox.Text = "Improved Pictobox";
            this.cImprovedPictobox.UseVisualStyleBackColor = true;
            // 
            // cElegySpeedups
            // 
            this.cElegySpeedups.AutoSize = true;
            this.cElegySpeedups.Location = new System.Drawing.Point(172, 223);
            this.cElegySpeedups.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cElegySpeedups.Name = "cElegySpeedups";
            this.cElegySpeedups.Size = new System.Drawing.Size(107, 19);
            this.cElegySpeedups.TabIndex = 37;
            this.cElegySpeedups.Text = "Elegy speedups";
            this.cElegySpeedups.UseVisualStyleBackColor = true;
            // 
            // cCloseCows
            // 
            this.cCloseCows.AutoSize = true;
            this.cCloseCows.Location = new System.Drawing.Point(10, 91);
            this.cCloseCows.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cCloseCows.Name = "cCloseCows";
            this.cCloseCows.Size = new System.Drawing.Size(87, 19);
            this.cCloseCows.TabIndex = 36;
            this.cCloseCows.Text = "Close Cows";
            this.cCloseCows.UseVisualStyleBackColor = true;
            // 
            // cArrowCycling
            // 
            this.cArrowCycling.AutoSize = true;
            this.cArrowCycling.Location = new System.Drawing.Point(172, 201);
            this.cArrowCycling.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cArrowCycling.Name = "cArrowCycling";
            this.cArrowCycling.Size = new System.Drawing.Size(99, 19);
            this.cArrowCycling.TabIndex = 35;
            this.cArrowCycling.Text = "Arrow cycling";
            this.cArrowCycling.UseVisualStyleBackColor = true;
            // 
            // cFreestanding
            // 
            this.cFreestanding.AutoSize = true;
            this.cFreestanding.Location = new System.Drawing.Point(172, 69);
            this.cFreestanding.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cFreestanding.Name = "cFreestanding";
            this.cFreestanding.Size = new System.Drawing.Size(139, 19);
            this.cFreestanding.TabIndex = 34;
            this.cFreestanding.Text = "Update world models";
            this.cFreestanding.UseVisualStyleBackColor = true;
            // 
            // cFastPush
            // 
            this.cFastPush.AutoSize = true;
            this.cFastPush.Location = new System.Drawing.Point(10, 69);
            this.cFastPush.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cFastPush.Name = "cFastPush";
            this.cFastPush.Size = new System.Drawing.Size(132, 19);
            this.cFastPush.TabIndex = 31;
            this.cFastPush.Text = "Increase push speed";
            this.cFastPush.UseVisualStyleBackColor = true;
            // 
            // cQText
            // 
            this.cQText.AutoSize = true;
            this.cQText.BackColor = System.Drawing.Color.Transparent;
            this.cQText.ForeColor = System.Drawing.Color.Black;
            this.cQText.Location = new System.Drawing.Point(10, 47);
            this.cQText.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cQText.Name = "cQText";
            this.cQText.Size = new System.Drawing.Size(80, 19);
            this.cQText.TabIndex = 6;
            this.cQText.Text = "Quick text";
            this.cQText.UseVisualStyleBackColor = false;
            // 
            // cShopAppearance
            // 
            this.cShopAppearance.AutoSize = true;
            this.cShopAppearance.BackColor = System.Drawing.Color.Transparent;
            this.cShopAppearance.ForeColor = System.Drawing.Color.Black;
            this.cShopAppearance.Location = new System.Drawing.Point(172, 25);
            this.cShopAppearance.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cShopAppearance.Name = "cShopAppearance";
            this.cShopAppearance.Size = new System.Drawing.Size(98, 19);
            this.cShopAppearance.TabIndex = 21;
            this.cShopAppearance.Text = "Update shops";
            this.cShopAppearance.UseVisualStyleBackColor = false;
            // 
            // cEponaSword
            // 
            this.cEponaSword.AutoSize = true;
            this.cEponaSword.BackColor = System.Drawing.Color.Transparent;
            this.cEponaSword.ForeColor = System.Drawing.Color.Black;
            this.cEponaSword.Location = new System.Drawing.Point(172, 135);
            this.cEponaSword.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cEponaSword.Name = "cEponaSword";
            this.cEponaSword.Size = new System.Drawing.Size(112, 19);
            this.cEponaSword.TabIndex = 22;
            this.cEponaSword.Text = "Fix Epona sword";
            this.cEponaSword.UseVisualStyleBackColor = false;
            // 
            // cUpdateChests
            // 
            this.cUpdateChests.AutoSize = true;
            this.cUpdateChests.BackColor = System.Drawing.Color.Transparent;
            this.cUpdateChests.ForeColor = System.Drawing.Color.Black;
            this.cUpdateChests.Location = new System.Drawing.Point(172, 47);
            this.cUpdateChests.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cUpdateChests.Name = "cUpdateChests";
            this.cUpdateChests.Size = new System.Drawing.Size(100, 19);
            this.cUpdateChests.TabIndex = 23;
            this.cUpdateChests.Text = "Update chests";
            this.cUpdateChests.UseVisualStyleBackColor = false;
            // 
            // cDisableCritWiggle
            // 
            this.cDisableCritWiggle.AutoSize = true;
            this.cDisableCritWiggle.BackColor = System.Drawing.Color.Transparent;
            this.cDisableCritWiggle.Location = new System.Drawing.Point(10, 25);
            this.cDisableCritWiggle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cDisableCritWiggle.Name = "cDisableCritWiggle";
            this.cDisableCritWiggle.Size = new System.Drawing.Size(122, 19);
            this.cDisableCritWiggle.TabIndex = 29;
            this.cDisableCritWiggle.Text = "Disable crit wiggle";
            this.cDisableCritWiggle.UseVisualStyleBackColor = false;
            // 
            // cQuestItemStorage
            // 
            this.cQuestItemStorage.AutoSize = true;
            this.cQuestItemStorage.BackColor = System.Drawing.Color.Transparent;
            this.cQuestItemStorage.Location = new System.Drawing.Point(172, 157);
            this.cQuestItemStorage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cQuestItemStorage.Name = "cQuestItemStorage";
            this.cQuestItemStorage.Size = new System.Drawing.Size(155, 19);
            this.cQuestItemStorage.TabIndex = 30;
            this.cQuestItemStorage.Text = "Quest item extra storage";
            this.cQuestItemStorage.UseVisualStyleBackColor = false;
            // 
            // cNoDowngrades
            // 
            this.cNoDowngrades.AutoSize = true;
            this.cNoDowngrades.BackColor = System.Drawing.Color.Transparent;
            this.cNoDowngrades.ForeColor = System.Drawing.Color.Black;
            this.cNoDowngrades.Location = new System.Drawing.Point(172, 113);
            this.cNoDowngrades.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cNoDowngrades.Name = "cNoDowngrades";
            this.cNoDowngrades.Size = new System.Drawing.Size(110, 19);
            this.cNoDowngrades.TabIndex = 18;
            this.cNoDowngrades.Text = "No downgrades";
            this.cNoDowngrades.UseVisualStyleBackColor = false;
            // 
            // tabShortenCutscenes
            // 
            this.tabShortenCutscenes.Controls.Add(this.tShortenCutscenes);
            this.tabShortenCutscenes.Location = new System.Drawing.Point(4, 24);
            this.tabShortenCutscenes.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabShortenCutscenes.Name = "tabShortenCutscenes";
            this.tabShortenCutscenes.Size = new System.Drawing.Size(884, 361);
            this.tabShortenCutscenes.TabIndex = 5;
            this.tabShortenCutscenes.Text = "Shorten Cutscenes";
            this.tabShortenCutscenes.UseVisualStyleBackColor = true;
            // 
            // tShortenCutscenes
            // 
            this.tShortenCutscenes.Location = new System.Drawing.Point(8, 5);
            this.tShortenCutscenes.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tShortenCutscenes.Name = "tShortenCutscenes";
            this.tShortenCutscenes.SelectedIndex = 0;
            this.tShortenCutscenes.Size = new System.Drawing.Size(868, 322);
            this.tShortenCutscenes.TabIndex = 0;
            // 
            // tabCosmetics
            // 
            this.tabCosmetics.Controls.Add(this.gCosmeticOther);
            this.tabCosmetics.Controls.Add(this.gCosmeticMusicSound);
            this.tabCosmetics.Controls.Add(this.cHUDGroupBox);
            this.tabCosmetics.Controls.Add(this.tFormCosmetics);
            this.tabCosmetics.Location = new System.Drawing.Point(4, 24);
            this.tabCosmetics.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabCosmetics.Name = "tabCosmetics";
            this.tabCosmetics.Size = new System.Drawing.Size(884, 361);
            this.tabCosmetics.TabIndex = 4;
            this.tabCosmetics.Text = "Cosmetics";
            this.tabCosmetics.UseVisualStyleBackColor = true;
            // 
            // gCosmeticOther
            // 
            this.gCosmeticOther.Controls.Add(this.cRainbowTunic);
            this.gCosmeticOther.Controls.Add(this.cBombTrapTunicColors);
            this.gCosmeticOther.Controls.Add(this.cInstantPictobox);
            this.gCosmeticOther.Controls.Add(this.cTatl);
            this.gCosmeticOther.Controls.Add(this.lTatl);
            this.gCosmeticOther.Controls.Add(this.cTargettingStyle);
            this.gCosmeticOther.Location = new System.Drawing.Point(299, 3);
            this.gCosmeticOther.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gCosmeticOther.Name = "gCosmeticOther";
            this.gCosmeticOther.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gCosmeticOther.Size = new System.Drawing.Size(572, 149);
            this.gCosmeticOther.TabIndex = 47;
            this.gCosmeticOther.TabStop = false;
            this.gCosmeticOther.Text = "Other";
            // 
            // cRainbowTunic
            // 
            this.cRainbowTunic.AutoSize = true;
            this.cRainbowTunic.BackColor = System.Drawing.Color.Transparent;
            this.cRainbowTunic.Location = new System.Drawing.Point(10, 115);
            this.cRainbowTunic.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cRainbowTunic.Name = "cRainbowTunic";
            this.cRainbowTunic.Size = new System.Drawing.Size(104, 19);
            this.cRainbowTunic.TabIndex = 44;
            this.cRainbowTunic.Text = "Rainbow Tunic";
            this.cRainbowTunic.UseVisualStyleBackColor = false;
            // 
            // cBombTrapTunicColors
            // 
            this.cBombTrapTunicColors.AutoSize = true;
            this.cBombTrapTunicColors.BackColor = System.Drawing.Color.Transparent;
            this.cBombTrapTunicColors.Location = new System.Drawing.Point(199, 37);
            this.cBombTrapTunicColors.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cBombTrapTunicColors.Name = "cBombTrapTunicColors";
            this.cBombTrapTunicColors.Size = new System.Drawing.Size(214, 19);
            this.cBombTrapTunicColors.TabIndex = 43;
            this.cBombTrapTunicColors.Text = "Bomb Traps Randomize Tunic Color";
            this.cBombTrapTunicColors.UseVisualStyleBackColor = false;
            // 
            // cInstantPictobox
            // 
            this.cInstantPictobox.AutoSize = true;
            this.cInstantPictobox.BackColor = System.Drawing.Color.Transparent;
            this.cInstantPictobox.Location = new System.Drawing.Point(10, 90);
            this.cInstantPictobox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cInstantPictobox.Name = "cInstantPictobox";
            this.cInstantPictobox.Size = new System.Drawing.Size(180, 19);
            this.cInstantPictobox.TabIndex = 42;
            this.cInstantPictobox.Text = "Instant Pictobox on Emulator";
            this.cInstantPictobox.UseVisualStyleBackColor = false;
            // 
            // cTatl
            // 
            this.cTatl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cTatl.FormattingEnabled = true;
            this.cTatl.Items.AddRange(new object[] {
            "Default",
            "Dark",
            "Hot",
            "Cool",
            "Random",
            "Rainbow (cycle)"});
            this.cTatl.Location = new System.Drawing.Point(10, 33);
            this.cTatl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cTatl.Name = "cTatl";
            this.cTatl.Size = new System.Drawing.Size(150, 23);
            this.cTatl.TabIndex = 41;
            // 
            // lTatl
            // 
            this.lTatl.AutoSize = true;
            this.lTatl.BackColor = System.Drawing.Color.Transparent;
            this.lTatl.ForeColor = System.Drawing.Color.Black;
            this.lTatl.Location = new System.Drawing.Point(7, 18);
            this.lTatl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lTatl.Name = "lTatl";
            this.lTatl.Size = new System.Drawing.Size(62, 15);
            this.lTatl.TabIndex = 40;
            this.lTatl.Text = "Tatl theme";
            // 
            // cTargettingStyle
            // 
            this.cTargettingStyle.AutoSize = true;
            this.cTargettingStyle.BackColor = System.Drawing.Color.Transparent;
            this.cTargettingStyle.Location = new System.Drawing.Point(10, 65);
            this.cTargettingStyle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cTargettingStyle.Name = "cTargettingStyle";
            this.cTargettingStyle.Size = new System.Drawing.Size(140, 19);
            this.cTargettingStyle.TabIndex = 37;
            this.cTargettingStyle.Text = "Default Hold Z-Target";
            this.cTargettingStyle.UseVisualStyleBackColor = false;
            // 
            // gCosmeticMusicSound
            // 
            this.gCosmeticMusicSound.Controls.Add(this.cDisableFanfares);
            this.gCosmeticMusicSound.Controls.Add(this.cMusicTrackNames);
            this.gCosmeticMusicSound.Controls.Add(this.cRemoveMinorMusic);
            this.gCosmeticMusicSound.Controls.Add(this.lLuckRoll);
            this.gCosmeticMusicSound.Controls.Add(this.tLuckRollPercentage);
            this.gCosmeticMusicSound.Controls.Add(this.lMusic);
            this.gCosmeticMusicSound.Controls.Add(this.cMusic);
            this.gCosmeticMusicSound.Controls.Add(this.cSFX);
            this.gCosmeticMusicSound.Controls.Add(this.cCombatMusicDisable);
            this.gCosmeticMusicSound.Controls.Add(this.cEnableNightMusic);
            this.gCosmeticMusicSound.Controls.Add(this.cLowHealthSFXComboBox);
            this.gCosmeticMusicSound.Controls.Add(this.lLowHealthSFXComboBox);
            this.gCosmeticMusicSound.Location = new System.Drawing.Point(8, 159);
            this.gCosmeticMusicSound.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gCosmeticMusicSound.Name = "gCosmeticMusicSound";
            this.gCosmeticMusicSound.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gCosmeticMusicSound.Size = new System.Drawing.Size(484, 195);
            this.gCosmeticMusicSound.TabIndex = 46;
            this.gCosmeticMusicSound.TabStop = false;
            this.gCosmeticMusicSound.Text = "Music / Sound";
            // 
            // cDisableFanfares
            // 
            this.cDisableFanfares.AutoSize = true;
            this.cDisableFanfares.Location = new System.Drawing.Point(154, 76);
            this.cDisableFanfares.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cDisableFanfares.Name = "cDisableFanfares";
            this.cDisableFanfares.Size = new System.Drawing.Size(111, 19);
            this.cDisableFanfares.TabIndex = 50;
            this.cDisableFanfares.Text = "Disable Fanfares";
            this.cDisableFanfares.UseVisualStyleBackColor = true;
            // 
            // cMusicTrackNames
            // 
            this.cMusicTrackNames.AutoSize = true;
            this.cMusicTrackNames.BackColor = System.Drawing.Color.Transparent;
            this.cMusicTrackNames.ForeColor = System.Drawing.Color.Black;
            this.cMusicTrackNames.Location = new System.Drawing.Point(154, 50);
            this.cMusicTrackNames.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cMusicTrackNames.Name = "cMusicTrackNames";
            this.cMusicTrackNames.Size = new System.Drawing.Size(125, 19);
            this.cMusicTrackNames.TabIndex = 49;
            this.cMusicTrackNames.Text = "Show Track Names";
            this.cMusicTrackNames.UseVisualStyleBackColor = false;
            // 
            // cRemoveMinorMusic
            // 
            this.cRemoveMinorMusic.AutoSize = true;
            this.cRemoveMinorMusic.BackColor = System.Drawing.Color.Transparent;
            this.cRemoveMinorMusic.ForeColor = System.Drawing.Color.Black;
            this.cRemoveMinorMusic.Location = new System.Drawing.Point(154, 23);
            this.cRemoveMinorMusic.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cRemoveMinorMusic.Name = "cRemoveMinorMusic";
            this.cRemoveMinorMusic.Size = new System.Drawing.Size(139, 19);
            this.cRemoveMinorMusic.TabIndex = 48;
            this.cRemoveMinorMusic.Text = "Remove Minor Music";
            this.cRemoveMinorMusic.UseVisualStyleBackColor = false;
            // 
            // lLuckRoll
            // 
            this.lLuckRoll.AutoSize = true;
            this.lLuckRoll.BackColor = System.Drawing.Color.Transparent;
            this.lLuckRoll.ForeColor = System.Drawing.Color.Black;
            this.lLuckRoll.Location = new System.Drawing.Point(154, 99);
            this.lLuckRoll.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lLuckRoll.Name = "lLuckRoll";
            this.lLuckRoll.Size = new System.Drawing.Size(119, 15);
            this.lLuckRoll.TabIndex = 47;
            this.lLuckRoll.Text = "Luck Roll Chance (%)";
            // 
            // tLuckRollPercentage
            // 
            this.tLuckRollPercentage.DecimalPlaces = 2;
            this.tLuckRollPercentage.Location = new System.Drawing.Point(154, 114);
            this.tLuckRollPercentage.Name = "tLuckRollPercentage";
            this.tLuckRollPercentage.Size = new System.Drawing.Size(117, 23);
            this.tLuckRollPercentage.TabIndex = 46;
            this.tLuckRollPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tLuckRollPercentage.Value = new decimal(new int[] {
            333,
            0,
            0,
            131072});
            // 
            // lMusic
            // 
            this.lMusic.AutoSize = true;
            this.lMusic.BackColor = System.Drawing.Color.Transparent;
            this.lMusic.ForeColor = System.Drawing.Color.Black;
            this.lMusic.Location = new System.Drawing.Point(4, 99);
            this.lMusic.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lMusic.Name = "lMusic";
            this.lMusic.Size = new System.Drawing.Size(39, 15);
            this.lMusic.TabIndex = 43;
            this.lMusic.Text = "Music";
            // 
            // cMusic
            // 
            this.cMusic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cMusic.FormattingEnabled = true;
            this.cMusic.Items.AddRange(new object[] {
            "Default",
            "Random",
            "None"});
            this.cMusic.Location = new System.Drawing.Point(7, 114);
            this.cMusic.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cMusic.Name = "cMusic";
            this.cMusic.Size = new System.Drawing.Size(140, 23);
            this.cMusic.TabIndex = 42;
            // 
            // cSFX
            // 
            this.cSFX.AutoSize = true;
            this.cSFX.BackColor = System.Drawing.Color.Transparent;
            this.cSFX.ForeColor = System.Drawing.Color.Black;
            this.cSFX.Location = new System.Drawing.Point(7, 23);
            this.cSFX.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cSFX.Name = "cSFX";
            this.cSFX.Size = new System.Drawing.Size(107, 19);
            this.cSFX.TabIndex = 36;
            this.cSFX.Text = "Randomize SFX";
            this.cSFX.UseVisualStyleBackColor = false;
            // 
            // cCombatMusicDisable
            // 
            this.cCombatMusicDisable.AutoSize = true;
            this.cCombatMusicDisable.Location = new System.Drawing.Point(7, 76);
            this.cCombatMusicDisable.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cCombatMusicDisable.Name = "cCombatMusicDisable";
            this.cCombatMusicDisable.Size = new System.Drawing.Size(145, 19);
            this.cCombatMusicDisable.TabIndex = 45;
            this.cCombatMusicDisable.Text = "Disable Combat Music";
            this.cCombatMusicDisable.UseVisualStyleBackColor = true;
            // 
            // cEnableNightMusic
            // 
            this.cEnableNightMusic.AutoSize = true;
            this.cEnableNightMusic.Location = new System.Drawing.Point(7, 50);
            this.cEnableNightMusic.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cEnableNightMusic.Name = "cEnableNightMusic";
            this.cEnableNightMusic.Size = new System.Drawing.Size(123, 19);
            this.cEnableNightMusic.TabIndex = 38;
            this.cEnableNightMusic.Text = "Enable Night BGM";
            this.cEnableNightMusic.UseVisualStyleBackColor = true;
            // 
            // cLowHealthSFXComboBox
            // 
            this.cLowHealthSFXComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cLowHealthSFXComboBox.FormattingEnabled = true;
            this.cLowHealthSFXComboBox.Location = new System.Drawing.Point(7, 163);
            this.cLowHealthSFXComboBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cLowHealthSFXComboBox.Name = "cLowHealthSFXComboBox";
            this.cLowHealthSFXComboBox.Size = new System.Drawing.Size(140, 23);
            this.cLowHealthSFXComboBox.TabIndex = 25;
            this.cLowHealthSFXComboBox.SelectedIndexChanged += new System.EventHandler(this.cLowHealthSFXComboBox_SelectedIndexChanged);
            // 
            // lLowHealthSFXComboBox
            // 
            this.lLowHealthSFXComboBox.AutoSize = true;
            this.lLowHealthSFXComboBox.Location = new System.Drawing.Point(4, 142);
            this.lLowHealthSFXComboBox.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lLowHealthSFXComboBox.Name = "lLowHealthSFXComboBox";
            this.lLowHealthSFXComboBox.Size = new System.Drawing.Size(89, 15);
            this.lLowHealthSFXComboBox.TabIndex = 25;
            this.lLowHealthSFXComboBox.Text = "Low Health SFX";
            // 
            // cHUDGroupBox
            // 
            this.cHUDGroupBox.Controls.Add(this.cHueShiftMiscUI);
            this.cHUDGroupBox.Controls.Add(this.cHUDTableLayoutPanel);
            this.cHUDGroupBox.Location = new System.Drawing.Point(499, 159);
            this.cHUDGroupBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cHUDGroupBox.Name = "cHUDGroupBox";
            this.cHUDGroupBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cHUDGroupBox.Size = new System.Drawing.Size(372, 195);
            this.cHUDGroupBox.TabIndex = 44;
            this.cHUDGroupBox.TabStop = false;
            this.cHUDGroupBox.Text = "HUD";
            // 
            // cHueShiftMiscUI
            // 
            this.cHueShiftMiscUI.AutoSize = true;
            this.cHueShiftMiscUI.Location = new System.Drawing.Point(10, 91);
            this.cHueShiftMiscUI.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cHueShiftMiscUI.Name = "cHueShiftMiscUI";
            this.cHueShiftMiscUI.Size = new System.Drawing.Size(216, 19);
            this.cHueShiftMiscUI.TabIndex = 1;
            this.cHueShiftMiscUI.Text = "Randomize Hue of Miscellaneous UI";
            this.cHueShiftMiscUI.UseVisualStyleBackColor = true;
            // 
            // cHUDTableLayoutPanel
            // 
            this.cHUDTableLayoutPanel.ColumnCount = 3;
            this.cHUDTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this.cHUDTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.cHUDTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 91F));
            this.cHUDTableLayoutPanel.Controls.Add(this.cHUDHeartsComboBox, 1, 0);
            this.cHUDTableLayoutPanel.Controls.Add(this.cHeartsLabel, 0, 0);
            this.cHUDTableLayoutPanel.Controls.Add(this.cMagicLabel, 0, 1);
            this.cHUDTableLayoutPanel.Controls.Add(this.cHUDMagicComboBox, 1, 1);
            this.cHUDTableLayoutPanel.Controls.Add(this.btn_hud, 2, 0);
            this.cHUDTableLayoutPanel.Location = new System.Drawing.Point(2, 18);
            this.cHUDTableLayoutPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cHUDTableLayoutPanel.Name = "cHUDTableLayoutPanel";
            this.cHUDTableLayoutPanel.RowCount = 2;
            this.cHUDTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.cHUDTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.cHUDTableLayoutPanel.Size = new System.Drawing.Size(362, 66);
            this.cHUDTableLayoutPanel.TabIndex = 0;
            // 
            // cHUDHeartsComboBox
            // 
            this.cHUDHeartsComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cHUDHeartsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cHUDHeartsComboBox.FormattingEnabled = true;
            this.cHUDHeartsComboBox.Location = new System.Drawing.Point(62, 3);
            this.cHUDHeartsComboBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cHUDHeartsComboBox.Name = "cHUDHeartsComboBox";
            this.cHUDHeartsComboBox.Size = new System.Drawing.Size(205, 23);
            this.cHUDHeartsComboBox.TabIndex = 32;
            this.cHUDHeartsComboBox.SelectedIndexChanged += new System.EventHandler(this.cHUDHeartsComboBox_SelectedIndexChanged);
            // 
            // cHeartsLabel
            // 
            this.cHeartsLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cHeartsLabel.AutoSize = true;
            this.cHeartsLabel.Location = new System.Drawing.Point(4, 9);
            this.cHeartsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.cHeartsLabel.Name = "cHeartsLabel";
            this.cHeartsLabel.Size = new System.Drawing.Size(44, 15);
            this.cHeartsLabel.TabIndex = 33;
            this.cHeartsLabel.Text = "Hearts:";
            // 
            // cMagicLabel
            // 
            this.cMagicLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cMagicLabel.AutoSize = true;
            this.cMagicLabel.Location = new System.Drawing.Point(4, 42);
            this.cMagicLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.cMagicLabel.Name = "cMagicLabel";
            this.cMagicLabel.Size = new System.Drawing.Size(43, 15);
            this.cMagicLabel.TabIndex = 34;
            this.cMagicLabel.Text = "Magic:";
            // 
            // cHUDMagicComboBox
            // 
            this.cHUDMagicComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cHUDMagicComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cHUDMagicComboBox.FormattingEnabled = true;
            this.cHUDMagicComboBox.Location = new System.Drawing.Point(62, 36);
            this.cHUDMagicComboBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cHUDMagicComboBox.Name = "cHUDMagicComboBox";
            this.cHUDMagicComboBox.Size = new System.Drawing.Size(205, 23);
            this.cHUDMagicComboBox.TabIndex = 35;
            this.cHUDMagicComboBox.SelectedIndexChanged += new System.EventHandler(this.cHUDMagicComboBox_SelectedIndexChanged);
            // 
            // btn_hud
            // 
            this.btn_hud.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_hud.AutoSize = true;
            this.btn_hud.Location = new System.Drawing.Point(275, 11);
            this.btn_hud.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_hud.Name = "btn_hud";
            this.cHUDTableLayoutPanel.SetRowSpan(this.btn_hud, 2);
            this.btn_hud.Size = new System.Drawing.Size(83, 43);
            this.btn_hud.TabIndex = 31;
            this.btn_hud.Text = "Customize";
            this.btn_hud.UseVisualStyleBackColor = true;
            this.btn_hud.Click += new System.EventHandler(this.btn_hud_Click);
            // 
            // tFormCosmetics
            // 
            this.tFormCosmetics.Location = new System.Drawing.Point(7, 3);
            this.tFormCosmetics.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tFormCosmetics.Name = "tFormCosmetics";
            this.tFormCosmetics.SelectedIndex = 0;
            this.tFormCosmetics.Size = new System.Drawing.Size(285, 149);
            this.tFormCosmetics.TabIndex = 39;
            // 
            // cDrawHash
            // 
            this.cDrawHash.AutoSize = true;
            this.cDrawHash.BackColor = System.Drawing.Color.Transparent;
            this.cDrawHash.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cDrawHash.Location = new System.Drawing.Point(132, 74);
            this.cDrawHash.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cDrawHash.Name = "cDrawHash";
            this.cDrawHash.Size = new System.Drawing.Size(111, 19);
            this.cDrawHash.TabIndex = 28;
            this.cDrawHash.Text = "Hash Icons .png";
            this.cDrawHash.UseVisualStyleBackColor = false;
            this.cDrawHash.CheckedChanged += new System.EventHandler(this.cDrawHash_CheckedChanged);
            // 
            // gGameOutput
            // 
            this.gGameOutput.Controls.Add(this.cHTMLLog);
            this.gGameOutput.Controls.Add(this.cPatch);
            this.gGameOutput.Controls.Add(this.cDrawHash);
            this.gGameOutput.Controls.Add(this.cSpoiler);
            this.gGameOutput.Controls.Add(this.cN64);
            this.gGameOutput.Controls.Add(this.cVC);
            this.gGameOutput.Location = new System.Drawing.Point(15, 468);
            this.gGameOutput.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gGameOutput.Name = "gGameOutput";
            this.gGameOutput.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gGameOutput.Size = new System.Drawing.Size(264, 103);
            this.gGameOutput.TabIndex = 16;
            this.gGameOutput.TabStop = false;
            this.gGameOutput.Text = "Outputs";
            // 
            // cHTMLLog
            // 
            this.cHTMLLog.AutoSize = true;
            this.cHTMLLog.BackColor = System.Drawing.Color.Transparent;
            this.cHTMLLog.ForeColor = System.Drawing.Color.Black;
            this.cHTMLLog.Location = new System.Drawing.Point(132, 48);
            this.cHTMLLog.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cHTMLLog.Name = "cHTMLLog";
            this.cHTMLLog.Size = new System.Drawing.Size(121, 19);
            this.cHTMLLog.TabIndex = 14;
            this.cHTMLLog.Text = "Item Tracker .html";
            this.cHTMLLog.UseVisualStyleBackColor = false;
            // 
            // cPatch
            // 
            this.cPatch.AutoSize = true;
            this.cPatch.BackColor = System.Drawing.Color.Transparent;
            this.cPatch.ForeColor = System.Drawing.Color.Black;
            this.cPatch.Location = new System.Drawing.Point(19, 74);
            this.cPatch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cPatch.Name = "cPatch";
            this.cPatch.Size = new System.Drawing.Size(88, 19);
            this.cPatch.TabIndex = 15;
            this.cPatch.Text = "Patch .mmr";
            this.cPatch.UseVisualStyleBackColor = false;
            this.cPatch.CheckedChanged += new System.EventHandler(this.cPatch_CheckedChanged);
            // 
            // cSpoiler
            // 
            this.cSpoiler.AutoSize = true;
            this.cSpoiler.BackColor = System.Drawing.Color.Transparent;
            this.cSpoiler.ForeColor = System.Drawing.Color.Black;
            this.cSpoiler.Location = new System.Drawing.Point(132, 22);
            this.cSpoiler.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cSpoiler.Name = "cSpoiler";
            this.cSpoiler.Size = new System.Drawing.Size(102, 19);
            this.cSpoiler.TabIndex = 8;
            this.cSpoiler.Text = "Spoiler log .txt";
            this.cSpoiler.UseVisualStyleBackColor = false;
            // 
            // cN64
            // 
            this.cN64.AutoSize = true;
            this.cN64.BackColor = System.Drawing.Color.Transparent;
            this.cN64.Checked = true;
            this.cN64.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cN64.ForeColor = System.Drawing.Color.Black;
            this.cN64.Location = new System.Drawing.Point(19, 21);
            this.cN64.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cN64.Name = "cN64";
            this.cN64.Size = new System.Drawing.Size(100, 19);
            this.cN64.TabIndex = 10;
            this.cN64.Text = "N64 ROM .z64";
            this.cN64.UseVisualStyleBackColor = false;
            // 
            // cVC
            // 
            this.cVC.AutoSize = true;
            this.cVC.BackColor = System.Drawing.Color.Transparent;
            this.cVC.ForeColor = System.Drawing.Color.Black;
            this.cVC.Location = new System.Drawing.Point(19, 47);
            this.cVC.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cVC.Name = "cVC";
            this.cVC.Size = new System.Drawing.Size(89, 19);
            this.cVC.TabIndex = 9;
            this.cVC.Text = "Wii VC .wad";
            this.cVC.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(263, 420);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(277, 15);
            this.label1.TabIndex = 12;
            this.label1.Text = "ROM must be Majora\'s Mask (U) ending with \".z64\"";
            // 
            // bApplyPatch
            // 
            this.bApplyPatch.Location = new System.Drawing.Point(461, 10);
            this.bApplyPatch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bApplyPatch.Name = "bApplyPatch";
            this.bApplyPatch.Size = new System.Drawing.Size(115, 54);
            this.bApplyPatch.TabIndex = 16;
            this.bApplyPatch.Text = "Apply Patch";
            this.bApplyPatch.UseVisualStyleBackColor = true;
            this.bApplyPatch.Click += new System.EventHandler(this.bApplyPatch_Click);
            // 
            // saveROM
            // 
            this.saveROM.DefaultExt = "z64";
            this.saveROM.Filter = "ROM files|*.z64";
            // 
            // cTunic
            // 
            this.cTunic.Color = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(105)))), ((int)(((byte)(27)))));
            // 
            // bRandomise
            // 
            this.bRandomise.Location = new System.Drawing.Point(461, 10);
            this.bRandomise.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bRandomise.Name = "bRandomise";
            this.bRandomise.Size = new System.Drawing.Size(115, 54);
            this.bRandomise.TabIndex = 5;
            this.bRandomise.Text = "Randomize";
            this.bRandomise.UseVisualStyleBackColor = true;
            this.bRandomise.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bRandomise_MouseDown);
            // 
            // bReroll
            // 
            this.bReroll.Location = new System.Drawing.Point(366, 10);
            this.bReroll.Name = "bReroll";
            this.bReroll.Size = new System.Drawing.Size(88, 23);
            this.bReroll.TabIndex = 8;
            this.bReroll.Text = "Re-Roll Seed";
            this.bReroll.UseVisualStyleBackColor = true;
            this.bReroll.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bReroll_MouseDown);
            // 
            // saveWad
            // 
            this.saveWad.DefaultExt = "wad";
            this.saveWad.Filter = "VC files|*.wad";
            // 
            // mMenu
            // 
            this.mMenu.BackColor = System.Drawing.SystemColors.Control;
            this.mMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mFile,
            this.mCustomise,
            this.toolsToolStripMenuItem,
            this.mHelp});
            this.mMenu.Location = new System.Drawing.Point(0, 0);
            this.mMenu.Name = "mMenu";
            this.mMenu.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.mMenu.Size = new System.Drawing.Size(896, 24);
            this.mMenu.TabIndex = 12;
            this.mMenu.Text = "mMenu";
            // 
            // mFile
            // 
            this.mFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveSettingsToolStripMenuItem,
            this.loadSettingsToolStripMenuItem,
            this.mExit});
            this.mFile.Name = "mFile";
            this.mFile.Size = new System.Drawing.Size(37, 20);
            this.mFile.Text = "File";
            // 
            // saveSettingsToolStripMenuItem
            // 
            this.saveSettingsToolStripMenuItem.Name = "saveSettingsToolStripMenuItem";
            this.saveSettingsToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.saveSettingsToolStripMenuItem.Text = "Save Settings...";
            this.saveSettingsToolStripMenuItem.Click += new System.EventHandler(this.SaveSettingsToolStripMenuItem_Click);
            // 
            // loadSettingsToolStripMenuItem
            // 
            this.loadSettingsToolStripMenuItem.Name = "loadSettingsToolStripMenuItem";
            this.loadSettingsToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.loadSettingsToolStripMenuItem.Text = "Load Settings...";
            this.loadSettingsToolStripMenuItem.Click += new System.EventHandler(this.LoadSettingsToolStripMenuItem_Click);
            // 
            // mExit
            // 
            this.mExit.Name = "mExit";
            this.mExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.mExit.Size = new System.Drawing.Size(154, 22);
            this.mExit.Text = "Exit";
            this.mExit.Click += new System.EventHandler(this.mExit_Click);
            // 
            // mCustomise
            // 
            this.mCustomise.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mDPadConfig});
            this.mCustomise.Name = "mCustomise";
            this.mCustomise.Size = new System.Drawing.Size(75, 20);
            this.mCustomise.Text = "Customize";
            // 
            // mDPadConfig
            // 
            this.mDPadConfig.Name = "mDPadConfig";
            this.mDPadConfig.Size = new System.Drawing.Size(184, 22);
            this.mDPadConfig.Text = "D-Pad Configuration";
            this.mDPadConfig.Click += new System.EventHandler(this.mDPadConfig_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mLogicEdit});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // mLogicEdit
            // 
            this.mLogicEdit.Name = "mLogicEdit";
            this.mLogicEdit.Size = new System.Drawing.Size(137, 22);
            this.mLogicEdit.Text = "Logic editor";
            this.mLogicEdit.Click += new System.EventHandler(this.mLogicEdit_Click);
            // 
            // mHelp
            // 
            this.mHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mManual,
            this.mSep1,
            this.mAbout});
            this.mHelp.Name = "mHelp";
            this.mHelp.Size = new System.Drawing.Size(44, 20);
            this.mHelp.Text = "Help";
            // 
            // mManual
            // 
            this.mManual.Name = "mManual";
            this.mManual.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.mManual.Size = new System.Drawing.Size(133, 22);
            this.mManual.Text = "Manual";
            this.mManual.Click += new System.EventHandler(this.mManual_Click);
            // 
            // mSep1
            // 
            this.mSep1.Name = "mSep1";
            this.mSep1.Size = new System.Drawing.Size(130, 6);
            // 
            // mAbout
            // 
            this.mAbout.Name = "mAbout";
            this.mAbout.Size = new System.Drawing.Size(133, 22);
            this.mAbout.Text = "About";
            this.mAbout.Click += new System.EventHandler(this.mAbout_Click);
            // 
            // openBROM
            // 
            this.openBROM.Filter = "ROM files|*.z64;*.v64;*.n64";
            // 
            // pProgress
            // 
            this.pProgress.Location = new System.Drawing.Point(15, 592);
            this.pProgress.Margin = new System.Windows.Forms.Padding(2);
            this.pProgress.Name = "pProgress";
            this.pProgress.Size = new System.Drawing.Size(866, 22);
            this.pProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pProgress.TabIndex = 13;
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            // 
            // lStatus
            // 
            this.lStatus.AutoSize = true;
            this.lStatus.BackColor = System.Drawing.Color.Transparent;
            this.lStatus.Location = new System.Drawing.Point(13, 573);
            this.lStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(48, 15);
            this.lStatus.TabIndex = 13;
            this.lStatus.Text = "Ready...";
            // 
            // tSeed
            // 
            this.tSeed.Location = new System.Drawing.Point(90, 10);
            this.tSeed.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tSeed.MaxLength = 10;
            this.tSeed.Name = "tSeed";
            this.tSeed.Size = new System.Drawing.Size(269, 23);
            this.tSeed.TabIndex = 2;
            this.tSeed.Enter += new System.EventHandler(this.tSeed_Enter);
            this.tSeed.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tSeed_KeyDown);
            this.tSeed.Leave += new System.EventHandler(this.tSeed_Leave);
            // 
            // lSeed
            // 
            this.lSeed.AutoSize = true;
            this.lSeed.BackColor = System.Drawing.Color.Transparent;
            this.lSeed.ForeColor = System.Drawing.Color.Black;
            this.lSeed.Location = new System.Drawing.Point(4, 14);
            this.lSeed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lSeed.Name = "lSeed";
            this.lSeed.Size = new System.Drawing.Size(82, 15);
            this.lSeed.TabIndex = 3;
            this.lSeed.Text = "Random seed:";
            // 
            // cDummy
            // 
            this.cDummy.AutoSize = true;
            this.cDummy.Enabled = false;
            this.cDummy.Location = new System.Drawing.Point(684, 582);
            this.cDummy.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cDummy.Name = "cDummy";
            this.cDummy.Size = new System.Drawing.Size(83, 19);
            this.cDummy.TabIndex = 9;
            this.cDummy.Text = "checkBox1";
            this.cDummy.UseVisualStyleBackColor = true;
            this.cDummy.Visible = false;
            // 
            // openPatch
            // 
            this.openPatch.Filter = "MMR Patch files|*.mmr";
            // 
            // ttOutput
            // 
            this.ttOutput.Controls.Add(this.tpOutputSettings);
            this.ttOutput.Controls.Add(this.tpPatchSettings);
            this.ttOutput.Location = new System.Drawing.Point(289, 470);
            this.ttOutput.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ttOutput.Name = "ttOutput";
            this.ttOutput.SelectedIndex = 0;
            this.ttOutput.Size = new System.Drawing.Size(594, 103);
            this.ttOutput.TabIndex = 15;
            this.ttOutput.SelectedIndexChanged += new System.EventHandler(this.ttOutput_Changed);
            // 
            // tpOutputSettings
            // 
            this.tpOutputSettings.Controls.Add(this.bReroll);
            this.tpOutputSettings.Controls.Add(this.bRandomise);
            this.tpOutputSettings.Controls.Add(this.tSeed);
            this.tpOutputSettings.Controls.Add(this.lSeed);
            this.tpOutputSettings.Location = new System.Drawing.Point(4, 24);
            this.tpOutputSettings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tpOutputSettings.Name = "tpOutputSettings";
            this.tpOutputSettings.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tpOutputSettings.Size = new System.Drawing.Size(586, 75);
            this.tpOutputSettings.TabIndex = 0;
            this.tpOutputSettings.Text = "Output settings";
            this.tpOutputSettings.UseVisualStyleBackColor = true;
            // 
            // tpPatchSettings
            // 
            this.tpPatchSettings.Controls.Add(this.tPatch);
            this.tpPatchSettings.Controls.Add(this.bApplyPatch);
            this.tpPatchSettings.Controls.Add(this.bLoadPatch);
            this.tpPatchSettings.Location = new System.Drawing.Point(4, 24);
            this.tpPatchSettings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tpPatchSettings.Name = "tpPatchSettings";
            this.tpPatchSettings.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tpPatchSettings.Size = new System.Drawing.Size(586, 75);
            this.tpPatchSettings.TabIndex = 1;
            this.tpPatchSettings.Text = "Patch settings";
            this.tpPatchSettings.UseVisualStyleBackColor = true;
            // 
            // tPatch
            // 
            this.tPatch.Location = new System.Drawing.Point(7, 40);
            this.tPatch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tPatch.Name = "tPatch";
            this.tPatch.ReadOnly = true;
            this.tPatch.Size = new System.Drawing.Size(446, 23);
            this.tPatch.TabIndex = 17;
            // 
            // bLoadPatch
            // 
            this.bLoadPatch.Location = new System.Drawing.Point(6, 7);
            this.bLoadPatch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bLoadPatch.Name = "bLoadPatch";
            this.bLoadPatch.Size = new System.Drawing.Size(120, 30);
            this.bLoadPatch.TabIndex = 16;
            this.bLoadPatch.Text = "Load Patch...";
            this.bLoadPatch.UseVisualStyleBackColor = true;
            this.bLoadPatch.Click += new System.EventHandler(this.BLoadPatch_Click);
            // 
            // bSkip
            // 
            this.bSkip.Location = new System.Drawing.Point(808, 591);
            this.bSkip.Name = "bSkip";
            this.bSkip.Size = new System.Drawing.Size(75, 23);
            this.bSkip.TabIndex = 17;
            this.bSkip.Text = "Skip";
            this.bSkip.UseVisualStyleBackColor = true;
            this.bSkip.Visible = false;
            this.bSkip.Click += new System.EventHandler(this.bSkip_Click);
            // 
            // cImportanceCount
            // 
            this.cImportanceCount.AutoSize = true;
            this.cImportanceCount.BackColor = System.Drawing.Color.Transparent;
            this.cImportanceCount.ForeColor = System.Drawing.Color.Black;
            this.cImportanceCount.Location = new System.Drawing.Point(117, 55);
            this.cImportanceCount.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cImportanceCount.Name = "cImportanceCount";
            this.cImportanceCount.Size = new System.Drawing.Size(123, 19);
            this.cImportanceCount.TabIndex = 31;
            this.cImportanceCount.Text = "Importance Count";
            this.cImportanceCount.UseVisualStyleBackColor = false;
            // 
            // cImportanceCountGaro
            // 
            this.cImportanceCountGaro.AutoSize = true;
            this.cImportanceCountGaro.BackColor = System.Drawing.Color.Transparent;
            this.cImportanceCountGaro.ForeColor = System.Drawing.Color.Black;
            this.cImportanceCountGaro.Location = new System.Drawing.Point(117, 55);
            this.cImportanceCountGaro.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cImportanceCountGaro.Name = "cImportanceCountGaro";
            this.cImportanceCountGaro.Size = new System.Drawing.Size(123, 19);
            this.cImportanceCountGaro.TabIndex = 32;
            this.cImportanceCountGaro.Text = "Importance Count";
            this.cImportanceCountGaro.UseVisualStyleBackColor = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(896, 627);
            this.Controls.Add(this.bSkip);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bopen);
            this.Controls.Add(this.tROMName);
            this.Controls.Add(this.lStatus);
            this.Controls.Add(this.gGameOutput);
            this.Controls.Add(this.ttOutput);
            this.Controls.Add(this.cDummy);
            this.Controls.Add(this.pProgress);
            this.Controls.Add(this.tSettings);
            this.Controls.Add(this.mMenu);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.mMenu;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.mmrMain_Load);
            this.tSettings.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tOtherCustomizations.ResumeLayout(false);
            this.tOtherCustomization.ResumeLayout(false);
            this.tOtherCustomization.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabItemPool.ResumeLayout(false);
            this.tabItemPool.PerformLayout();
            this.tabGimmicks.ResumeLayout(false);
            this.tabGimmicks.PerformLayout();
            this.gGimmicksChallenges.ResumeLayout(false);
            this.gGimmicksChallenges.PerformLayout();
            this.gGimmicksMovement.ResumeLayout(false);
            this.gGimmicksMovement.PerformLayout();
            this.gTraps.ResumeLayout(false);
            this.gTraps.PerformLayout();
            this.gGimmicksClock.ResumeLayout(false);
            this.gGimmicksClock.PerformLayout();
            this.gGimmicksOther.ResumeLayout(false);
            this.gGimmicksOther.PerformLayout();
            this.tabComfort.ResumeLayout(false);
            this.gHintsGeneral.ResumeLayout(false);
            this.gHintsGeneral.PerformLayout();
            this.gGaroHints.ResumeLayout(false);
            this.gGaroHints.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nMaxGaroCT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMaxGaroFoolish)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMaxGaroWotH)).EndInit();
            this.gSpeedUps.ResumeLayout(false);
            this.gSpeedUps.PerformLayout();
            this.gHints.ResumeLayout(false);
            this.gHints.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nMaxGossipCT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMaxGossipFoolish)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMaxGossipWotH)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.tabShortenCutscenes.ResumeLayout(false);
            this.tabCosmetics.ResumeLayout(false);
            this.gCosmeticOther.ResumeLayout(false);
            this.gCosmeticOther.PerformLayout();
            this.gCosmeticMusicSound.ResumeLayout(false);
            this.gCosmeticMusicSound.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tLuckRollPercentage)).EndInit();
            this.cHUDGroupBox.ResumeLayout(false);
            this.cHUDGroupBox.PerformLayout();
            this.cHUDTableLayoutPanel.ResumeLayout(false);
            this.cHUDTableLayoutPanel.PerformLayout();
            this.gGameOutput.ResumeLayout(false);
            this.gGameOutput.PerformLayout();
            this.mMenu.ResumeLayout(false);
            this.mMenu.PerformLayout();
            this.ttOutput.ResumeLayout(false);
            this.tpOutputSettings.ResumeLayout(false);
            this.tpOutputSettings.PerformLayout();
            this.tpPatchSettings.ResumeLayout(false);
            this.tpPatchSettings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bopen;
        private System.Windows.Forms.OpenFileDialog openROM;
        private System.Windows.Forms.OpenFileDialog openPatch;
        private System.Windows.Forms.OpenFileDialog openLogic;
        private System.Windows.Forms.OpenFileDialog loadSettings;
        private System.Windows.Forms.TextBox tROMName;
        private System.Windows.Forms.ComboBox cMode;
        private System.Windows.Forms.Label lMode;
        private System.Windows.Forms.SaveFileDialog saveROM;
        private System.Windows.Forms.SaveFileDialog saveSettings;
        private System.Windows.Forms.ComboBox cLink;
        private System.Windows.Forms.Label lLink;
        private System.Windows.Forms.CheckBox cQText;
        private System.Windows.Forms.CheckBox cEnemy;
        private System.Windows.Forms.CheckBox cDEnt;
        private System.Windows.Forms.CheckBox cMixSongs;
        private System.Windows.Forms.ColorDialog cEnergy;
        private System.Windows.Forms.ColorDialog cTunic;
        private System.Windows.Forms.Button bRandomise;
        private System.Windows.Forms.Button bReroll;
        private System.Windows.Forms.CheckBox cSoS;
        private System.Windows.Forms.TabControl tSettings;
        private System.Windows.Forms.TabPage tabMain;
        private System.Windows.Forms.TabPage tabComfort;
        private System.Windows.Forms.Label lFloors;
        private System.Windows.Forms.Label lGravity;
        private System.Windows.Forms.Label lDType;
        private System.Windows.Forms.Label lDMult;
        private System.Windows.Forms.ComboBox cFloors;
        private System.Windows.Forms.ComboBox cDType;
        private System.Windows.Forms.ComboBox cDMult;
        private System.Windows.Forms.ComboBox cGravity;
        private System.Windows.Forms.SaveFileDialog saveWad;
        private System.Windows.Forms.CheckBox cVC;
        private System.Windows.Forms.CheckBox cN64;
        private System.Windows.Forms.MenuStrip mMenu;
        private System.Windows.Forms.ToolStripMenuItem mFile;
        private System.Windows.Forms.ToolStripMenuItem mExit;
        private System.Windows.Forms.ToolStripMenuItem mHelp;
        private System.Windows.Forms.ToolStripMenuItem mManual;
        private System.Windows.Forms.ToolStripMenuItem mAbout;
        private System.Windows.Forms.ToolStripSeparator mSep1;
        private System.Windows.Forms.OpenFileDialog openBROM;
        private System.Windows.Forms.ToolStripMenuItem mCustomise;
        private System.Windows.Forms.ProgressBar pProgress;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Label lStatus;
        private System.Windows.Forms.TextBox tSeed;
        private System.Windows.Forms.Label lSeed;
        private System.Windows.Forms.CheckBox cDummy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cFreeHints;
        private System.Windows.Forms.CheckBox cPatch;
        private System.Windows.Forms.Button bApplyPatch;
        private System.Windows.Forms.TabControl ttOutput;
        private System.Windows.Forms.TabPage tpOutputSettings;
        private System.Windows.Forms.TabPage tpPatchSettings;
        private System.Windows.Forms.TextBox tPatch;
        private System.Windows.Forms.Button bLoadPatch;
        private System.Windows.Forms.CheckBox cClearHints;
        private System.Windows.Forms.Label lClockSpeed;
        private System.Windows.Forms.Label lNutAndStickDrops;
        private System.Windows.Forms.ComboBox cNutAndStickDrops;
        private System.Windows.Forms.ComboBox cClockSpeed;
        private System.Windows.Forms.CheckBox cHideClock;
        private System.Windows.Forms.CheckBox cNoDowngrades;
        private System.Windows.Forms.Label lGossip;
        private System.Windows.Forms.ComboBox cGossipHints;
        private System.Windows.Forms.CheckBox cShopAppearance;
        private System.Windows.Forms.CheckBox cEponaSword;
        private System.Windows.Forms.CheckBox cUpdateChests;
        private System.Windows.Forms.GroupBox gGameOutput;
        private System.Windows.Forms.TextBox tbUserLogic;
        private System.Windows.Forms.Button bLoadLogic;
        private System.Windows.Forms.ComboBox cBlastCooldown;
        private System.Windows.Forms.Label lBlastMask;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button bStartingItemEditor;
        private System.Windows.Forms.TextBox tStartingItemList;
        private System.Windows.Forms.Label lCustomStartingItemAmount;
        private System.Windows.Forms.CheckBox cGoodDogRaceRNG;
        private System.Windows.Forms.CheckBox cFasterLabFish;
        private System.Windows.Forms.CheckBox cGoodDampeRNG;
        private System.Windows.Forms.CheckBox cSkipBeaver;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label lJunkLocationsAmount;
        private System.Windows.Forms.Button bJunkLocationsEditor;
        private System.Windows.Forms.TextBox tJunkLocationsList;
        private System.Windows.Forms.ToolStripMenuItem mDPadConfig;
        private System.Windows.Forms.CheckBox cSunsSong;
        private System.Windows.Forms.ToolStripMenuItem saveSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSettingsToolStripMenuItem;
        private System.Windows.Forms.CheckBox cUnderwaterOcarina;
        private System.Windows.Forms.CheckBox cDrawHash;
        private System.Windows.Forms.CheckBox cQuestItemStorage;
        private System.Windows.Forms.CheckBox cDisableCritWiggle;
        private System.Windows.Forms.CheckBox cFastPush;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.GroupBox gHints;
        private System.Windows.Forms.TabPage tabGimmicks;
        private System.Windows.Forms.CheckBox cHTMLLog;
        private System.Windows.Forms.CheckBox cSpoiler;
        private System.Windows.Forms.GroupBox gSpeedUps;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mLogicEdit;
        private System.Windows.Forms.CheckBox cEnableNightMusic;
        private System.Windows.Forms.CheckBox cFreestanding;
        private System.Windows.Forms.CheckBox cFDAnywhere;
        private System.Windows.Forms.CheckBox cArrowCycling;
        private System.Windows.Forms.CheckBox cFasterBank;
        private System.Windows.Forms.CheckBox cCloseCows;
        private System.Windows.Forms.ComboBox cLowHealthSFXComboBox;
        private System.Windows.Forms.Label lLowHealthSFXComboBox;
        private System.Windows.Forms.Button bToggleTricks;
        private System.Windows.Forms.CheckBox cByoAmmo;
        private System.Windows.Forms.CheckBox cDeathMoonCrash;
        private System.Windows.Forms.CheckBox cElegySpeedups;
        private System.Windows.Forms.CheckBox cContinuousDekuHopping;
        private System.Windows.Forms.CheckBox cProgressiveUpgrades;
        private System.Windows.Forms.Label lTrapAmount;
        private System.Windows.Forms.ComboBox cTrapsAppearance;
        private System.Windows.Forms.ComboBox cTrapAmount;
        private System.Windows.Forms.CheckBox cIceTrapQuirks;
        private System.Windows.Forms.TabPage tabCosmetics;
        private System.Windows.Forms.CheckBox cSFX;
        private System.Windows.Forms.CheckBox cTargettingStyle;
        private System.Windows.Forms.Label lTatl;
        private System.Windows.Forms.GroupBox cHUDGroupBox;
        private System.Windows.Forms.TableLayoutPanel cHUDTableLayoutPanel;
        private System.Windows.Forms.ComboBox cHUDHeartsComboBox;
        private System.Windows.Forms.Label cHeartsLabel;
        private System.Windows.Forms.Label cMagicLabel;
        private System.Windows.Forms.ComboBox cHUDMagicComboBox;
        private System.Windows.Forms.Button btn_hud;
        private System.Windows.Forms.Label lMusic;
        private System.Windows.Forms.CheckBox cCombatMusicDisable;
        private System.Windows.Forms.ComboBox cTatl;
        private System.Windows.Forms.ComboBox cMusic;
        private System.Windows.Forms.TabControl tFormCosmetics;
        private System.Windows.Forms.CheckBox cHueShiftMiscUI;
        private System.Windows.Forms.GroupBox gCosmeticOther;
        private System.Windows.Forms.GroupBox gCosmeticMusicSound;
        private System.Windows.Forms.TabPage tabShortenCutscenes;
        private System.Windows.Forms.TabControl tShortenCutscenes;
        private System.Windows.Forms.GroupBox gGimmicksOther;
        private System.Windows.Forms.GroupBox gGimmicksChallenges;
        private System.Windows.Forms.GroupBox gGimmicksMovement;
        private System.Windows.Forms.GroupBox gTraps;
        private System.Windows.Forms.Label lTrapsAppearance;
        private System.Windows.Forms.GroupBox gGimmicksClock;
        private System.Windows.Forms.TabControl tOtherCustomizations;
        private System.Windows.Forms.TabPage tOtherCustomization;
        private System.Windows.Forms.TabPage tabItemPool;
        private System.Windows.Forms.TableLayoutPanel tableItemPool;
        private System.Windows.Forms.Button bItemPoolEdit;
        private System.Windows.Forms.TextBox tItemPool;
        private System.Windows.Forms.Label lItemPoolText;
        private System.Windows.Forms.ComboBox cStartingItems;
        private System.Windows.Forms.Label lStartingItems;
        private System.Windows.Forms.CheckBox cInstantPictobox;
        private System.Windows.Forms.CheckBox cImprovedPictobox;
        private System.Windows.Forms.CheckBox cHookshotAnySurface;
        private System.Windows.Forms.CheckBox cClimbMostSurfaces;
        private System.Windows.Forms.CheckBox cLenientGoronSpikes;
        private System.Windows.Forms.CheckBox cTargetHealth;
        private System.Windows.Forms.CheckBox cFreeScarecrow;
        private System.Windows.Forms.CheckBox cDoubleArcheryRewards;
        private System.Windows.Forms.CheckBox cFillWallet;
        private System.Windows.Forms.ComboBox cAutoInvert;
        private System.Windows.Forms.Label lAutoInvert;
        private System.Windows.Forms.CheckBox cInvisSparkle;
        private System.Windows.Forms.Panel pClassicItemPool;
        private System.Windows.Forms.CheckBox cItemPoolAdvanced;
        private System.Windows.Forms.Panel pLocationCategories;
        private System.Windows.Forms.GroupBox gGaroHints;
        private System.Windows.Forms.Label lGaroHints;
        private System.Windows.Forms.ComboBox cGaroHint;
        private System.Windows.Forms.CheckBox cMixGaroWithGossip;
        private System.Windows.Forms.CheckBox cClearGaroHints;
        private System.Windows.Forms.GroupBox gHintsGeneral;
        private System.Windows.Forms.CheckBox cHintImportance;
        private System.Windows.Forms.NumericUpDown nMaxGossipWotH;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lGossipWothConfig;
        private System.Windows.Forms.NumericUpDown nMaxGaroCT;
        private System.Windows.Forms.NumericUpDown nMaxGaroFoolish;
        private System.Windows.Forms.NumericUpDown nMaxGaroWotH;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nMaxGossipCT;
        private System.Windows.Forms.NumericUpDown nMaxGossipFoolish;
        private System.Windows.Forms.CheckBox cCustomGaroWoth;
        private System.Windows.Forms.CheckBox cCustomGossipWoth;
        private System.Windows.Forms.Button bCustomizeHintPriorities;
        private System.Windows.Forms.CheckBox cFreeGaroHints;
        private System.Windows.Forms.Label lLuckRoll;
        private System.Windows.Forms.NumericUpDown tLuckRollPercentage;
        private System.Windows.Forms.Button bSkip;
        private System.Windows.Forms.ComboBox cItemPlacement;
        private System.Windows.Forms.Label lItemPlacement;
        private System.Windows.Forms.Label lNumTricksEnabled;
        private System.Windows.Forms.CheckBox cSaferGlitches;
        private System.Windows.Forms.CheckBox cSpeedupBabyCucco;
        private System.Windows.Forms.Label lChestGame;
        private System.Windows.Forms.ComboBox cChestGameMinimap;
        private System.Windows.Forms.CheckBox cShuffleBosses;
        private System.Windows.Forms.CheckBox cAddBombchuDrops;
        private System.Windows.Forms.Label lTrapWeightings;
        private System.Windows.Forms.CheckBox cRainbowTunic;
        private System.Windows.Forms.CheckBox cBombTrapTunicColors;
        private System.Windows.Forms.CheckBox cInstantTransformations;
        private System.Windows.Forms.CheckBox cBombArrows;
        private System.Windows.Forms.CheckBox cUpdateNpcText;
        private System.Windows.Forms.CheckBox cQuestItemKeep;
        private System.Windows.Forms.CheckBox cRemoveMinorMusic;
        private System.Windows.Forms.ComboBox cRequiredBossRemains;
        private System.Windows.Forms.Label lRequiredRemains;
        private System.Windows.Forms.CheckBox cGossipsTolerant;
        private System.Windows.Forms.CheckBox cMusicTrackNames;
        private System.Windows.Forms.CheckBox cDisableFanfares;
        private System.Windows.Forms.CheckBox cGiantMaskAnywhere;
        private System.Windows.Forms.CheckBox cImprovedCamera;
        private System.Windows.Forms.CheckBox cFewerHealthDrops;
        private System.Windows.Forms.CheckBox cIronGoron;
        private System.Windows.Forms.CheckBox cEasyFrameByFrame;
        private System.Windows.Forms.CheckBox cSkulltulaTokenSounds;
        private System.Windows.Forms.CheckBox cFairyMaskShimmer;
        private System.Windows.Forms.CheckBox cVanillaMoonTrials;
        private System.Windows.Forms.CheckBox cTakeDamageOnEpona;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cTakeDamageFromGibdosFaster;
        private System.Windows.Forms.CheckBox cTakeDamageGettingCaught;
        private System.Windows.Forms.CheckBox cTakeDamageFromGorons;
        private System.Windows.Forms.CheckBox cTakeDamageFromDog;
        private System.Windows.Forms.CheckBox cTakeDamageFromVoid;
        private System.Windows.Forms.CheckBox cTakeDamageWhileShielding;
        private System.Windows.Forms.CheckBox cTakeDamageFromDexihands;
        private System.Windows.Forms.CheckBox cOathHint;
        private System.Windows.Forms.CheckBox cRemainsHint;
        private System.Windows.Forms.CheckBox cFairyAndSkullHints;
        private System.Windows.Forms.CheckBox cImportanceCountGaro;
        private System.Windows.Forms.CheckBox cImportanceCount;
    }
}

