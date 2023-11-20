using System;

namespace MMR.Randomizer.Attributes
{
    public class OverwritableAttribute : BaseSettingsConditionAttribute
    {
        public ItemSlot Slot { get; }

        public OverwritableAttribute(ItemSlot slot)
        {
            Slot = slot;
            Condition = (setting) => true;
        }

        public OverwritableAttribute(ItemSlot slot, string settingProperty, object settingValue, bool isEqual = true)
        {
            Slot = slot;
            Condition = CreateConditionFunction(settingProperty, settingValue, isEqual);
        }

        public enum ItemSlot
        {
            None,
            Trade,
            KeyExpress,
            PendantKafei,
            Bottle,
            Sword,
            Shield,
            Quiver,
            BombBag,
            Wallet,
        }
    }
}
