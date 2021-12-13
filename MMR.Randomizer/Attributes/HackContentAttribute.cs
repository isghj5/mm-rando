using System;
using System.Reflection;

namespace MMR.Randomizer.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class HackContentAttribute : Attribute
    {
        public byte[] HackContent { get; }
        public bool ApplyOnlyIfItemIsDifferent { get; }

        public HackContentAttribute(string modResourcePropertyName, bool applyOnlyIfItemIsDifferent = true)
        {
            ApplyOnlyIfItemIsDifferent = applyOnlyIfItemIsDifferent;
            if (modResourcePropertyName != null)
            {
                HackContent = (byte[])typeof(Resources.mods).GetProperty(modResourcePropertyName, BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
            }
        }
    }
}
