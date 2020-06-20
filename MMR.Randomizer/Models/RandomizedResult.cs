﻿using MMR.Randomizer.Models.Rom;
using MMR.Randomizer.Models.SoundEffects;
using MMR.Randomizer.Models.Settings;
using MMR.Randomizer.GameObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MMR.Randomizer.Models
{
    public class RandomizedResult
    {
        public GameplaySettings Settings { get; private set; }
        public int Seed { get; set; }

        public ItemList ItemList { get; set; }
        public List<MessageEntry> GossipQuotes { get; set; }
        public List<ItemLogic> Logic { get; set; }
        public ReadOnlyCollection<Item> ImportantItems { get; set; }
        public ReadOnlyCollection<Item> ItemsRequiredForMoonAccess { get; set; }
        public int FileSelectSkybox { get; internal set; }
        public int FileSelectColor { get; internal set; }
        public int TitleLogoColor { get; internal set; }

        public int[] NewEntrances = new int[] { -1, -1, -1, -1 };
        public int[] NewExits = new int[] { -1, -1, -1, -1 };

        public int[] NewExitIndices = new int[] { -1, -1, -1, -1 };
        public int[] NewDCFlags = new int[] { -1, -1, -1, -1 };
        public int[] NewDCMasks = new int[] { -1, -1, -1, -1 };
        public int[] NewDestinationIndices = new int[] { -1, -1, -1, -1 };

        public RandomizedResult(GameplaySettings settings, int seed)
        {
            Settings = settings;
            Seed = seed;
        }

    }
}
