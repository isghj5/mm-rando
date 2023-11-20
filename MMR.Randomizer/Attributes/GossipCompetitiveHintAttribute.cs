using MMR.Randomizer.GameObjects;

namespace MMR.Randomizer.Attributes
{
    public class GossipCompetitiveHintAttribute : BaseSettingsConditionAttribute
    {
        public int Priority { get; private set; }

        public GossipCompetitiveHintAttribute(int priority = 0)
        {
            Priority = priority;
        }

        public GossipCompetitiveHintAttribute(int priority, string settingProperty, object settingValue, bool isEqual = true) : this(priority)
        {
            Condition = CreateConditionFunction(settingProperty, settingValue, isEqual);
        }

        public GossipCompetitiveHintAttribute(int priority, ItemCategory itemCategory, bool doesContain) : this(priority)
        {
            Condition = CreateConditionFunction(itemCategory, doesContain);
        }

        public GossipCompetitiveHintAttribute(int priority, ItemCategory itemCategory, bool doesContain, string settingFlagProperty, int flagValue, bool hasFlag) : this(priority)
        {
            var itemCategoryFunc = CreateConditionFunction(itemCategory, doesContain);
            var flagFunc = CreateConditionFunction(settingFlagProperty, flagValue, hasFlag);
            Condition = settings => itemCategoryFunc(settings) && flagFunc(settings);
        }

        public GossipCompetitiveHintAttribute(int priority, ItemCategory itemCategory, bool doesContain, string settingProperty, object settingValue, bool isEqual = true) : this(priority)
        {
            var itemCategoryFunc = CreateConditionFunction(itemCategory, doesContain);
            var settingValueFunc = CreateConditionFunction(settingProperty, settingValue, isEqual);
            Condition = settings => itemCategoryFunc(settings) && settingValueFunc(settings);
        }
    }
}
