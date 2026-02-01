using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Extensions;

namespace MMR.UI.Forms
{
    public partial class ItemSelectorForm : Form
    {
        public List<Item> ReturnItems;

        private IEnumerable<Item> _baseItemList;

        private Func<Item, string> _labelExtractor;

        public ItemSelectorForm(IEnumerable<Item> baseItemList, IEnumerable<Item> selectedItems, Func<Item, string> labelExtractor)
        {
            InitializeComponent();
            _baseItemList = baseItemList;
            _labelExtractor = labelExtractor;
            ReturnItems = selectedItems.ToList();
            UpdateItems();
            this.ActiveControl = textBoxFilter;
            lItems.CheckBoxes = true;
        }

        public void UpdateItems(string filter = null)
        {
            lItems.Clear();
            var items = _baseItemList;
            if (!string.IsNullOrWhiteSpace(filter))
            {
                items = items.Where(item => _labelExtractor(item).ToLower().Contains(filter));
            }
            foreach (var item in items)
            {
                var label = _labelExtractor(item);
                var listViewItem = new ListViewItem(label);
                listViewItem.Tag = item;
                listViewItem.Checked = ReturnItems.Contains(item);
                lItems.Items.Add(listViewItem);
            }
        }

        private void bDone_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void lItems_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lItems.CheckBoxes || lItems.SelectedItems.Count == 0)
            {
                return;
            }
            ;
            ReturnItems = new List<Item> { (Item)lItems.SelectedItems[0].Tag };
            DialogResult = DialogResult.OK;
            Close();
        }

        private void lItems_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                ReturnItems.Add((Item)e.Item.Tag);
            }
            else
            {
                ReturnItems.Remove((Item)e.Item.Tag);
            }
        }

        private void textBoxFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                var filter = textBoxFilter.Text.ToLower();
                //_filteredItems = _logic.Logic.Where(item => GetLabel(item.Id).ToLower().Contains(filter)).Select(item => item.Id).ToList();
                UpdateItems(filter);
            }
        }
    }
}
