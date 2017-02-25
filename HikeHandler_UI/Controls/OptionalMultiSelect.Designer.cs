namespace HikeHandler_UI.Controls
{
    partial class OptionalMultiSelect
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.optionsComboBox = new System.Windows.Forms.ComboBox();
            this.multipleCheckBox = new System.Windows.Forms.CheckBox();
            this.addButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.selectedBox = new System.Windows.Forms.ListBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // optionsComboBox
            // 
            this.optionsComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.optionsComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.optionsComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.optionsComboBox.FormattingEnabled = true;
            this.optionsComboBox.Location = new System.Drawing.Point(67, 3);
            this.optionsComboBox.Name = "optionsComboBox";
            this.optionsComboBox.Size = new System.Drawing.Size(114, 21);
            this.optionsComboBox.TabIndex = 0;
            this.optionsComboBox.SelectedIndexChanged += new System.EventHandler(this.optionsComboBox_SelectedIndexChanged);
            // 
            // multipleCheckBox
            // 
            this.multipleCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.multipleCheckBox.AutoSize = true;
            this.multipleCheckBox.Location = new System.Drawing.Point(187, 7);
            this.multipleCheckBox.Name = "multipleCheckBox";
            this.multipleCheckBox.Size = new System.Drawing.Size(47, 17);
            this.multipleCheckBox.TabIndex = 1;
            this.multipleCheckBox.Text = "több";
            this.multipleCheckBox.UseVisualStyleBackColor = true;
            this.multipleCheckBox.CheckedChanged += new System.EventHandler(this.multipleCheckBox_CheckedChanged);
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addButton.Location = new System.Drawing.Point(159, 30);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 21);
            this.addButton.TabIndex = 2;
            this.addButton.Text = "Hozzáadás";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.removeButton.Location = new System.Drawing.Point(159, 52);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 21);
            this.removeButton.TabIndex = 3;
            this.removeButton.Text = "Törlés";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // selectedBox
            // 
            this.selectedBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedBox.FormattingEnabled = true;
            this.selectedBox.Location = new System.Drawing.Point(3, 30);
            this.selectedBox.Name = "selectedBox";
            this.selectedBox.Size = new System.Drawing.Size(150, 43);
            this.selectedBox.TabIndex = 4;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(3, 8);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(64, 13);
            this.nameLabel.TabIndex = 5;
            this.nameLabel.Text = "LabelName:";
            // 
            // OptionalMultiSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.selectedBox);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.multipleCheckBox);
            this.Controls.Add(this.optionsComboBox);
            this.Name = "OptionalMultiSelect";
            this.Size = new System.Drawing.Size(238, 77);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox optionsComboBox;
        private System.Windows.Forms.CheckBox multipleCheckBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.ListBox selectedBox;
        private System.Windows.Forms.Label nameLabel;
    }
}
