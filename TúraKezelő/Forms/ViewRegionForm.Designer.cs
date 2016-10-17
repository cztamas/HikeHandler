namespace HikeHandler.Forms
{
    partial class ViewRegionForm
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
            this.deleteButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.descriptionBox = new System.Windows.Forms.RichTextBox();
            this.hikeCountBox = new System.Windows.Forms.TextBox();
            this.countryBox = new System.Windows.Forms.TextBox();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.showCPsButton = new System.Windows.Forms.Button();
            this.showHikesButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.editButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.saveEditButton = new System.Windows.Forms.Button();
            this.cancelEditButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.deleteButton);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.hikeCountBox);
            this.groupBox1.Controls.Add(this.countryBox);
            this.groupBox1.Controls.Add(this.nameBox);
            this.groupBox1.Controls.Add(this.showCPsButton);
            this.groupBox1.Controls.Add(this.showHikesButton);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(421, 146);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Adatok";
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(306, 117);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(107, 23);
            this.deleteButton.TabIndex = 9;
            this.deleteButton.Text = "Tájegység törlése...";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.descriptionBox);
            this.groupBox2.Location = new System.Drawing.Point(0, 45);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(291, 101);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Leírás";
            // 
            // descriptionBox
            // 
            this.descriptionBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descriptionBox.Location = new System.Drawing.Point(3, 16);
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.Size = new System.Drawing.Size(285, 82);
            this.descriptionBox.TabIndex = 0;
            this.descriptionBox.Text = "";
            // 
            // hikeCountBox
            // 
            this.hikeCountBox.Enabled = false;
            this.hikeCountBox.Location = new System.Drawing.Point(380, 19);
            this.hikeCountBox.Name = "hikeCountBox";
            this.hikeCountBox.Size = new System.Drawing.Size(33, 20);
            this.hikeCountBox.TabIndex = 7;
            // 
            // countryBox
            // 
            this.countryBox.Enabled = false;
            this.countryBox.Location = new System.Drawing.Point(197, 19);
            this.countryBox.Name = "countryBox";
            this.countryBox.Size = new System.Drawing.Size(100, 20);
            this.countryBox.TabIndex = 6;
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(42, 19);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(100, 20);
            this.nameBox.TabIndex = 5;
            // 
            // showCPsButton
            // 
            this.showCPsButton.Location = new System.Drawing.Point(306, 88);
            this.showCPsButton.Name = "showCPsButton";
            this.showCPsButton.Size = new System.Drawing.Size(107, 23);
            this.showCPsButton.TabIndex = 4;
            this.showCPsButton.Text = "CheckPointok...";
            this.showCPsButton.UseVisualStyleBackColor = true;
            this.showCPsButton.Click += new System.EventHandler(this.showCPsButton_Click);
            // 
            // showHikesButton
            // 
            this.showHikesButton.Location = new System.Drawing.Point(306, 59);
            this.showHikesButton.Name = "showHikesButton";
            this.showHikesButton.Size = new System.Drawing.Size(107, 23);
            this.showHikesButton.TabIndex = 3;
            this.showHikesButton.Text = "Túrák...";
            this.showHikesButton.UseVisualStyleBackColor = true;
            this.showHikesButton.Click += new System.EventHandler(this.showHikesButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(303, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Túrák száma:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(148, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Ország:";
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
            // editButton
            // 
            this.editButton.Location = new System.Drawing.Point(12, 155);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(201, 23);
            this.editButton.TabIndex = 1;
            this.editButton.Text = "Szerkesztés...";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(318, 155);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(107, 23);
            this.closeButton.TabIndex = 2;
            this.closeButton.Text = "Bezárás";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // saveEditButton
            // 
            this.saveEditButton.Location = new System.Drawing.Point(12, 155);
            this.saveEditButton.Name = "saveEditButton";
            this.saveEditButton.Size = new System.Drawing.Size(111, 23);
            this.saveEditButton.TabIndex = 3;
            this.saveEditButton.Text = "Változások mentése";
            this.saveEditButton.UseVisualStyleBackColor = true;
            this.saveEditButton.Click += new System.EventHandler(this.saveEditButton_Click);
            // 
            // cancelEditButton
            // 
            this.cancelEditButton.Location = new System.Drawing.Point(129, 155);
            this.cancelEditButton.Name = "cancelEditButton";
            this.cancelEditButton.Size = new System.Drawing.Size(84, 23);
            this.cancelEditButton.TabIndex = 4;
            this.cancelEditButton.Text = "Mégse";
            this.cancelEditButton.UseVisualStyleBackColor = true;
            this.cancelEditButton.Click += new System.EventHandler(this.cancelEditButton_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(219, 155);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(84, 23);
            this.refreshButton.TabIndex = 5;
            this.refreshButton.Text = "Frissítés";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // ViewRegionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(439, 182);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.cancelEditButton);
            this.Controls.Add(this.saveEditButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.groupBox1);
            this.Name = "ViewRegionForm";
            this.ShowIcon = false;
            this.Text = "tájegységnév";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button showCPsButton;
        private System.Windows.Forms.Button showHikesButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox descriptionBox;
        private System.Windows.Forms.TextBox hikeCountBox;
        private System.Windows.Forms.TextBox countryBox;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button saveEditButton;
        private System.Windows.Forms.Button cancelEditButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button refreshButton;
    }
}