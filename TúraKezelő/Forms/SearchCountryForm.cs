using System;
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
    public partial class SearchCountryForm : Form
    {
        public SearchCountryForm()
        {
            InitializeComponent();
        }

        public SearchCountryForm(MySqlConnection connection)
        {
            InitializeComponent();
            sqlConnection = connection;
        }

        private MySqlConnection sqlConnection;

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
