﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TúraKezelő.Forms
{
    public partial class SearchCountryForm : Form
    {
        public SearchCountryForm()
        {
            InitializeComponent();
        }

        private void Clear()
        { }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            this.Clear();
        }
    }
}
