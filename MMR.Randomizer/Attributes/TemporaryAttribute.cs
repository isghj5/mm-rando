namespace MMR.Randomizer.Attributes
{
    public class TemporaryAttribute : BaseSettingsConditionAttribute
    {
        public TemporaryAttribute()
        {
            Condition = settings => true;
        }

        public TemporaryAttribute(string settingProperty, object settingValue, bool isEqual = true)
        {
            Condition = CreateConditionFunction(settingProperty, settingValue, isEqual);
        }

        public TemporaryAttribute(string settingFlagProperty, int flagValue, bool hasFlag)
        {
            Condition = CreateConditionFunction(settingFlagProperty, flagValue, hasFlag);
        }
    }
}
