using System;

namespace MMR.Randomizer.Attributes
{
    public class OverwritableAttribute : BaseSettingsConditionAttribute
    {
        public OverwritableAttribute()
        {
            Condition = (setting) => true;
        }

        public OverwritableAttribute(string settingProperty, object settingValue)
        {
            Condition = CreateConditionFunction(settingProperty, settingValue);
        }
    }
}
