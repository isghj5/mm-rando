namespace MMR.Randomizer.Attributes
{
    public class TemporaryAttribute : BaseSettingsConditionAttribute
    {
        public TemporaryAttribute()
        {
            Condition = settings => true;
        }

        public TemporaryAttribute(string settingProperty, object settingValue)
        {
            Condition = CreateConditionFunction(settingProperty, settingValue);
        }

        public TemporaryAttribute(string settingFlagProperty, int flagValue, bool hasFlag)
        {
            Condition = CreateConditionFunction(settingFlagProperty, flagValue, hasFlag);
        }
    }
}
