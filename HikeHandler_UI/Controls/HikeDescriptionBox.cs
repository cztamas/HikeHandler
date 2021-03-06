﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HikeHandler.UI
{
    public partial class HikeDescriptionBox : UserControl
    {
        public HikeDescriptionBox()
        {
            InitializeComponent();
        }

        public override string Text
        {
            get
            {
                return descriptionBox.Text;
            }
            set
            {
                descriptionBox.Text = value;
            }
        }

        public string Label
        {
            get
            {
                return frameGroupBox.Text;
            }
            set
            {
                frameGroupBox.Text = value;
            }
        }

        public bool IsTextboxActive = false;
        public event EventHandler TextBoxFocusEnter;
        public event EventHandler TextBoxFocusLeave;

        private void InsertSpecialCharacter(string character)
        {
            int cursorPosition = descriptionBox.Text.Length;
            if (IsTextboxActive)
                cursorPosition = descriptionBox.SelectionStart;
            descriptionBox.Text = descriptionBox.Text.Insert(cursorPosition, character);
            descriptionBox.Focus();
            descriptionBox.SelectionStart = cursorPosition + 1;
            descriptionBox.SelectionLength = 0;
        }

        private void writeOmegaButton_Click(object sender, EventArgs e)
        {
            InsertSpecialCharacter("\u03A9");
        }

        private void writeSquareButton_Click(object sender, EventArgs e)
        {
            InsertSpecialCharacter("\u25A0");
        }

        private void writeTriangleButton_Click(object sender, EventArgs e)
        {
            InsertSpecialCharacter("\u25B2");
        }

        private void writeCircleButton_Click(object sender, EventArgs e)
        {
            InsertSpecialCharacter("\u2B24");
        }

        private void writeArrowCircleButton_Click(object sender, EventArgs e)
        {
            InsertSpecialCharacter("⟲");
        }

        private void writeRingButton_Click(object sender, EventArgs e)
        {
            InsertSpecialCharacter("⨀");
        }

        private void descriptionBox_Enter(object sender, EventArgs e)
        {
            IsTextboxActive = true;
            TextBoxFocusEnter?.Invoke(this, new EventArgs());
        }

        private void descriptionBox_Leave(object sender, EventArgs e)
        {
            TextBoxFocusLeave?.Invoke(this, new EventArgs());
        }
    }
}
