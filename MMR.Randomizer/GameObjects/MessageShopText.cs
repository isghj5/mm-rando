using System;

namespace MMR.Randomizer.GameObjects
{
    public enum MessageShopText
    {
        [MessageShop(MessageShopStyle.Tingle, Item.ItemTingleMapTown, Item.ItemTingleMapWoodfall)]
        Town = 0x1D11,

        [MessageShop(MessageShopStyle.Tingle, Item.ItemTingleMapWoodfall, Item.ItemTingleMapSnowhead)]
        Swamp = 0x1D12,

        [MessageShop(MessageShopStyle.Tingle, Item.ItemTingleMapSnowhead, Item.ItemTingleMapRanch)]
        Mountain = 0x1D13,

        [MessageShop(MessageShopStyle.Tingle, Item.ItemTingleMapRanch, Item.ItemTingleMapGreatBay)]
        Ranch = 0x1D14,

        [MessageShop(MessageShopStyle.Tingle, Item.ItemTingleMapGreatBay, Item.ItemTingleMapStoneTower)]
        Ocean = 0x1D15,

        [MessageShop(MessageShopStyle.Tingle, Item.ItemTingleMapStoneTower, Item.ItemTingleMapTown)]
        Canyon = 0x1D16,

        [MessageShop(MessageShopStyle.MilkBar, Item.ShopItemMilkBarMilk, Item.ShopItemMilkBarChateau)]
        MilkBar = 0x2B0B,
    }

    public enum MessageShopStyle
    {
        Tingle,
        MilkBar,
    }

    public class MessageShopAttribute : Attribute
    {
        public MessageShopStyle MessageShopStyle { get; }
        public Item[] Items { get; }

        public MessageShopAttribute(MessageShopStyle messageShopStyle, Item item1, Item item2)
        {
            MessageShopStyle = messageShopStyle;
            Items = new Item[] { item1, item2 };
        }
    }
}
