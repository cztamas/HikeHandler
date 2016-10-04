namespace HikeHandler.Forms
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
            this.hikeCountBox = new System.Windows.Forms.TextBox();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.hikesOfCountryButton = new System.Windows.Forms.Button();
            this.cpsOfCountryButton = new System.Windows.Forms.Button();
            this.regionsOfCountryButton = new System.Windows.Forms.Button();
            this.descriptionBox = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.saveEditButton = new System.Windows.Forms.Button();
            this.cancelEditButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.deleteCountryButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.refreshButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.deleteCountryButton);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.hikeCountBox);
            this.groupBox1.Controls.Add(this.nameBox);
            this.groupBox1.Controls.Add(this.hikesOfCountryButton);
            this.groupBox1.Controls.Add(this.cpsOfCountryButton);
            this.groupBox1.Controls.Add(this.regionsOfCountryButton);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(5, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(410, 134);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Adatok";
            // 
            // hikeCountBox
            // 
            this.hikeCountBox.Enabled = false;
            this.hikeCountBox.Location = new System.Drawing.Point(235, 19);
            this.hikeCountBox.Name = "hikeCountBox";
            this.hikeCountBox.Size = new System.Drawing.Size(46, 20);
            this.hikeCountBox.TabIndex = 13;
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
            this.hikesOfCountryButton.Location = new System.Drawing.Point(294, 74);
            this.hikesOfCountryButton.Name = "hikesOfCountryButton";
            this.hikesOfCountryButton.Size = new System.Drawing.Size(109, 23);
            this.hikesOfCountryButton.TabIndex = 8;
            this.hikesOfCountryButton.Text = "Túrák...";
            this.hikesOfCountryButton.UseVisualStyleBackColor = true;
            // 
            // cpsOfCountryButton
            // 
            this.cpsOfCountryButton.Location = new System.Drawing.Point(294, 45);
            this.cpsOfCountryButton.Name = "cpsOfCountryButton";
            this.cpsOfCountryButton.Size = new System.Drawing.Size(109, 23);
            this.cpsOfCountryButton.TabIndex = 7;
            this.cpsOfCountryButton.Text = "CheckPointok...";
            this.cpsOfCountryButton.UseVisualStyleBackColor = true;
            // 
            // regionsOfCountryButton
            // 
            this.regionsOfCountryButton.Location = new System.Drawing.Point(294, 16);
            this.regionsOfCountryButton.Name = "regionsOfCountryButton";
            this.regionsOfCountryButton.Size = new System.Drawing.Size(109, 23);
            this.regionsOfCountryButton.TabIndex = 6;
            this.regionsOfCountryButton.Text = "Tájegységek...";
            this.regionsOfCountryButton.UseVisualStyleBackColor = true;
            // 
            // descriptionBox
            // 
            this.descriptionBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descriptionBox.Location = new System.Drawing.Point(3, 16);
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.Size = new System.Drawing.Size(275, 70);
            this.descriptionBox.TabIndex = 5;
            this.descriptionBox.Text = "";
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
            this.saveEditButton.Location = new System.Drawing.Point(8, 152);
            this.saveEditButton.Name = "saveEditButton";
            this.saveEditButton.Size = new System.Drawing.Size(111, 23);
            this.saveEditButton.TabIndex = 11;
            this.saveEditButton.Text = "Változások mentése";
            this.saveEditButton.UseVisualStyleBackColor = true;
            // 
            // cancelEditButton
            // 
            this.cancelEditButton.Location = new System.Drawing.Point(125, 152);
            this.cancelEditButton.Name = "cancelEditButton";
            this.cancelEditButton.Size = new System.Drawing.Size(80, 23);
            this.cancelEditButton.TabIndex = 10;
            this.cancelEditButton.Text = "Mégse";
            this.cancelEditButton.UseVisualStyleBackColor = true;
            this.cancelEditButton.Click += new System.EventHandler(this.cancelEditButton_Click);
            // 
            // editButton
            // 
            this.editButton.Location = new System.Drawing.Point(8, 152);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(197, 23);
            this.editButton.TabIndex = 9;
            this.editButton.Text = "Szerkesztés...";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(299, 152);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(109, 23);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "Bezárás";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // deleteCountryButton
            // 
            this.deleteCountryButton.Location = new System.Drawing.Point(294, 103);
            this.deleteCountryButton.Name = "deleteCountryButton";
            this.deleteCountryButton.Size = new System.Drawing.Size(109, 23);
            this.deleteCountryButton.TabIndex = 12;
            this.deleteCountryButton.Text = "Ország törlése...";
            this.deleteCountryButton.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.descriptionBox);
            this.groupBox2.Location = new System.Drawing.Point(0, 45);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(281, 89);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Leírás";
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(211, 152);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(82, 23);
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
            this.ClientSize = new System.Drawing.Size(421, 184);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancelEditButton);
            this.Controls.Add(this.saveEditButton);
            this.Controls.Add(this.editButton);
            this.Name = "ViewCountryForm";
            this.ShowIcon = false;
            this.Text = "országnév";
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
        private System.Windows.Forms.TextBox hikeCountBox;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Button deleteCountryButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button refreshButton;
    }
}