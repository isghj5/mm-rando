using MMR.Common.Extensions;
using MMR.Randomizer.Attributes.Setting;
using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Models;
using MMR.Randomizer.Models.Settings;
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
    public partial class RandomStartingItemsForm : Form
    {
        public List<RandomStartingItemGroup> Result { get; private set; }

        public RandomStartingItemsForm(IEnumerable<RandomStartingItemGroup> randomStartingItemGroups)
        {
            InitializeComponent();

            tRandomStartingItems.MaximumSize = new Size(panel1.Width - SystemInformation.VerticalScrollBarWidth, 0);

            Result = randomStartingItemGroups?.ToList() ?? new List<RandomStartingItemGroup>();

            if (Result != null)
            {
                for (var i = 0; i < Result.Count; i++)
                {
                    var list = Result[i];
                    AddItems(list);
                }
            }
        }

        private void AddItems(RandomStartingItemGroup group)
        {
            var i = tRandomStartingItems.RowCount - 1;
            tRandomStartingItems.RowCount++;

            foreach (Control c in tRandomStartingItems.Controls)
            {
                if (tRandomStartingItems.GetRow(c) == i)
                {
                    tRandomStartingItems.SetRow(c, i + 1);
                }
            }

            var editButton = new Button
            {
                Text = "+",
            };
            editButton.Click += editButton_Click;
            tRandomStartingItems.Controls.Add(editButton, 0, i);

            var deleteButton = new Button
            {
                Text = "-",
            };
            deleteButton.Click += deleteButton_Click;
            tRandomStartingItems.Controls.Add(deleteButton, 1, i);

            var label = new Label
            {
                Text = string.Join(", ", group.Items.Select(item => item.Name())),
                AutoSize = true,
                Margin = new Padding(10, 10, 10, 10),
            };
            tRandomStartingItems.Controls.Add(label, 2, i);

            var amountInput = new TextBox
            {
                Text = group.Amount.ToString(),
                AutoSize = true,
            };
            amountInput.TextChanged += textAmount_TextChanged;
            tRandomStartingItems.Controls.Add(amountInput, 3, i);
            RestrictAmount(amountInput);
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            var control = (Control)sender;
            var index = tRandomStartingItems.GetRow(control);

            var settingItemListAttribute = typeof(RandomStartingItemGroup).GetProperty(nameof(RandomStartingItemGroup.Items)).GetAttribute<SettingItemListAttribute>();
            var form = new ItemSelectorForm(settingItemListAttribute.ItemList, Result[index].Items, settingItemListAttribute.LabelExtractor);
            form.ShowDialog();

            if (form.DialogResult == DialogResult.OK)
            {
                if (form.ReturnItems.Count > 0)
                {
                    Result[index].Items = form.ReturnItems;
                    var label = (Label)tRandomStartingItems.GetControlFromPosition(2, index);
                    label.Text = string.Join(", ", Result[index].Items.Select(item => item.Name()));
                }
                else
                {
                    DeleteRow(index);
                }
            }
        }

        private void DeleteRow(int index)
        {
            Result.RemoveAt(index);

            tRandomStartingItems.SuspendLayout();

            for (var x = 0; x < tRandomStartingItems.ColumnCount; x++)
            {
                var c = tRandomStartingItems.GetControlFromPosition(x, index);
                tRandomStartingItems.Controls.Remove(c);
            }

            for (var y = index + 1; y < tRandomStartingItems.RowCount; y++)
            {
                for (var x = 0; x < tRandomStartingItems.ColumnCount; x++)
                {
                    var c = tRandomStartingItems.GetControlFromPosition(x, y);
                    if (c != null)
                    {
                        tRandomStartingItems.SetRow(c, y - 1);
                    }
                }
            }

            tRandomStartingItems.RowCount--;

            tRandomStartingItems.ResumeLayout();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete this row?", "Confirm", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                var control = (Control)sender;
                var index = tRandomStartingItems.GetRow(control);

                DeleteRow(index);
            }
        }

        private void textAmount_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            var index = tRandomStartingItems.GetRow(textBox);
            if (int.TryParse(textBox.Text, out var amount))
            {
                Result[index].Amount = amount;
            }
            else
            {
                Result[index].Amount = 1;
            }
            RestrictAmount(textBox);
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            if (Result?.Count == 0)
            {
                Result = null;
            }
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void bAddLevel_Click(object sender, EventArgs e)
        {
            var settingItemListAttribute = typeof(RandomStartingItemGroup).GetProperty(nameof(RandomStartingItemGroup.Items)).GetAttribute<SettingItemListAttribute>();
            var form = new ItemSelectorForm(settingItemListAttribute.ItemList, Enumerable.Empty<Item>(), settingItemListAttribute.LabelExtractor);
            form.ShowDialog();

            if (form.DialogResult == DialogResult.OK && form.ReturnItems.Count > 0)
            {
                var group = new RandomStartingItemGroup
                {
                    Items = form.ReturnItems,
                    Amount = 1
                };
                Result.Add(group);

                tRandomStartingItems.SuspendLayout();
                AddItems(group);
                tRandomStartingItems.ResumeLayout();
            }
        }

        private void RestrictAmount(TextBox textAmount)
        {
            var row = tRandomStartingItems.GetRow(textAmount);
            var amount = Result[row].Amount;
            if (amount < 1)
            {
                amount = 1;
            }
            else if (amount > Result[row].Items.Count)
            {
                amount = Result[row].Items.Count;
            }
            if (amount != Result[row].Amount)
            {
                textAmount.Text = amount.ToString();
            }
        }
    }
}
