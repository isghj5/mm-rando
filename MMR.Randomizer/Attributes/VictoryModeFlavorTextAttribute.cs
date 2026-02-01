using System;

namespace MMR.Randomizer.Attributes
{
    public class VictoryModeFlavorTextAttribute : Attribute
    {
        public string Text { get; }

        public VictoryModeFlavorTextAttribute(string text)
        {
            Text = text;
        }
    }
}
