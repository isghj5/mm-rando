namespace MMR.UI.Forms
{
    partial class CustomizeHintPrioritiesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomizeHintPrioritiesForm));
            this.label1 = new System.Windows.Forms.Label();
            this.bOK = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.tHintPriorities = new System.Windows.Forms.TableLayoutPanel();
            this.bAddLevel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tHintPriorities.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(429, 127);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // bOK
            // 
            this.bOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.bOK.Location = new System.Drawing.Point(46, 554);
            this.bOK.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(197, 27);
            this.bOK.TabIndex = 2;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bCancel
            // 
            this.bCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.bCancel.Location = new System.Drawing.Point(250, 554);
            this.bCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(197, 27);
            this.bCancel.TabIndex = 3;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // tHintPriorities
            // 
            this.tHintPriorities.AutoSize = true;
            this.tHintPriorities.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tHintPriorities.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tHintPriorities.ColumnCount = 7;
            this.tHintPriorities.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tHintPriorities.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tHintPriorities.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tHintPriorities.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tHintPriorities.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tHintPriorities.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tHintPriorities.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tHintPriorities.Controls.Add(this.bAddLevel, 3, 0);
            this.tHintPriorities.Location = new System.Drawing.Point(0, 0);
            this.tHintPriorities.Name = "tHintPriorities";
            this.tHintPriorities.RowCount = 1;
            this.tHintPriorities.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tHintPriorities.Size = new System.Drawing.Size(459, 31);
            this.tHintPriorities.TabIndex = 4;
            // 
            // bAddLevel
            // 
            this.bAddLevel.Location = new System.Drawing.Point(97, 4);
            this.bAddLevel.Name = "bAddLevel";
            this.bAddLevel.Size = new System.Drawing.Size(257, 23);
            this.bAddLevel.TabIndex = 1;
            this.bAddLevel.Text = "+";
            this.bAddLevel.UseVisualStyleBackColor = true;
            this.bAddLevel.Click += new System.EventHandler(this.bAddLevel_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.tHintPriorities);
            this.panel1.Location = new System.Drawing.Point(15, 145);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(461, 403);
            this.panel1.TabIndex = 5;
            // 
            // CustomizeHintPrioritiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 594);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.label1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "CustomizeHintPrioritiesForm";
            this.Text = "Customize Hint Priorities";
            this.tHintPriorities.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.TableLayoutPanel tHintPriorities;
        private System.Windows.Forms.Button bAddLevel;
        private System.Windows.Forms.Panel panel1;
    }
}
