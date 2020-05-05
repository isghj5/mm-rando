using MMR.Randomizer.Models;
using MMR.Randomizer.Utils;
using MMR.UI.Forms.Tooltips;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MMR.UI.Forms
{
    public partial class ToggleTricksForm : Form
    {
        public List<int> Result { get; private set; }

        public ToggleTricksForm(LogicMode logicMode, string userLogicFilename, IEnumerable<int> tricksEnabled)
        {
            InitializeComponent();
            Result = tricksEnabled.ToList();

            var lines = LogicUtils.ReadRulesetFromResources(logicMode, userLogicFilename);
            var itemList = LogicUtils.PopulateItemListFromLogicData(lines);

            var y = 9;
            var deltaY = 23;
            var tricks = itemList.Where(io => io.IsTrick);
            foreach (var itemObject in tricks.OrderBy(io => io.Name))
            {
                var cTrick = new CheckBox();
                cTrick.Tag = itemObject;
                cTrick.Checked = tricksEnabled.Contains(itemObject.ID);
                cTrick.Text = itemObject.Name;
                TooltipBuilder.SetTooltip(cTrick, itemObject.TrickTooltip);
                cTrick.Location = new Point(9, y);
                cTrick.Size = new Size(pTricks.Width - 50, deltaY);
                cTrick.CheckedChanged += cTrick_CheckedChanged;
                pTricks.Controls.Add(cTrick);
                y += deltaY;
            }
        }

        private void cTrick_CheckedChanged(object sender, EventArgs e)
        {
            var checkbox = (CheckBox)sender;
            var itemObject = (ItemObject)checkbox.Tag;
            if (checkbox.Checked)
            {
                Result.Add(itemObject.ID);
            }
            else
            {
                Result.Remove(itemObject.ID);
            }
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
