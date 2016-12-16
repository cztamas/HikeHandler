namespace HikeHandler.UI
{
    partial class CPHandler
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.newCPButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.regionComboBox = new System.Windows.Forms.ComboBox();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.cpNameComboBox = new System.Windows.Forms.ComboBox();
            this.removeCPButton = new System.Windows.Forms.Button();
            this.anyOrderCheckBox = new System.Windows.Forms.CheckBox();
            this.otherRegionCheckBox = new System.Windows.Forms.CheckBox();
            this.moveDownButton = new System.Windows.Forms.Button();
            this.moveUpButton = new System.Windows.Forms.Button();
            this.cpGridView = new System.Windows.Forms.DataGridView();
            this.addCPButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cpGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.newCPButton);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.regionComboBox);
            this.groupBox1.Controls.Add(this.RefreshButton);
            this.groupBox1.Controls.Add(this.cpNameComboBox);
            this.groupBox1.Controls.Add(this.removeCPButton);
            this.groupBox1.Controls.Add(this.anyOrderCheckBox);
            this.groupBox1.Controls.Add(this.otherRegionCheckBox);
            this.groupBox1.Controls.Add(this.moveDownButton);
            this.groupBox1.Controls.Add(this.moveUpButton);
            this.groupBox1.Controls.Add(this.cpGridView);
            this.groupBox1.Controls.Add(this.addCPButton);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(299, 237);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CheckPointok";
            // 
            // newCPButton
            // 
            this.newCPButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.newCPButton.Location = new System.Drawing.Point(191, 50);
            this.newCPButton.Name = "newCPButton";
            this.newCPButton.Size = new System.Drawing.Size(102, 23);
            this.newCPButton.TabIndex = 13;
            this.newCPButton.Text = "Új CP rögzítése...";
            this.newCPButton.UseVisualStyleBackColor = true;
            this.newCPButton.Click += new System.EventHandler(this.newCPButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Tájegység:";
            // 
            // regionComboBox
            // 
            this.regionComboBox.Enabled = false;
            this.regionComboBox.FormattingEnabled = true;
            this.regionComboBox.Location = new System.Drawing.Point(71, 50);
            this.regionComboBox.Name = "regionComboBox";
            this.regionComboBox.Size = new System.Drawing.Size(114, 21);
            this.regionComboBox.TabIndex = 11;
            this.regionComboBox.SelectedValueChanged += new System.EventHandler(this.regionComboBox_SelectedValueChanged);
            // 
            // RefreshButton
            // 
            this.RefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RefreshButton.Location = new System.Drawing.Point(191, 203);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(102, 23);
            this.RefreshButton.TabIndex = 10;
            this.RefreshButton.Text = "Frissítés";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // cpNameComboBox
            // 
            this.cpNameComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cpNameComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cpNameComboBox.FormattingEnabled = true;
            this.cpNameComboBox.Location = new System.Drawing.Point(42, 23);
            this.cpNameComboBox.Name = "cpNameComboBox";
            this.cpNameComboBox.Size = new System.Drawing.Size(143, 21);
            this.cpNameComboBox.TabIndex = 9;
            // 
            // removeCPButton
            // 
            this.removeCPButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.removeCPButton.Location = new System.Drawing.Point(191, 160);
            this.removeCPButton.Name = "removeCPButton";
            this.removeCPButton.Size = new System.Drawing.Size(102, 23);
            this.removeCPButton.TabIndex = 8;
            this.removeCPButton.Text = "Kijelölt törlése";
            this.removeCPButton.UseVisualStyleBackColor = true;
            this.removeCPButton.Click += new System.EventHandler(this.removeCPButton_Click);
            // 
            // anyOrderCheckBox
            // 
            this.anyOrderCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.anyOrderCheckBox.AutoSize = true;
            this.anyOrderCheckBox.Checked = true;
            this.anyOrderCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.anyOrderCheckBox.Location = new System.Drawing.Point(167, 79);
            this.anyOrderCheckBox.Name = "anyOrderCheckBox";
            this.anyOrderCheckBox.Size = new System.Drawing.Size(126, 17);
            this.anyOrderCheckBox.TabIndex = 7;
            this.anyOrderCheckBox.Text = "bármilyen sorrendben";
            this.anyOrderCheckBox.UseVisualStyleBackColor = true;
            // 
            // otherRegionCheckBox
            // 
            this.otherRegionCheckBox.AutoSize = true;
            this.otherRegionCheckBox.Location = new System.Drawing.Point(9, 77);
            this.otherRegionCheckBox.Name = "otherRegionCheckBox";
            this.otherRegionCheckBox.Size = new System.Drawing.Size(101, 17);
            this.otherRegionCheckBox.TabIndex = 6;
            this.otherRegionCheckBox.Text = "másik tájegység";
            this.otherRegionCheckBox.UseVisualStyleBackColor = true;
            this.otherRegionCheckBox.CheckedChanged += new System.EventHandler(this.otherRegionCheckBox_CheckedChanged);
            // 
            // moveDownButton
            // 
            this.moveDownButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.moveDownButton.Location = new System.Drawing.Point(191, 131);
            this.moveDownButton.Name = "moveDownButton";
            this.moveDownButton.Size = new System.Drawing.Size(102, 23);
            this.moveDownButton.TabIndex = 5;
            this.moveDownButton.Text = "Lefelé mozgat";
            this.moveDownButton.UseVisualStyleBackColor = true;
            this.moveDownButton.Click += new System.EventHandler(this.moveDownButton_Click);
            // 
            // moveUpButton
            // 
            this.moveUpButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.moveUpButton.Location = new System.Drawing.Point(191, 102);
            this.moveUpButton.Name = "moveUpButton";
            this.moveUpButton.Size = new System.Drawing.Size(102, 23);
            this.moveUpButton.TabIndex = 4;
            this.moveUpButton.Text = "Fölfelé mozgat";
            this.moveUpButton.UseVisualStyleBackColor = true;
            this.moveUpButton.Click += new System.EventHandler(this.moveUpButton_Click);
            // 
            // cpGridView
            // 
            this.cpGridView.AllowUserToAddRows = false;
            this.cpGridView.AllowUserToDeleteRows = false;
            this.cpGridView.AllowUserToResizeColumns = false;
            this.cpGridView.AllowUserToResizeRows = false;
            this.cpGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cpGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.cpGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cpGridView.ColumnHeadersVisible = false;
            this.cpGridView.Location = new System.Drawing.Point(6, 102);
            this.cpGridView.Name = "cpGridView";
            this.cpGridView.ReadOnly = true;
            this.cpGridView.RowHeadersVisible = false;
            this.cpGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.cpGridView.Size = new System.Drawing.Size(179, 124);
            this.cpGridView.TabIndex = 3;
            // 
            // addCPButton
            // 
            this.addCPButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addCPButton.Location = new System.Drawing.Point(191, 21);
            this.addCPButton.Name = "addCPButton";
            this.addCPButton.Size = new System.Drawing.Size(102, 23);
            this.addCPButton.TabIndex = 2;
            this.addCPButton.Text = "Hozzáadás";
            this.addCPButton.UseVisualStyleBackColor = true;
            this.addCPButton.Click += new System.EventHandler(this.addCPButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Név:";
            // 
            // CPHandler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "CPHandler";
            this.Size = new System.Drawing.Size(299, 237);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cpGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button addCPButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox anyOrderCheckBox;
        private System.Windows.Forms.CheckBox otherRegionCheckBox;
        private System.Windows.Forms.Button moveDownButton;
        private System.Windows.Forms.Button moveUpButton;
        private System.Windows.Forms.DataGridView cpGridView;
        private System.Windows.Forms.Button removeCPButton;
        private System.Windows.Forms.ComboBox cpNameComboBox;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox regionComboBox;
        private System.Windows.Forms.Button newCPButton;
    }
}
