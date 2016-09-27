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
            this.searchBox = new System.Windows.Forms.GroupBox();
            this.searchCountryButton = new System.Windows.Forms.Button();
            this.searchRegionButton = new System.Windows.Forms.Button();
            this.searchCPButton = new System.Windows.Forms.Button();
            this.searchHikeButton = new System.Windows.Forms.Button();
            this.addBox = new System.Windows.Forms.GroupBox();
            this.addCountryButton = new System.Windows.Forms.Button();
            this.addCPButton = new System.Windows.Forms.Button();
            this.addHikeButton = new System.Windows.Forms.Button();
            this.addRegionButton = new System.Windows.Forms.Button();
            this.connectionStateLabel = new System.Windows.Forms.Label();
            this.connectDBButton = new System.Windows.Forms.Button();
            this.searchBox.SuspendLayout();
            this.addBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchBox
            // 
            this.searchBox.Controls.Add(this.searchCountryButton);
            this.searchBox.Controls.Add(this.searchRegionButton);
            this.searchBox.Controls.Add(this.searchCPButton);
            this.searchBox.Controls.Add(this.searchHikeButton);
            this.searchBox.Location = new System.Drawing.Point(12, 12);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(253, 94);
            this.searchBox.TabIndex = 0;
            this.searchBox.TabStop = false;
            this.searchBox.Text = "Keresés";
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
            // addBox
            // 
            this.addBox.Controls.Add(this.addCountryButton);
            this.addBox.Controls.Add(this.addCPButton);
            this.addBox.Controls.Add(this.addHikeButton);
            this.addBox.Controls.Add(this.addRegionButton);
            this.addBox.Location = new System.Drawing.Point(12, 112);
            this.addBox.Name = "addBox";
            this.addBox.Size = new System.Drawing.Size(253, 92);
            this.addBox.TabIndex = 1;
            this.addBox.TabStop = false;
            this.addBox.Text = "Hozzáadás";
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
            // connectionStateLabel
            // 
            this.connectionStateLabel.AutoSize = true;
            this.connectionStateLabel.Location = new System.Drawing.Point(15, 215);
            this.connectionStateLabel.Name = "connectionStateLabel";
            this.connectionStateLabel.Size = new System.Drawing.Size(158, 13);
            this.connectionStateLabel.TabIndex = 2;
            this.connectionStateLabel.Text = "Nincs kapcsolat az adatbázissal";
            // 
            // connectDBButton
            // 
            this.connectDBButton.Location = new System.Drawing.Point(179, 210);
            this.connectDBButton.Name = "connectDBButton";
            this.connectDBButton.Size = new System.Drawing.Size(86, 23);
            this.connectDBButton.TabIndex = 3;
            this.connectDBButton.Text = "Csatlakozás...";
            this.connectDBButton.UseVisualStyleBackColor = true;
            this.connectDBButton.Click += new System.EventHandler(this.connectDBButton_Click);
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 240);
            this.Controls.Add(this.connectDBButton);
            this.Controls.Add(this.connectionStateLabel);
            this.Controls.Add(this.addBox);
            this.Controls.Add(this.searchBox);
            this.Name = "BaseForm";
            this.Text = "TúraKezelő";
            this.searchBox.ResumeLayout(false);
            this.addBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox searchBox;
        private System.Windows.Forms.Button searchCountryButton;
        private System.Windows.Forms.Button searchRegionButton;
        private System.Windows.Forms.Button searchCPButton;
        private System.Windows.Forms.Button searchHikeButton;
        private System.Windows.Forms.GroupBox addBox;
        private System.Windows.Forms.Button addCountryButton;
        private System.Windows.Forms.Button addCPButton;
        private System.Windows.Forms.Button addHikeButton;
        private System.Windows.Forms.Button addRegionButton;
        private System.Windows.Forms.Label connectionStateLabel;
        private System.Windows.Forms.Button connectDBButton;
    }
}

