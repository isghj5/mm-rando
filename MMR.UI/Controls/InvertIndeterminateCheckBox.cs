using System;
using System.Windows.Forms;

namespace MMR.UI.Controls
{
    internal class InvertIndeterminateCheckBox : CheckBox
    {
        protected override void OnClick(EventArgs e)
        {
            CheckState = CheckState switch
            {
                CheckState.Checked => CheckState.Unchecked,
                CheckState.Unchecked => CheckState.Checked,
                CheckState.Indeterminate => CheckState.Checked,
                _ => CheckState,
            };
        }
    }
}
