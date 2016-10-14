namespace HikeHandler.Forms
{
    partial class ViewHikeForm
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
            this.positionBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.regionBox = new System.Windows.Forms.TextBox();
            this.countryBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.dateBox = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.allRegionCheckBox = new System.Windows.Forms.CheckBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.cpNameComboBox = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.descriptionBox = new System.Windows.Forms.RichTextBox();
            this.editButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.saveEditButton = new System.Windows.Forms.Button();
            this.cancelEditButton = new System.Windows.Forms.Button();
            this.deleteHikeButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.positionBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.regionBox);
            this.groupBox1.Controls.Add(this.countryBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.typeComboBox);
            this.groupBox1.Controls.Add(this.dateBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(242, 128);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Alapadatok";
            // 
            // positionBox
            // 
            this.positionBox.Enabled = false;
            this.positionBox.Location = new System.Drawing.Point(183, 71);
            this.positionBox.Name = "positionBox";
            this.positionBox.Size = new System.Drawing.Size(48, 20);
            this.positionBox.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(127, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Sorszám:";
            // 
            // regionBox
            // 
            this.regionBox.Enabled = false;
            this.regionBox.Location = new System.Drawing.Point(71, 44);
            this.regionBox.Name = "regionBox";
            this.regionBox.Size = new System.Drawing.Size(160, 20);
            this.regionBox.TabIndex = 9;
            // 
            // countryBox
            // 
            this.countryBox.Enabled = false;
            this.countryBox.Location = new System.Drawing.Point(71, 18);
            this.countryBox.Name = "countryBox";
            this.countryBox.Size = new System.Drawing.Size(160, 20);
            this.countryBox.TabIndex = 8;
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
            this.typeComboBox.Location = new System.Drawing.Point(71, 70);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(50, 21);
            this.typeComboBox.TabIndex = 6;
            // 
            // dateBox
            // 
            this.dateBox.Enabled = false;
            this.dateBox.Location = new System.Drawing.Point(71, 97);
            this.dateBox.Name = "dateBox";
            this.dateBox.Size = new System.Drawing.Size(160, 20);
            this.dateBox.TabIndex = 5;
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.allRegionCheckBox);
            this.groupBox2.Controls.Add(this.button6);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.cpNameComboBox);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.listView1);
            this.groupBox2.Location = new System.Drawing.Point(260, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(334, 228);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CheckPointok";
            // 
            // allRegionCheckBox
            // 
            this.allRegionCheckBox.AutoSize = true;
            this.allRegionCheckBox.Location = new System.Drawing.Point(42, 46);
            this.allRegionCheckBox.Name = "allRegionCheckBox";
            this.allRegionCheckBox.Size = new System.Drawing.Size(115, 17);
            this.allRegionCheckBox.TabIndex = 9;
            this.allRegionCheckBox.Text = "más tájegységek is";
            this.allRegionCheckBox.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(220, 119);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(105, 23);
            this.button6.TabIndex = 8;
            this.button6.Text = "Lefelé mozgat";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(220, 90);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(105, 23);
            this.button5.TabIndex = 7;
            this.button5.Text = "Fölfelé mozgat";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // cpNameComboBox
            // 
            this.cpNameComboBox.FormattingEnabled = true;
            this.cpNameComboBox.Location = new System.Drawing.Point(42, 18);
            this.cpNameComboBox.Name = "cpNameComboBox";
            this.cpNameComboBox.Size = new System.Drawing.Size(172, 21);
            this.cpNameComboBox.TabIndex = 5;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(220, 148);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(105, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "Kijelölt törlése";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(220, 18);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(105, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "CP hozzáadása";
            this.button3.UseVisualStyleBackColor = true;
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
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(9, 76);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(205, 146);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.descriptionBox);
            this.groupBox3.Location = new System.Drawing.Point(12, 146);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(242, 123);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Leírás";
            // 
            // descriptionBox
            // 
            this.descriptionBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descriptionBox.Location = new System.Drawing.Point(3, 16);
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.Size = new System.Drawing.Size(236, 104);
            this.descriptionBox.TabIndex = 9;
            this.descriptionBox.Text = "";
            // 
            // editButton
            // 
            this.editButton.Location = new System.Drawing.Point(260, 246);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(165, 23);
            this.editButton.TabIndex = 12;
            this.editButton.Text = "Szerkesztés...";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(518, 246);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(76, 23);
            this.closeButton.TabIndex = 13;
            this.closeButton.Text = "Bezárás";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // saveEditButton
            // 
            this.saveEditButton.Location = new System.Drawing.Point(260, 246);
            this.saveEditButton.Name = "saveEditButton";
            this.saveEditButton.Size = new System.Drawing.Size(110, 23);
            this.saveEditButton.TabIndex = 14;
            this.saveEditButton.Text = "Változások mentése";
            this.saveEditButton.UseVisualStyleBackColor = true;
            this.saveEditButton.Click += new System.EventHandler(this.saveEditButton_Click);
            // 
            // cancelEditButton
            // 
            this.cancelEditButton.Location = new System.Drawing.Point(376, 246);
            this.cancelEditButton.Name = "cancelEditButton";
            this.cancelEditButton.Size = new System.Drawing.Size(49, 23);
            this.cancelEditButton.TabIndex = 15;
            this.cancelEditButton.Text = "Mégse";
            this.cancelEditButton.UseVisualStyleBackColor = true;
            this.cancelEditButton.Click += new System.EventHandler(this.cancelEditButton_Click);
            // 
            // deleteHikeButton
            // 
            this.deleteHikeButton.Location = new System.Drawing.Point(431, 246);
            this.deleteHikeButton.Name = "deleteHikeButton";
            this.deleteHikeButton.Size = new System.Drawing.Size(81, 23);
            this.deleteHikeButton.TabIndex = 9;
            this.deleteHikeButton.Text = "Túra törlése...";
            this.deleteHikeButton.UseVisualStyleBackColor = true;
            // 
            // ViewHikeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(603, 276);
            this.Controls.Add(this.deleteHikeButton);
            this.Controls.Add(this.cancelEditButton);
            this.Controls.Add(this.saveEditButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ViewHikeForm";
            this.ShowIcon = false;
            this.Text = "ViewHikeForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.DateTimePicker dateBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ComboBox cpNameComboBox;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox descriptionBox;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button saveEditButton;
        private System.Windows.Forms.Button cancelEditButton;
        private System.Windows.Forms.TextBox regionBox;
        private System.Windows.Forms.TextBox countryBox;
        private System.Windows.Forms.Button deleteHikeButton;
        private System.Windows.Forms.CheckBox allRegionCheckBox;
        private System.Windows.Forms.TextBox positionBox;
        private System.Windows.Forms.Label label5;
    }
}