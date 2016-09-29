using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HikeHandler.Forms
{
    public partial class ViewHikeForm : Form
    {
        public ViewHikeForm()
        {
            InitializeComponent();
        }

        private bool isEditable;

        private void MakeEditable()
        { }

        private void MakeUneditable()
        { }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /* richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(9, 18);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(316, 204);
            this.richTextBox2.TabIndex = 16;
            this.richTextBox2.Text = "";

        // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(6, 19);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(250, 69);
            this.richTextBox1.TabIndex = 9;
            this.richTextBox1.Text = "";
        */

    }
}
