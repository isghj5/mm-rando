﻿using MMR.Randomizer.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using MMR.Randomizer.GameObjects;

namespace MMR.Randomizer.Models
{
    [DataContract]
    public class ItemLogic
    {
        [DataMember]
        public int ItemId;

        [DataMember]
        public int? NewLocationId;

        [DataMember]
        public List<int> RequiredItemIds;

        [DataMember]
        public List<List<int>> ConditionalItemIds;

        [DataMember]
        public bool Acquired;

        [DataMember]
        public bool IsFakeItem;

        [DataMember]
        public bool ShouldAutoAcquire;

        [DataMember]
        public bool IsItemRemoved;

        public ItemLogic(ItemLogic copyFrom)
        {
            ItemId = copyFrom.ItemId;
            NewLocationId = copyFrom.NewLocationId;
            RequiredItemIds = copyFrom.RequiredItemIds?.ToList();
            ConditionalItemIds = copyFrom.ConditionalItemIds?.Select(c => c.ToList()).ToList();
            Acquired = copyFrom.Acquired;
            IsFakeItem = copyFrom.IsFakeItem;
            ShouldAutoAcquire = copyFrom.ShouldAutoAcquire;
            IsItemRemoved = copyFrom.IsItemRemoved;
        }

        public ItemLogic(ItemObject itemObject)
        {
            ItemId = itemObject.ID;
            NewLocationId = (int?)itemObject.NewLocation;
            RequiredItemIds = itemObject.DependsOnItems?.Cast<int>().ToList();
            ConditionalItemIds = itemObject.Conditionals?.Select(c => c.Cast<int>().ToList()).ToList();
            IsFakeItem = itemObject.Item.IsFake() && itemObject.Item.Entrance() == null;
            IsItemRemoved = itemObject.ItemOverride.HasValue;

            // Remove fake requirements
            switch (itemObject.Item)
            {
                case Item.UpgradeBigBombBag:
                case Item.MaskBlast:
                case Item.NotebookSaveOldLady:
                    RequiredItemIds?.Remove((int)Item.TradeItemKafeiLetter);
                    RequiredItemIds?.Remove((int)Item.TradeItemPendant);
                    break;
                case Item.UpgradeMirrorShield:
                    ConditionalItemIds?.ForEach(c =>
                    {
                        c.Remove((int)Item.TradeItemKafeiLetter);
                        c.Remove((int)Item.TradeItemPendant);
                    });
                    break;
                case Item.BottleCatchPrincess:
                case Item.BottleCatchBigPoe:
                    RequiredItemIds?.Remove((int)Item.BottleCatchEgg);
                    RequiredItemIds?.Remove((int)Item.BottleCatchBug);
                    RequiredItemIds?.Remove((int)Item.BottleCatchFish);
                    break;
                case Item.BottleCatchEgg:
                    RequiredItemIds?.Remove((int)Item.BottleCatchFish);
                    break;
            }
        }
    }
}
