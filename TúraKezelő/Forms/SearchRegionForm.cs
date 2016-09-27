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
    public partial class SearchRegionForm : Form
    {
        public SearchRegionForm()
        {
            InitializeComponent();
        }

        public SearchRegionForm(MySqlConnection connection)
        {
            InitializeComponent();
            sqlConnection = connection;
        }

        private MySqlConnection sqlConnection;

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            this.Clear();
        }

        private void Clear()
        { }
    }
}
