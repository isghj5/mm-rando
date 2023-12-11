using System;

namespace MMR.Randomizer.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class TextVisibleAttribute : BaseSettingsConditionAttribute
    {
        public TextVisibleAttribute()
        {
            Condition = settings => true;
        }

        public TextVisibleAttribute(string settingProperty, object settingValue, bool isEqual = true)
        {
            Condition = CreateConditionFunction(settingProperty, settingValue, isEqual);
        }

        public TextVisibleAttribute(string settingFlagProperty, int flagValue, bool hasFlag)
        {
            Condition = CreateConditionFunction(settingFlagProperty, flagValue, hasFlag);
        }
    }
}
