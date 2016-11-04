namespace HikeHandler
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
            this.closeButton = new System.Windows.Forms.Button();
            this.summaryBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.countryLabel = new System.Windows.Forms.Label();
            this.hikeLabel = new System.Windows.Forms.Label();
            this.cpLabel = new System.Windows.Forms.Label();
            this.regionLabel = new System.Windows.Forms.Label();
            this.searchBox.SuspendLayout();
            this.addBox.SuspendLayout();
            this.summaryBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchBox
            // 
            this.searchBox.Controls.Add(this.searchCountryButton);
            this.searchBox.Controls.Add(this.searchRegionButton);
            this.searchBox.Controls.Add(this.searchCPButton);
            this.searchBox.Controls.Add(this.searchHikeButton);
            this.searchBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchBox.Location = new System.Drawing.Point(0, 0);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(247, 94);
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
            this.searchHikeButton.Size = new System.Drawing.Size(234, 23);
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
            this.addBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.addBox.Location = new System.Drawing.Point(0, 94);
            this.addBox.Name = "addBox";
            this.addBox.Size = new System.Drawing.Size(247, 92);
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
            this.addCPButton.TabIndex = 1;
            this.addCPButton.Text = "CheckPoint";
            this.addCPButton.UseVisualStyleBackColor = true;
            this.addCPButton.Click += new System.EventHandler(this.addCPButton_Click);
            // 
            // addHikeButton
            // 
            this.addHikeButton.Location = new System.Drawing.Point(5, 28);
            this.addHikeButton.Name = "addHikeButton";
            this.addHikeButton.Size = new System.Drawing.Size(235, 23);
            this.addHikeButton.TabIndex = 0;
            this.addHikeButton.Text = "Túra hozzáadása";
            this.addHikeButton.UseVisualStyleBackColor = true;
            this.addHikeButton.Click += new System.EventHandler(this.addHikeButton_Click);
            // 
            // addRegionButton
            // 
            this.addRegionButton.Location = new System.Drawing.Point(88, 57);
            this.addRegionButton.Name = "addRegionButton";
            this.addRegionButton.Size = new System.Drawing.Size(75, 23);
            this.addRegionButton.TabIndex = 2;
            this.addRegionButton.Text = "Tájegység";
            this.addRegionButton.UseVisualStyleBackColor = true;
            this.addRegionButton.Click += new System.EventHandler(this.addRegionButton_Click);
            // 
            // connectionStateLabel
            // 
            this.connectionStateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.connectionStateLabel.AutoSize = true;
            this.connectionStateLabel.Location = new System.Drawing.Point(2, 267);
            this.connectionStateLabel.Name = "connectionStateLabel";
            this.connectionStateLabel.Size = new System.Drawing.Size(83, 13);
            this.connectionStateLabel.TabIndex = 2;
            this.connectionStateLabel.Text = "Nincs kapcsolat";
            // 
            // connectDBButton
            // 
            this.connectDBButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.connectDBButton.Location = new System.Drawing.Point(88, 262);
            this.connectDBButton.Name = "connectDBButton";
            this.connectDBButton.Size = new System.Drawing.Size(75, 23);
            this.connectDBButton.TabIndex = 3;
            this.connectDBButton.Text = "Csatlakozás";
            this.connectDBButton.UseVisualStyleBackColor = true;
            this.connectDBButton.Click += new System.EventHandler(this.connectDBButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(169, 262);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(72, 23);
            this.closeButton.TabIndex = 4;
            this.closeButton.Text = "Bezárás";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // summaryBox
            // 
            this.summaryBox.Controls.Add(this.regionLabel);
            this.summaryBox.Controls.Add(this.cpLabel);
            this.summaryBox.Controls.Add(this.hikeLabel);
            this.summaryBox.Controls.Add(this.countryLabel);
            this.summaryBox.Controls.Add(this.label4);
            this.summaryBox.Controls.Add(this.label3);
            this.summaryBox.Controls.Add(this.label2);
            this.summaryBox.Controls.Add(this.label1);
            this.summaryBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.summaryBox.Location = new System.Drawing.Point(0, 186);
            this.summaryBox.Name = "summaryBox";
            this.summaryBox.Size = new System.Drawing.Size(247, 71);
            this.summaryBox.TabIndex = 5;
            this.summaryBox.TabStop = false;
            this.summaryBox.Text = "Összesítés";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Túrák:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(113, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "CheckPointok:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Országok:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(113, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Tájegységek:";
            // 
            // countryLabel
            // 
            this.countryLabel.AutoSize = true;
            this.countryLabel.Location = new System.Drawing.Point(72, 47);
            this.countryLabel.Name = "countryLabel";
            this.countryLabel.Size = new System.Drawing.Size(13, 13);
            this.countryLabel.TabIndex = 4;
            this.countryLabel.Text = "?";
            // 
            // hikeLabel
            // 
            this.hikeLabel.AutoSize = true;
            this.hikeLabel.Location = new System.Drawing.Point(72, 25);
            this.hikeLabel.Name = "hikeLabel";
            this.hikeLabel.Size = new System.Drawing.Size(13, 13);
            this.hikeLabel.TabIndex = 5;
            this.hikeLabel.Text = "?";
            // 
            // cpLabel
            // 
            this.cpLabel.AutoSize = true;
            this.cpLabel.Location = new System.Drawing.Point(196, 25);
            this.cpLabel.Name = "cpLabel";
            this.cpLabel.Size = new System.Drawing.Size(13, 13);
            this.cpLabel.TabIndex = 6;
            this.cpLabel.Text = "?";
            // 
            // regionLabel
            // 
            this.regionLabel.AutoSize = true;
            this.regionLabel.Location = new System.Drawing.Point(196, 47);
            this.regionLabel.Name = "regionLabel";
            this.regionLabel.Size = new System.Drawing.Size(13, 13);
            this.regionLabel.TabIndex = 7;
            this.regionLabel.Text = "?";
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(247, 290);
            this.Controls.Add(this.summaryBox);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.connectDBButton);
            this.Controls.Add(this.connectionStateLabel);
            this.Controls.Add(this.addBox);
            this.Controls.Add(this.searchBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "BaseForm";
            this.ShowIcon = false;
            this.Text = "TúraKezelő";
            this.searchBox.ResumeLayout(false);
            this.addBox.ResumeLayout(false);
            this.summaryBox.ResumeLayout(false);
            this.summaryBox.PerformLayout();
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
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.GroupBox summaryBox;
        private System.Windows.Forms.Label regionLabel;
        private System.Windows.Forms.Label cpLabel;
        private System.Windows.Forms.Label hikeLabel;
        private System.Windows.Forms.Label countryLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

