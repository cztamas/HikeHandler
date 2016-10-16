namespace HikeHandler
{
    partial class AddHikeForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.dateBox = new System.Windows.Forms.DateTimePicker();
            this.regionComboBox = new System.Windows.Forms.ComboBox();
            this.countryComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.addHikeButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.descriptionBox = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.refreshButton = new System.Windows.Forms.Button();
            this.cpGridView = new System.Windows.Forms.DataGridView();
            this.allRegionCheckBox = new System.Windows.Forms.CheckBox();
            this.moveDownButton = new System.Windows.Forms.Button();
            this.moveUpButton = new System.Windows.Forms.Button();
            this.cpNameComboBox = new System.Windows.Forms.ComboBox();
            this.removeCPButton = new System.Windows.Forms.Button();
            this.addCPButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cpGridView)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.typeComboBox);
            this.groupBox1.Controls.Add(this.dateBox);
            this.groupBox1.Controls.Add(this.regionComboBox);
            this.groupBox1.Controls.Add(this.countryComboBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 128);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Alapadatok";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Dátum:";
            // 
            // typeComboBox
            // 
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Location = new System.Drawing.Point(71, 72);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(141, 21);
            this.typeComboBox.TabIndex = 3;
            // 
            // dateBox
            // 
            this.dateBox.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateBox.Location = new System.Drawing.Point(71, 99);
            this.dateBox.Name = "dateBox";
            this.dateBox.Size = new System.Drawing.Size(141, 20);
            this.dateBox.TabIndex = 4;
            // 
            // regionComboBox
            // 
            this.regionComboBox.FormattingEnabled = true;
            this.regionComboBox.Location = new System.Drawing.Point(71, 45);
            this.regionComboBox.Name = "regionComboBox";
            this.regionComboBox.Size = new System.Drawing.Size(141, 21);
            this.regionComboBox.TabIndex = 2;
            this.regionComboBox.SelectedValueChanged += new System.EventHandler(this.regionComboBox_SelectedValueChanged);
            // 
            // countryComboBox
            // 
            this.countryComboBox.FormattingEnabled = true;
            this.countryComboBox.Location = new System.Drawing.Point(71, 18);
            this.countryComboBox.Name = "countryComboBox";
            this.countryComboBox.Size = new System.Drawing.Size(141, 21);
            this.countryComboBox.TabIndex = 1;
            this.countryComboBox.SelectedValueChanged += new System.EventHandler(this.countryComboBox_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Típus:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tájegység:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ország:";
            // 
            // addHikeButton
            // 
            this.addHikeButton.Location = new System.Drawing.Point(238, 221);
            this.addHikeButton.Name = "addHikeButton";
            this.addHikeButton.Size = new System.Drawing.Size(171, 23);
            this.addHikeButton.TabIndex = 1;
            this.addHikeButton.Text = "Túra hozzáadása";
            this.addHikeButton.UseVisualStyleBackColor = true;
            this.addHikeButton.Click += new System.EventHandler(this.addHikeButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(415, 221);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(105, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Mégse";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // descriptionBox
            // 
            this.descriptionBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descriptionBox.Location = new System.Drawing.Point(3, 16);
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.Size = new System.Drawing.Size(214, 79);
            this.descriptionBox.TabIndex = 5;
            this.descriptionBox.Text = "";
            this.descriptionBox.Enter += new System.EventHandler(this.descriptionBox_Enter);
            this.descriptionBox.Leave += new System.EventHandler(this.descriptionBox_Leave);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.refreshButton);
            this.groupBox2.Controls.Add(this.cpGridView);
            this.groupBox2.Controls.Add(this.allRegionCheckBox);
            this.groupBox2.Controls.Add(this.moveDownButton);
            this.groupBox2.Controls.Add(this.moveUpButton);
            this.groupBox2.Controls.Add(this.cpNameComboBox);
            this.groupBox2.Controls.Add(this.removeCPButton);
            this.groupBox2.Controls.Add(this.addCPButton);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(238, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(288, 203);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CheckPointok";
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(177, 174);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(105, 23);
            this.refreshButton.TabIndex = 11;
            this.refreshButton.Text = "Frissítés";
            this.refreshButton.UseVisualStyleBackColor = true;
            // 
            // cpGridView
            // 
            this.cpGridView.AllowUserToAddRows = false;
            this.cpGridView.AllowUserToDeleteRows = false;
            this.cpGridView.AllowUserToResizeRows = false;
            this.cpGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cpGridView.Location = new System.Drawing.Point(6, 68);
            this.cpGridView.Name = "cpGridView";
            this.cpGridView.ReadOnly = true;
            this.cpGridView.RowHeadersVisible = false;
            this.cpGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.cpGridView.Size = new System.Drawing.Size(165, 129);
            this.cpGridView.TabIndex = 10;
            // 
            // allRegionCheckBox
            // 
            this.allRegionCheckBox.AutoSize = true;
            this.allRegionCheckBox.Location = new System.Drawing.Point(42, 45);
            this.allRegionCheckBox.Name = "allRegionCheckBox";
            this.allRegionCheckBox.Size = new System.Drawing.Size(115, 17);
            this.allRegionCheckBox.TabIndex = 9;
            this.allRegionCheckBox.Text = "más tájegységek is";
            this.allRegionCheckBox.UseVisualStyleBackColor = true;
            this.allRegionCheckBox.CheckedChanged += new System.EventHandler(this.allRegionCheckBox_CheckedChanged);
            // 
            // moveDownButton
            // 
            this.moveDownButton.Location = new System.Drawing.Point(177, 97);
            this.moveDownButton.Name = "moveDownButton";
            this.moveDownButton.Size = new System.Drawing.Size(105, 23);
            this.moveDownButton.TabIndex = 8;
            this.moveDownButton.Text = "Lefelé mozgat";
            this.moveDownButton.UseVisualStyleBackColor = true;
            this.moveDownButton.Click += new System.EventHandler(this.moveDownButton_Click);
            // 
            // moveUpButton
            // 
            this.moveUpButton.Location = new System.Drawing.Point(177, 68);
            this.moveUpButton.Name = "moveUpButton";
            this.moveUpButton.Size = new System.Drawing.Size(105, 23);
            this.moveUpButton.TabIndex = 7;
            this.moveUpButton.Text = "Fölfelé mozgat";
            this.moveUpButton.UseVisualStyleBackColor = true;
            this.moveUpButton.Click += new System.EventHandler(this.moveUpButton_Click);
            // 
            // cpNameComboBox
            // 
            this.cpNameComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cpNameComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cpNameComboBox.FormattingEnabled = true;
            this.cpNameComboBox.Location = new System.Drawing.Point(42, 18);
            this.cpNameComboBox.MaxDropDownItems = 6;
            this.cpNameComboBox.Name = "cpNameComboBox";
            this.cpNameComboBox.Size = new System.Drawing.Size(129, 21);
            this.cpNameComboBox.TabIndex = 5;
            // 
            // removeCPButton
            // 
            this.removeCPButton.Location = new System.Drawing.Point(177, 126);
            this.removeCPButton.Name = "removeCPButton";
            this.removeCPButton.Size = new System.Drawing.Size(105, 23);
            this.removeCPButton.TabIndex = 4;
            this.removeCPButton.Text = "Kijelölt törlése";
            this.removeCPButton.UseVisualStyleBackColor = true;
            this.removeCPButton.Click += new System.EventHandler(this.removeCPButton_Click);
            // 
            // addCPButton
            // 
            this.addCPButton.Location = new System.Drawing.Point(177, 16);
            this.addCPButton.Name = "addCPButton";
            this.addCPButton.Size = new System.Drawing.Size(105, 23);
            this.addCPButton.TabIndex = 3;
            this.addCPButton.Text = "CP hozzáadása";
            this.addCPButton.UseVisualStyleBackColor = true;
            this.addCPButton.Click += new System.EventHandler(this.addCPButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Név:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.descriptionBox);
            this.groupBox3.Location = new System.Drawing.Point(12, 146);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(220, 98);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Leírás";
            // 
            // AddHikeForm
            // 
            this.AcceptButton = this.addHikeButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(533, 249);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.addHikeButton);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AddHikeForm";
            this.ShowIcon = false;
            this.Text = "Túra hozzáadása";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cpGridView)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.DateTimePicker dateBox;
        private System.Windows.Forms.ComboBox regionComboBox;
        private System.Windows.Forms.ComboBox countryComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button addHikeButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.RichTextBox descriptionBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cpNameComboBox;
        private System.Windows.Forms.Button removeCPButton;
        private System.Windows.Forms.Button addCPButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button moveDownButton;
        private System.Windows.Forms.Button moveUpButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox allRegionCheckBox;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.DataGridView cpGridView;
    }
}