﻿namespace MMR.UI.Forms
{
    partial class ItemEditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemEditForm));
            this.lItems = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tSetting = new System.Windows.Forms.TextBox();
            this.tLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // lItems
            // 
            this.lItems.CheckBoxes = true;
            this.lItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lItems.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lItems.Location = new System.Drawing.Point(3, 3);
            this.lItems.Name = "lItems";
            this.lItems.ShowItemToolTips = true;
            this.lItems.Size = new System.Drawing.Size(1076, 469);
            this.lItems.TabIndex = 1;
            this.lItems.UseCompatibleStateImageBehavior = false;
            this.lItems.View = System.Windows.Forms.View.List;
            this.lItems.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lItems_ItemChecked);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 160;
            // 
            // tSetting
            // 
            this.tSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tSetting.Location = new System.Drawing.Point(3, 478);
            this.tSetting.Name = "tSetting";
            this.tSetting.Size = new System.Drawing.Size(1076, 20);
            this.tSetting.TabIndex = 2;
            this.tSetting.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tSetting_KeyDown);
            // 
            // tLayout
            // 
            this.tLayout.ColumnCount = 1;
            this.tLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tLayout.Controls.Add(this.lItems, 0, 0);
            this.tLayout.Controls.Add(this.tSetting, 0, 1);
            this.tLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tLayout.Location = new System.Drawing.Point(0, 0);
            this.tLayout.Name = "tLayout";
            this.tLayout.RowCount = 2;
            this.tLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tLayout.Size = new System.Drawing.Size(1082, 503);
            this.tLayout.TabIndex = 3;
            // 
            // ItemEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 503);
            this.Controls.Add(this.tLayout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ItemEditForm";
            this.Text = "Item List Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fItemEdit_FormClosing);
            this.tLayout.ResumeLayout(false);
            this.tLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lItems;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.TextBox tSetting;
        private System.Windows.Forms.TableLayoutPanel tLayout;
    }
}