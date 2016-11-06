namespace HikeHandler.UI
{
    partial class ViewCountryForm
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
            this.hikeCountLabel = new System.Windows.Forms.Label();
            this.cpCountLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.regionCountLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.deleteCountryButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.descriptionBox = new System.Windows.Forms.RichTextBox();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.hikesOfCountryButton = new System.Windows.Forms.Button();
            this.cpsOfCountryButton = new System.Windows.Forms.Button();
            this.regionsOfCountryButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.saveEditButton = new System.Windows.Forms.Button();
            this.cancelEditButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.hikeCountLabel);
            this.groupBox1.Controls.Add(this.cpCountLabel);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.regionCountLabel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.deleteCountryButton);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.nameBox);
            this.groupBox1.Controls.Add(this.hikesOfCountryButton);
            this.groupBox1.Controls.Add(this.cpsOfCountryButton);
            this.groupBox1.Controls.Add(this.regionsOfCountryButton);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(5, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(391, 164);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Adatok";
            // 
            // hikeCountLabel
            // 
            this.hikeCountLabel.AutoSize = true;
            this.hikeCountLabel.Location = new System.Drawing.Point(241, 22);
            this.hikeCountLabel.Name = "hikeCountLabel";
            this.hikeCountLabel.Size = new System.Drawing.Size(25, 13);
            this.hikeCountLabel.TabIndex = 19;
            this.hikeCountLabel.Text = "???";
            // 
            // cpCountLabel
            // 
            this.cpCountLabel.AutoSize = true;
            this.cpCountLabel.Location = new System.Drawing.Point(241, 50);
            this.cpCountLabel.Name = "cpCountLabel";
            this.cpCountLabel.Size = new System.Drawing.Size(25, 13);
            this.cpCountLabel.TabIndex = 18;
            this.cpCountLabel.Text = "???";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(158, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "CheckPointok:";
            // 
            // regionCountLabel
            // 
            this.regionCountLabel.AutoSize = true;
            this.regionCountLabel.Location = new System.Drawing.Point(117, 50);
            this.regionCountLabel.Name = "regionCountLabel";
            this.regionCountLabel.Size = new System.Drawing.Size(25, 13);
            this.regionCountLabel.TabIndex = 16;
            this.regionCountLabel.Text = "???";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Tájegységek száma:";
            // 
            // deleteCountryButton
            // 
            this.deleteCountryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteCountryButton.Location = new System.Drawing.Point(275, 133);
            this.deleteCountryButton.Name = "deleteCountryButton";
            this.deleteCountryButton.Size = new System.Drawing.Size(109, 23);
            this.deleteCountryButton.TabIndex = 12;
            this.deleteCountryButton.Text = "Ország törlése...";
            this.deleteCountryButton.UseVisualStyleBackColor = true;
            this.deleteCountryButton.Click += new System.EventHandler(this.deleteCountryButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.descriptionBox);
            this.groupBox2.Location = new System.Drawing.Point(0, 74);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(269, 90);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Leírás";
            // 
            // descriptionBox
            // 
            this.descriptionBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descriptionBox.Location = new System.Drawing.Point(3, 16);
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.Size = new System.Drawing.Size(263, 71);
            this.descriptionBox.TabIndex = 5;
            this.descriptionBox.Text = "";
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(42, 19);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(110, 20);
            this.nameBox.TabIndex = 12;
            // 
            // hikesOfCountryButton
            // 
            this.hikesOfCountryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hikesOfCountryButton.Location = new System.Drawing.Point(275, 74);
            this.hikesOfCountryButton.Name = "hikesOfCountryButton";
            this.hikesOfCountryButton.Size = new System.Drawing.Size(109, 23);
            this.hikesOfCountryButton.TabIndex = 8;
            this.hikesOfCountryButton.Text = "Túrák...";
            this.hikesOfCountryButton.UseVisualStyleBackColor = true;
            this.hikesOfCountryButton.Click += new System.EventHandler(this.hikesOfCountryButton_Click);
            // 
            // cpsOfCountryButton
            // 
            this.cpsOfCountryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cpsOfCountryButton.Location = new System.Drawing.Point(275, 45);
            this.cpsOfCountryButton.Name = "cpsOfCountryButton";
            this.cpsOfCountryButton.Size = new System.Drawing.Size(109, 23);
            this.cpsOfCountryButton.TabIndex = 7;
            this.cpsOfCountryButton.Text = "CheckPointok...";
            this.cpsOfCountryButton.UseVisualStyleBackColor = true;
            this.cpsOfCountryButton.Click += new System.EventHandler(this.cpsOfCountryButton_Click);
            // 
            // regionsOfCountryButton
            // 
            this.regionsOfCountryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.regionsOfCountryButton.Location = new System.Drawing.Point(275, 16);
            this.regionsOfCountryButton.Name = "regionsOfCountryButton";
            this.regionsOfCountryButton.Size = new System.Drawing.Size(109, 23);
            this.regionsOfCountryButton.TabIndex = 6;
            this.regionsOfCountryButton.Text = "Tájegységek...";
            this.regionsOfCountryButton.UseVisualStyleBackColor = true;
            this.regionsOfCountryButton.Click += new System.EventHandler(this.regionsOfCountryButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(158, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Túrák száma:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Név:";
            // 
            // saveEditButton
            // 
            this.saveEditButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.saveEditButton.Location = new System.Drawing.Point(8, 176);
            this.saveEditButton.Name = "saveEditButton";
            this.saveEditButton.Size = new System.Drawing.Size(111, 23);
            this.saveEditButton.TabIndex = 11;
            this.saveEditButton.Text = "Változások mentése";
            this.saveEditButton.UseVisualStyleBackColor = true;
            this.saveEditButton.Click += new System.EventHandler(this.saveEditButton_Click);
            // 
            // cancelEditButton
            // 
            this.cancelEditButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelEditButton.Location = new System.Drawing.Point(125, 176);
            this.cancelEditButton.Name = "cancelEditButton";
            this.cancelEditButton.Size = new System.Drawing.Size(65, 23);
            this.cancelEditButton.TabIndex = 10;
            this.cancelEditButton.Text = "Mégse";
            this.cancelEditButton.UseVisualStyleBackColor = true;
            this.cancelEditButton.Click += new System.EventHandler(this.cancelEditButton_Click);
            // 
            // editButton
            // 
            this.editButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.editButton.Location = new System.Drawing.Point(8, 176);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(182, 23);
            this.editButton.TabIndex = 9;
            this.editButton.Text = "Szerkesztés...";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(280, 176);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(109, 23);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "Bezárás";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.refreshButton.Location = new System.Drawing.Point(196, 176);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(75, 23);
            this.refreshButton.TabIndex = 15;
            this.refreshButton.Text = "Frissítés";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // ViewCountryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(402, 207);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancelEditButton);
            this.Controls.Add(this.saveEditButton);
            this.Controls.Add(this.editButton);
            this.Name = "ViewCountryForm";
            this.ShowIcon = false;
            this.Text = "országnév";
            this.Load += new System.EventHandler(this.ViewCountryForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button saveEditButton;
        private System.Windows.Forms.Button cancelEditButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button hikesOfCountryButton;
        private System.Windows.Forms.Button cpsOfCountryButton;
        private System.Windows.Forms.Button regionsOfCountryButton;
        private System.Windows.Forms.RichTextBox descriptionBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Button deleteCountryButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Label cpCountLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label regionCountLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label hikeCountLabel;
    }
}