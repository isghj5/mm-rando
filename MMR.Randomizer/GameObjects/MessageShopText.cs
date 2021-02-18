using System;

namespace MMR.Randomizer.GameObjects
{
    public enum MessageShopText
    {
        [MessageShop(MessageShopStyle.Tingle, Item.ItemTingleMapTown, 5, Item.ItemTingleMapWoodfall, 40)]
        Town = 0x1D11,

        [MessageShop(MessageShopStyle.Tingle, Item.ItemTingleMapWoodfall, 20, Item.ItemTingleMapSnowhead, 40)]
        Swamp = 0x1D12,

        [MessageShop(MessageShopStyle.Tingle, Item.ItemTingleMapSnowhead, 20, Item.ItemTingleMapRanch, 40)]
        Mountain = 0x1D13,

        [MessageShop(MessageShopStyle.Tingle, Item.ItemTingleMapRanch, 20, Item.ItemTingleMapGreatBay, 40)]
        Ranch = 0x1D14,

        [MessageShop(MessageShopStyle.Tingle, Item.ItemTingleMapGreatBay, 20, Item.ItemTingleMapStoneTower, 40)]
        Ocean = 0x1D15,

        [MessageShop(MessageShopStyle.Tingle, Item.ItemTingleMapStoneTower, 20, Item.ItemTingleMapTown, 40)]
        Canyon = 0x1D16,

        [MessageShop(MessageShopStyle.MilkBar, Item.ShopItemMilkBarMilk, 20, Item.ShopItemMilkBarChateau, 200)]
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
        public int[] Prices { get; }

        public MessageShopAttribute(MessageShopStyle messageShopStyle, Item item1, int price1, Item item2, int price2)
        {
            MessageShopStyle = messageShopStyle;
            Items = new Item[] { item1, item2 };
            Prices = new int[] { price1, price2 };
        }
    }
}
