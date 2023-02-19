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

        public GossipCompetitiveHintAttribute(int priority, string settingProperty, object settingValue) : this(priority)
        {
            Condition = CreateConditionFunction(settingProperty, settingValue);
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

        public GossipCompetitiveHintAttribute(int priority, ItemCategory itemCategory, bool doesContain, string settingProperty, object settingValue) : this(priority)
        {
            var itemCategoryFunc = CreateConditionFunction(itemCategory, doesContain);
            var settingValueFunc = CreateConditionFunction(settingProperty, settingValue);
            Condition = settings => itemCategoryFunc(settings) && settingValueFunc(settings);
        }
    }
}
