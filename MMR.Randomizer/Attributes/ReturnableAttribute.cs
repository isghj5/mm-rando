namespace MMR.Randomizer.Attributes
{
    public class ReturnableAttribute : BaseSettingsConditionAttribute
    {
        public ReturnableAttribute(string settingFlagProperty, int flagValue, bool hasFlag)
        {
            Condition = CreateConditionFunction(settingFlagProperty, flagValue, hasFlag);
        }

        public ReturnableAttribute(string settingProperty, object settingValue, bool isEqual = true)
        {
            Condition = CreateConditionFunction(settingProperty, settingValue, isEqual);
        }
    }
}
