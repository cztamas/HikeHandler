namespace HikeHandler.UI
{
    partial class SearchHikeForm
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
            this.dateBox = new System.Windows.Forms.TextBox();
            this.hikePositionBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.regionComboBox = new System.Windows.Forms.ComboBox();
            this.countryComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.searchButton = new System.Windows.Forms.Button();
            this.resultGroupBox = new System.Windows.Forms.GroupBox();
            this.resultView = new System.Windows.Forms.DataGridView();
            this.clearButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.detailsButton = new System.Windows.Forms.Button();
            this.checkPointHandler = new HikeHandler.UI.CPHandler();
            this.groupBox1.SuspendLayout();
            this.resultGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dateBox);
            this.groupBox1.Controls.Add(this.hikePositionBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.typeComboBox);
            this.groupBox1.Controls.Add(this.regionComboBox);
            this.groupBox1.Controls.Add(this.countryComboBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(254, 130);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Alapadatok";
            // 
            // dateBox
            // 
            this.dateBox.Location = new System.Drawing.Point(71, 98);
            this.dateBox.Name = "dateBox";
            this.dateBox.Size = new System.Drawing.Size(171, 20);
            this.dateBox.TabIndex = 12;
            // 
            // hikePositionBox
            // 
            this.hikePositionBox.Location = new System.Drawing.Point(71, 72);
            this.hikePositionBox.Name = "hikePositionBox";
            this.hikePositionBox.Size = new System.Drawing.Size(57, 20);
            this.hikePositionBox.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Sorszám:";
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
            this.typeComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.typeComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Location = new System.Drawing.Point(178, 72);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(64, 21);
            this.typeComboBox.TabIndex = 6;
            // 
            // regionComboBox
            // 
            this.regionComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.regionComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.regionComboBox.FormattingEnabled = true;
            this.regionComboBox.Location = new System.Drawing.Point(71, 45);
            this.regionComboBox.Name = "regionComboBox";
            this.regionComboBox.Size = new System.Drawing.Size(171, 21);
            this.regionComboBox.TabIndex = 4;
            // 
            // countryComboBox
            // 
            this.countryComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.countryComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.countryComboBox.FormattingEnabled = true;
            this.countryComboBox.Location = new System.Drawing.Point(71, 18);
            this.countryComboBox.Name = "countryComboBox";
            this.countryComboBox.Size = new System.Drawing.Size(171, 21);
            this.countryComboBox.TabIndex = 3;
            this.countryComboBox.SelectedValueChanged += new System.EventHandler(this.countryComboBox_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(134, 75);
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
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(12, 360);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(254, 23);
            this.searchButton.TabIndex = 5;
            this.searchButton.Text = "Keresés";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // resultGroupBox
            // 
            this.resultGroupBox.AutoSize = true;
            this.resultGroupBox.Controls.Add(this.resultView);
            this.resultGroupBox.Location = new System.Drawing.Point(280, 12);
            this.resultGroupBox.Name = "resultGroupBox";
            this.resultGroupBox.Size = new System.Drawing.Size(398, 342);
            this.resultGroupBox.TabIndex = 6;
            this.resultGroupBox.TabStop = false;
            this.resultGroupBox.Text = "Találatok";
            // 
            // resultView
            // 
            this.resultView.AllowUserToAddRows = false;
            this.resultView.AllowUserToDeleteRows = false;
            this.resultView.AllowUserToResizeColumns = false;
            this.resultView.AllowUserToResizeRows = false;
            this.resultView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.resultView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultView.Location = new System.Drawing.Point(3, 16);
            this.resultView.Name = "resultView";
            this.resultView.ReadOnly = true;
            this.resultView.RowHeadersVisible = false;
            this.resultView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.resultView.Size = new System.Drawing.Size(392, 323);
            this.resultView.TabIndex = 0;
            this.resultView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.resultView_CellDoubleClick);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(420, 357);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(125, 23);
            this.clearButton.TabIndex = 7;
            this.clearButton.Text = "Új keresés";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(551, 357);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(124, 23);
            this.closeButton.TabIndex = 8;
            this.closeButton.Text = "Bezárás";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // detailsButton
            // 
            this.detailsButton.Location = new System.Drawing.Point(283, 357);
            this.detailsButton.Name = "detailsButton";
            this.detailsButton.Size = new System.Drawing.Size(131, 23);
            this.detailsButton.TabIndex = 9;
            this.detailsButton.Text = "Részletek...";
            this.detailsButton.UseVisualStyleBackColor = true;
            this.detailsButton.Click += new System.EventHandler(this.detailsButton_Click);
            // 
            // checkPointHandler
            // 
            this.checkPointHandler.Location = new System.Drawing.Point(12, 148);
            this.checkPointHandler.Name = "checkPointHandler";
            this.checkPointHandler.RegionID = 0;
            this.checkPointHandler.Size = new System.Drawing.Size(254, 206);
            this.checkPointHandler.TabIndex = 10;
            // 
            // SearchHikeForm
            // 
            this.AcceptButton = this.searchButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(682, 390);
            this.Controls.Add(this.checkPointHandler);
            this.Controls.Add(this.detailsButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.resultGroupBox);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SearchHikeForm";
            this.ShowIcon = false;
            this.Text = "Túra keresése";
            this.Load += new System.EventHandler(this.SearchHikeForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.resultGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.resultView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.ComboBox regionComboBox;
        private System.Windows.Forms.ComboBox countryComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox dateBox;
        private System.Windows.Forms.TextBox hikePositionBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.GroupBox resultGroupBox;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.DataGridView resultView;
        private System.Windows.Forms.Button detailsButton;
        private CPHandler checkPointHandler;
    }
}