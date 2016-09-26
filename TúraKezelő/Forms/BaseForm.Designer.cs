namespace TúraKezelő
{
    partial class BaseForm
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
            this.searchCountryButton = new System.Windows.Forms.Button();
            this.searchRegionButton = new System.Windows.Forms.Button();
            this.searchCPButton = new System.Windows.Forms.Button();
            this.searchHikeButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.addCountryButton = new System.Windows.Forms.Button();
            this.addCPButton = new System.Windows.Forms.Button();
            this.addHikeButton = new System.Windows.Forms.Button();
            this.addRegionButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.searchCountryButton);
            this.groupBox1.Controls.Add(this.searchRegionButton);
            this.groupBox1.Controls.Add(this.searchCPButton);
            this.groupBox1.Controls.Add(this.searchHikeButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(253, 94);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Keresés";
            // 
            // searchCountryButton
            // 
            this.searchCountryButton.Location = new System.Drawing.Point(169, 58);
            this.searchCountryButton.Name = "searchCountryButton";
            this.searchCountryButton.Size = new System.Drawing.Size(72, 23);
            this.searchCountryButton.TabIndex = 3;
            this.searchCountryButton.Text = "Ország";
            this.searchCountryButton.UseVisualStyleBackColor = true;
            this.searchCountryButton.Click += new System.EventHandler(this.searchCountryButton_Click);
            // 
            // searchRegionButton
            // 
            this.searchRegionButton.Location = new System.Drawing.Point(88, 58);
            this.searchRegionButton.Name = "searchRegionButton";
            this.searchRegionButton.Size = new System.Drawing.Size(75, 23);
            this.searchRegionButton.TabIndex = 2;
            this.searchRegionButton.Text = "Tájegység";
            this.searchRegionButton.UseVisualStyleBackColor = true;
            this.searchRegionButton.Click += new System.EventHandler(this.searchRegionButton_Click);
            // 
            // searchCPButton
            // 
            this.searchCPButton.Location = new System.Drawing.Point(6, 58);
            this.searchCPButton.Name = "searchCPButton";
            this.searchCPButton.Size = new System.Drawing.Size(76, 23);
            this.searchCPButton.TabIndex = 1;
            this.searchCPButton.Text = "CheckPoint";
            this.searchCPButton.UseVisualStyleBackColor = true;
            this.searchCPButton.Click += new System.EventHandler(this.searchCPButton_Click);
            // 
            // searchHikeButton
            // 
            this.searchHikeButton.Location = new System.Drawing.Point(6, 29);
            this.searchHikeButton.Name = "searchHikeButton";
            this.searchHikeButton.Size = new System.Drawing.Size(235, 23);
            this.searchHikeButton.TabIndex = 0;
            this.searchHikeButton.Text = "Túra keresése";
            this.searchHikeButton.UseVisualStyleBackColor = true;
            this.searchHikeButton.Click += new System.EventHandler(this.searchHikeButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.addCountryButton);
            this.groupBox2.Controls.Add(this.addCPButton);
            this.groupBox2.Controls.Add(this.addHikeButton);
            this.groupBox2.Controls.Add(this.addRegionButton);
            this.groupBox2.Location = new System.Drawing.Point(12, 112);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(253, 92);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Hozzáadás";
            // 
            // addCountryButton
            // 
            this.addCountryButton.Location = new System.Drawing.Point(169, 57);
            this.addCountryButton.Name = "addCountryButton";
            this.addCountryButton.Size = new System.Drawing.Size(72, 23);
            this.addCountryButton.TabIndex = 3;
            this.addCountryButton.Text = "Ország";
            this.addCountryButton.UseVisualStyleBackColor = true;
            this.addCountryButton.Click += new System.EventHandler(this.addCountryButton_Click);
            // 
            // addCPButton
            // 
            this.addCPButton.Location = new System.Drawing.Point(7, 57);
            this.addCPButton.Name = "addCPButton";
            this.addCPButton.Size = new System.Drawing.Size(75, 23);
            this.addCPButton.TabIndex = 2;
            this.addCPButton.Text = "CheckPoint";
            this.addCPButton.UseVisualStyleBackColor = true;
            this.addCPButton.Click += new System.EventHandler(this.addCPButton_Click);
            // 
            // addHikeButton
            // 
            this.addHikeButton.Location = new System.Drawing.Point(6, 28);
            this.addHikeButton.Name = "addHikeButton";
            this.addHikeButton.Size = new System.Drawing.Size(235, 23);
            this.addHikeButton.TabIndex = 1;
            this.addHikeButton.Text = "Túra hozzáadása";
            this.addHikeButton.UseVisualStyleBackColor = true;
            this.addHikeButton.Click += new System.EventHandler(this.addHikeButton_Click);
            // 
            // addRegionButton
            // 
            this.addRegionButton.Location = new System.Drawing.Point(88, 57);
            this.addRegionButton.Name = "addRegionButton";
            this.addRegionButton.Size = new System.Drawing.Size(75, 23);
            this.addRegionButton.TabIndex = 0;
            this.addRegionButton.Text = "Tájegység";
            this.addRegionButton.UseVisualStyleBackColor = true;
            this.addRegionButton.Click += new System.EventHandler(this.addRegionButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 215);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nincs kapcsolat az adatbázissal";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(181, 210);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 240);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "BaseForm";
            this.Text = "TúraKezelő";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button searchCountryButton;
        private System.Windows.Forms.Button searchRegionButton;
        private System.Windows.Forms.Button searchCPButton;
        private System.Windows.Forms.Button searchHikeButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button addCountryButton;
        private System.Windows.Forms.Button addCPButton;
        private System.Windows.Forms.Button addHikeButton;
        private System.Windows.Forms.Button addRegionButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}

