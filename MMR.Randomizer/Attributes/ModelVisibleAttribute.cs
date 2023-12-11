namespace MMR.Randomizer.Attributes
{
    public class ModelVisibleAttribute : BaseSettingsConditionAttribute
    {
        public ModelVisibleAttribute()
        {
            Condition = settings => true;
        }

        public ModelVisibleAttribute(string settingProperty, object settingValue, bool isEqual = true)
        {
            Condition = CreateConditionFunction(settingProperty, settingValue, isEqual);
        }

        public ModelVisibleAttribute(string settingFlagProperty, int flagValue, bool hasFlag)
        {
            Condition = CreateConditionFunction(settingFlagProperty, flagValue, hasFlag);
        }
    }
}
