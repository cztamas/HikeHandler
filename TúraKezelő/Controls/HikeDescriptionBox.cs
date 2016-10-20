using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TúraKezelő.Controls
{
    public partial class HikeDescriptionBox : UserControl
    {
        public HikeDescriptionBox()
        {
            InitializeComponent();
            InitButtons();
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

        private void InitButtons()
        {
            writeArrowCircleButton.Text = "\u21BB";
            writeCircleButton.Text = "\u2B24";
            writeOmegaButton.Text = "\u03A9";
            writeRingButton.Text = "\u25CE";
            writeSquareButton.Text = "\u25A0";
            writeTriangleButton.Text = "\u25B2";
        }

        private void writeOmegaButton_Click(object sender, EventArgs e)
        {
            descriptionBox.Text = descriptionBox.Text.Insert(descriptionBox.SelectionStart, "\u03A9");
            descriptionBox.Focus();
        }

        private void writeSquareButton_Click(object sender, EventArgs e)
        {
            descriptionBox.Text = descriptionBox.Text.Insert(descriptionBox.SelectionStart, "\u25A0");
            descriptionBox.Focus();
        }

        private void writeTriangleButton_Click(object sender, EventArgs e)
        {
            descriptionBox.Text = descriptionBox.Text.Insert(descriptionBox.SelectionStart, "\u25B2");
            descriptionBox.Focus();
        }

        private void writeCircleButton_Click(object sender, EventArgs e)
        {
            descriptionBox.Text = descriptionBox.Text.Insert(descriptionBox.SelectionStart, "\u2B24");
            descriptionBox.Focus();
        }

        private void writeArrowCircleButton_Click(object sender, EventArgs e)
        {
            descriptionBox.Text = descriptionBox.Text.Insert(descriptionBox.SelectionStart, "\u21BB");
            descriptionBox.Focus();
        }

        private void writeRingButton_Click(object sender, EventArgs e)
        {
            descriptionBox.Text = descriptionBox.Text.Insert(descriptionBox.SelectionStart, "\u25CE");
            descriptionBox.Focus();
        }
    }
}
