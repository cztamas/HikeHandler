namespace HikeHandler.Forms
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
            this.checkPointHandler = new HikeHandler.Forms.CPHandler();
            this.descriptionBox = new TúraKezelő.Controls.HikeDescriptionBox();
            this.groupBox1.SuspendLayout();
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
            this.typeComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.typeComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
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
            this.regionComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.regionComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.regionComboBox.FormattingEnabled = true;
            this.regionComboBox.Location = new System.Drawing.Point(71, 45);
            this.regionComboBox.Name = "regionComboBox";
            this.regionComboBox.Size = new System.Drawing.Size(141, 21);
            this.regionComboBox.TabIndex = 2;
            // 
            // countryComboBox
            // 
            this.countryComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.countryComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
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
            this.addHikeButton.Location = new System.Drawing.Point(238, 250);
            this.addHikeButton.Name = "addHikeButton";
            this.addHikeButton.Size = new System.Drawing.Size(148, 23);
            this.addHikeButton.TabIndex = 1;
            this.addHikeButton.Text = "Túra hozzáadása";
            this.addHikeButton.UseVisualStyleBackColor = true;
            this.addHikeButton.Click += new System.EventHandler(this.addHikeButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(392, 250);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(89, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Mégse";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // checkPointHandler
            // 
            this.checkPointHandler.Location = new System.Drawing.Point(238, 12);
            this.checkPointHandler.Name = "checkPointHandler";
            this.checkPointHandler.RegionID = 0;
            this.checkPointHandler.Size = new System.Drawing.Size(254, 232);
            this.checkPointHandler.TabIndex = 11;
            // 
            // descriptionBox
            // 
            this.descriptionBox.Label = "Leírás";
            this.descriptionBox.Location = new System.Drawing.Point(12, 146);
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.Size = new System.Drawing.Size(220, 133);
            this.descriptionBox.TabIndex = 12;
            this.descriptionBox.Enter += new System.EventHandler(this.descriptionBox_Enter);
            this.descriptionBox.Leave += new System.EventHandler(this.descriptionBox_Leave);
            // 
            // AddHikeForm
            // 
            this.AcceptButton = this.addHikeButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(499, 291);
            this.Controls.Add(this.descriptionBox);
            this.Controls.Add(this.checkPointHandler);
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
        private Forms.CPHandler checkPointHandler;
        private TúraKezelő.Controls.HikeDescriptionBox descriptionBox;
    }
}