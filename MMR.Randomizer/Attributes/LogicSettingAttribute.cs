using MMR.Randomizer.GameObjects;

namespace MMR.Randomizer.Attributes
{
    public class LogicSettingAttribute : BaseSettingsConditionAttribute
    {
        public LogicSettingAttribute(string settingFlagProperty, int flagValue, bool hasFlag)
        {
            Condition = CreateConditionFunction(settingFlagProperty, flagValue, hasFlag);
        }

        public LogicSettingAttribute(string settingProperty, object settingValue, bool isEqual = true)
        {
            Condition = CreateConditionFunction(settingProperty, settingValue, isEqual);
        }

        public LogicSettingAttribute(ItemCategory itemCategory, bool doesContain)
        {
            Condition = CreateConditionFunction(itemCategory, doesContain);
        }

        public LogicSettingAttribute(Item item, bool isRandomized)
        {
            Condition = CreateConditionFunction(item, isRandomized);
        }
    }
}
