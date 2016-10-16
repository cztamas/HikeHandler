namespace HikeHandler.Forms
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
            this.RefreshButton = new System.Windows.Forms.Button();
            this.cpNameComboBox = new System.Windows.Forms.ComboBox();
            this.removeCPButton = new System.Windows.Forms.Button();
            this.anyOrderCheckBox = new System.Windows.Forms.CheckBox();
            this.allRegionCheckBox = new System.Windows.Forms.CheckBox();
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
            this.groupBox1.Controls.Add(this.RefreshButton);
            this.groupBox1.Controls.Add(this.cpNameComboBox);
            this.groupBox1.Controls.Add(this.removeCPButton);
            this.groupBox1.Controls.Add(this.anyOrderCheckBox);
            this.groupBox1.Controls.Add(this.allRegionCheckBox);
            this.groupBox1.Controls.Add(this.moveDownButton);
            this.groupBox1.Controls.Add(this.moveUpButton);
            this.groupBox1.Controls.Add(this.cpGridView);
            this.groupBox1.Controls.Add(this.addCPButton);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(254, 206);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CheckPointok";
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(154, 174);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(90, 23);
            this.RefreshButton.TabIndex = 10;
            this.RefreshButton.Text = "Frissítés";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // cpNameComboBox
            // 
            this.cpNameComboBox.FormattingEnabled = true;
            this.cpNameComboBox.Location = new System.Drawing.Point(42, 23);
            this.cpNameComboBox.Name = "cpNameComboBox";
            this.cpNameComboBox.Size = new System.Drawing.Size(106, 21);
            this.cpNameComboBox.TabIndex = 9;
            // 
            // removeCPButton
            // 
            this.removeCPButton.Location = new System.Drawing.Point(154, 131);
            this.removeCPButton.Name = "removeCPButton";
            this.removeCPButton.Size = new System.Drawing.Size(90, 23);
            this.removeCPButton.TabIndex = 8;
            this.removeCPButton.Text = "Kijelölt törlése";
            this.removeCPButton.UseVisualStyleBackColor = true;
            this.removeCPButton.Click += new System.EventHandler(this.removeCPButton_Click);
            // 
            // anyOrderCheckBox
            // 
            this.anyOrderCheckBox.AutoSize = true;
            this.anyOrderCheckBox.Checked = true;
            this.anyOrderCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.anyOrderCheckBox.Location = new System.Drawing.Point(120, 50);
            this.anyOrderCheckBox.Name = "anyOrderCheckBox";
            this.anyOrderCheckBox.Size = new System.Drawing.Size(126, 17);
            this.anyOrderCheckBox.TabIndex = 7;
            this.anyOrderCheckBox.Text = "bármilyen sorrendben";
            this.anyOrderCheckBox.UseVisualStyleBackColor = true;
            // 
            // allRegionCheckBox
            // 
            this.allRegionCheckBox.AutoSize = true;
            this.allRegionCheckBox.Location = new System.Drawing.Point(6, 50);
            this.allRegionCheckBox.Name = "allRegionCheckBox";
            this.allRegionCheckBox.Size = new System.Drawing.Size(108, 17);
            this.allRegionCheckBox.TabIndex = 6;
            this.allRegionCheckBox.Text = "minden tájegység";
            this.allRegionCheckBox.UseVisualStyleBackColor = true;
            this.allRegionCheckBox.CheckedChanged += new System.EventHandler(this.allRegionCheckBox_CheckedChanged);
            // 
            // moveDownButton
            // 
            this.moveDownButton.Location = new System.Drawing.Point(154, 102);
            this.moveDownButton.Name = "moveDownButton";
            this.moveDownButton.Size = new System.Drawing.Size(90, 23);
            this.moveDownButton.TabIndex = 5;
            this.moveDownButton.Text = "Lefelé mozgat";
            this.moveDownButton.UseVisualStyleBackColor = true;
            this.moveDownButton.Click += new System.EventHandler(this.moveDownButton_Click);
            // 
            // moveUpButton
            // 
            this.moveUpButton.Location = new System.Drawing.Point(154, 73);
            this.moveUpButton.Name = "moveUpButton";
            this.moveUpButton.Size = new System.Drawing.Size(90, 23);
            this.moveUpButton.TabIndex = 4;
            this.moveUpButton.Text = "Fölfelé mozgat";
            this.moveUpButton.UseVisualStyleBackColor = true;
            this.moveUpButton.Click += new System.EventHandler(this.moveUpButton_Click);
            // 
            // cpGridView
            // 
            this.cpGridView.AllowUserToAddRows = false;
            this.cpGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cpGridView.Location = new System.Drawing.Point(6, 73);
            this.cpGridView.Name = "cpGridView";
            this.cpGridView.ReadOnly = true;
            this.cpGridView.Size = new System.Drawing.Size(142, 124);
            this.cpGridView.TabIndex = 3;
            // 
            // addCPButton
            // 
            this.addCPButton.Location = new System.Drawing.Point(154, 21);
            this.addCPButton.Name = "addCPButton";
            this.addCPButton.Size = new System.Drawing.Size(90, 23);
            this.addCPButton.TabIndex = 2;
            this.addCPButton.Text = "CP hozzáadása";
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
            this.Size = new System.Drawing.Size(254, 206);
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
        private System.Windows.Forms.CheckBox allRegionCheckBox;
        private System.Windows.Forms.Button moveDownButton;
        private System.Windows.Forms.Button moveUpButton;
        private System.Windows.Forms.DataGridView cpGridView;
        private System.Windows.Forms.Button removeCPButton;
        private System.Windows.Forms.ComboBox cpNameComboBox;
        private System.Windows.Forms.Button RefreshButton;
    }
}
