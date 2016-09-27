﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HikeHandler.Forms
{
    public partial class AddCPForm : Form
    {
        public AddCPForm()
        {
            InitializeComponent();
        }

        public AddCPForm(MySqlConnection connection)
        {
            InitializeComponent();
            sqlConnection = connection;
        }

        private MySqlConnection sqlConnection;               

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
