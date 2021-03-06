﻿namespace HikeHandler.UI
{
    partial class HikeDescriptionBox
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
            this.frameGroupBox = new System.Windows.Forms.GroupBox();
            this.writeRingButton = new System.Windows.Forms.Button();
            this.writeOmegaButton = new System.Windows.Forms.Button();
            this.writeSquareButton = new System.Windows.Forms.Button();
            this.writeTriangleButton = new System.Windows.Forms.Button();
            this.writeCircleButton = new System.Windows.Forms.Button();
            this.writeArrowCircleButton = new System.Windows.Forms.Button();
            this.descriptionBox = new System.Windows.Forms.RichTextBox();
            this.frameGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // frameGroupBox
            // 
            this.frameGroupBox.Controls.Add(this.writeRingButton);
            this.frameGroupBox.Controls.Add(this.writeOmegaButton);
            this.frameGroupBox.Controls.Add(this.writeSquareButton);
            this.frameGroupBox.Controls.Add(this.writeTriangleButton);
            this.frameGroupBox.Controls.Add(this.writeCircleButton);
            this.frameGroupBox.Controls.Add(this.writeArrowCircleButton);
            this.frameGroupBox.Controls.Add(this.descriptionBox);
            this.frameGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frameGroupBox.Location = new System.Drawing.Point(0, 0);
            this.frameGroupBox.Name = "frameGroupBox";
            this.frameGroupBox.Size = new System.Drawing.Size(227, 139);
            this.frameGroupBox.TabIndex = 11;
            this.frameGroupBox.TabStop = false;
            this.frameGroupBox.Text = "Leírás";
            // 
            // writeRingButton
            // 
            this.writeRingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.writeRingButton.Location = new System.Drawing.Point(201, 12);
            this.writeRingButton.Name = "writeRingButton";
            this.writeRingButton.Size = new System.Drawing.Size(23, 23);
            this.writeRingButton.TabIndex = 11;
            this.writeRingButton.Text = "⨀";
            this.writeRingButton.UseVisualStyleBackColor = true;
            this.writeRingButton.Click += new System.EventHandler(this.writeRingButton_Click);
            // 
            // writeOmegaButton
            // 
            this.writeOmegaButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.writeOmegaButton.Location = new System.Drawing.Point(56, 12);
            this.writeOmegaButton.Name = "writeOmegaButton";
            this.writeOmegaButton.Size = new System.Drawing.Size(23, 23);
            this.writeOmegaButton.TabIndex = 10;
            this.writeOmegaButton.Text = "Ω";
            this.writeOmegaButton.UseVisualStyleBackColor = true;
            this.writeOmegaButton.Click += new System.EventHandler(this.writeOmegaButton_Click);
            // 
            // writeSquareButton
            // 
            this.writeSquareButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.writeSquareButton.Location = new System.Drawing.Point(85, 12);
            this.writeSquareButton.Name = "writeSquareButton";
            this.writeSquareButton.Size = new System.Drawing.Size(23, 23);
            this.writeSquareButton.TabIndex = 9;
            this.writeSquareButton.Text = "■";
            this.writeSquareButton.UseVisualStyleBackColor = true;
            this.writeSquareButton.Click += new System.EventHandler(this.writeSquareButton_Click);
            // 
            // writeTriangleButton
            // 
            this.writeTriangleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.writeTriangleButton.Location = new System.Drawing.Point(114, 12);
            this.writeTriangleButton.Name = "writeTriangleButton";
            this.writeTriangleButton.Size = new System.Drawing.Size(23, 23);
            this.writeTriangleButton.TabIndex = 8;
            this.writeTriangleButton.Text = "▲";
            this.writeTriangleButton.UseVisualStyleBackColor = true;
            this.writeTriangleButton.Click += new System.EventHandler(this.writeTriangleButton_Click);
            // 
            // writeCircleButton
            // 
            this.writeCircleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.writeCircleButton.Location = new System.Drawing.Point(143, 12);
            this.writeCircleButton.Name = "writeCircleButton";
            this.writeCircleButton.Size = new System.Drawing.Size(23, 23);
            this.writeCircleButton.TabIndex = 7;
            this.writeCircleButton.Text = "⬤";
            this.writeCircleButton.UseVisualStyleBackColor = true;
            this.writeCircleButton.Click += new System.EventHandler(this.writeCircleButton_Click);
            // 
            // writeArrowCircleButton
            // 
            this.writeArrowCircleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.writeArrowCircleButton.Location = new System.Drawing.Point(172, 12);
            this.writeArrowCircleButton.Name = "writeArrowCircleButton";
            this.writeArrowCircleButton.Size = new System.Drawing.Size(23, 23);
            this.writeArrowCircleButton.TabIndex = 6;
            this.writeArrowCircleButton.Text = "⟲";
            this.writeArrowCircleButton.UseVisualStyleBackColor = true;
            this.writeArrowCircleButton.Click += new System.EventHandler(this.writeArrowCircleButton_Click);
            // 
            // descriptionBox
            // 
            this.descriptionBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionBox.Location = new System.Drawing.Point(3, 41);
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.Size = new System.Drawing.Size(221, 92);
            this.descriptionBox.TabIndex = 5;
            this.descriptionBox.Text = "";
            this.descriptionBox.Enter += new System.EventHandler(this.descriptionBox_Enter);
            this.descriptionBox.Leave += new System.EventHandler(this.descriptionBox_Leave);
            // 
            // HikeDescriptionBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.frameGroupBox);
            this.Name = "HikeDescriptionBox";
            this.Size = new System.Drawing.Size(227, 139);
            this.frameGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox frameGroupBox;
        private System.Windows.Forms.Button writeRingButton;
        private System.Windows.Forms.Button writeOmegaButton;
        private System.Windows.Forms.Button writeSquareButton;
        private System.Windows.Forms.Button writeTriangleButton;
        private System.Windows.Forms.Button writeCircleButton;
        private System.Windows.Forms.Button writeArrowCircleButton;
        private System.Windows.Forms.RichTextBox descriptionBox;
    }
}
