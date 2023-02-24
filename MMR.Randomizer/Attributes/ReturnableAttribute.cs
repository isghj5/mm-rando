namespace MMR.Randomizer.Attributes
{
    public class ReturnableAttribute : BaseSettingsConditionAttribute
    {
        public ReturnableAttribute(string settingFlagProperty, int flagValue, bool hasFlag)
        {
            Condition = CreateConditionFunction(settingFlagProperty, flagValue, hasFlag);
        }
    }
}
