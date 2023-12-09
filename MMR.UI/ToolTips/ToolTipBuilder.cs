﻿using System.Windows.Forms;

namespace MMR.UI.Forms.Tooltips
{
    public static class TooltipBuilder
    {
        public static void SetTooltip(Control control, string text)
        {
            var tooltip = new ToolTip();

            // Set up the delays for the ToolTip.
            tooltip.InitialDelay = 1000;
            tooltip.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            tooltip.ShowAlways = true;
            tooltip.AutoPopDelay = 30000;

            tooltip.SetToolTip(control, text);
        }
    }
}
